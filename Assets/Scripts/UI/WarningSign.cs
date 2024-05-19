using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class WarningSign : MonoBehaviour
	{
		float timer = 0;

        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 18)
            {
                gameObject.SetActive(false);
            }
        }

        public void Setup(Vector2 position)
        {
            transform.position = position;
            timer = 0;
        }
    }
}