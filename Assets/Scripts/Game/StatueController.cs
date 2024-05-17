using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class StatueController : MonoBehaviour
	{
		[SerializeField] private int StartHealth = default;

        public int CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            private set
            {
                currentHealth = value;
            }
        }
        private int currentHealth;


        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
        }
    }
}