using HexTecGames.Basics;
using HexTecGames.Basics.UI;
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

        [SerializeField] private Resource waterBalance = default;
        [SerializeField] private int maximumWater = default;
        [SerializeField] private int currentWater = default;
        [SerializeField] private TextDisplay waterDisplay = default;

        private List<Crop> crops = new List<Crop>();

        [SerializeField] private int defaultBalance = 4;
        private int lastBalance;
        [SerializeField] private Material waterMat = default;
        void Awake()
        {
            grid.OnTileAdded += Grid_OnTileAdded;
            grid.OnTileRemoved += Grid_OnTileRemoved;
            grid.OnTileObjectAdded += Grid_OnTileObjectAdded;
            grid.OnTileObjectRemoved += Grid_OnTileObjectRemoved;
            //grid.OnGridGenerated += Grid_OnGridGenerated;
            WeatherController.OnWeatherChanged += WeatherController_OnWeatherChanged;
            GameController.OnBeforeTick += GameController_OnBeforeTick;

            waterDisplay.SetText($"{currentWater}/{maximumWater}");
        }

        void Start()
        {
            waterBalance.OnValidate();
        }

        //private void Grid_OnGridGenerated()
        //{
        //    List<WaterSource> waterSources = grid.GetAllTiles<WaterSource>();
        //    if (waterSources.Count == 0)
        //    {
        //        Debug.Log("No Watersources in Scene!");
        //        return;
        //    }
        //    corners = new List<Tile>();
        //    int minX = waterSources.Min(x => x.X);
        //    int maxX = waterSources.Max(x => x.X);
        //    int minY = waterSources.Min(x => x.Y);
        //    int maxY = waterSources.Max(x => x.Y);
        //    corners.Add(waterSources.Find(t => t.X == minX && t.Y == minY));
        //    corners.Add(waterSources.Find(t => t.X == maxX && t.Y == minY));
        //    corners.Add(waterSources.Find(t => t.X == minX && t.Y == maxY));
        //    corners.Add(waterSources.Find(t => t.X == maxX && t.Y == maxY));

        //    foreach (var waterSource in waterSources)
        //    {
        //        waterSource.FindBestWaterGroup();
        //    }
        //}

        void OnDestroy()
        {
            grid.OnTileAdded -= Grid_OnTileAdded;
            grid.OnTileRemoved -= Grid_OnTileRemoved;
            grid.OnTileObjectAdded -= Grid_OnTileObjectAdded;
            grid.OnTileObjectRemoved -= Grid_OnTileObjectRemoved;
            //grid.OnGridGenerated -= Grid_OnGridGenerated;
            WeatherController.OnWeatherChanged -= WeatherController_OnWeatherChanged;
            GameController.OnBeforeTick -= GameController_OnBeforeTick;
        }

        private void GameController_OnBeforeTick()
        {
            currentWater += defaultBalance + weatherController.WeatherType.RangeChange;
            waterBalance.Value = currentWater - lastBalance;
            lastBalance = currentWater;
            currentWater = Mathf.Clamp(currentWater, 0, maximumWater);

            waterDisplay.SetText($"{currentWater}/{maximumWater}");
        }
        private void Grid_OnTileRemoved(Tile obj)
        {
            if (obj is WaterStorage)
            {
                maximumWater -= 4;
                waterDisplay.SetText($"{currentWater}/{maximumWater}");
            }
        }

        private void Grid_OnTileAdded(Tile obj)
        {
            if (obj is WaterStorage)
            {
                maximumWater += 4;
                waterDisplay.SetText($"{currentWater}/{maximumWater}");
            }
        }

        void FixedUpdate()
        {
            ChangeMaterial();
        }


        public void ChangeMaterial()
        {
            waterMat.SetFloat("_percent", Mathf.Lerp(0.54f, 0f, currentWater / 50f));
        }

        public bool TryToUseWater(int amount)
        {
            if (currentWater < amount)
            {
                return false;
            }
            currentWater -= amount;
            return true;
        }

        private void WeatherController_OnWeatherChanged(WeatherType obj)
        {
        }
        private void Grid_OnTileObjectRemoved(TileObject obj)
        {
            if (obj is Crop crop)
            {
                crops.Remove(crop);
                
            }
        }

        private void Grid_OnTileObjectAdded(TileObject obj)
        {
            if (obj is Crop crop)
            {
                crop.WaterController = this;
                crops.Add(crop);
            }
            //if (obj is Water water)
            //{
            //    waterTiles.Add(water);
            //    water.CurrentWater = CalculateWaterHeight(water);
            //    waterDisplaySpawner.Spawn().Setup(water);
            //}          
        }

        //private int CalculateWaterHeight(Tile tile)
        //{
        //    int distance = CalculateDistance(tile);
        //    return defaultRange + weatherController.WeatherType.RangeChange - distance;
        //}
        //private int CalculateDistance(Tile tile)
        //{
        //    int closestDistance = -1;
        //    for (int i = 0; i < corners.Count; i++)
        //    {
        //        int distance = TileCoord.GetDistance(tile.Center, corners[i].Center);
        //        if (closestDistance == -1 || distance < closestDistance)
        //        {
        //            closestDistance = distance;
        //        }
        //    }
        //    return closestDistance;
        //}


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