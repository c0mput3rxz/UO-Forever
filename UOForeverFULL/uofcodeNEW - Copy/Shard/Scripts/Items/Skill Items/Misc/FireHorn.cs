#region References
using System;
using System.Collections.Generic;

using Server.Engines.XmlSpawner2;
using Server.Network;
using Server.Spells;
using Server.Targeting;
#endregion

namespace Server.Items
{
	public class FireHorn : Item
	{
		public override int LabelNumber { get { return 1060456; } } // fire horn

		[Constructable]
		public FireHorn()
			: base(0xFC7)
		{
			Hue = 0x466;
			Weight = 1.0;
		}

		public FireHorn(Serial serial)
			: base(serial)
		{ }

		private bool CheckUse(Mobile from)
		{
			if (!IsAccessibleTo(from))
			{
				return false;
			}

			if (from.Map != Map || !from.InRange(GetWorldLocation(), 2))
			{
				from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
				return false;
			}

			if (!from.CanBeginAction(typeof(FireHorn)))
			{
				from.SendLocalizedMessage(1049615); // You must take a moment to catch your breath.
				return false;
			}

			int sulfAsh = EraAOS ? 4 : 15;
			if (from.Backpack == null || from.Backpack.GetAmount(typeof(SulfurousAsh)) < sulfAsh)
			{
				from.SendLocalizedMessage(1049617); // You do not have enough sulfurous ash.
				return false;
			}

			return true;
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (CheckUse(from))
			{
				from.SendLocalizedMessage(1049620); // Select an area to incinerate.
				from.Target = new InternalTarget(this);
			}
		}

		public void Use(Mobile from, IPoint3D loc)
		{
			if (!CheckUse(from))
			{
				return;
			}

			from.BeginAction(typeof(FireHorn));
			Timer.DelayCall(EraAOS ? TimeSpan.FromSeconds(6.0) : TimeSpan.FromSeconds(12.0), EndAction, from);

			int music = from.Skills[SkillName.Musicianship].Fixed;

			int sucChance = 500 + (music - 775) * 2;
			double dSucChance = (sucChance) / 1000.0;

			if (!from.CheckSkill(SkillName.Musicianship, dSucChance))
			{
				from.SendLocalizedMessage(1049618); // The horn emits a pathetic squeak.
				from.PlaySound(0x18A);
				return;
			}

			int sulfAsh = EraAOS ? 4 : 15;
			from.Backpack.ConsumeUpTo(typeof(SulfurousAsh), sulfAsh);

			from.PlaySound(0x15F);
			Effects.SendPacket(
				from,
				from.Map,
				new HuedEffect(EffectType.Moving, from.Serial, Serial.Zero, 0x36D4, from.Location, loc, 5, 0, false, true, 0, 0));

			var targets = new List<Mobile>();
			bool playerVsPlayer = false;

			IPooledEnumerable eable = from.Map.GetMobilesInRange(new Point3D(loc), 2);

			foreach (Mobile m in eable)
			{
				if (from != m && SpellHelper.ValidIndirectTarget(from, m) && from.CanBeHarmful(m, false))
				{
					if (EraAOS && !from.InLOS(m))
					{
						continue;
					}

					targets.Add(m);

					if (m.Player)
					{
						playerVsPlayer = true;
					}
				}
			}

			eable.Free();

			if (targets.Count > 0)
			{
				int prov = from.Skills[SkillName.Provocation].Fixed;
				int disc = from.Skills[SkillName.Discordance].Fixed;
				int peace = from.Skills[SkillName.Peacemaking].Fixed;

				int minDamage, maxDamage;

				if (EraAOS)
				{
					int musicScaled = music + Math.Max(0, music - 900) * 2;
					int provScaled = prov + Math.Max(0, prov - 900) * 2;
					int discScaled = disc + Math.Max(0, disc - 900) * 2;
					int peaceScaled = peace + Math.Max(0, peace - 900) * 2;

					int weightAvg = (musicScaled + provScaled * 3 + discScaled * 3 + peaceScaled) / 80;

					int avgDamage;
					if (playerVsPlayer)
					{
						avgDamage = weightAvg / 3;
					}
					else
					{
						avgDamage = weightAvg / 2;
					}

					minDamage = (avgDamage * 9) / 10;
					maxDamage = (avgDamage * 10) / 9;
				}
				else
				{
					int total = prov + disc / 5 + peace / 5;

					if (playerVsPlayer)
					{
						total /= 3;
					}

					maxDamage = (total * 2) / 30;
					minDamage = (maxDamage * 7) / 10;
				}

				double damage = Utility.RandomMinMax(minDamage, maxDamage);

				if (EraAOS && targets.Count > 1)
				{
					damage = (damage * 2) / targets.Count;
				}
				else if (!EraAOS)
				{
					damage /= targets.Count;
				}

				foreach (Mobile m in targets)
				{
					double toDeal = damage;

					if (!EraAOS && m.CheckSkill(SkillName.MagicResist, 0.0, 120.0))
					{
						toDeal *= 0.5;
						m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
					}

					from.DoHarmful(m);
					SpellHelper.Damage(null, TimeSpan.Zero, m, from, toDeal);

					Effects.SendTargetEffect(m, 0x3709, 10, 30);
				}
			}

			double breakChance = EraAOS ? 0.01 : 0.16;

			if (Utility.RandomDouble() >= breakChance)
			{
				return;
			}

			from.SendLocalizedMessage(1049619); // The fire horn crumbles in your hands.
			Delete();
		}

		private static void EndAction(Mobile m)
		{
			m.EndAction(typeof(FireHorn));
			m.SendLocalizedMessage(1049621); // You catch your breath.
		}

		private class InternalTarget : Target
		{
			private readonly FireHorn m_Horn;

			public InternalTarget(FireHorn horn)
				: base(horn.EraAOS ? 3 : 2, true, TargetFlags.Harmful)
			{
				m_Horn = horn;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (m_Horn.Deleted)
				{
					return;
				}

				var entity = targeted as IEntity;

				if (UberScriptTriggers.Trigger(entity, from, TriggerName.onTargeted, m_Horn))
				{
					return;
				}

				IPoint3D loc;

				if (targeted is Item)
				{
					loc = ((Item)targeted).GetWorldLocation();
				}
				else
				{
					loc = targeted as IPoint3D;
				}

				m_Horn.Use(from, loc);
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.WriteEncodedInt(0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			reader.ReadEncodedInt();
		}
	}
}