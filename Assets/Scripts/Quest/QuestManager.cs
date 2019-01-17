using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] List<Quest> availableQuests = new List<Quest>();
        [SerializeField] List<Quest> currentQuests = new List<Quest>();

        public void Initialize()
        {
            for (int i = 0; i < 10; i++)
            {
                availableQuests.Add(QuestGenerator.Generate());
            }
        }
    }
}