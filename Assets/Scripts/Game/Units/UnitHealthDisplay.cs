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
            if (this.target != null)
            {
                target.OnDied -= Target_OnDied;
            }
            base.Setup(unit);
            this.target = unit;
            target.OnDied += Target_OnDied;
        }

        private void Target_OnDied(Unit obj)
        {
            target.OnDied -= Target_OnDied;
            gameObject.SetActive(false);
        }
    }
}