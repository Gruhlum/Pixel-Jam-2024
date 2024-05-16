using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	[CreateAssetMenu(menuName = "HexTecGames/WaterSourceData")]
	public class WaterSourceData : RuleTileData
	{
        public override GridObject CreateObject(Coord center, BaseGrid grid)
        {
            return new WaterSource(center, grid, this);
        }
    }
}