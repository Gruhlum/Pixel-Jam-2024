using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	[System.Serializable]
	public class Path
	{
		public List<Tile> pathTiles = new List<Tile>();

		private bool isVertical;

		private int startIndex;
		private int centerIndex;
		private int endIndex;

		public void Setup(List<Tile> tiles)
		{
			pathTiles = tiles;
		}
	}
}