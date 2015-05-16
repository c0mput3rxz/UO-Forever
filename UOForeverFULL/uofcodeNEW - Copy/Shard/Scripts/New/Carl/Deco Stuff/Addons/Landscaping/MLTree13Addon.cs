/////////////////////////////////////////////////
//
// Automatically generated by the
// AddonGenerator script by Arya
//
/////////////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MLTree13Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new MLTree13AddonDeed();
			}
		}

		[ Constructable ]
		public MLTree13Addon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 15021 );
			AddComponent( ac, 3, -1, 0 );
			ac = new AddonComponent( 15020 );
			AddComponent( ac, 3, 0, 0 );
			ac = new AddonComponent( 15018 );
			AddComponent( ac, -3, 1, 0 );
			ac = new AddonComponent( 15017 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 15016 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 15015 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 15014 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 15013 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 15012 );
			AddComponent( ac, 3, 1, 0 );

		}

		public MLTree13Addon( Serial serial ) : base( serial )
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

	public class MLTree13AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new MLTree13Addon();
			}
		}

		[Constructable]
		public MLTree13AddonDeed()
		{
			Name = "MLTree13";
		}

		public MLTree13AddonDeed( Serial serial ) : base( serial )
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