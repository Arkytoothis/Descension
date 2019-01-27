using Descension.Characters;
using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Descension
{
    public class SkillsPanel : MonoBehaviour
    {
        [SerializeField] GameObject skillPrefab = null;
        [SerializeField] Transform skillsParent = null;
        [SerializeField] List<GameObject> skillElements = new List<GameObject>();

        public void Initialize()
        {
            for (int i = 0; i < (int)Skill.Number; i++)
            {
                GameObject skillObject = Instantiate(skillPrefab, skillsParent);
                skillObject.name = "Skill Element - " + ((Skill)i).ToString();

                SkillElement skillScript = skillObject.GetComponent<SkillElement>();
                skillScript.SetData(null);

                skillElements.Add(skillObject);
            }
        }

        public void SetData(PcData pcData)
        {
            for (int i = 0; i < (int)Skill.Number; i++)
            {
                skillElements[i].GetComponent<SkillElement>().SetData(pcData.Attributes.GetSkill(i));
            }
        }
    }
}