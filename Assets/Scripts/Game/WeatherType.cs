using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	[CreateAssetMenu(menuName = "HexTecGames/WeatherType")]
	public class WeatherType : ScriptableObject
	{
        public int RangeChange
        {
            get
            {
                return rangeChange;
            }
            private set
            {
                rangeChange = value;
            }
        }
        [SerializeField] private int rangeChange;

    }
}