using Descension.Characters;
using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class CombatManager : Singleton<CombatManager>
    {
        public void ProcessMeleeAttack(CharacterData attacker, CharacterData defender)
        {
            int attackRoll = Random.Range(1, 101) + attacker.Attributes.GetAttributeValue(AttributeListType.Derived, AttributeComponentType.Current, (int)DerivedAttribute.Might_Attack);
            int defenseRoll = Random.Range(1, 101) + attacker.Attributes.GetAttributeValue(AttributeListType.Derived, AttributeComponentType.Current, (int)DerivedAttribute.Block);
            int damageRoll = Random.Range(1, 10);

            if (attackRoll > defenseRoll)
            {
                //Debug.Log(attacker.Name.ShortName + " hits " + defender.Name.ShortName + " for " + damageRoll + " damage" + ", " + attackRoll + " vs " + defenseRoll);
                defender.Attributes.ModifyAttribute(AttributeType.Derived, (int)DerivedAttribute.Health, -damageRoll);
            }
            else
            {
                //Debug.Log(defender.Name.ShortName + " blocks attack from " + attacker.Name.ShortName + ", " + attackRoll + " vs " + defenseRoll);
            }
        }
    }
}