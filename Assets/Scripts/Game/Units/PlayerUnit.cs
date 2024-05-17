using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    public class PlayerUnit : Unit
    {
        public override void Setup(UnitController unitC, Waypoint waypoint, UnitData data)
        {
            base.Setup(unitC, waypoint, data);
            transform.position = waypoint.GetEndPosition();
            targetPosition = transform.position;
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
    }
}