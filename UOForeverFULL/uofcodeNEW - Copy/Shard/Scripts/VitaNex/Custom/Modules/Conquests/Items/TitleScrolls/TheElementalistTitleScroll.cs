#region References

using Server.Mobiles;
using Server.Network;

#endregion

namespace Server.Engines.CustomTitles
{
    public class ConquestElementalistTitle : ConquestTitleScroll
    {
        public ConquestElementalistTitle(Serial serial)
            : base(serial)
        {}

        [Constructable]
        public ConquestElementalistTitle()
            : base("The Elementalist")
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            int version = writer.SetVersion(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.GetVersion();
        }
    }
}