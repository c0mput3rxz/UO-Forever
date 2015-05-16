//Script Transformed By: Cherokee/Mule II aka. HotShot

using System;
using Server;
using Server.Misc;
using System.Collections;
using Server.Network;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a guardian daemon corpse" )]
	public class GuardianDaemon : BaseCreature
	{
		public Timer m_DeathTimer;

		[Constructable]
		public GuardianDaemon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a guardian daemon";
			Body = 9;
			BaseSoundID = 357;
			Hue = 2949;

			SetStr( 1396, 1485 );
			SetDex( 125, 195 );
			SetInt( 906, 1026 );

			SetHits( 10000, 12500 );

			SetDamage( 34, 40 );

			
			
			
			
			

			
			
			
			
			

			SetSkill( SkillName.EvalInt, 125.1, 130.0 );
			SetSkill( SkillName.Magery, 125.1, 130.0 );
			SetSkill( SkillName.Meditation, 125.1, 130.0 );
			SetSkill( SkillName.MagicResist, 140.5, 170.0 );
			SetSkill( SkillName.Tactics, 125.1, 130.0 );
			SetSkill( SkillName.Wrestling, 125.1, 130.0 );

			Fame = 50000;
			Karma = -50000;

			VirtualArmor = 100;

			PackGem();
			PackGem();
			PackPotion();
			PackGold( 30000, 40000 );
			PackScroll( 2, 8 );
			PackMagicItems( 3, 5, 0.95, 0.95 );
			PackMagicItems( 4, 5, 0.80, 0.65 );
			PackMagicItems( 4, 5, 0.80, 0.65 );
			PackSlayer();

			if ( Utility.RandomDouble() <= 0.30 )
			{
				PackItem( new DaemonEgg() );
			}
		}

		public override int GetIdleSound()
		{
			return 0x2D3;
		}

		public override int GetHurtSound()
		{
			return 0x2D1;
		}

		public override bool AutoDispel{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public GuardianDaemon( Serial serial ) : base( serial )
		{
		}

		private DateTime m_NextBreathe;

		public override void OnActionCombat()
		{
			Mobile combatant = Combatant;

			if ( combatant == null || combatant.Deleted || combatant.Map != Map || !InRange( combatant, 12 ) || !CanBeHarmful( combatant ) || !InLOS( combatant ) )
				return;

			if ( DateTime.UtcNow >= m_NextBreathe )
			{
				Breathe( combatant );

				m_NextBreathe = DateTime.UtcNow + TimeSpan.FromSeconds( 12.0 + (3.0 * Utility.RandomDouble()) ); // 12-15 seconds
			}
		}

		public void Breathe( Mobile m )
		{
			DoHarmful( m );

			new BreatheTimer( m, this ).Start();

			this.Frozen = true;

			this.MovingParticles( m, 0x1FBE, 1, 0, false, true, Utility.RandomList( 1157, 1175, 1172, 1171, 1170, 1169, 1168, 1167, 1166, 1165 ), 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
		}

		private class BreatheTimer : Timer
		{
			private GuardianDaemon d;
			private Mobile m_Mobile;

			public BreatheTimer( Mobile m, GuardianDaemon owner ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
			{
				d = owner;
				m_Mobile = m;
			}

			protected override void OnTick()
			{
				d.Frozen = false;

				m_Mobile.PlaySound( 0x11D );
				m_Mobile.Damage( Utility.RandomMinMax( 250, 300 ));
				Stop();
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}