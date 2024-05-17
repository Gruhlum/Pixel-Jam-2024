using HexTecGames.GridBaseSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    public abstract class Unit : MonoBehaviour
    {
        public int CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            private set
            {
                if (value > MaxHealth)
                {
                    value = MaxHealth;
                }
                currentHealth = value;
                if (currentHealth <= 0)
                {
                    Die();
                }
            }
        }
        private int currentHealth;

        public int MaxHealth
        {
            get
            {
                return unitData.MaxHealth;
            }
        }

        private UnitData unitData;
        private UnitController unitC;

        protected Waypoint path;

        protected Vector2 targetPosition;
        protected Unit targetUnit;

        private float attackTimer;

        public event Action<Unit> OnDied;

        private void FixedUpdate()
        {
            if (targetUnit == null)
            {
                DetectEnemies();
            }
            if (targetUnit != null)
            {
                if (Vector2.Distance(targetUnit.transform.position, transform.position) > unitData.Range)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetUnit.transform.position, unitData.MoveSpeed * Time.deltaTime);
                }
                attackTimer += Time.deltaTime;
                if (attackTimer > unitData.AttackDelay)
                {
                    attackTimer = 0;
                    targetUnit.TakeDamage(unitData.Damage);
                }
            }
            else
            {
                if (Vector2.Distance(targetPosition, transform.position) > 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, unitData.MoveSpeed * Time.deltaTime);
                }
                else targetPosition = GetNextTargetPosition();
            }         
        }
        public virtual void Setup(UnitController unitC, Waypoint waypoint, UnitData data)
        {
            this.unitData = data;
            this.unitC = unitC;
            this.path = waypoint;
            CurrentHealth = MaxHealth;
        }
        protected abstract bool GetOppositeType(Collider2D coll, out Unit unit);
        private void DetectEnemies()
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1, Vector2.zero);
            if (hit.collider != null && GetOppositeType(hit.collider, out Unit unit) && unit.path == this.path)
            {
                targetUnit = unit;
                targetUnit.OnDied += TargetUnit_OnDied;
                attackTimer = 0;
            }
        }

        private void TargetUnit_OnDied(Unit obj)
        {
            obj.OnDied -= TargetUnit_OnDied;
            targetUnit = null;
        }

        public abstract Vector2 GetNextTargetPosition();

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            Debug.Log("Taking Damage!");
        }
        public void Heal(int heal)
        {
            CurrentHealth += heal;
        }
        private void Die()
        {
            OnDied?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}