using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames
{
	public abstract class ActionButton : MonoBehaviour
	{
		[SerializeField] private Color normalColor = default;
		[SerializeField] private Color activeColor = default;

        [SerializeField] private Image img = default;


        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                img.color = isSelected ? activeColor : normalColor;
            }
        }
        private bool isSelected;
    }
}