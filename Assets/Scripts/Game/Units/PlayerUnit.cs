using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    public class PlayerUnit : Unit
    {
        public ResourceController ResourceController
        {
            get
            {
                return resourceController;
            }
            set
            {
                resourceController = value;
            }
        }
        private ResourceController resourceController;


        public override void Setup(UnitController unitC, Waypoint waypoint, UnitData data)
        {
            base.Setup(unitC, waypoint, data);
            Vector2 pathPosition = waypoint.GetEndPosition();
            targetPosition = pathPosition;
            StartCoroutine(AnimationStart(transform.position, pathPosition));
        }
        public override Vector2 GetNextTargetPosition()
        {
            return path.GetCenterPosition() + Random.insideUnitCircle;
        }
        protected override bool GetOppositeType(Collider2D coll, out Unit unit)
        {
            bool result = coll.TryGetComponent(out EnemyUnit playerUnit);
            unit = playerUnit;
            return result;
        }

        private IEnumerator AnimationStart(Vector2 spawnPoint, Vector2 targetPoint)
        {
            allowMoveToTarget = false;

            float timer = 0;
            while (timer < 1f)
            {
                timer += Time.deltaTime * 2;
                yield return null;
                transform.position = Vector2.Lerp(spawnPoint, targetPoint, timer);
            }
            allowMoveToTarget = true;
        }
    }
}