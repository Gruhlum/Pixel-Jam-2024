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
		[SerializeField] protected PathController pathC = default;
		[SerializeField] protected BaseGrid pathGrid = default;
		[SerializeField] protected WaypointController waypointController = default;

		public void SpawnUnit(Waypoint waypoint, UnitData unitData)
		{
			unitSpawner.Spawn().Setup(this, waypoint, unitData);
		}
	}
}