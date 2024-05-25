using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    public class PlayerUnitController : UnitController
    {
        [SerializeField] private ResourceController resourceC = default;

        void Awake()
        {
            //farmGrid.OnTileObjectAdded += FarmGrid_OnTileObjectAdded;
        }
        void OnDestroy()
        {
            //farmGrid.OnTileObjectAdded += FarmGrid_OnTileObjectAdded;
        }

        //private void FarmGrid_OnTileObjectAdded(TileObject obj)
        //{
        //    if (obj is Crop crop)
        //    {
        //        crop.OnRemoved += Crop_OnRemoved;
        //    }
        //}

        public void SetupUnit(Crop crop)
        {
            Waypoint waypoint = waypointController.GetClosestWaypoint(crop.GetWorldPosition());
            if (waypoint != null)
            {
                SpawnUnit(waypoint, crop.CropData.UnitData, crop.GetWorldPosition());
            }
        }

        //private void Crop_OnRemoved(GridObject obj)
        //{
        //    if (obj is Crop crop)
        //    {
        //        crop.OnRemoved -= Crop_OnRemoved;
        //        crop.OnFullyGrown -= Crop_OnFullyGrown;
        //        crops.Remove(crop);
        //    }
        //}
        public override void SetupUnit(Unit unit, Waypoint waypoint, UnitData unitData)
        {
            base.SetupUnit(unit, waypoint, unitData);
            if (unit is PlayerUnit playerUnit)
            {
                playerUnit.ResourceController = resourceC;
            }
        }
    }
}