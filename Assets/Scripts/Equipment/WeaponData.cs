using UnityEngine;
using System.Collections.Generic;
using Descension.Characters;
using Descension.Core;

namespace Descension.Equipment
{
    public enum AttackType { Might, Finesse, Spell, Number, None }

    [System.Serializable]
    public class WeaponData
    {
        public WeaponType Type;
        public WeaponGripType gripType;
        public AttackType AttackType;
        public AmmoType AmmoType;

        public string idleAnimation;
        public string attackAnimation;

        public string aimSound;

        public string attackSound;
        public string hitSound;

        public List<ItemAttribute> Attributes;
        public List<DamageData> Damage;

        public WeaponData()
        {
            Type = WeaponType.None;
            AmmoType = AmmoType.None;
            Attributes = new List<ItemAttribute>();
            Damage = new List<DamageData>();
        }

        public WeaponData(WeaponType type, AmmoType ammo, AttackType attack_type, WeaponGripType gripType, int attack, int range, int actions, int parry, List<DamageData> damage,
            string idleAnimation, string attackAnimation, string aimSound, string attackSound, string hitSound)
        {
            Type = type;
            AmmoType = ammo;
            AttackType = attack_type;

            this.gripType = gripType;

            this.idleAnimation = idleAnimation;
            this.attackAnimation = attackAnimation;

            this.aimSound = aimSound;
            this.attackSound = attackSound;
            this.hitSound = hitSound;

            Attributes = new List<ItemAttribute>();
            Attributes.Add(new ItemAttribute((int)WeaponAttributes.Actions, actions));
            Attributes.Add(new ItemAttribute((int)WeaponAttributes.Attack, attack));
            Attributes.Add(new ItemAttribute((int)WeaponAttributes.Parry, parry));
            Attributes.Add(new ItemAttribute((int)WeaponAttributes.Range, range));

            Damage = new List<DamageData>();
            if (damage != null)
            {
                for (int i = 0; i < damage.Count; i++)
                {
                    Damage.Add(new DamageData(damage[i]));
                }
            }
        }

        public WeaponData(WeaponData data)
        {
            Type = data.Type;
            AmmoType = data.AmmoType;
            AttackType = data.AttackType;

            this.idleAnimation = data.idleAnimation;
            this.attackAnimation = data.attackAnimation;

            this.aimSound = data.aimSound;
            this.attackSound = data.attackSound;
            this.hitSound = data.hitSound;

            Attributes = new List<ItemAttribute>();
            for (int i = 0; i < data.Attributes.Count; i++)
            {
                Attributes.Add(data.Attributes[i]);
            }

            Damage = new List<DamageData>();

            if (data.Damage != null)
            {
                for (int i = 0; i < data.Damage.Count; i++)
                {
                    Damage.Add(new DamageData(data.Damage[i]));
                }
            }
        }

        public string GetText()
        {
            string s = "";

            s += " - " + Type;
            s += "\n" + Attributes[(int)WeaponAttributes.Attack].Value + " " + AttackType + " Attack";

            if (Attributes[(int)WeaponAttributes.Actions].Value != 1)
                s += "\n" + Attributes[(int)WeaponAttributes.Actions].Value + " Actions";
            else
                s += "\n" + Attributes[(int)WeaponAttributes.Actions].Value + " Action";

            s += "<pos=50%>" + Attributes[(int)WeaponAttributes.Range].Value + " tile Range";

            if (Damage != null)
            {
                for (int i = 0; i < Damage.Count; i++)
                {
                    s += "\n" + Damage[i].ToString();
                }
            }

            s += "\n" + Attributes[(int)WeaponAttributes.Parry].Value + "% Parry";

            return s;
        }
    }
}