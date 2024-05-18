using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    [CreateAssetMenu(menuName = "HexTecGames/WaterData")]
    public class WaterData : RuleTileData
    {
        [SerializeField] private WaterSourceData waterSourceData = default;
        public override GridObject CreateObject(Coord center, BaseGrid grid)
        {
            return new Water(center, grid, this);
        }
        public override bool IsValidCoord(Coord coord, BaseGrid grid)
        {
            if (!base.IsValidCoord(coord, grid))
            {
                return false;
            }
            else if (!grid.IsTileEmpty(coord))
            {
                return false;
            }
            else
            {
                var results = grid.GetNeighbourTiles(coord);
                foreach (var result in results)
                {
                    if (result.Data == this || result.Data == waterSourceData)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}