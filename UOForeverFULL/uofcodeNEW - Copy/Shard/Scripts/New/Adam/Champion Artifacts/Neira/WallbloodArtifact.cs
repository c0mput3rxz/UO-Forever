﻿#region References

using System.Linq;
using Server.Network;

#endregion

namespace Server.Items
{
    public class WallBloodArtifact : Item
    {
        [Constructable]
        public WallBloodArtifact()
            : base(7571)
        {
            Name = "defiled blood";
            Weight = 2;
            Movable = true;
            Hue = 1372;
        }

        public WallBloodArtifact(Serial serial)
            : base(serial)
        {}

        public override void OnSingleClick(Mobile m)
        {
            base.OnSingleClick(m);
            LabelTo(m,
                "[Champion Artifact]",
                134);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}