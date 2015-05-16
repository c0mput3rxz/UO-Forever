﻿using System;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
    public class GasTrapArtifact : BaseTrap
    {
        private Poison m_Poison;

        [CommandProperty(AccessLevel.GameMaster)]
        public Poison Poison
        {
            get { return m_Poison; }
            set { m_Poison = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public GasTrapType Type
        {
            get
            {
                switch (ItemID)
                {
                    case 0x113C: return GasTrapType.NorthWall;
                    case 0x1147: return GasTrapType.WestWall;
                    case 0x11A8: return GasTrapType.Floor;
                }

                return GasTrapType.WestWall;
            }
            set
            {
                ItemID = GetBaseID(value);
            }
        }

        public static int GetBaseID(GasTrapType type)
        {
            switch (type)
            {
                case GasTrapType.NorthWall: return 0x113C;
                case GasTrapType.WestWall: return 0x1147;
                case GasTrapType.Floor: return 0x11A8;
            }

            return 0;
        }

        [Constructable]
        public GasTrapArtifact()
            : this(GasTrapType.Floor)
        {
            Name = "noxious cloud";
            Hue = 2966;
            Movable = true;
        }

        [Constructable]
        public GasTrapArtifact(GasTrapType type)
            : this(type, Poison.Lesser)
        {
        }

        [Constructable]
        public GasTrapArtifact(Poison poison)
            : this(GasTrapType.Floor, Poison.Lesser)
        {
        }

        [Constructable]
        public GasTrapArtifact(GasTrapType type, Poison poison)
            : base(GetBaseID(type))
        {
            m_Poison = poison;
        }

        public override bool PassivelyTriggered { get { return false; } }
        public override TimeSpan PassiveTriggerDelay { get { return TimeSpan.Zero; } }
        public override int PassiveTriggerRange { get { return 2; } }
        public override TimeSpan ResetDelay { get { return TimeSpan.FromSeconds(0.0); } }

        public override void OnTrigger(Mobile from)
        {
            if (from == null || m_Poison == null || from.IsDeadBondedPet || !from.Alive || from.AccessLevel > AccessLevel.Player)
                return;

            if ((from.Player || (from is BaseCreature && ((BaseCreature)from).ControlMaster != null && ((BaseCreature)from).ControlMaster.Player)))
            {
                Effects.SendLocationEffect(Location, Map, GetBaseID(this.Type) - 2, 16, 3, GetEffectHue(), 0);
                Effects.PlaySound(Location, Map, 0x231);
            }
        }

        public GasTrapArtifact(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            Poison.Serialize(m_Poison, writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Poison = Poison.Deserialize(reader);
                        break;
                    }
            }
        }
    }
}