using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Core
{
    [System.Serializable]
    public class GameValue
    {
        public bool IsRandom;
        public int Number;
        public int Die;
        public int Modifer;
        public int Total;

        public GameValue()
        {
            IsRandom = false;
            Number = 0;
            Die = 0;
            Modifer = 0;
            Total = 0;
        }

        public GameValue(int number)
        {
            IsRandom = false;
            Number = number;
            Die = 0;
            Modifer = 0;
        }

        public GameValue(int number, int die)
        {
            IsRandom = true;
            Number = number;
            Die = die;
            Modifer = 0;
        }

        public GameValue(int number, int die, int modifier)
        {
            IsRandom = true;
            Number = number;
            Die = die;
            Modifer = modifier;
        }

        public GameValue(GameValue value)
        {
            IsRandom = value.IsRandom;
            Number = value.Number;
            Die = value.Die;
            Modifer = value.Modifer;
        }

        public int Roll(bool log)
        {
            int Result = 0;

            if (log == true)
                Debug.Log("Rolling " + Number + Die.ToString() + " +" + Modifer);

            for (int i = 0; i < Number; i++)
            {
                int rnd = UnityEngine.Random.Range(0, Die) + 1;
                Result += rnd;

                if (log == true)
                    Debug.Log("1" + Die.ToString() + " result " + rnd + ", total " + Result);
            }

            if (log == true)
                Debug.Log("Total is " + Total + Modifer);

            Total = Result + Modifer;

            return Total;
        }

        public static int Roll(GameValue value, bool log)
        {
            if (log == true) Debug.Log("Rolling " + value.Number + value.Die.ToString());
            value.Roll(log);
            return value.Total + value.Modifer;
        }

        public static int Roll(int min, int max)
        {
            return UnityEngine.Random.Range(min, max + 1);
        }

        public static GameValue Zero
        {
            get { return new GameValue(0, 0); }
        }

        public override string ToString()
        {
            string s = "";

            if (IsRandom == true)
                s = Number + "d" + Die.ToString();
            else if (IsRandom == false)
                s = Number.ToString();

            return s;
        }
    }
}