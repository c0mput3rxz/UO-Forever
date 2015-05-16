using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class EmeraldPower : Item
	{
    
		public override string DefaultName{ get{ return "Emerald Power"; } }
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public EmeraldPower() : this( 1 )
		{
		}

		[Constructable]
		public EmeraldPower( int amount ) : base( 0x3198 )
		{
			Stackable = true;
			Hue = 496;
            Amount = amount;
}

		public EmeraldPower( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}