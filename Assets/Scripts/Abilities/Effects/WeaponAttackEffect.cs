using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Characters;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class WeaponAttackEffect : AbilityEffect
    {
        public int AttackMod;
        public DamageType DamageType;
        public int ProcChance;
        public GameValue Damage;

        public WeaponAttackEffect()
        {
            EffectType = AbilityEffectType.Weapon_Attack;
            AttackMod = 0;
            DamageType = DamageType.None;
            ProcChance = 0;
            Damage = new GameValue();
        }

        public WeaponAttackEffect(int attack_mod, DamageType damage_type, int proc, GameValue damage)
        {
            EffectType = AbilityEffectType.Weapon_Attack;
            AttackMod = attack_mod;
            DamageType = damage_type;
            ProcChance = proc;
            Damage = new GameValue(damage);
        }

        public override string GetTooltipString()
        {
            string s = "";

            if (ProcChance > 0)
                s += ProcChance + "% ";

            s += Damage.ToString();

            return s;
        }
    }
}