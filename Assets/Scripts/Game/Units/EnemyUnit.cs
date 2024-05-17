using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    public class EnemyUnit : Unit
    {

        public override void Setup(UnitController unitC, Waypoint waypoint, UnitData data)
        {
            base.Setup(unitC, waypoint, data);
            transform.position = waypoint.GetStartPosition() + Random.insideUnitCircle;
            targetPosition = waypoint.GetEndPosition() + Random.insideUnitCircle;
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