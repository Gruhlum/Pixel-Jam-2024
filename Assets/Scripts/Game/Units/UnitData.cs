using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    [CreateAssetMenu(menuName = "HexTecGames/UnitData")]
    public class UnitData : ScriptableObject
	{
        public int MaxHealth
        {
            get
            {
                return maxHealth;
            }
            private set
            {
                maxHealth = value;
            }
        }
        [SerializeField] private int maxHealth;

        public int Damage
        {
            get
            {
                return damage;
            }
            private set
            {
                damage = value;
            }
        }
        [SerializeField] private int damage;

        public float AttackDelay
        {
            get
            {
                return attackDelay;
            }
            private set
            {
                attackDelay = value;
            }
        }
        [SerializeField] private float attackDelay;

        public float Range
        {
            get
            {
                return range;
            }
            private set
            {
                range = value;
            }
        }
        [SerializeField] private float range;

        public float MoveSpeed
        {
            get
            {
                return moveSpeed;
            }
            private set
            {
                moveSpeed = value;
            }
        }
        [SerializeField] private float moveSpeed;

        public int Duration
        {
            get
            {
                return duration;
            }
            private set
            {
                duration = value;
            }
        }
        [SerializeField] private int duration = 24;

        public int SellPrice
        {
            get
            {
                return reward;
            }
            private set
            {
                reward = value;
            }
        }
        [SerializeField] private int reward = 4;


        public Sprite sprite;
    }
}