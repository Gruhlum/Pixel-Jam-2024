using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{	
	public interface IWaterStorage
	{
        public WaterGroup WaterGroup
        {
            get;
            set;
        }
    }
}