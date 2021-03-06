using System;

namespace Server.Items
{
	public class Basin : Item
	{
		[Constructable]
		public Basin() : base( 0x1009 )
		{
		}

		public Basin( Serial serial ) : base( serial )
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