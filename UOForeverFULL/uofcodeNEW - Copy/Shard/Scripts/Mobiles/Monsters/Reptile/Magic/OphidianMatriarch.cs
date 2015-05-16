#region References

using Server.Items;

#endregion

namespace Server.Mobiles
{
    [CorpseName("an ophidian corpse")]
    public class OphidianMatriarch : BaseCreature
    {
        public override string DefaultName { get { return "an ophidian matriarch"; } }

        [Constructable]
        public OphidianMatriarch() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = 87;
            BaseSoundID = 644;

            Alignment = Alignment.Inhuman;

            SetStr(416, 505);
            SetDex(96, 115);
            SetInt(366, 455);

            SetHits(250, 303);

            SetDamage(11, 13);


            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.Meditation, 5.4, 25.0);
            SetSkill(SkillName.MagicResist, 90.1, 100.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 16000;
            Karma = -16000;

            VirtualArmor = 50;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.MedScrolls, 2);

            if (0.04 > Utility.RandomDouble()) // 2 percent - multipy number x 100 to get percent
            {
                var scroll = new SkillScroll();
                scroll.Randomize();
                PackItem(scroll);
            }
        }

        public override Poison PoisonImmune { get { return Poison.Greater; } }
        public override int TreasureMapLevel { get { return 4; } }

        public override OppositionGroup OppositionGroup { get { return OppositionGroup.TerathansAndOphidians; } }

        public OphidianMatriarch(Serial serial) : base(serial)
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