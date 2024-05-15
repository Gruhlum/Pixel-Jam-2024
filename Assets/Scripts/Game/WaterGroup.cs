using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames
{
    [System.Serializable]
    public class WaterGroup
    {
        public List<Water> waterStorages = new List<Water>();

        public Color color;

        public bool hasBeenChecked;

        public WaterGroup()
        {
            color = Color.white;
            //color = new Color(Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), 1);
        }

        public void BalanceWater()
        {
            int totalWater = waterStorages.Sum(x => x.CurrentWater);
            int totalStorage = waterStorages.Sum(x => x.WaterData.MaximumWater);
            float percent = totalWater / (float)totalStorage;
            
            foreach (var waterStorage in waterStorages)
            {
                int water = Mathf.FloorToInt(waterStorage.WaterData.MaximumWater * percent);
                waterStorage.CurrentWater = water;
                totalWater -= water;
            }
            while (totalWater > 0)
            {
                foreach (var waterStorage in waterStorages)
                {
                    waterStorage.CurrentWater++;
                    totalWater--;
                    if (totalWater <= 0)
                    {
                        break;
                    }
                }
            }
        }
        public void Merge(WaterGroup waterGroup)
        {
            foreach (var water in waterStorages)
            {
                water.waterGroup = waterGroup;
                waterGroup.Add(water);
            }
        }
        public void Add(Water water)
        {
            waterStorages.Add(water);
            water.Color = color;
        }
        public void Clear()
        {
            waterStorages.Clear();
        }
    }
}