using HexTecGames.GridBaseSystem;
using HexTecGames.RectGridSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames
{
    public class Water : WaterStorage
    {
        public int CurrentWater
        {
            get
            {
                return currentWater;
            }
            set
            {             
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

        public override bool HasWater
        {
            get
            {
                return CurrentWater > 0;
            }
        }

        public event Action<int> OnWaterChanged;

        public Water(Coord center, BaseGrid grid, WaterData data) : base(center, grid, data)
        {
            WaterData = data;           
        }      
    }
}