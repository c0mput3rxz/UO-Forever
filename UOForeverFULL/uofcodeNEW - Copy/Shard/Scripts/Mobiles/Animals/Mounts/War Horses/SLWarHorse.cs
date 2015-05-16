using System;
using Server;

namespace Server.Mobiles
{
	public class SLWarHorse : BaseWarHorse
	{
        public override int InternalItemItemID { get { return 0x3EB0; } }
        
        [Constructable]
		public SLWarHorse() : base( 0x79, AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
		}

		public SLWarHorse( Serial serial ) : base( serial )
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