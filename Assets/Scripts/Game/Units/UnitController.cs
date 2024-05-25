using HexTecGames.Basics;
using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public abstract class UnitController : MonoBehaviour
	{
		[SerializeField] private Spawner<Unit> unitSpawner = default;
		[SerializeField] protected BaseGrid grid = default;
		[SerializeField] protected WaypointController waypointController = default;
		[SerializeField] private Spawner<UnitHealthDisplay> healthDisplaySpawner = default;


        public void SpawnUnit(Waypoint waypoint, UnitData unitData, Vector2 spawnPoint)
        {
            Unit unit = unitSpawner.Spawn();
            unit.Setup(this, waypoint, unitData, spawnPoint);
            healthDisplaySpawner.Spawn().Setup(unit);
        }
        public void SpawnUnit(Waypoint waypoint, UnitData unitData)
		{
			Unit unit = unitSpawner.Spawn();
			SetupUnit(unit, waypoint, unitData);
			healthDisplaySpawner.Spawn().Setup(unit);
        }
		public virtual void SetupUnit(Unit unit, Waypoint waypoint, UnitData unitData)
		{
            unit.Setup(this, waypoint, unitData);
        }
	}
}