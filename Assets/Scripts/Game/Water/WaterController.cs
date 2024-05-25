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

        [SerializeField] private ResourceController resourceC = default;
        [SerializeField] private ResourceType goldType = default;
        [SerializeField] private Resource waterBalance = default;
        [SerializeField] private int maximumWater = default;
        [SerializeField] private int currentWater = default;
        [SerializeField] private TextDisplay waterDisplay = default;

        private List<Crop> crops = new List<Crop>();

        private int lastBalance;
        [SerializeField] private Material waterMat = default;

        private float targetValue;
        private float currentValue;

        private int waterGain = 4;
        private int trenchAmount = 0;

        void Awake()
        {
            grid.OnTileAdded += Grid_OnTileAdded;
            grid.OnTileRemoved += Grid_OnTileRemoved;
            grid.OnTileObjectAdded += Grid_OnTileObjectAdded;
            grid.OnTileObjectRemoved += Grid_OnTileObjectRemoved;
            //grid.OnGridGenerated += Grid_OnGridGenerated;
            WeatherController.OnWeatherChanged += WeatherController_OnWeatherChanged;
            GameController.OnBeforeTick += GameController_OnBeforeTick;
            lastBalance = currentWater;
            waterDisplay.SetText($"{currentWater}/{maximumWater}");
        }

        void Start()
        {
            waterBalance.OnValidate();
        }

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
        public void IncreaseWaterProduction()
        {
            waterGain++;
        }

        private void GameController_OnBeforeTick()
        {
            currentWater += waterGain;
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
                trenchAmount--;
            }
        }

        private void Grid_OnTileAdded(Tile obj)
        {
            if (obj is WaterStorage)
            {
                maximumWater += 4;
                waterDisplay.SetText($"{currentWater}/{maximumWater}");
                trenchAmount++;
            }
        }

        void FixedUpdate()
        {
            ChangeMaterial();

        }


        public void ChangeMaterial()
        {
            currentValue = Mathf.Lerp(0.56f, 0f, currentWater / (float)(maximumWater));
            targetValue = Mathf.Lerp(targetValue, currentValue, 0.1f);
            waterMat.SetFloat("_percent", targetValue);
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