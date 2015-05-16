﻿#region References

using System.Linq;
using Server.Network;

#endregion

namespace Server.Items
{
    public class DemonSkullArtifact : Item
    {
        [Constructable]
        public DemonSkullArtifact()
            : base(8783)
        {
            Name = "demon skull";
            Weight = 2;
            Movable = true;
            ItemID = GetItemID();
            Hue = 33;
        }

        public DemonSkullArtifact(Serial serial)
            : base(serial)
        {}

        public override void OnSingleClick(Mobile m)
        {
            base.OnSingleClick(m);
            LabelTo(m,
                "[Champion Artifact]",
                134);
        }

        public int GetItemID()
        {
            if (Utility.RandomDouble() <= 0.05)
            {
                return 8785;
            }
            return 8783;
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