using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a swamp tentacle corpse" )]
	public class SwampTentacle : BaseCreature
	{
		public override string DefaultName{ get{ return "a swamp tentacle"; } }

		[Constructable]
		public SwampTentacle() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 66;
			BaseSoundID = 352;

			SetStr( 96, 120 );
			SetDex( 66, 85 );
			SetInt( 16, 30 );

			SetHits( 58, 72 );
			SetMana( 0 );

			SetDamage( 6, 12 );

			
			

			
			
			
			
			

			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 65.1, 80.0 );
			SetSkill( SkillName.Wrestling, 65.1, 80.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 28;

			PackReg( 3 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override Poison PoisonImmune{ get{ return Poison.Greater; } }

		public SwampTentacle( Serial serial ) : base( serial )
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