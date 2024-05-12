using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public abstract class CostObjectData : TileObjectData, ICost
    {
        [SerializeField] private List<ResourceValue> costs = new List<ResourceValue>();

        public bool IsAffordable(List<Resource> resources)
        {
            foreach (var cost in costs)
            {
                Resource result = resources.Find(x => x.Data == cost.Data);
                if (result == null)
                {
                    Debug.Log("Could not find resource of type: " + cost.Data);
                }
                if (result.Value < cost.Value)
                {
                    return false;
                }
            }
            return true;
        }

        public void SubtractResources(List<Resource> resources)
        {
            foreach (var cost in costs)
            {
                Resource result = resources.Find(x => x.Data == cost.Data);
                if (result == null)
                {
                    Debug.Log("Could not find resource of type: " + cost.Data);
                }
                result.Value -= cost.Value;
            }
        }

        public ResourceValue GetCost()
        {
            return costs[0];
        }
    }
}