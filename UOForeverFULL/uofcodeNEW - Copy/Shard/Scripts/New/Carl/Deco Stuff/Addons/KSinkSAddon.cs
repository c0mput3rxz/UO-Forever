// Automatically generated by the
// AddonGenerator script by Arya
// Generator edited 10.Mar.07 by Papler
using System;
using Server;
using Server.Items;
namespace Server.Items
{
	public class KSinkSAddon : BaseAddon {
		public override BaseAddonDeed Deed{get{return new KSinkSAddonDeed();}}
		[ Constructable ]
		public KSinkSAddon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 4323 );
			ac.Hue = 1153;
			ac.Name = "Sink";
			AddComponent( ac, 0, 0, 9 );

			ac = new AddonComponent( 3625 );
			ac.Name = "Dawn";
			AddComponent( ac, 1, 0, 14 );

			ac = new AddonComponent( 3607 );
			ac.Hue = 55;
			ac.Name = "sponge";
			AddComponent( ac, 0, 0, 16 );

			ac = new AddonComponent( 9231 );
			ac.Hue = 1150;
			ac.Name = "door";
			AddComponent( ac, 1, 1, 0 );

			ac = new AddonComponent( 9231 );
			ac.Hue = 1150;
			ac.Name = "door";
			AddComponent( ac, 0, 1, 0 );

			ac = new AddonComponent( 4100 );
			ac.Name = "faucet";
			AddComponent( ac, 0, 0, 14 );

			ac = new AddonComponent( 2534 );
			AddComponent( ac, 1, 0, 13 );

			ac = new AddonComponent( 2585 );
			AddComponent( ac, 1, 0, 11 );

			ac = new AddonComponent( 677 );
			ac.Hue = 1150;
			AddComponent( ac, 1, -1, 12 );

			ac = new AddonComponent( 677 );
			ac.Hue = 1150;
			AddComponent( ac, 0, -1, 12 );

			ac = new AddonComponent( 676 );
			ac.Hue = 1150;
			AddComponent( ac, 1, -1, 5 );

			ac = new AddonComponent( 1801 );
			ac.Hue = 1150;
			ac.Name = "Sink";
			AddComponent( ac, 1, 0, 5 );

			ac = new AddonComponent( 1801 );
			ac.Hue = 1150;
			ac.Name = "Sink";
			AddComponent( ac, 0, 0, 5 );

			ac = new AddonComponent( 1801 );
			ac.Hue = 1150;
			ac.Name = "Sink";
			AddComponent( ac, 0, 0, 7 );

			ac = new AddonComponent( 1801 );
			ac.Hue = 1150;
			ac.Name = "Sink";
			AddComponent( ac, 1, 0, 7 );


		}
		public KSinkSAddon( Serial serial ) : base( serial ){}
		public override void Serialize( GenericWriter writer ){base.Serialize( writer );writer.Write( 0 );}
		public override void Deserialize( GenericReader reader ){base.Deserialize( reader );reader.ReadInt();}
	}

	public class KSinkSAddonDeed : BaseAddonDeed {
		public override BaseAddon Addon{get{return new KSinkSAddon();}}
		[Constructable]
		public KSinkSAddonDeed(){Name = "KSinkS";}
		public KSinkSAddonDeed( Serial serial ) : base( serial ){}
		public override void Serialize( GenericWriter writer ){	base.Serialize( writer );writer.Write( 0 );}
		public override void	Deserialize( GenericReader reader )	{base.Deserialize( reader );reader.ReadInt();}
	}
}