using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Characters
{
    public enum AttributeCalculationOpperator
    {
        Add, Subtract, Multiply, Multiply_Neg, Average,
        None, Number
    }

    [System.Serializable]
    public class AttributeCalculation
    {
        public AttributeModifier Attribute1;
        public AttributeModifier Attribute2;
        public AttributeModifier Attribute3;
        public AttributeCalculationOpperator Operator1;
        public AttributeCalculationOpperator Operator2;

        public AttributeCalculation()
        {
            Attribute1 = new AttributeModifier();
            Attribute2 = new AttributeModifier();
            Attribute3 = new AttributeModifier();
            Operator1 = AttributeCalculationOpperator.None;
            Operator2 = AttributeCalculationOpperator.None;
        }

        public AttributeCalculation(AttributeModifier att1, AttributeModifier att2, AttributeModifier att3, AttributeCalculationOpperator op1, AttributeCalculationOpperator op2)
        {
            if (att1 != null)
                Attribute1 = new AttributeModifier(att1);
            if (att2 != null)
                Attribute2 = new AttributeModifier(att2);
            if (att3 != null)
                Attribute3 = new AttributeModifier(att3);
            Operator1 = op1;
            Operator2 = op2;
        }

        public AttributeCalculation(AttributeCalculation calc)
        {
            if (calc != null)
            {
                if (calc.Attribute1 != null)
                    Attribute1 = new AttributeModifier(calc.Attribute1);
                if (calc.Attribute2 != null)
                    Attribute2 = new AttributeModifier(calc.Attribute2);
                if (calc.Attribute3 != null)
                    Attribute3 = new AttributeModifier(calc.Attribute3);
                Operator1 = calc.Operator1;
                Operator2 = calc.Operator2;
            }
        }
    }
}