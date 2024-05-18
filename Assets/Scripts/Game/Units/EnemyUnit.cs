using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    public class EnemyUnit : Unit
    {
        public StatueController StatueController
        {
            get
            {
                return statueController;
            }
            set
            {
                statueController = value;
            }
        }
        private StatueController statueController;

        private float coreAttackTimer;

        public override void Setup(UnitController unitC, Waypoint waypoint, UnitData data)
        {
            base.Setup(unitC, waypoint, data);
            transform.position = waypoint.GetStartPosition() + Random.insideUnitCircle;
            targetPosition = waypoint.GetEndPosition() + Random.insideUnitCircle;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (targetUnit == null && Vector2.Distance(targetPosition, transform.position) < 0.5f)
            {
                coreAttackTimer += Time.deltaTime;
                if (coreAttackTimer >= 1)
                {
                    coreAttackTimer -= 1;
                    StatueController.TakeDamage(1);
                }
            }
        }
        public override Vector2 GetNextTargetPosition()
        {
            return path.GetEndPosition();
        }

        protected override bool GetOppositeType(Collider2D coll, out Unit unit)
        {
            bool result = coll.TryGetComponent(out PlayerUnit playerUnit);
            unit = playerUnit;
            return result;
        }
    }
}