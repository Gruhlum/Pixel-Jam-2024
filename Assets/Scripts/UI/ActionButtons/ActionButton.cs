using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames
{
	public abstract class ActionButton : MonoBehaviour
	{
		[SerializeField] private Sprite normalSprite = default;
		[SerializeField] private Sprite activeSprite = default;

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
                img.sprite = isSelected ? activeSprite : normalSprite;
            }
        }
        private bool isSelected;


    }
}