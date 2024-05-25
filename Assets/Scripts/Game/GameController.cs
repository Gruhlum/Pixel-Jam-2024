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
        public static event Action OnGameOver;

        private float timer = -3;
        private int minutes = 7;
        private int seconds = 59;
        private bool gameOver;

        void FixedUpdate()
        {
            if (gameOver)
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
                    if (minutes <= 0)
                    {
                        WinGame();
                        return;
                    }
                    minutes--;
                    seconds = 59;
                }

                survivalTimer.text = "<mspace=28>" + minutes.ToString("00") + ":" + seconds.ToString("00");
            }
        }

        private void WinGame()
        {
            gameOver = true;
            winScreen.SetActive(true);
            OnGameOver?.Invoke();
        }

        public void GameOver()
        {
            gameOver = true;
            endScreen.Setup(Time.timeSinceLevelLoad.ToString("#00:00"));
            OnGameOver?.Invoke();
        }
    }
}