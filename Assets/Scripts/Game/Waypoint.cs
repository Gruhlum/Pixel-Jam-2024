using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class Waypoint : MonoBehaviour
	{
		public Transform start;
		public Transform end;

		public Vector2 GetStartPosition()
		{
			return start.position;
		}
		public Vector2 GetEndPosition()
		{
			return end.position;
		}
		public Vector2 GetCenterPosition()
		{
			return Vector2.Lerp(start.position, end.position, 0.5f) ;
		}
        public Vector2 GetRandomPosition()
        {
			return Vector2.Lerp(start.position, end.position, Random.Range(0f, 1f));
        }
    }
}