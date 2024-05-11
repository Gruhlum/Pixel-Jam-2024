using HexTecGames.GridBaseSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class Crop : TileObject
	{
        public CropData CropData
        {
            get
            {
                return cropData;
            }
            private set
            {
                cropData = value;
            }
        }
        private CropData cropData;

        public int CurrentGrowthTicks
        {
            get
            {
                return currentGrowthTicks;
            }
            private set
            {
                currentGrowthTicks = value;
                
            }
        }
        private int currentGrowthTicks;

        public Crop(Coord center, BaseGrid grid, CropData data) : base(center, grid, data)
        {
            CropData = data;
            GameController.OnTick += GameController_OnTick;
            Sprite = CropData.GetCurrentSprite(CurrentGrowthTicks);
        }
        public override void Remove()
        {
            GameController.OnTick -= GameController_OnTick;
            base.Remove();
        }
        private void GameController_OnTick()
        {
            IncreaseGrowth(1);
        }

        public void IncreaseGrowth(int amount)
        {
            CurrentGrowthTicks += amount;
            Sprite = CropData.GetCurrentSprite(CurrentGrowthTicks);
        }
    }
}