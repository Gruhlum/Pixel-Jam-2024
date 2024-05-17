using HexTecGames.GridBaseSystem;
using HexTecGames.RectGridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class PathController : MonoBehaviour
	{
		[SerializeField] private BaseGrid grid = default;

		private List<Path> paths = new List<Path>();

        void Awake()
        {
            grid.OnGridGenerated += Grid_OnGridGenerated;
        }
        void OnDestroy()
        {
            grid.OnGridGenerated += Grid_OnGridGenerated;
        }
        private void Grid_OnGridGenerated()
        {
            CalculatePaths();
        }

        public Tile GetPathStartTile(int pathIndex)
        {
            return paths[pathIndex].GetPathStartTile();
        }

        public Tile GetClosestPathTile(Coord coord)
        {
            for (int i = 0; i < 10; i++)
            {
                List<Coord> results = grid.GetRing(coord, i);
                foreach (var result in results)
                {
                    if (!grid.IsTileEmpty(result))
                    {
                        continue;
                    }
                    foreach (var path in paths)
                    {
                        if (path.ContainsCoord(result))
                        {
                            return grid.GetTile(result);
                        }
                    }
                }
            }
            Debug.Log("Shouldn't happen!");
            return null;
        }
        private void CalculatePaths()
        {
            var results = grid.GetAllConnectedCoords();
            foreach (var result in results)
            {
                paths.Add(new Path(grid, grid.GetTiles(result), grid.Center));
            }
        }
    }
}