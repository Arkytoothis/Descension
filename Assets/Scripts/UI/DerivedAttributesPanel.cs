using Descension.Characters;
using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public enum DerivedAttributeLabels
    {
        Actions, Armor, Health, Stamina, Essence, Morale,
        Might, Finesse, Spell_Attack,
        Roll, Defense, Initiative, Perception, Concentration, Resistance,
        Number, None
    }

    public class DerivedAttributesPanel : MonoBehaviour
    {
        [SerializeField] List<GameObject> attributeElements = new List<GameObject>();

        public void Initialize()
        {
        }

        public void SetData(PcData pcData)
        {
            SetAttribute((int)DerivedAttributeLabels.Actions, "Actions/Movement",
                pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Actions).Maximum.ToString() + " " +
                " + " + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Bonus_Actions).Current + 
                " / " + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Movement).Current + "m");

            SetAttribute((int)DerivedAttributeLabels.Armor, "Armor", pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Armor).Maximum.ToString());

            SetAttribute((int)DerivedAttributeLabels.Health, "Health",
                pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Health).Current + "/" + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Health).Maximum);

            SetAttribute((int)DerivedAttributeLabels.Stamina, "Stamina",
                pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Stamina).Current + "/" + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Stamina).Maximum);

            SetAttribute((int)DerivedAttributeLabels.Essence, "Essence",
                pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Essence).Current + "/" + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Essence).Maximum);

            SetAttribute((int)DerivedAttributeLabels.Morale, "Morale",
                pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Morale).Current + "/" + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Morale).Maximum);


            SetAttribute((int)DerivedAttributeLabels.Might, "Might Attack",
                pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Might_Attack).Current + "% +" + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Might_Damage).Current + "% damage");

            SetAttribute((int)DerivedAttributeLabels.Finesse, "Finesse Attack",
                pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Finesse_Attack).Current + "% +" + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Finesse_Damage).Current + "% damage");

            SetAttribute((int)DerivedAttributeLabels.Spell_Attack, "Spell Attack",
                pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Spell_Attack).Current + "% +" +
                "% damage +" + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Spell_Power).Current + "% power");

            SetAttribute((int)DerivedAttributeLabels.Roll,
                "Fumble 1-" + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Fumble).Current +
                " Critical Hit " + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Critical_Strike).Current + "-100" +
                " +" + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Critical_Damage).Current + "% damage", "");

            SetAttribute((int)DerivedAttributeLabels.Defense, "Block " + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Block).Current + "%        Dodge " +
                pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Dodge).Current + "%        Parry " + pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Parry).Current + "%", "");

            SetAttribute((int)DerivedAttributeLabels.Initiative, "Initiative", pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Initiative).Current.ToString());
            SetAttribute((int)DerivedAttributeLabels.Perception, "Perception", pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Perception).Current.ToString());
            SetAttribute((int)DerivedAttributeLabels.Concentration, "Concentration", pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Concentration).Current + "%");
            SetAttribute((int)DerivedAttributeLabels.Resistance, "Magic Resistance", pcData.Attributes.GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Resistance).Current + "%");
        }

        public void SetAttribute(int index, string text, string value)
        {
            attributeElements[index].GetComponent<DerivedAttributeElement>().SetData(text, value);
        }
    }
}