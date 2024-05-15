using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public abstract class UnitController : MonoBehaviour
	{
		[SerializeField] private Spawner<Unit> unitSpawner = default;

		public void SpawnUnit(Vector2 spawnPoint, UnitData unitData)
		{
			unitSpawner.Spawn().Setup(unitData, spawnPoint, this);
		}
	}
}