using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a cougar corpse" )]
	public class Cougar : BaseCreature
	{
		public override string DefaultName{ get{ return "a cougar"; } }

		[Constructable]
		public Cougar() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Body = 63;
			BaseSoundID = 0x73;

			SetStr( 56, 80 );
			SetDex( 66, 85 );
			SetInt( 26, 50 );

			SetHits( 34, 48 );
			SetMana( 0 );

			SetDamage( 4, 10 );

			

			
			
			
			

			SetSkill( SkillName.MagicResist, 15.1, 30.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 16;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 41.1;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 10; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Feline; } }

		public Cougar(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}