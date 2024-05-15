using HexTecGames.GridBaseSystem;
using HexTecGames.RectGridSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames
{
    public class Water : Tile
    {
        public int CurrentWater
        {
            get
            {
                return currentWater;
            }
            set
            {
                if (value > waterData.MaximumWater)
                {
                    value = waterData.MaximumWater;
                }
                if (value == currentWater)
                {
                    return;
                }
                currentWater = value;
                OnWaterChanged?.Invoke(currentWater);
            }
        }
        private int currentWater;

        public WaterData WaterData
        {
            get
            {
                return waterData;
            }
            private set
            {
                waterData = value;
            }
        }
        private WaterData waterData;

        public WaterGroup waterGroup;
        private List<Crop> cropNeighbours = new List<Crop>();

        public event Action<int> OnWaterChanged;


        public Water(Coord center, BaseGrid grid, WaterData data) : base(center, grid, data)
        {
            WaterData = data;
            CurrentWater = data.MaximumWater / 2;

            GameController.OnBeforeTick += GameController_OnBeforeTick;
            GameController.OnTick += GameController_OnTick;
            GameController.OnAfterTick += GameController_OnAfterTick;

            var neighboursCoords = Grid.GetNeighbourCoords(Center);
            List<Tile> tileNeighbours = grid.GetTiles(neighboursCoords);
            List<Water> neighbours = new List<Water>();

            foreach (var tile in tileNeighbours)
            {
                if (tile is Water water)
                {
                    neighbours.Add(water);
                }
            }

            if (neighbours.Count == 0)
            {
                waterGroup = new WaterGroup();
                waterGroup.Add(this);
                return;
            }
            List<WaterGroup> allWaterGroups = new List<WaterGroup>();
            foreach (var neighbour in neighbours)
            {
                if (!allWaterGroups.Contains(neighbour.waterGroup))
                {
                    if (neighbour.waterGroup == null)
                    {
                        Debug.Log("Is null for some reason");
                        continue;
                    }
                    allWaterGroups.Add(neighbour.waterGroup);
                }
            }
            if (allWaterGroups.Count == 1)
            {
                waterGroup = allWaterGroups[0];
                allWaterGroups[0].Add(this);
                return;
            }
            int maxItems = allWaterGroups.Max(x => x.waterStorages.Count);
            WaterGroup biggestWaterGroup = allWaterGroups.Find(x => x.waterStorages.Count == maxItems);
            foreach (var waterGroup in allWaterGroups)
            {
                if (waterGroup != biggestWaterGroup)
                {
                    waterGroup.Merge(biggestWaterGroup);
                }
            }
            waterGroup = biggestWaterGroup;
            biggestWaterGroup.Add(this);
        }

        private void GameController_OnAfterTick()
        {
            waterGroup.BalanceWater();
        }

        private void GameController_OnBeforeTick()
        {
            waterGroup.hasBeenChecked = false;
        }

        public override void Remove()
        {
            GameController.OnBeforeTick -= GameController_OnBeforeTick;
            GameController.OnTick -= GameController_OnTick;
            GameController.OnAfterTick -= GameController_OnAfterTick;
            base.Remove();
        }
        private void GetCropNeighbours()
        {
            cropNeighbours = Grid.GetNeighbourObjects<Crop>(new List<Coord>() { Center });
        }
        private void GameController_OnTick()
        {
            if (CurrentWater <= 0)
            {
                return;
            }
            GetCropNeighbours();
            foreach (var crop in cropNeighbours)
            {
                if (crop.IsFullyGrown || crop.IsWatered)
                {
                    continue;
                }
                crop.WaterCrop();
                CurrentWater--;
                if (CurrentWater <= 0)
                {
                    return;
                }
            }
        }
    }
}