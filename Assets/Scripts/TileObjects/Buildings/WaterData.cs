using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    [CreateAssetMenu(menuName = "HexTecGames/WaterData")]
    public class WaterData : CostObjectData
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


        public override bool IsWall
        {
            get
            {
                return true;
            }
        }
        public override TileObject CreateTileObject(Coord center, BaseGrid grid)
        {
            return new Water(center, grid, this);
        }
    }
}