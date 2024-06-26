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
        public UnitData UnitData
        {
            get
            {
                return unitData;
            }
            private set
            {
                unitData = value;
            }
        }
        [SerializeField] private UnitData unitData;

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

        public int MaximumDryTicks
        {
            get
            {
                return maximumDryTicks;
            }
            private set
            {
                maximumDryTicks = value;
            }
        }
        [SerializeField] private int maximumDryTicks = 12;


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

        public int sellPrice = 4;

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

        [SerializeField] private List<TileData> validEarthTiles = default;

        public override GridObject CreateObject(Coord center, BaseGrid grid)
        {
            return new Crop(center, grid, this);
        }

        public override bool IsValidCoord(Coord coord, BaseGrid grid)
        {
            if (!grid.IsTileEmpty(coord))
            {
                return false;
            }
            return validEarthTiles.Contains(grid.GetTile(coord).Data);
        }
    }
}