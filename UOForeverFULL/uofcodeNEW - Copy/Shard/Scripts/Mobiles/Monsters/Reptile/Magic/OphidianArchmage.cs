using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an ophidian corpse" )]
	[TypeAlias( "Server.Mobiles.OphidianJusticar", "Server.Mobiles.OphidianZealot" )]
	public class OphidianArchmage : BaseCreature
	{
		private static string[] m_Names = new string[]
			{
				"an ophidian justicar",
				"an ophidian zealot"
			};

		[Constructable]
		public OphidianArchmage() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = m_Names[Utility.Random( m_Names.Length )];
			Body = 85;
			BaseSoundID = 639;

            Alignment = Alignment.Inhuman;

			SetStr( 281, 305 );
			SetDex( 191, 215 );
			SetInt( 226, 250 );

			SetHits( 169, 183 );
			SetStam( 36, 45 );

			SetDamage( 5, 10 );

			

			
			
			
			
			

			SetSkill( SkillName.EvalInt, 95.1, 100.0 );
			SetSkill( SkillName.Magery, 95.1, 100.0 );
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.2, 60.0 );

			Fame = 11500;
			Karma = -11500;

			VirtualArmor = 44;

			PackReg( 5, 15 );
			PackNecroReg( 5, 15 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );

		if ( 0.04 > Utility.RandomDouble() )// 2 percent - multipy number x 100 to get percent
            { SkillScroll scroll = new SkillScroll(); scroll.Randomize(); PackItem(scroll); }
		}

		public override int Meat{ get{ return 1; } }

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.TerathansAndOphidians; }
		}

		public OphidianArchmage( Serial serial ) : base( serial )
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