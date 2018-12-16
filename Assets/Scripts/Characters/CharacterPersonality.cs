using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Characters
{
    public enum OrderScale { Chaotic = -100, Neutral = 0, Law = 100 };
    public enum MoralityScale { Pure_Evil = -100, Evil = -50, Mostly_Evil = -25, Somewhat_Evil = -10, Neutral = 0, Somewhat_Good = 10, Mostly_Good = 25, Good = 50, Saintly = 100 };
    public enum EgoScale { Very_Selfish = -100, Selfish = -50, Mostly_Selfish = -25, Somehat_Selfish = -10, Neutral = 0, Somewhat_Selfless = 10, Mostly_Selfless = 25, Selfless = 50, Very_Selfless = 100 };
    public enum BraveryScale { Very_Cowardly = -100, Cowardly = -50, Mostly_Cowardly = -25, Somewhat_Cowardly = -10, Neutral = 0, Somewhat_Courageous = 10, Mostly_Courageous = 25, Courageous = 50, Very_Courageous = 100 };
    public enum FaithScale { Extremely_Zealous = -100, Zealous = -50, Mostly_Zealous = -25, Somwhat_Zealous = -10, Neutral = 0, Somwhat_Spiritual = 10, Mostly_Spiritual = 25, Spiritual = 50, Enlightened = 100 };

    [System.Serializable]
    public class CharacterPersonality
    {
        public int Order;
        public int Morality;
        public int Ego;
        public int Bravery;
        public int Faith;

        public CharacterPersonality()
        {
            Order = (int)OrderScale.Neutral;
            Morality = (int)MoralityScale.Neutral;
            Ego = (int)EgoScale.Neutral;
            Bravery = (int)BraveryScale.Neutral;
            Faith = (int)FaithScale.Neutral;
        }

        public CharacterPersonality(int order, int morality, int ego, int bravery, int faith)
        {
            Order = order;
            Morality = morality;
            Ego = ego;
            Bravery = bravery;
            Faith = faith;
        }

        public CharacterPersonality(CharacterPersonality alignment)
        {
            Order = alignment.Order;
            Morality = alignment.Morality;
            Ego = alignment.Ego;
            Bravery = alignment.Bravery;
            Faith = alignment.Faith;
        }

        public override string ToString()
        {
            string s = "";

            s = ((OrderScale)Order).ToString() + "(" + Order + "), ";
            s += ((MoralityScale)Morality).ToString() + "(" + Morality + "), ";
            s += ((EgoScale)Ego).ToString() + "(" + Ego + "), ";
            s += ((BraveryScale)Bravery).ToString() + "(" + Bravery + "), ";
            s += ((FaithScale)Faith).ToString() + "(" + Faith + ")";

            return s;
        }
    }
}