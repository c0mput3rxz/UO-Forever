using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly corpse" )]
	public class Spectre : BaseCreature
	{
		public override string DefaultName{ get{ return "a spectre"; } }

		[Constructable]
		public Spectre() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 26;
			Hue = 0x4001;
			BaseSoundID = 0x482;

			SetStr( 76, 100 );
			SetDex( 76, 95 );
			SetInt( 36, 60 );

			SetHits( 46, 60 );

			SetDamage( 7, 11 );





            Alignment = Alignment.Undead;
			

			SetSkill( SkillName.EvalInt, 55.1, 70.0 );
			SetSkill( SkillName.Magery, 55.1, 70.0 );
			SetSkill( SkillName.MagicResist, 55.1, 70.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 55.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 28;

			PackReg( 10 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool OnBeforeDeath()
		{
			if ( base.OnBeforeDeath() )
			{
				if ( !Summoned && !NoKillAwards && ( Region != null && Region.IsPartOf( "Skara Brae" ) ) )
					PackItem( new EtherealResidue() );

				return true;
			}

			return false;
		}

		public override bool BleedImmune{ get{ return true; } }

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int DefaultBloodHue{ get{ return -1; } }

		public Spectre( Serial serial ) : base( serial )
		{
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