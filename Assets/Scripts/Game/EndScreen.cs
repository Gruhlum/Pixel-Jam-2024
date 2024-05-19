using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HexTecGames
{
	public class EndScreen : MonoBehaviour
	{
		[SerializeField] private TMP_Text timeGUI = default;


		public void Setup(string time)
		{
			timeGUI.text = time;
			gameObject.SetActive(true);
		}
	}
}