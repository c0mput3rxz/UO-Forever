using System;
using Server;

namespace Server.Items
{
	public class BlazeOfDeath : Halberd
	{
		public override int LabelNumber{ get{ return 1063486; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public BlazeOfDeath()
		{
			Hue = 0x501;			
		}

		public BlazeOfDeath( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}