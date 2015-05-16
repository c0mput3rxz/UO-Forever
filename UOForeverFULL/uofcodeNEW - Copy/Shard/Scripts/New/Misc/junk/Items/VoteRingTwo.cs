using System;
using Server;

namespace Server.Items
{
	public class VoteEarringsTwo : GoldEarrings
	{
		public override int ArtifactRarity{ get{ return 11; } }

		[Constructable]
		public VoteEarringsTwo()
		{
			Name = "Mage's Oblivion";
			Hue = 1153;
		}

		public VoteEarringsTwo( Serial serial ) : base( serial )
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

			if ( Hue == 1153 )
				Hue = 1153;
		}
	}
}