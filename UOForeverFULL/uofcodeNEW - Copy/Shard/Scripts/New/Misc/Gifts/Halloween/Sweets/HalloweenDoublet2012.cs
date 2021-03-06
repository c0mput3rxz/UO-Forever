// Original Author Unknown
// Updated to be halloween 2012 by boba

using System;
using Server;
using Server.Items;

namespace Server.Items
{  
	public class HalloweenDoublet2012 : Doublet
	{
           	[Constructable]
           	public HalloweenDoublet2012()
           	{
           		Name = "a Spectral Doublet";
			Hue = 0x4001;
			LootType = LootType.Blessed;
           	}

           	[Constructable]
           	public HalloweenDoublet2012(int amount)
           	{
           	}

           	public HalloweenDoublet2012(Serial serial) : base( serial )
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
