using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames
{
    public class WeatherController : MonoBehaviour
    {
        [SerializeField] private List<WeatherType> weatherTypes = default;

        string order = "11223221243320220334433221";

        private int currentIndex;

        public static event Action<WeatherType> OnWeatherChanged;

        private int currentTicks;
        [SerializeField] private int ticksToNextWeather = 40;

        public WeatherType WeatherType
        {
            get
            {
                return weatherType;
            }
            private set
            {
                weatherType = value;
            }
        }
        private WeatherType weatherType;


        void Awake()
        {
            //GameController.OnTick += GameController_OnTick;
            SetWeather(2);
        }

        private void GameController_OnTick()
        {
            currentTicks++;
            if (currentTicks >= ticksToNextWeather)
            {
                currentTicks = 0;
                SetWeather(int.Parse(order.ElementAt(currentIndex).ToString()));
                currentIndex++;
                if (currentIndex >= order.Length)
                {
                    currentIndex = 0;
                }
            }
        }
        private void SetWeather(int index)
        {
            Debug.Log("Weather index: " + index);
            WeatherType = weatherTypes[index];
            OnWeatherChanged?.Invoke(WeatherType);
            Debug.Log("Setting Weather to: " + WeatherType.name);
        }
    }
}