using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globals
{
	public class Globals
	{
		public const float PI = 3.141592653589f;

		public static Vector3 CreateVector(float t_x, float t_y)
		{
			Vector3 vector;
			vector.x = t_x;
			vector.y = t_y;
			vector.z = 0;
			return vector;
		}

		public static float Magnitude(Vector3 t_vector)
		{
			return Mathf.Sqrt(Mathf.Pow(t_vector.x, 2.0f) + Mathf.Pow(t_vector.y, 2.0f));
		}

		public static Vector3 Normalise(Vector3 t_vector)
		{
			if (Magnitude(t_vector) > 0)
			{
				return t_vector / Magnitude(t_vector);
			}
			else
			{
				return t_vector;
			}
		}

		public static float GetNewOrientation(float t_currentOrientation, Vector3 t_targetVector)
		{
			if (Magnitude(t_targetVector) > 0)
			{
				return Mathf.Atan2(-t_targetVector.x, t_targetVector.y);
			}
			else
			{
				return t_currentOrientation;
			}
		}
	}
}
