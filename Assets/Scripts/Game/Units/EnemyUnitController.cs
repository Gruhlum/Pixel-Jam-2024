using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class EnemyUnitController : UnitController
    {
		[SerializeField] private List<UnitData> enemyDatas = default;

        private int currentTicks;

        [SerializeField] private int ticksToSpawn = default;
        [SerializeField] private StatueController statueC = default;

        void Awake()
        {
            GameController.OnTick += GameController_OnTick;
        }

        void OnDestroy()
        {
            GameController.OnTick -= GameController_OnTick;
        }
        private void GameController_OnTick()
        {
            currentTicks++;

            if (currentTicks >= ticksToSpawn)
            {
                currentTicks = 0;
                SpawnUnit(waypointController.GetWaypoint(0), enemyDatas.Random());
            }
        }
        public override void SetupUnit(Unit unit, Waypoint waypoint, UnitData unitData)
        {
            base.SetupUnit(unit, waypoint, unitData);
            if (unit is EnemyUnit enemy)
            {
                enemy.StatueController = statueC;
            }
        }
    }
}