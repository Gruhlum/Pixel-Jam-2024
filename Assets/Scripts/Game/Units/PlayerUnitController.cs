using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class PlayerUnitController : UnitController
    {
		[SerializeField] private BaseGrid grid = default;
        [SerializeField] private TileData pathData = default;
        void Awake()
        {
            grid.OnGridGenerated += Grid_OnGridGenerated;
        }
        void OnDestroy()
        {
            grid.OnGridGenerated -= Grid_OnGridGenerated;
        }
        private void Grid_OnGridGenerated()
        {
            var paths = grid.GetAllTiles(pathData);
        }
    }
}