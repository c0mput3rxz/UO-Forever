﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Items;
using Server.Misc;
using Server.Network;

namespace Server.Scripts.VitaNex.Custom.Modules.Conquests.Items
{
    public class ConquestSashAlchemist : ConquestBaseWerable
    {
        [Constructable]
        public ConquestSashAlchemist()
            : base(5441, Layer.Earrings)
        {
            Weight = 1.0;
            Name = "Sash of the Master Alchemist";
            Hue = 1462;
        }

        public ConquestSashAlchemist(Serial serial)
            : base(serial)
        { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}
