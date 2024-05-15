using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    [CreateAssetMenu(menuName = "HexTecGames/WaterData")]
    public class WaterData : RuleTileData
    {
        public int MaximumWater
        {
            get
            {
                return maximumWater;
            }
            private set
            {
                maximumWater = value;
            }
        }
        [SerializeField] private int maximumWater;

        public override GridObject CreateObject(Coord center, BaseGrid grid)
        {
            return new Water(center, grid, this);
        }
    }
}