using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
	public const float PI = 3.141592653589f;

    public static float MAX_ARRIVE_RADIUS = 0.1f;
    public static float MAX_SLOW_RADIUS = 0.6f;
    public static float MAX_SPEED_RANGE = 0.08f;
    public static float MAX_SPEED = 10.0f;
    public static float MAX_ACCELERATION = 0.5f;
    public static float VELOCITY_SLOW = 0.1f;

	public static float MAX_ZOOM = 20.0f;

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

    public static void CapSpeed(ref CameraData t_data, float t_cap)
    {
        if (t_data.GetCurrentSpeed() > t_cap)
        {
            t_data.SetCurrentSpeed(t_cap);
        }
    }

    public static Vector3 GetSeeringToLocation(Vector3 t_vector1, Vector3 t_vector2)
    {
        Vector3 linearSteering = t_vector1 - t_vector2;
        linearSteering = Normalise(linearSteering);
        linearSteering *= MAX_ACCELERATION;
        return linearSteering;
    }
}
