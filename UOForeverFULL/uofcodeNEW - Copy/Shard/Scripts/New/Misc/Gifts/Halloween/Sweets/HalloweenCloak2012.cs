// Original Author Unknown
// Updated to be halloween 2012 by boba

using System;
using Server;
using Server.Items;

namespace Server.Items
{  
	public class HalloweenCloak2012 : Cloak
	{
           	[Constructable]
           	public HalloweenCloak2012()
           	{
           		Name = "a Spectral Cloak";
			Hue = 0x4001;
			LootType = LootType.Blessed;
           	}

           	[Constructable]
           	public HalloweenCloak2012(int amount)
           	{
           	}

           	public HalloweenCloak2012(Serial serial) : base( serial )
           	{
           	}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( "Halloween 2012" );
		}

          	public override void Serialize(GenericWriter writer)
          	{
           		base.Serialize(writer);

           		writer.Write((int)0); // version 
     		}

           	public override void Deserialize(GenericReader reader)
      	{
           		base.Deserialize(reader);

          		int version = reader.ReadInt();
           	}
	}
}
