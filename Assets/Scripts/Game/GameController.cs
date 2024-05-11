using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class GameController : MonoBehaviour
    {
        [SerializeField] private int secondsPerTick = 4;

        public static event Action OnTick;

        private float timer;

        void FixedUpdate()
        {
            timer += Time.deltaTime;
            if (timer >= secondsPerTick)
            {
                timer -= secondsPerTick;
                OnTick?.Invoke();
            }
        }
    }
}