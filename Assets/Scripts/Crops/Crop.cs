using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class Crop : MonoBehaviour
	{
        public int TotalGrowthTicks
        {
            get
            {
                return totalGrowthTicks;
            }
            private set
            {
                totalGrowthTicks = value;
            }
        }
        [SerializeField] private int totalGrowthTicks;

        public int WaterPerTick
        {
            get
            {
                return waterPerTick;
            }
            private set
            {
                waterPerTick = value;
            }
        }
        [SerializeField] private int waterPerTick;

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

    }
}