#region References
using System;

using Server.Mobiles;
#endregion

namespace Server.Spells.Eighth
{
	public class AirElementalSpell : MagerySpell
	{
		private static readonly SpellInfo m_Info = new SpellInfo(
			"Air Elemental", "Kal Vas Xen Hur", 269, 9010, false, Reagent.Bloodmoss, Reagent.MandrakeRoot, Reagent.SpidersSilk);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }

		public AirElementalSpell(Mobile caster, Item scroll)
			: base(caster, scroll, m_Info)
		{ }

		public override bool CheckCast()
		{
			if (!base.CheckCast())
			{
				return false;
			}

			if ((Caster.Followers + 2) > Caster.FollowersMax)
			{
				Caster.SendLocalizedMessage(1049645); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if (CheckSequence())
			{
				TimeSpan duration = TimeSpan.FromSeconds((2 * Caster.Skills.Magery.Fixed) / 5);

				if (Caster.EraAOS)
				{
					SpellHelper.Summon(new SummonedAirElemental(), Caster, 0x217, duration, false, false);
				}
				else if (Caster.EraUOR)
				{
					SpellHelper.Summon(new AirElemental(), Caster, 0x217, duration, false, false);
				}
				else
				{
					// HACK: Convert to T2A mechanics.
					SpellHelper.Summon(new AirElemental(), Caster, 0x217, duration, false, false);
				}
			}

			FinishSequence();
		}
	}
}