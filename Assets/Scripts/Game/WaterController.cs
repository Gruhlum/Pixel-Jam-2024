using HexTecGames.Basics;
using HexTecGames.GridBaseSystem;
using HexTecGames.RectGridSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames
{
	public class WaterController : MonoBehaviour
	{
		[SerializeField] private BaseGrid grid = default;
        [SerializeField] private WeatherController weatherController = default;
        // private List<WaterGroup> waterGroups = new List<WaterGroup>();

        //private List<TileObject> waterStorages = new List<TileObject>();

        [SerializeField] private Spawner<WaterDisplay> waterDisplaySpawner = default;



        private List<Tile> corners = default;

        private List<Water> waterTiles = new List<Water>();

        [SerializeField] private int defaultRange = 4;

        void Awake()
        {
            grid.OnTileAdded += Grid_OnTileObjectAdded;
            grid.OnTileRemoved += Grid_OnTileObjectRemoved;
            grid.OnGridGenerated += Grid_OnGridGenerated;
            WeatherController.OnWeatherChanged += WeatherController_OnWeatherChanged;
        }

        private void Grid_OnGridGenerated()
        {
            List<WaterSource> waterSources = grid.GetAllTiles<WaterSource>();
            if (waterSources.Count == 0)
            {
                Debug.Log("No Watersources in Scene!");
                return;
            }
            corners = new List<Tile>();
            int minX = waterSources.Min(x => x.X);
            int maxX = waterSources.Max(x => x.X);
            int minY = waterSources.Min(x => x.Y);
            int maxY = waterSources.Max(x => x.Y);
            corners.Add(waterSources.Find(t => t.X == minX && t.Y == minY));
            corners.Add(waterSources.Find(t => t.X == maxX && t.Y == minY));
            corners.Add(waterSources.Find(t => t.X == minX && t.Y == maxY));
            corners.Add(waterSources.Find(t => t.X == maxX && t.Y == maxY));

            foreach (var waterSource in waterSources)
            {
                waterSource.FindBestWaterGroup();
            }
        }

        void OnDestroy()
        {
            grid.OnTileAdded -= Grid_OnTileObjectAdded;
            grid.OnTileRemoved -= Grid_OnTileObjectRemoved;
            grid.OnGridGenerated -= Grid_OnGridGenerated;
            WeatherController.OnWeatherChanged -= WeatherController_OnWeatherChanged;
        }
        private void WeatherController_OnWeatherChanged(WeatherType obj)
        {
            foreach (var water in waterTiles)
            {
                water.CurrentWater = CalculateWaterHeight(water);
            }
        }
        private void Grid_OnTileObjectRemoved(Tile obj)
        {
            if (obj is IWaterStorage)
            {
                //waterStorages.Remove(obj);
                //CalculateWaterGroups();
            }
        }

        private void Grid_OnTileObjectAdded(Tile obj)
        {
            if (obj is Water water)
            {
                waterTiles.Add(water);
                water.CurrentWater = CalculateWaterHeight(water);
                waterDisplaySpawner.Spawn().Setup(water);
            }
            
        }

        private int CalculateWaterHeight(Tile tile)
        {
            int distance = CalculateDistance(tile);
            return defaultRange + weatherController.WeatherType.RangeChange - distance;
        }
        private int CalculateDistance(Tile tile)
        {
            int closestDistance = -1;
            for (int i = 0; i < corners.Count; i++)
            {
                int distance = TileCoord.GetDistance(tile.Center, corners[i].Center);
                if (closestDistance == -1 || distance < closestDistance)
                {
                    closestDistance = distance;
                }
            }
            return closestDistance;
        }


        //private void CalculateWaterGroups()
        //{
        //    waterGroups.Clear();
        //    List<TileObject> checkStorages = new List<TileObject>();
        //    foreach (var waterStorage in waterStorages)
        //    {
        //        checkStorages.Add(waterStorage);
        //        List<Coord> tileCoords = waterStorage.GetNormalizedCoords(waterStorage.Center);
        //        List<Coord> neighbourCoords = GetAllNeighbourCoords(tileCoords);
        //        List<TileObject>
        //        List<TileObject> neighbours = waterStorage
        //    }
        //}
    }
}