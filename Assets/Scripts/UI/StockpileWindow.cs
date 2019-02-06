using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Descension.Characters;

namespace Descension
{
    public class StockpileWindow : MonoBehaviour
    {
        [SerializeField] GameObject stockpileButtonPrefab = null;
        [SerializeField] Transform stockpileButtonsParent = null;
        [SerializeField] List<StockpileButton> stockpileButtons = new List<StockpileButton>();

        public void Initialize()
        {
            for (int i = 0; i < StockpileManager.instance.MaxItems; i++)
            {
                GameObject buttonObject = Instantiate(stockpileButtonPrefab, stockpileButtonsParent);
                buttonObject.name = "Stockpile Button " + i;

                StockpileButton buttonScript = buttonObject.GetComponent<StockpileButton>();
                buttonScript.Setup(i, true);

                stockpileButtons.Add(buttonScript);
            }

            ViewStockpile();
        }

        public void ViewStockpile()
        {
            for (int i = 0; i < StockpileManager.instance.MaxItems; i++)
            {
                if (StockpileManager.instance.Items[i] != null)
                {
                    stockpileButtons[i].SetData(StockpileManager.instance.Items[i], true);
                }
                else
                {
                    stockpileButtons[i].Clear();
                }
            }
        }
    }
}