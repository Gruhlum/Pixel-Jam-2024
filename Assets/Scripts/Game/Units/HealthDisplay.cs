using HexTecGames.Basics.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames
{
	public class HealthDisplay : MonoBehaviour
	{
        [SerializeField] private Slider slider = default;
        [SerializeField] private MonoBehaviour mono = default;

        private IHasHealth hasHealth;


        void OnValidate()
        {
			if (mono != null && mono is not IHasHealth)
			{
				Debug.Log(mono.name + " is not inheriting IHasHealth");
			}
        }

        private void Awake()
        {
            if (mono != null)
            {

                hasHealth = mono as IHasHealth;
                hasHealth.OnHealthChanged += HasHealth_OnHealthChanged;
            }                  
        }

        private void Start()
        {
            if (hasHealth != null)
            {
                InitSlider();
            }
        }

        private void InitSlider()
        {
            slider.maxValue = hasHealth.MaximumHealth;
            slider.value = hasHealth.CurrentHealth;
        }

        public void Setup(IHasHealth hasHealth)
        {
            if (this.hasHealth != null)
            {
                hasHealth.OnHealthChanged -= HasHealth_OnHealthChanged;
            }
            this.hasHealth = hasHealth;
            hasHealth.OnHealthChanged += HasHealth_OnHealthChanged;
            InitSlider();
        }
        private void HasHealth_OnHealthChanged(int health)
        {          
            if (health <= 0)
            {
                hasHealth.OnHealthChanged -= HasHealth_OnHealthChanged;
                gameObject.SetActive(false);
            }
            else UpdateSlider();
        }
        private void UpdateSlider()
        {
            slider.value = hasHealth.CurrentHealth;
        }
    }
}