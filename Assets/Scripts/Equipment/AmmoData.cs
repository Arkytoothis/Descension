using UnityEngine;
using System.Collections.Generic;
using Descension.Characters;
using Descension.Core;

namespace Descension.Equipment
{
    [System.Serializable]
    public class AmmoData
    {
        public AmmoType Type;

        public string hitSound;

        public List<ItemAttribute> Attributes;
        public List<DamageData> Damage;

        public AmmoData()
        {
            Type = AmmoType.None;
            Attributes = new List<ItemAttribute>();
            Damage = new List<DamageData>();
        }

        public AmmoData(AmmoType type, int attack, int range, int actions, List<DamageData> damage, string hitSound)
        {
            Type = type;
            this.hitSound = hitSound;

            Attributes = new List<ItemAttribute>();
            Attributes.Add(new ItemAttribute((int)AmmoAttributes.Actions, actions));
            Attributes.Add(new ItemAttribute((int)AmmoAttributes.Attack, attack));
            Attributes.Add(new ItemAttribute((int)AmmoAttributes.Range, range));

            Damage = new List<DamageData>();
            if (damage != null)
            {
                for (int i = 0; i < damage.Count; i++)
                {
                    Damage.Add(new DamageData(damage[i]));
                }
            }
        }

        public AmmoData(AmmoData data)
        {
            Type = data.Type;
            hitSound = data.hitSound;

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
            s += "\n" + Attributes[(int)AmmoAttributes.Attack].Value + " Attack";
            s += "<pos=50%>" + Attributes[(int)AmmoAttributes.Range].Value + " tile Range";

            if (Attributes[(int)AmmoAttributes.Actions].Value != 1)
                s += "\n" + Attributes[(int)AmmoAttributes.Actions].Value + " Actions";
            else
                s += "\n" + Attributes[(int)AmmoAttributes.Actions].Value + " Action";


            if (Damage != null)
            {
                for (int i = 0; i < Damage.Count; i++)
                {
                    s += "\n" + Damage[i].ToString();
                }
            }

            return s;
        }
    }
}