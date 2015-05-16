using System;

namespace Server.Items
{
	[FlipableAttribute( 0x2B67, 0x315E )]
	public class WoodlandChest : BaseArmor
	{
		
		
		
		
		

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }

		
		public override int OldStrReq{ get{ return 95; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }
		public override Race RequiredRace { get { return Race.Elf; } }

		[Constructable]
		public WoodlandChest() : base( 0x2B67 )
		{
			Weight = 8.0;
		}

		public WoodlandChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}