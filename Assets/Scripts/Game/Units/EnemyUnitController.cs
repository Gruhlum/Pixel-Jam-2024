using HexTecGames.Basics;
using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames
{
	public class EnemyUnitController : UnitController
    {
		[SerializeField] private List<UnitData> enemyDatas = default;

        private int currentTicks;

        [SerializeField] private int ticksToSpawn = default;
        [SerializeField] private StatueController statueC = default;

        [SerializeField] private List<LevelData> levelDatas = default;
        [SerializeField] private Spawner<WarningSign> warningSpawner = default;
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
            int frequency = 10 - (currentTicks / 60);
            if (currentTicks > 170 && currentTicks % frequency == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    SpawnWarningSign(i);
                    if (currentTicks > 190)
                    {
                        SpawnUnit(waypointController.GetWaypoint(i), enemyDatas.Random());
                    }
                }
                
            }
            for (int i = 0; i < levelDatas.Count; i++)
            {
                if (levelDatas[i].timers.Any(x => x == currentTicks + 18))
                {
                    SpawnWarningSign(i);
                }
                if (levelDatas[i].timers.Any(x => x == currentTicks))
                {
                    SpawnUnit(waypointController.GetWaypoint(i), enemyDatas.Random());
                }
            }
        }
        private void SpawnWarningSign(int index)
        {
            warningSpawner.Spawn().Setup(waypointController.GetStartPosition(index) + Random.insideUnitCircle);
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