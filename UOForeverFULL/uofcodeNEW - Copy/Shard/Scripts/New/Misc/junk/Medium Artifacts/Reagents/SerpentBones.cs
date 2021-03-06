using System;
using Server;

namespace Server.Items
{
    public class SerpentBones : Item
    {
        [Constructable]
		public SerpentBones() : this( 1 )
		{
		}

		[Constructable]
		public SerpentBones( int amount ) : base( 0x0F7E )
		{
           		 Name = "Serpent Bones";
			 Stackable = true;
           	  	 Hue = 1367;
			 Amount = amount;
		}

        public SerpentBones(Serial serial)
            : base(serial)
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