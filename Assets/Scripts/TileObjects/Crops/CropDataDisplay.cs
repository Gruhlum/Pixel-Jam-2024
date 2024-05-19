using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HexTecGames
{
	public class CropDataDisplay : PlaceableDisplay
	{
		[SerializeField] private ResourceDisplay resourceDisplay = default;

        protected override void DrawItem(GridObjectData item)
        {
            base.DrawItem(item);
            if (item is ICost cost)
            {
                resourceDisplay.SetData(cost.GetCost());
                //resourceDisplay.gameObject.SetActive(true);
            }
            //else resourceDisplay.gameObject.SetActive(false);
        }
    }
}