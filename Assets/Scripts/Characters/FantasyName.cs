using System;
using Descension.Characters;
using Descension.Core;

namespace Descension.Name
{
    [System.Serializable]
    public class FantasyName
    {
        public Gender Gender { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string NickName { get; set; }
        public string LastName { get; set; }
        public string Postfix { get; set; }
        public string Land { get; set; }

        public FantasyName()
        {
            Gender = Gender.None;
            Title = "";
            FirstName = "First";
            NickName = "";
            LastName = "Last";
            Postfix = "";
            Land = "";
        }

        public FantasyName(string first, string nick, string last)
        {
            FirstName = first;
            NickName = nick;
            LastName = last;
        }

        public FantasyName(FantasyName name)
        {
            Gender = name.Gender;
            Title = name.Title;
            FirstName = name.FirstName;
            NickName = name.NickName;
            LastName = name.LastName;
            Postfix = name.Postfix;
            Land = name.Land;
        }

        public string FullName
        {
            get { return Title + " " + FirstName + " " + LastName + " " + Postfix; }
        }

        public string ShortName
        {
            get { return FirstName + " " + LastName; }
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", Gender, FirstName, LastName, Postfix, Land);
        }
    }
}