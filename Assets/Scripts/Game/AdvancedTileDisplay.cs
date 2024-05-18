using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class AdvancedTileDisplay : TileDisplay
	{
		[SerializeField] private Material defaultMat = default;
        [SerializeField] private Material waterMat = default;
        public override void Setup(Tile tile)
        {
            base.Setup(tile);
            if (tile is WaterStorage)
            {
                sr.material = waterMat;
            }
            else sr.material = defaultMat;
        }
    }
}