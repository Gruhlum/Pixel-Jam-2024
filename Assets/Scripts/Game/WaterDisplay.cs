using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HexTecGames
{
	public class WaterDisplay : MonoBehaviour
	{
        [SerializeField] private TMP_Text textGUI = default;

        private Water water;

		public void Setup(Water water)
		{
            this.water = water;
            transform.position = water.GetWorldPosition();
            water.OnRemoved += Water_OnRemoved;
            water.OnWaterChanged += Water_OnWaterChanged;
            UpdateWaterDisplay();
        }

        private void Water_OnWaterChanged(int amount)
        {
            UpdateWaterDisplay();
        }

        private void UpdateWaterDisplay()
        {
            if (water.WaterData == null)
            {
                Debug.Log("null");
            }
            textGUI.text = $"{water.CurrentWater}/{water.WaterData.MaximumWater}";
        }
        private void Water_OnRemoved(GridBaseSystem.TileObject obj)
        {
            if (water != null)
            {
                water.OnRemoved -= Water_OnRemoved;
                water.OnWaterChanged -= Water_OnWaterChanged;
            }
            gameObject.SetActive(false);
        }
    }
}