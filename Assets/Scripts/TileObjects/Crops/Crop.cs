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
                return isFullyGrown;
            }
            set
            {
                isFullyGrown = value;
            }
        }
        private bool isFullyGrown;

        public event Action<Crop> OnFullyGrown;

        public WaterController WaterController
        {
            get
            {
                return waterController;
            }
            set
            {
                waterController = value;
            }
        }
        private WaterController waterController;


        public Crop(Coord center, BaseGrid grid, CropData data) : base(center, grid, data)
        {
            CropData = data;
            GameController.OnTick += GameController_OnTick;
            Sprite = CropData.Sprite;
        }
        public override void Remove()
        {
            GameController.OnTick -= GameController_OnTick;
            base.Remove();
        }

        private void GameController_OnTick()
        {
            var results = Grid.GetNeighbourTiles<WaterStorage>(Center);
            if (results.Count <= 0)
            {
                IsWatered = false;
                return;
            }
            if (IsFullyGrown)
            {
                return;
            }
            if (!waterController.TryToUseWater())
            {
                IncreaseDryTicks();
                IsWatered = false;
            }
            else
            {
                IncreaseGrowth(1);
                IsWatered = true;
            } 
            
            UpdateSprite();
        }
        private void IncreaseDryTicks()
        {
            DryTicks++;
            if (DryTicks > CropData.MaximumDryTicks)
            {
                Remove();
            }
        }
        private void IncreaseGrowth(int amount)
        {
            CurrentGrowthTicks += amount;
            DryTicks = 0;
            if (IsFullyGrown)
            {
                return;
            }
            if (CurrentGrowthTicks >= CropData.RequiredGrowthTicks)
            {
                IsFullyGrown = true;
                OnFullyGrown?.Invoke(this);
            }
        }
        public void UpdateSprite()
        {
            Sprite sprite = FindCorrectSprite();
            if (Sprite != sprite)
            {
                Sprite = sprite;
                CropData.GrowSound?.Play();
            }
        }
        private Sprite FindCorrectSprite()
        {
            int index = GetSpriteIndex();
            if (IsWatered)
            {
                return CropData.sprites[index];
            }
            else return CropData.drySprites[index];
            
        }
        private int GetSpriteIndex()
        {
            if (CurrentGrowthTicks >= CropData.RequiredGrowthTicks)
            {
                return 2;
            }
            else if (CurrentGrowthTicks < CropData.RequiredGrowthTicks / 2)
            {
                return 0;
            }
            else return 1;
        }
    }
}