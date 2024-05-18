using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    public class UnitHealthDisplay : HealthDisplay
    {
        [SerializeField] private Vector3 offset = new Vector2(0, 0.33f);
        private Unit target;

        private void Update()
        {
            if (target != null)
            {
                transform.position = target.transform.position + offset;
            }
        }

        public void Setup(Unit unit)
        {
            base.Setup(unit);
            this.target = unit;
        }
    }
}