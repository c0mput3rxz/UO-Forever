using System;

namespace Server.Items
{
	[FlipableAttribute( 0x1415, 0x1416 )]
	public class WardenChest : PlateChest
	{
		[Constructable]
		public WardenChest() : base( 0x1415 )
		{
            Name = "plate chest of the warden";
            Hue = 1157;
			Weight = 10.0;
		}

        public override void OnSingleClick(Mobile from)
        {
            base.OnSingleClick(from);
            LabelTo(from, "Covered in congealing blood.", 1157);
        }

        public WardenChest(Serial serial)
            : base(serial)
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

			if ( Weight == 1.0 )
				Weight = 5.0;
		}
	}
}