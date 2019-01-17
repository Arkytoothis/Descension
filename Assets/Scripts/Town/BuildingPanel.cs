using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Descension
{
    public class BuildingPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text detailsLabel = null;
        [SerializeField] Button openScreenButton = null;

        public Button OpenScreenButton { get { return openScreenButton; } }

        public void Initialize(string name, string details, UnityAction action)
        {
            nameLabel.text = name;
            detailsLabel.text = details;
            openScreenButton.onClick.AddListener(action);
        }
    }
}