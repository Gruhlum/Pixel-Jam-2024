using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class Unit : MonoBehaviour
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

		public void Setup(UnitData unitData, Vector2 spawnPoint, UnitController unitC)
        {
			this.unitData = unitData;
			transform.position = spawnPoint;
			this.unitC = unitC;
		}

		public void TakeDamage(int damage)
		{
			CurrentHealth -= damage;
		}
		public void Heal(int heal)
		{
			CurrentHealth += heal;
		}
		private void Die()
		{
			gameObject.SetActive(false);
		}
	}
}