using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HexTecGames
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int secondsPerTick = 4;
        [SerializeField] private EndScreen endScreen = default;
        [SerializeField] private GameObject winScreen = default;
        [SerializeField] private TMP_Text survivalTimer = default;
        public static event Action OnBeforeTick;
        public static event Action OnTick;
        public static event Action OnAfterTick;

        private float timer = -3;
        private int minutes = 7;
        private int seconds = 59;
        private bool hasLost;

        void FixedUpdate()
        {
            if (hasLost)
            {
                return;
            }
            timer += Time.deltaTime;
            if (timer >= secondsPerTick)
            {
                timer -= secondsPerTick;
                OnBeforeTick?.Invoke();
                OnTick?.Invoke();
                OnAfterTick?.Invoke();
                seconds--;
                if (seconds < 0)
                {
                    minutes--;
                    seconds = 59;
                }
                survivalTimer.text = "<mspace=28>" + minutes.ToString("00") + ":" + seconds.ToString("00");
            }
        }

        public void GameOver()
        {
            hasLost = true;
            endScreen.Setup(Time.timeSinceLevelLoad.ToString("#00:00"));
        }
    }
}