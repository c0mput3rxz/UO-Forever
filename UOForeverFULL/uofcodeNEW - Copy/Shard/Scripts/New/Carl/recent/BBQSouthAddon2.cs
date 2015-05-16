/////////////////////////////////////////////////
//                                             //
// Automatically generated by the              //
// AddonGenerator script by Arya               //
//                                             //
/////////////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BBQSouthAddon2 : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new BBQSouthAddon2Deed();
			}
		}

		[ Constructable ]
		public BBQSouthAddon2()
		{
			AddComponent( new AddonComponent( 7135 ), 3, 1, 1 );
			AddComponent( new AddonComponent( 1822 ), 0, 1, 2 );
			AddComponent( new AddonComponent( 1822 ), 0, 1, 0 );
			AddComponent( new AddonComponent( 6467 ), 0, 1, 4 );
			AddComponent( new AddonComponent( 1822 ), -2, 0, 4 );
			AddComponent( new AddonComponent( 5628 ), -2, 0, 9 );
			AddComponent( new AddonComponent( 1822 ), -2, 1, 0 );
			AddComponent( new AddonComponent( 4846 ), -2, 1, 5 );
			AddComponent( new AddonComponent( 7704 ), -2, 1, 7 );
			AddComponent( new AddonComponent( 1822 ), -3, 1, 2 );
			AddComponent( new AddonComponent( 1822 ), -3, 1, 0 );
			AddComponent( new AddonComponent( 2450 ), -3, 1, 7 );
			AddComponent( new AddonComponent( 5110 ), -3, 1, 8 );
			AddComponent( new AddonComponent( 1822 ), 1, 0, 4 );
			AddComponent( new AddonComponent( 5625 ), 1, 0, 9 );
			AddComponent( new AddonComponent( 1822 ), 2, 0, 3 );
			AddComponent( new AddonComponent( 1822 ), 2, 0, 0 );
			AddComponent( new AddonComponent( 5637 ), 2, 0, 8 );
			AddComponent( new AddonComponent( 1822 ), 2, 1, 2 );
			AddComponent( new AddonComponent( 1822 ), 2, 1, 0 );
			AddComponent( new AddonComponent( 4329 ), 2, 1, 7 );
			AddComponent( new AddonComponent( 1822 ), 1, 1, 0 );
			AddComponent( new AddonComponent( 4846 ), 1, 1, 5 );
			AddComponent( new AddonComponent( 2420 ), 1, 1, 6 );
			AddComponent( new AddonComponent( 2416 ), 1, 1, 14 );
			AddComponent( new AddonComponent( 1822 ), -3, 0, 3 );
			AddComponent( new AddonComponent( 2518 ), -3, 0, 8 );
			AddComponent( new AddonComponent( 1822 ), -1, 1, 0 );
			AddComponent( new AddonComponent( 4846 ), -1, 1, 5 );
			AddComponent( new AddonComponent( 7704 ), -1, 1, 7 );
			AddComponent( new AddonComponent( 1822 ), -1, 0, 1 );
			AddComponent( new AddonComponent( 1822 ), -1, 0, 6 );
			AddComponent( new AddonComponent( 5634 ), -1, 0, 11 );
			AddComponent( new AddonComponent( 1822 ), 0, 0, 1 );
			AddComponent( new AddonComponent( 1822 ), 0, 0, 6 );
			AddComponent( new AddonComponent( 2459 ), 0, 0, 11 );
			AddonComponent ac;
			ac = new AddonComponent( 1822 );
			AddComponent( ac, -3, 1, 2 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, -3, 0, 3 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, -2, 0, 4 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, -1, 0, 1 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, 0, 1, 2 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, 0, 0, 1 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, 1, 0, 4 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, 2, 1, 2 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, 2, 0, 3 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, -3, 1, 0 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, -1, 0, 6 );
			ac = new AddonComponent( 1822 );
			AddComponent( ac, 0, 0, 6 );
			ac = new AddonComponent( 4846 );
			ac.Light = LightType.Circle225;
			AddComponent( ac, -2, 1, 5 );
			ac = new AddonComponent( 4846 );
			ac.Light = LightType.Circle225;
			AddComponent( ac, -1, 1, 5 );
            ac = new AddonComponent( 3530 );
            ac.Hue = 1;
            ac.Name = "Grill";
            AddComponent( ac, -2, 1, 6 );
            ac = new AddonComponent( 3530 );
            ac.Hue = 1;
            ac.Name = "Grill";
            AddComponent( ac, -1, 1, 6 );
            ac = new AddonComponent( 3530 );
            ac.Hue = 1;
            ac.Name = "Grill";
            AddComponent( ac, 1, 1, 6 );
            ac = new AddonComponent( 7704 );
            ac.Name = "Grilled Salmon";
            AddComponent( ac, -2, 1, 7 );
            ac = new AddonComponent( 7704 );
            ac.Name = "Grilled Salmon";
            AddComponent( ac, -1, 1, 7 );
            ac = new AddonComponent( 4846 );
            ac.Light = LightType.Circle225;
            AddComponent( ac, 1, 1, 5 );
            ac = new AddonComponent( 7135 );
            ac.Name = "Mesquite Logs";
            AddComponent( ac, 3, 1, 1 );
			ac = new AddonComponent( 2420 );
			AddComponent( ac, 1, 1, 6 );
			ac = new AddonComponent( 4329 );
			AddComponent( ac, 2, 1, 7 );
			ac = new AddonComponent( 2416 );
			AddComponent( ac, 1, 1, 14 );
			ac = new AddonComponent( 2450 );
			AddComponent( ac, -3, 1, 7 );
			ac = new AddonComponent( 5637 );
			AddComponent( ac, 2, 0, 8 );
			ac = new AddonComponent( 5628 );
			AddComponent( ac, -2, 0, 9 );
			ac = new AddonComponent( 5634 );
			AddComponent( ac, -1, 0, 11 );
			ac = new AddonComponent( 5625 );
			AddComponent( ac, 1, 0, 9 );
			ac = new AddonComponent( 2459 );
			ac.Name = "Cooking Wine";
			AddComponent( ac, 0, 0, 11 );
			ac = new AddonComponent( 2518 );
			ac.Name = "Marinade";
			AddComponent( ac, -3, 0, 8 );
            ac = new AddonComponent( 6467 );
            ac.Name = "Imported Spices";
            AddComponent( ac, 0, 1, 4 );
			ac = new AddonComponent( 5110 );
			AddComponent( ac, -3, 1, 8 );

		}

		public BBQSouthAddon2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class BBQSouthAddon2Deed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new BBQSouthAddon2();
			}
		}

		[Constructable]
		public BBQSouthAddon2Deed()
		{
			Name = "BBQSouth";
		}

		public BBQSouthAddon2Deed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}