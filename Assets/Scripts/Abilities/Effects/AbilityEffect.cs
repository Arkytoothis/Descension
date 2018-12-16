using UnityEngine;
using System.Collections.Generic;
using System;

namespace Descension.Abilities
{
    public enum AbilityEffectType
    {
        Alter_Characteristic, Blind, Conjure_Item, Damage, Knockback, Knockdown, Restore, Stun, Summon_Creature, Taunt, Teleport, Weapon_Attack,
        Number, None
    }

    public enum AbilityEffectTypeShort
    {
        Alter_Char, Blind, Conjure, Damage, Knockback, Knockdown, Restore, Stun, Summon, Taunt, Teleport, Attack,
        Number, None
    }

    [System.Serializable]
    public abstract class AbilityEffect
    {
        public AbilityEffectType EffectType;
        public List<AbilityPartWidgetType> Widgets;

        //abstract public void Setup();
        abstract public string GetTooltipString();
    }
}