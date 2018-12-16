using UnityEngine;
using System.Collections.Generic;
using Descension.Characters;
using Descension.Core;

namespace Descension.Equipment
{
    [System.Serializable]
    public class DamageData
    {
        public DamageType Type;
        public int Attribute;
        public GameValue DamageDice;
        public float Multiplier;
        public int LevelModified;
        public GameValue Duration;
        public int ArmorPierce;
        public int BarrierPierce;

        public DamageData()
        {
            Type = DamageType.None;
            Attribute = 0;
            DamageDice = GameValue.Zero;
            Multiplier = 0f;
            LevelModified = 0;
            Duration = GameValue.Zero;
            ArmorPierce = 0;
            BarrierPierce = 0;
        }

        public DamageData(DamageType type, int attribute, GameValue damage, GameValue duration, int ap, int bp, float multiplier = 0f, int level = 0)
        {
            Type = type;
            Attribute = attribute;
            DamageDice = new GameValue(damage);
            Duration = new GameValue(duration);
            Multiplier = multiplier;
            LevelModified = level;
            ArmorPierce = ap;
            BarrierPierce = bp;
        }

        public DamageData(DamageData data)
        {
            Type = data.Type;
            Attribute = data.Attribute;
            DamageDice = new GameValue(data.DamageDice);
            Duration = new GameValue(data.Duration);
            Multiplier = data.Multiplier;
            LevelModified = data.LevelModified;
            ArmorPierce = data.ArmorPierce;
            BarrierPierce = data.BarrierPierce;
        }

        public override string ToString()
        {
            string text = "";

            if (DamageDice.IsRandom == true)
            {
                text = DamageDice.Number + "d" + DamageDice.Die.ToString();
            }
            else
            {
                text = DamageDice.Number.ToString();
            }

            if (DamageDice.Modifer > 0)
                text += " +" + DamageDice.Modifer;
            else if (DamageDice.Modifer < 0)
                text += " -" + DamageDice.Modifer;

            text += " " + Type + "/" + Database.DerivedAttributes[Attribute].Name + " damage ";

            if (LevelModified != 0)
            {
                text += " per " + LevelModified + " level";
                if (LevelModified > 1)
                    text += "s";
            }

            if (Duration.Number != 0)
                text += " for " + Duration.ToString() + " turns";

            if (ArmorPierce > 0)
                text += " " + ArmorPierce + " ap ";
            if (BarrierPierce > 0)
                text += BarrierPierce + " bp";

            return text;
        }
    }
}