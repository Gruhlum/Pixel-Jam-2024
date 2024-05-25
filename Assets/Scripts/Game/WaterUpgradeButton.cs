using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HexTecGames
{
	public class WaterUpgradeButton : MonoBehaviour
	{
		[SerializeField] private TMP_Text costGUI = default;
        [SerializeField] private ResourceController resourceC = default;
        [SerializeField] private ResourceType goldType = default;
        [SerializeField] private WaterController waterC = default;


        private int cost = 3;


        void OnEnable()
        {
            UpdateCostText();
        }

        public void BuyWaterProduction()
        {
            Resource gold = resourceC.GetResources().Find(x => x.Data == goldType);
            if (gold.Value < cost)
            {
                return;
            }
            gold.Value -= cost;
            cost++;
            waterC.IncreaseWaterProduction();
            UpdateCostText();
        }
        private void UpdateCostText()
        {
            costGUI.text = cost.ToString();
        }
    }
}