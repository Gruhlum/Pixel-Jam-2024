using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames
{
    [System.Serializable]
    public class WaterGroup
    {
        public List<WaterStorage> waterStorages = new List<WaterStorage>();

        public Color color;

        public bool hasSource;

        private bool changeColor = false;


        public WaterGroup()
        {
            //color = Color.white;
            color = new Color(Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), 1);
        }

        public void Merge(WaterGroup waterGroup)
        {
            foreach (var water in waterStorages)
            {
                water.WaterGroup = waterGroup;
                waterGroup.Add(water);
            }
        }
        public void Add(WaterStorage water)
        {
            waterStorages.Add(water);
            if (changeColor) water.Color = color;
        }
        public void Clear()
        {
            waterStorages.Clear();
        }
    }
}