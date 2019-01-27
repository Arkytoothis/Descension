using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using Descension.Core;

namespace Descension.Characters
{
    [System.Serializable]
    public class Attribute
    {
        [SerializeField] AttributeType type;
        [SerializeField] int start;
        [SerializeField] int current;
        [SerializeField] int modifier;
        [SerializeField] int minimum;
        [SerializeField] int maximum;
        [SerializeField] int spent;
        [SerializeField] int expCost;
        [SerializeField] int index;

        public AttributeType Type { get { return type; } }
        public int Start { get { return start; } }
        public int Current { set { current = value; } get { return current; } }
        public int Modifier { get { return modifier; } }
        public int Minimum { get { return minimum; } }
        public int Maximum { get { return maximum; } }
        public int Spent { get { return spent; } }
        public int ExpCost { get { return expCost; } }
        public int Index { get { return index; } set { index = value; } }

        public int ExpModifier;

        public Attribute()
        {
            type = AttributeType.None;
            index = 0;
            start = 0;
            current = 0;
            modifier = 0;
            minimum = 0;
            maximum = 0;
            spent = 0;
            expCost = 0;
            ExpModifier = 0;
        }

        public Attribute(int index, AttributeType type, int start, int minimum, int maximum)
        {
            this.type = type;
            this.index = index;
            current = 0;
            modifier = 0;
            spent = 0;
            expCost = 0;
            ExpModifier = 0;

            SetStart(start, minimum, maximum);
        }

        public Attribute(AttributeType type, int index, int exp_mod)
        {
            this.type = type;
            this.index = index;
            start = 0;
            current = 0;
            modifier = 0;
            minimum = 0;
            maximum = 0;
            spent = 0;
            ExpModifier = exp_mod;
        }
        public Attribute(Attribute attribute)
        {
            type = attribute.Type;
            index = attribute.Index;
            start = attribute.start;
            current = attribute.current;
            modifier = attribute.modifier;
            minimum = attribute.minimum;
            maximum = attribute.maximum;
            spent = attribute.spent;
            expCost = attribute.expCost;
            ExpModifier = attribute.ExpModifier;
        }

        public Attribute(AttributeType type, int index, int start, int current, int modifier, int minimum, int maximum, int exp_mod)
        {
            this.type = type;
            this.index = index;
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
            minimum = min;
            maximum = max;
            current = start;
            spent = 0;

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