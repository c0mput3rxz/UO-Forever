using System;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x26C2, 0x26CC )]
	public class CompositeBow : BaseRanged
	{
		public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MovingShot; } }

		public override int OldStrengthReq{ get{ return 45; } }
		public override int NewMinDamage{ get{ return 8; } }
		public override int NewMaxDamage{ get{ return 43; } }
		//public override int DiceDamage { get { return Utility.Dice( 5, 8, 3 ); } } // 5d8+3 (8-43)
		public override int OldSpeed{ get{ return 24; } }

		public override int DefMaxRange{ get{ return 10; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public CompositeBow() : base( 0x26C2 )
		{
			Weight = 5.0;
		}

		public CompositeBow( Serial serial ) : base( serial )
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