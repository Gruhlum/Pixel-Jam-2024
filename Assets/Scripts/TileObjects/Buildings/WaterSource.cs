using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    [System.Serializable]
    public class WaterSource : WaterStorage
    {
        public override bool HasWater
        {
            get
            {
                return true;
            }
        }

        public WaterSource(Coord coord, BaseGrid grid, WaterSourceData tileData) : base(coord, grid, tileData)
        {
        }     
    }
}