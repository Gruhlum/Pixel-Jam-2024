using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	[CreateAssetMenu(menuName = "HexTecGames/CropData")]
	public class CropData : CostObjectData
    {
        public int RequiredGrowthTicks
        {
            get
            {
                return requiredGrowthTicks;
            }
            private set
            {
                requiredGrowthTicks = value;
            }
        }
        [SerializeField] private int requiredGrowthTicks;

        public int WaterPerTick
        {
            get
            {
                return waterPerTick;
            }
            private set
            {
                waterPerTick = value;
            }
        }
        [SerializeField] private int waterPerTick;


        public override bool IsDraggable
        {
            get
            {
                return true;
            }
        }

        public override bool IsWall
        {
            get
            {
                return false;
            }
        }     

        [SerializeField] public Sprite[] sprites = new Sprite[3];

        public override Sprite GetSprite()
        {
            return sprites[2];
        }

        public Sprite GetCurrentSprite(int currentGrowth)
        {
            if (currentGrowth >= RequiredGrowthTicks)
            {
                return sprites[2];
            }
            else if (currentGrowth < RequiredGrowthTicks / 2)
            {
                return sprites[0];
            }
            else return sprites[1];
        }

        public override TileObject CreateTileObject(Coord center, BaseGrid grid)
        {
            return new Crop(center, grid, this);
        }

        
    }
}