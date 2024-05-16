using HexTecGames.GridBaseSystem;
using HexTecGames.SoundSystem;
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
        [SerializeField] public Sprite[] drySprites = new Sprite[3];
        public SoundClipBase GrowSound
        {
            get
            {
                return growSound;
            }
        }
        [SerializeField] private SoundClipBase growSound = default;       

        public override GridObject CreateObject(Coord center, BaseGrid grid)
        {
            return new Crop(center, grid, this);
        }

        public override bool IsValidCoord(Coord coord, BaseGrid grid)
        {
            return grid.IsTileEmpty(coord);
        }
    }
}