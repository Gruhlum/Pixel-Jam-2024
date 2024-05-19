using HexTecGames.GridBaseSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    public abstract class Unit : MonoBehaviour, IHasHealth
    {
        [SerializeField] private SpriteRenderer sr = default;

        public int CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            private set
            {
                if (value > MaximumHealth)
                {
                    value = MaximumHealth;
                }
                currentHealth = value;
                OnHealthChanged?.Invoke(currentHealth);
                if (currentHealth <= 0)
                {
                    Die();
                }
            }
        }
        private int currentHealth;

        public int MaximumHealth
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
        private float duration;
        public event Action<Unit> OnDied;
        public event Action<int> OnHealthChanged;

        protected bool allowMoveToTarget = true;

        protected virtual void FixedUpdate()
        {
            duration += Time.deltaTime;
            attackTimer += Time.deltaTime;
            if (targetUnit != null)
            {
                sr.flipX = transform.position.x < targetUnit.transform.position.x;
            }
            else sr.flipX = transform.position.x < targetPosition.x;
            if (duration > unitData.Duration)
            {
                gameObject.SetActive(false);
            }
            if (!allowMoveToTarget)
            {
                return;
            }
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
        public virtual void Setup(UnitController unitC, Waypoint waypoint, UnitData data, Vector2 spawnPoint)
        {
            transform.position = spawnPoint;
            Setup(unitC, waypoint, data);
        }
        public virtual void Setup(UnitController unitC, Waypoint waypoint, UnitData data)
        {
            this.unitData = data;
            this.unitC = unitC;
            this.path = waypoint;
            CurrentHealth = MaximumHealth;
            sr.sprite = data.sprite;
        }
        protected abstract bool GetOppositeType(Collider2D coll, out Unit unit);
        private void DetectEnemies()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, Mathf.Max( 1.5f, unitData.Range + 0.5f), Vector2.zero);
            foreach (var hit in hits)
            {
                if (hit.collider != null && GetOppositeType(hit.collider, out Unit unit) && unit.path == this.path)
                {
                    targetUnit = unit;
                    targetUnit.OnDied += TargetUnit_OnDied;
                    return;
                }
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
            //Debug.Log("Taking Damage!");
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