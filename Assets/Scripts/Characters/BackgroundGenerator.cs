using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Characters
{
    public static class BackgroundGenerator
    {
        static List<BackgroundDefinition> childhood;
        static List<BackgroundDefinition> youngAdulthood;
        static List<BackgroundDefinition> adulthood;
        static List<BackgroundDefinition> parents;
        static List<BackgroundDefinition> parentJobs;
        static List<BackgroundDefinition> siblings;
        static List<BackgroundDefinition> education;
        static List<BackgroundDefinition> jobs;

        static bool initialized = false;

        public static void Initialize()
        {
            if (initialized == false)
            {
                initialized = true;
                childhood = new List<BackgroundDefinition>();
                youngAdulthood = new List<BackgroundDefinition>();
                adulthood = new List<BackgroundDefinition>();
                parents = new List<BackgroundDefinition>();
                parentJobs = new List<BackgroundDefinition>();
                siblings = new List<BackgroundDefinition>();
                education = new List<BackgroundDefinition>();
                jobs = new List<BackgroundDefinition>();

                LoadDefs();
            }
        }

        public static Background Generate()
        {
            Background background = new Background();

            //background.Age = Random.Range(18, 65);
            //background.Childhood = childhood[Random.Range(0, childhood.Count - 1)].Description;
            //background.YoungAdulthood = youngAdulthood[Random.Range(0, youngAdulthood.Count - 1)].Description;
            //background.Adulthood = adulthood[Random.Range(0, adulthood.Count - 1)].Description;
            //background.Parents = parents[Random.Range(0, parents.Count - 1)].Description;
            //background.Siblings = siblings[Random.Range(0, siblings.Count - 1)].Description;
            //background.Education = education[Random.Range(0, education.Count - 1)].Name;
            //background.Job = jobs[Random.Range(0, jobs.Count - 1)].Name;

            return background;
        }

        public static void LoadDefs()
        {
            BackgroundDefinition def = new BackgroundDefinition("Orphan", "orphan", "Orphaned as a child.");
            childhood.Add(def);

            def = new BackgroundDefinition("Feral", "feral", "Grew up alone in the wilderness.");
            childhood.Add(def);

            def = new BackgroundDefinition("Normal", "normal", "Normal childhood.");
            childhood.Add(def);

            def = new BackgroundDefinition("Abandoned", "abandoned", "Abandoned as a young child.");
            childhood.Add(def);

            def = new BackgroundDefinition("Adopted", "adopted", "Adopted as a young child.");
            childhood.Add(def);


            def = new BackgroundDefinition("Student", "student", "Spent youth studying.");
            youngAdulthood.Add(def);

            def = new BackgroundDefinition("Work", "work", "Spent youth working.");
            youngAdulthood.Add(def);

            def = new BackgroundDefinition("Boarded", "boarded", "Spent youth at a boarding school.");
            youngAdulthood.Add(def);

            def = new BackgroundDefinition("Street", "street", "Spent youth on the street.");
            youngAdulthood.Add(def);

            def = new BackgroundDefinition("Violent", "violent", "Survived a violent youth.");
            youngAdulthood.Add(def);

            def = new BackgroundDefinition("Gang", "gang", "Spent youth in a gang.");
            youngAdulthood.Add(def);

            def = new BackgroundDefinition("Circus", "circus", "Joined the circus as a youth.");
            youngAdulthood.Add(def);


            def = new BackgroundDefinition("Student", "student", "Studied as an adult.");
            adulthood.Add(def);

            def = new BackgroundDefinition("Work", "work", "Worked as an adult.");
            adulthood.Add(def);

            def = new BackgroundDefinition("Travel", "travel", "Travelled as an adult.");
            adulthood.Add(def);

            def = new BackgroundDefinition("Street", "street", "Lived on the street as an adult.");
            youngAdulthood.Add(def);

            def = new BackgroundDefinition("Violent", "violent", "Survived a violent adulthood.");
            youngAdulthood.Add(def);

            def = new BackgroundDefinition("Gang", "gang", "Joined a gang once reached adulthood.");
            youngAdulthood.Add(def);

            def = new BackgroundDefinition("Circus", "circus", "Joined the circus once reached adulthood.");
            youngAdulthood.Add(def);


            def = new BackgroundDefinition("Parents Killed", "parents_killed", "Both parents were killed.");
            parents.Add(def);

            def = new BackgroundDefinition("Single Father", "single_father", "Raised by father.");
            parents.Add(def);

            def = new BackgroundDefinition("Single Mother", "single_mother", "Raised by mother.");
            parents.Add(def);

            def = new BackgroundDefinition("Two", "two", "Raised by both parents.");
            parents.Add(def);

            def = new BackgroundDefinition("Aunt", "aunt", "Raised by aunt.");
            parents.Add(def);

            def = new BackgroundDefinition("Uncle", "uncle", "Raised by uncle.");
            parents.Add(def);

            def = new BackgroundDefinition("Grandfather", "grandfather", "Raised by grandfather.");
            parents.Add(def);

            def = new BackgroundDefinition("Grandmother", "grandmother", "Raised by grandmother.");
            parents.Add(def);

            def = new BackgroundDefinition("Grandparents", "grandparents", "Raised by both grandparents.");
            parents.Add(def);

            def = new BackgroundDefinition("None", "None", "Has no parents.");
            parents.Add(def);


            def = new BackgroundDefinition("Sibling(s) killed", "Sibling(s) killed", "");
            siblings.Add(def);

            def = new BackgroundDefinition("Only child", "Only child", "");
            siblings.Add(def);

            def = new BackgroundDefinition("Number", "Number", "");
            parents.Add(def);


            def = new BackgroundDefinition("None", "none", "");
            education.Add(def);

            def = new BackgroundDefinition("Simple", "simple", "Simple Education.");
            education.Add(def);

            def = new BackgroundDefinition("Grammar", "grammar", "Grammar school education.");
            education.Add(def);

            def = new BackgroundDefinition("Prep", "prep", "Prep school education.");
            education.Add(def);

            def = new BackgroundDefinition("University", "university", "University education.");
            education.Add(def);


            def = new BackgroundDefinition("Farmer", "farmer", "");
            parentJobs.Add(def);
            jobs.Add(def);

            def = new BackgroundDefinition("Blacksmith", "blacksmith", "");
            parentJobs.Add(def);
            jobs.Add(def);

            def = new BackgroundDefinition("Teacher", "teacher", "");
            parentJobs.Add(def);
            jobs.Add(def);

            def = new BackgroundDefinition("Nurse", "nurse", "");
            parentJobs.Add(def);
            jobs.Add(def);

            def = new BackgroundDefinition("Priest", "priest", "");
            parentJobs.Add(def);
            jobs.Add(def);

            def = new BackgroundDefinition("Soldier", "soldier", "");
            parentJobs.Add(def);
            jobs.Add(def);

            def = new BackgroundDefinition("Mercenary", "mercenary", "");
            parentJobs.Add(def);
            jobs.Add(def);

            def = new BackgroundDefinition("Fisherman", "fisherman", "");
            parentJobs.Add(def);
            jobs.Add(def);

            def = new BackgroundDefinition("Lumberjack", "lumberjack", "");
            parentJobs.Add(def);
            jobs.Add(def);

            def = new BackgroundDefinition("Rancher", "rancher", "");
            parentJobs.Add(def);
            jobs.Add(def);

            def = new BackgroundDefinition("Scholar", "scholar", "");
            parentJobs.Add(def);
            jobs.Add(def);

            def = new BackgroundDefinition("Beggar", "beggar", "");
            parentJobs.Add(def);
            jobs.Add(def);

            def = new BackgroundDefinition("Jester", "jester", "");
            parentJobs.Add(def);
            jobs.Add(def);
        }
    }
}