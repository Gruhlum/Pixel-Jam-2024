using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames
{
	[System.Serializable]
	public abstract class WaterStorage : Tile, IWaterStorage
    {
        public WaterGroup WaterGroup
        {
            get
            {
                return waterGroup;
            }

            set
            {
                waterGroup = value;
            }
        }
        private WaterGroup waterGroup;

        private List<Crop> cropNeighbours = new List<Crop>();

        public abstract bool HasWater
        {
            get;
        }

        public WaterStorage(Coord center, BaseGrid grid, TileData data) : base(center, grid, data)
        {
            //FindBestWaterGroup();

            //GameController.OnBeforeTick += GameController_OnBeforeTick;
        }
        public void FindBestWaterGroup()
        {
            var neighboursCoords = Grid.GetNeighbourCoords(Center);
            List<Tile> tileNeighbours = Grid.GetTiles(neighboursCoords);
            List<WaterStorage> neighbours = new List<WaterStorage>();

            foreach (var tile in tileNeighbours)
            {
                if (tile is WaterStorage water)
                {
                    neighbours.Add(water);
                }
            }

            if (neighbours.Count == 0)
            {
                waterGroup = new WaterGroup();
                waterGroup.Add(this);
            }
            else HandleMultipleWaterGroups(neighbours);
        }
        private void HandleMultipleWaterGroups(List<WaterStorage> neighbours)
        {
            List<WaterGroup> allWaterGroups = new List<WaterGroup>();
            foreach (var neighbour in neighbours)
            {
                if (!allWaterGroups.Contains(neighbour.WaterGroup))
                {
                    if (neighbour.WaterGroup == null)
                    {
                        Debug.Log("Is null for some reason");
                        continue;
                    }
                    allWaterGroups.Add(neighbour.WaterGroup);
                }
            }
            if (allWaterGroups.Count == 1)
            {
                waterGroup = allWaterGroups[0];
                allWaterGroups[0].Add(this);
                return;
            }
            int maxItems = allWaterGroups.Max(x => x.waterStorages.Count);
            WaterGroup biggestWaterGroup = allWaterGroups.Find(x => x.waterStorages.Count == maxItems);
            foreach (var waterGroup in allWaterGroups)
            {
                if (waterGroup != biggestWaterGroup)
                {
                    waterGroup.Merge(biggestWaterGroup);
                }
            }
            waterGroup = biggestWaterGroup;
            biggestWaterGroup.Add(this);
        }
        //public override void Remove()
        //{
        //    GameController.OnBeforeTick -= GameController_OnBeforeTick;
        //    base.Remove();
        //}
        //private void GameController_OnBeforeTick()
        //{
        //    //if (!HasWater)
        //    //{
        //    //    return;
        //    //}
        //    //GetCropNeighbours();
        //    //foreach (var crop in cropNeighbours)
        //    //{
        //    //    if (crop.IsWatered)
        //    //    {
        //    //        continue;
        //    //    }
        //    //    crop.WaterCrop();
        //    //}
        //}

        protected void GetCropNeighbours()
        {
            cropNeighbours = Grid.GetNeighbourObjects<Crop>(new List<Coord>() { Center });
        }
    }
}