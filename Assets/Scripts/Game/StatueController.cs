using HexTecGames.SoundSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class StatueController : MonoBehaviour, IHasHealth
	{
        [SerializeField] private GameController gc = default;

        public int CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            private set
            {
                if (value > maximumHealth)
                {
                    value = maximumHealth;
                }
                if (value == currentHealth)
                {
                    return;
                }
                currentHealth = value;
                OnHealthChanged?.Invoke(currentHealth);
            }
        }
        private int currentHealth;

        [SerializeField] private SoundClipGroup takeDamageSound = default;

        public int MaximumHealth
        {
            get
            {
                return maximumHealth;
            }
            private set
            {
                maximumHealth = value;
            }
        }
        [SerializeField] private int maximumHealth = default;
        

        public event Action<int> OnHealthChanged;

        void Awake()
        {
            CurrentHealth = MaximumHealth;
        }

        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            takeDamageSound?.Play();
            if (CurrentHealth <= 0)
            {
                gc.GameOver();
            }
        }
    }
}