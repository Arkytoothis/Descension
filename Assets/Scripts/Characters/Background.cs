using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Characters
{
    [System.Serializable]
    public class Background
    {
        public int Age;

        public string Childhood;
        public string YoungAdulthood;
        public string Adulthood;
        public string Parents;
        public string Siblings;
        public string Education;
        public string Job;

        public Background()
        {
            Age = 0;
            Childhood = "";
            YoungAdulthood = "";
            Adulthood = "";
            Parents = "";
            Siblings = "";
            Education = "";
            Job = "";
        }

        public Background(int age, string child, string young, string adult, string parents, string siblings, string education, string job)
        {
            Age = age;
            Childhood = child;
            YoungAdulthood = young;
            Adulthood = adult;
            Parents = parents;
            Siblings = siblings;
            Education = education;
            Job = job;
        }

        public Background(Background def)
        {
            Age = def.Age;
            Childhood = def.Childhood;
            YoungAdulthood = def.YoungAdulthood;
            Adulthood = def.Adulthood;
            Parents = def.Parents;
            Siblings = def.Siblings;
            Education = def.Education;
            Job = def.Job;
        }

        public override string ToString()
        {
            string s = "";

            s = Age + " years old.";
            s += Childhood;
            s += YoungAdulthood;
            s += Adulthood;
            s += Parents;
            s += Siblings;
            s += Education;
            s += Job;

            return s;
        }
    }
}