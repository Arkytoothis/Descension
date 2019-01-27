using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using Descension.Core;

namespace Descension
{
    public class SpriteManager : Singleton<SpriteManager>
    {
        bool initialized = false;

        [SerializeField] List<Sprite> guiList = new List<Sprite>();
        [SerializeField] List<Sprite> itemList = new List<Sprite>();
        [SerializeField] List<Sprite> abilityList = new List<Sprite>();

        [SerializeField] Dictionary<string, Sprite> guiIcons = new Dictionary<string, Sprite>();
        [SerializeField] Dictionary<string, Sprite> itemIcons = new Dictionary<string, Sprite>();
        [SerializeField] Dictionary<string, Sprite> abilityIcons = new Dictionary<string, Sprite>();

        public Dictionary<string, Sprite> GuiIcons { get { return guiIcons; } }
        public Dictionary<string, Sprite> ItemIcons { get { return itemIcons; } }
        public Dictionary<string, Sprite> AbilityIcons { get { return abilityIcons; } }

        private void Awake()
        {
            Reload();
        }

        public void Initialize()
        {
            if (initialized == false)
            {
                initialized = true;

                foreach (Sprite s in guiList)
                {
                    guiIcons.Add(s.name, s);
                }

                foreach (Sprite s in itemList)
                {
                    itemIcons.Add(s.name, s);
                }

                foreach (Sprite s in abilityList)
                {
                    abilityIcons.Add(s.name, s);
                }
            }
        }

        public Sprite GetItemIcon(string key)
        {
            if (itemIcons.ContainsKey(key) == false)
            {
                Debug.LogWarning("itemSprites key: " + key + " does not exist");
                return itemIcons["blank"];
            }
            else
            {
                return itemIcons[key];
            }
        }

        public Sprite GetAbilityIcon(string key)
        {
            if (abilityIcons.ContainsKey(key) == false)
            {
                Debug.LogWarning("abilitySprites key: " + key + " does not exist");
                return abilityIcons["blank"];
            }
            else
            {
                return abilityIcons[key];
            }
        }

        public Sprite GetGuiIcon(string key)
        {
            if (guiIcons.ContainsKey(key) == false)
            {
                Debug.LogWarning("guiIcons key: " + key + " does not exist");
                return guiIcons["blank"];
            }
            else
            {
                return guiIcons[key];
            }
        }
    }
}