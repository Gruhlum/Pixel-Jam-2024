using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class GameController : MonoBehaviour
    {
        [SerializeField] private int secondsPerTick = 4;

        public static event Action OnBeforeTick;
        public static event Action OnTick;
        public static event Action OnAfterTick;

        private float timer;

        void FixedUpdate()
        {
            timer += Time.deltaTime;
            if (timer >= secondsPerTick)
            {
                timer -= secondsPerTick;
                OnBeforeTick?.Invoke();
                OnTick?.Invoke();
                OnAfterTick?.Invoke();
            }
        }
    }
}