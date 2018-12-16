using UnityEngine;
using System.Collections.Generic;
using Descension.Core;

namespace Descension.Name
{
    public static class NameGenerator
    {
        public static FantasyName Get(Gender gender, string race, string profession)
        {
            FantasyName name = new FantasyName();

            name.FirstName = FirstName.Generate(name, race);
            name.LastName = LastName.Generate(name, profession);
            name.Title = PrefixName.Generate(50, name, gender, race);
            name.Postfix = PostfixName.Generate(50, name, gender, race);
            name.Land = LandName.Generate(name, race);

            return name;
        }
    }
}