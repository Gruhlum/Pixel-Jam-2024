using HexTecGames.Basics;
using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class WaterController : MonoBehaviour
	{
		[SerializeField] private BaseGrid grid = default;

       // private List<WaterGroup> waterGroups = new List<WaterGroup>();

        //private List<TileObject> waterStorages = new List<TileObject>();

        [SerializeField] private Spawner<WaterDisplay> waterDisplaySpawner = default;

        void Awake()
        {
            grid.OnTileObjectAdded += Grid_OnTileObjectAdded;
            grid.OnTileObjectRemoved += Grid_OnTileObjectRemoved;
        }
        void OnDestroy()
        {
            grid.OnTileObjectAdded -= Grid_OnTileObjectAdded;
            grid.OnTileObjectRemoved -= Grid_OnTileObjectRemoved;
        }
        private void Grid_OnTileObjectRemoved(TileObject obj)
        {
            if (obj is IWaterStorage)
            {
                //waterStorages.Remove(obj);
                //CalculateWaterGroups();
            }
        }

        private void Grid_OnTileObjectAdded(TileObject obj)
        {
            if (obj is Water water)
            {
                //waterStorages.Add(obj);
                waterDisplaySpawner.Spawn().Setup(water);
                //CalculateWaterGroups();
            }
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