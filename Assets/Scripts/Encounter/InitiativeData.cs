using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    [System.Serializable]
    public class InitiativeData
    {
        public enum Type { Player, Neutral, Enemy, Ability, NUmber, None }

        [SerializeField] Type type = Type.None;
        [SerializeField] string details = "";
        [SerializeField] int initiative = 0;
        [SerializeField] int index = 0;
        [SerializeField] Faction faction = Faction.None;

        public Type Type1 { get => type; set => type = value; }
        public string Details { get => details; set => details = value; }
        public int Initiative { get => initiative; set => initiative = value; }
        public int Index { get => index; set => index = value; }
        public Faction Faction { get => faction; set => faction = value; }

        public InitiativeData()
        {
            type = Type.None;
            details = "";
            initiative = 0;
            index = 0;
            faction = Faction.None;
        }

        public InitiativeData(Type type, string details, int initiative, int index, Faction faction)
        {
            this.type = type;
            this.details = details;
            this.initiative = initiative;
            this.index = index;
            this.faction = faction;
        }
    }
}