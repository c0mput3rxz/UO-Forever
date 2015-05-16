using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a doppleganger corpse" )]
	public class Doppleganger : BaseCreature
	{
		public override string DefaultName{ get{ return "a doppleganger"; } }

		[Constructable]
		public Doppleganger() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 0x309;
			BaseSoundID = 0x451;

			SetStr( 81, 110 );
			SetDex( 56, 75 );
			SetInt( 81, 105 );

			SetHits( 101, 120 );

			SetDamage( 8, 12 );

			
			
			
			
			

			SetSkill( SkillName.MagicResist, 75.1, 85.0 );
			SetSkill( SkillName.Tactics, 70.1, 80.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 55;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override int Hides{ get{ return 6; } }
		public override int Meat{ get{ return 1; } }

		public Doppleganger( Serial serial ) : base( serial )
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