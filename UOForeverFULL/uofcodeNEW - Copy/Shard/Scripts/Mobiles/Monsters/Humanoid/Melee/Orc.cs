#region References

using Server.Items;
using Server.Misc;

#endregion

namespace Server.Mobiles
{
    [CorpseName("an orcish corpse")]
    public class Orc : BaseCreature
    {
        public override InhumanSpeech SpeechType { get { return InhumanSpeech.Orc; } }

        [Constructable]
        public Orc() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("orc");
            Body = 17;
            BaseSoundID = 0x45A;

            SetStr(96, 120);
            SetDex(81, 105);
            SetInt(36, 60);

            SetHits(58, 72);

            SetDamage(5, 7);

            Alignment = Alignment.Orc;


            SetSkill(SkillName.MagicResist, 50.1, 75.0);
            SetSkill(SkillName.Tactics, 55.1, 80.0);
            SetSkill(SkillName.Wrestling, 50.1, 70.0);

            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 28;

            switch (Utility.Random(20))
            {
                case 0:
                    PackItem(new Scimitar());
                    break;
                case 1:
                    PackItem(new Katana());
                    break;
                case 2:
                    PackItem(new WarMace());
                    break;
                case 3:
                    PackItem(new WarHammer());
                    break;
                case 4:
                    PackItem(new Kryss());
                    break;
                case 5:
                    PackItem(new Pitchfork());
                    break;
            }

            PackItem(new ThighBoots());

            switch (Utility.Random(3))
            {
                case 0:
                    PackItem(new Ribs());
                    break;
                case 1:
                    PackItem(new Shaft());
                    break;
                case 2:
                    PackItem(new Candle());
                    break;
            }

            if (0.2 > Utility.RandomDouble())
            {
                PackItem(new Ribs());
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddPackedLoot(LootPack.PoorProvisions, typeof(Bag));
            AddPackedLoot(LootPack.MeagerProvisions, typeof(Bag));
        }

        public override bool CanRummageCorpses { get { return true; } }
        public override int TreasureMapLevel { get { return 1; } }
        public override int Meat { get { return 1; } }
        public override int DefaultBloodHue { get { return -2; } }
        public override int BloodHueTemplate { get { return Utility.RandomBlackHue(); } }

        public override OppositionGroup OppositionGroup { get { return OppositionGroup.SavagesAndOrcs; } }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
            {
                return false;
            }

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                aggressor.Damage(50);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
            }
        }

        public Orc(Serial serial) : base(serial)
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