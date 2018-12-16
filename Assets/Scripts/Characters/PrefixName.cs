using UnityEngine;
using System.Collections.Generic;
using Descension.Core;

namespace Descension.Name
{
    public static class PrefixName
    {
        public static string[] NamePrefix = new string[] { "Sir", "Lord", "Mr", "Sire" };

        public static string Generate(int prefix_chance, FantasyName name, Gender gender, string race)
        {
            string prefix = "";

            if (Random.Range(0, 100) < prefix_chance)
            {
                prefix = NamePrefix[Random.Range(0, NamePrefix.Length)];
            }

            return prefix;
        }
    }
}