using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xDF1, 0xDF0 )]
	public class StaffOfPower2 : BaseStaff
	{
public override int ArtifactRarity{ get{ return 10; } }
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.WhirlwindAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int OldStrengthReq{ get{ return 35; } }
		public override int NewMinDamage{ get{ return 8; } }
		public override int NewMaxDamage{ get{ return 33; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }

		[Constructable]
		public StaffOfPower2() : base( 0xDF0 )
		{
			Name = "Staff Of Power";
			Weight = 6.0;
			Hue = 606;
		}

		public StaffOfPower2( Serial serial ) : base( serial )
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