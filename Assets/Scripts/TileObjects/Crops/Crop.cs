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

        public int DryTicks
        {
            get
            {
                return dryTicks;
            }
            private set
            {
                dryTicks = value;
            }
        }
        private int dryTicks;


        public bool IsWatered
        {
            get
            {
                return isWatered;
            }
            private set
            {
                isWatered = value;
            }
        }
        private bool isWatered;

        public bool IsFullyGrown
        {
            get
            {
                return CurrentGrowthTicks >= CropData.RequiredGrowthTicks;
            }
        }

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

        public void WaterCrop()
        {
            IsWatered = true;
        }
        private void GameController_OnTick()
        {
            if (IsWatered)
            {
                IncreaseGrowth(1);
                IsWatered = false;
            }
            else IncreaseTryTicks();
        }
        private void IncreaseTryTicks()
        {
            DryTicks++;
        }
        private void IncreaseGrowth(int amount)
        {
            CurrentGrowthTicks += amount;
            Sprite = CropData.GetCurrentSprite(CurrentGrowthTicks);
        }
    }
}