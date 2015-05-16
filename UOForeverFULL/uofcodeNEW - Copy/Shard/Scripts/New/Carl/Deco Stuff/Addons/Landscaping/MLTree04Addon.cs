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
	public class MLTree04Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new MLTree04AddonDeed();
			}
		}

		[ Constructable ]
		public MLTree04Addon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 14826 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 14825 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 14824 );
			AddComponent( ac, -1, 1, 0 );

		}

		public MLTree04Addon( Serial serial ) : base( serial )
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

	public class MLTree04AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new MLTree04Addon();
			}
		}

		[Constructable]
		public MLTree04AddonDeed()
		{
			Name = "MLTree04";
		}

		public MLTree04AddonDeed( Serial serial ) : base( serial )
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