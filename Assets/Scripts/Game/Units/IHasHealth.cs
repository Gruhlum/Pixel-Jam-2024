using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{	
	public interface IHasHealth
	{
		public int MaximumHealth
		{
			get;
		}
		public int CurrentHealth
		{
			get;
		}
		public event Action<int> OnHealthChanged;
	}
}