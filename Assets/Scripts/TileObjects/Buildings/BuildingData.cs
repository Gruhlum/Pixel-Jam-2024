using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
    [CreateAssetMenu(menuName = "HexTecGames/BuildingData")]
    public class BuildingData : CostObjectData
    {
        public override bool IsWall
        {
            get
            {
                return false;
            }
        }
    }
}