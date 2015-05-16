#region References

using Server.Items;

#endregion

namespace Server.Mobiles
{
    [CorpseName("an ettins corpse")]
    public class Ettin : BaseCreature
    {
        public override string DefaultName { get { return "an ettin"; } }

        [Constructable]
        public Ettin() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = 18;
            BaseSoundID = 367;

            Alignment = Alignment.Giantkin;

            SetStr(136, 165);
            SetDex(56, 75);
            SetInt(31, 55);

            SetHits(82, 99);

            SetDamage(7, 17);


            SetSkill(SkillName.MagicResist, 40.1, 55.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 50.1, 60.0);

            Fame = 3000;
            Karma = -3000;

            VirtualArmor = 38;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Potions);
            AddPackedLoot(LootPack.MeagerProvisions, typeof(Bag));
            AddPackedLoot(LootPack.MeagerProvisions, typeof(Bag));
            if (0.4 > Utility.RandomDouble())
            {
                AddPackedLoot(LootPack.AverageProvisions, typeof(Pouch));
            }
        }

        public override bool CanRummageCorpses { get { return true; } }
        public override int TreasureMapLevel { get { return 1; } }
        public override int Meat { get { return 4; } }

        public Ettin(Serial serial) : base(serial)
        {}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}