using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class WaypointController : MonoBehaviour
	{
		[SerializeField] private List<Waypoint> paths = default;

		public Waypoint GetWaypoint(int index)
		{
			return paths[index];
		}
		public Vector2 GetStartPosition(int index)
		{
			return paths[index].GetStartPosition();
		}
		public Waypoint GetClosestWaypoint(Vector2 position)
		{
			Waypoint closest = null;
			float distance = -1;
			foreach (var waypoint in paths)
			{
				float currentDistance = Vector2.Distance(position, waypoint.GetStartPosition());
				if (closest == null || currentDistance < distance)
				{
					closest = waypoint;
					distance = currentDistance;
				}
			}
			return closest;
		}

	}
}