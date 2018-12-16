using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using Descension.Core;

namespace Descension.Characters
{
    [System.Serializable]
    public class Attribute
    {
        public AttributeType Type;
        public int Index;
        private int start;
        private int current;
        private int modifier;
        private int minimum;
        private int maximum;
        private int spent;
        private int expCost;

        public int Start { get { return start; } }
        public int Current { set { current = value; } get { return current; } }
        public int Modifier { get { return modifier; } }
        public int Minimum { get { return minimum; } }
        public int Maximum { get { return maximum; } }
        public int Spent { get { return spent; } }
        public int ExpCost { get { return expCost; } }

        public int ExpModifier;

        public Attribute()
        {
            Type = AttributeType.None;
            Index = 0;
            start = 0;
            current = 0;
            modifier = 0;
            minimum = 0;
            maximum = 0;
            spent = 0;
            expCost = 0;
            ExpModifier = 0;
        }

        public Attribute(AttributeType type, int index, int exp_mod)
        {
            Type = type;
            Index = index;
            start = 0;
            current = 0;
            modifier = 0;
            minimum = 0;
            maximum = 0;
            spent = 0;
            ExpModifier = exp_mod;
        }
        public Attribute(Attribute c)
        {
            Type = c.Type;
            Index = c.Index;
            start = c.start;
            current = c.current;
            modifier = c.modifier;
            minimum = c.minimum;
            maximum = c.maximum;
            spent = c.spent;
            expCost = c.expCost;
            ExpModifier = c.ExpModifier;
        }

        public Attribute(AttributeType type, int index, int start, int current, int modifier, int minimum, int maximum, int exp_mod)
        {
            Type = type;
            Index = index;
            this.start = start;
            this.current = current;
            this.modifier = modifier;
            this.minimum = minimum;
            this.maximum = maximum;
            spent = 0;
            ExpModifier = exp_mod;
        }

        public void Reset()
        {
            current = maximum;
        }

        public static Attribute Max { get { return new Attribute(AttributeType.Base, 0, 999999, 999999, 999999, 0, 999999, 999999); } }

        public void SetStart(int start, int min, int max)
        {
            this.start = start;
            this.minimum = min;
            this.maximum = max;
            this.current = start;
            this.spent = 0;
            Check();
        }

        public void SetMax(int max, bool set_cur)
        {
            maximum = max;

            if (set_cur == true)
                current = maximum;
        }

        public void ModifyStart(int value)
        {
            start += value;
            maximum = start;
            current = start;
            Check();
        }

        public void SetModifier(int mod)
        {
            modifier = mod;
        }

        public void AddToModifier(int mod)
        {
            modifier += mod;
        }

        public void AddSpent()
        {
            spent++;
            current = start + spent;
        }

        public void SubtractSpent()
        {
            spent--;
            current = start + spent;
        }

        void Check()
        {
            if (current < minimum)
                current = minimum;

            if (start < minimum)
                start = minimum;

            if (current > maximum)
                current = maximum;
        }

        public void SetExpModifier(int mod)
        {
            ExpModifier = mod;
        }

        public void CalculateExpCost()
        {
            expCost = current * ExpModifier;
        }

        public void Modify(AttributeComponentType type, int value)
        {
            switch (type)
            {
                case AttributeComponentType.Current:
                    current += value;
                    break;
                case AttributeComponentType.Start:
                    start += value;
                    break;
                case AttributeComponentType.Minimum:
                    minimum += value;
                    break;
                case AttributeComponentType.Maximum:
                    maximum += value;
                    break;
                case AttributeComponentType.Modifier:
                    modifier += value;
                    break;
                case AttributeComponentType.Spent:
                    spent += value;
                    break;
                case AttributeComponentType.Exp_Cost:
                    expCost += value;
                    break;
                default:
                    break;
            }

            Check();
        }

        public int Get(AttributeComponentType type)
        {
            int val = 0;

            switch (type)
            {
                case AttributeComponentType.Current:
                    val = current;
                    break;
                case AttributeComponentType.Start:
                    val = start;
                    break;
                case AttributeComponentType.Minimum:
                    val = minimum;
                    break;
                case AttributeComponentType.Maximum:
                    val = maximum;
                    break;
                case AttributeComponentType.Modifier:
                    val = modifier;
                    break;
                case AttributeComponentType.Spent:
                    val = spent;
                    break;
                case AttributeComponentType.Exp_Cost:
                    val = expCost;
                    break;
                default:
                    break;
            }

            return val;
        }
    }
}