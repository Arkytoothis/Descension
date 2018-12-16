using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Characters;
using Descension.Equipment;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class DamageEffect : AbilityEffect
    {
        public DamageData Data;

        public DamageEffect()
        {
            EffectType = AbilityEffectType.Damage;
            Data = new DamageData();
        }

        public DamageEffect(DamageType type, int attribute, GameValue damage, GameValue duration, int armor_pierce, int barrier_pierce, float multiplier = 0f, int level = 0)
        {
            EffectType = AbilityEffectType.Damage;

            Data = new DamageData();
            Data.Type = type;
            Data.ArmorPierce = armor_pierce;
            Data.BarrierPierce = barrier_pierce;
            Data.Attribute = attribute;
            Data.DamageDice = new GameValue(damage);
            Data.Duration = new GameValue(duration);
            Data.Multiplier = multiplier;
            Data.LevelModified = level;
        }

        public override string GetTooltipString()
        {
            string text = "";

            text = Data.ToString();

            return text;
        }
    }
}