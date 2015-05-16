using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x26C2, 0x26CC )]
	public class DragonSlayer : BaseRanged
	{
public override int ArtifactRarity{ get{ return 15; } }
		public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MovingShot; } }
        
		public override int OldStrengthReq{ get{ return 45; } }
		public override int NewMinDamage{ get{ return 15; } }
		public override int NewMaxDamage{ get{ return 17; } }
		public override int OldSpeed{ get{ return 25; } }

		public override int DefMaxRange{ get{ return 10; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public DragonSlayer() : base( 0x26C2 )
		{
			Name = "Dragon Slayer";
			Weight = 5.0;
			Hue = 531;
		}

		public DragonSlayer( Serial serial ) : base( serial )
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