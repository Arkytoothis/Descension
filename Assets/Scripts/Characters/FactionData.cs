using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Characters
{
    public enum ReputationLevel
    {
        Hated, Neutral, Friendly,
        Number, None
    }

    public class FactionData
    {
        public string name;
        public string key;

        public Dictionary<string, Reputation> reputations;

        public FactionData()
        {
            name = "";
            reputations = new Dictionary<string, Reputation>();
        }

        public FactionData(string name, string key)
        {
            reputations = new Dictionary<string, Reputation>();

            this.name = name;
            this.key = key;
        }

        public FactionData(FactionData faction)
        {
            name = faction.name;
            key = faction.key;

            reputations = new Dictionary<string, Reputation>();

            foreach (KeyValuePair<string, Reputation> kvp in faction.reputations)
            {
                reputations.Add(kvp.Key, new Reputation(kvp.Value));
            }
        }

        public ReputationLevel GetReputation(string faction)
        {
            if (reputations.ContainsKey(faction) == false)
            {
                Debug.Log("Does not contain data for faction");
                return ReputationLevel.None;
            }
            else
            {
                return reputations[faction].level;
            }
        }
    }

    public class Reputation
    {
        public ReputationLevel level;

        public Reputation()
        {
            level = ReputationLevel.None;
        }

        public Reputation(Reputation reputation)
        {
            level = reputation.level;
        }

        public Reputation(ReputationLevel level)
        {
            this.level = level;
        }
    }
}