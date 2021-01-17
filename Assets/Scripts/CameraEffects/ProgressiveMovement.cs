using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///@author Francis Carroll
///@date 09/11/20

public class ProgressiveMovement
{
    public static void Move(ref CameraData t_data, Vector3 t_target)
	{
        if (t_data.GetCameraState() != CameraMovementState.None)
        {
            if (SlowOnArrive(ref t_data, t_target))
            {
                return;
            }
            Globals.CapSpeed(ref t_data, Globals.MAX_SPEED);

            //if the current camera's state is set to steering
            if (t_data.GetCameraState() == CameraMovementState.Steering)
            {
                SteeringMovement(ref t_data, t_target);
            }

            Vector3 orientationVector = new Vector3(0.0f, 0.0f, 0.0f);
            if (t_data.GetOrientation() != 0.0f)
            {
                //calculates the orientaion as a vector
                orientationVector = Globals.CreateVector(-Mathf.Sin(t_data.GetOrientation()), Mathf.Cos(t_data.GetOrientation()));
            }

            t_data.SetVelocity2(t_data.GetCurrentSpeed() * (orientationVector));

            t_data.SetOrientation(Globals.GetNewOrientation(t_data.GetOrientation(), t_target - t_data.GetPosition()));
            t_data.SetPosition(t_data.GetPosition() + (t_data.GetVelocity() * Time.deltaTime));
        }
	}

    public static bool SlowOnArrive(ref CameraData t_data, Vector3 t_target)
    {
        float l_distance = Globals.Magnitude(t_target - t_data.GetPosition());

        //calculate the speed based on the position relative to the target
        if (l_distance < Globals.MAX_ARRIVE_RADIUS)
        {
            t_data.SetCurrentSpeed(0.0f);
            return true;
        }
		//slow down when within slow down radius
		else if (l_distance >= Globals.MAX_ARRIVE_RADIUS && l_distance < Globals.MAX_SLOW_RADIUS)
		{
			t_data.SetCurrentSpeed(t_data.GetInitialSpeed() * (l_distance / Globals.MAX_SLOW_RADIUS));
		}
		return false;
    }

    public static void SteeringMovement(ref CameraData t_data, Vector3 t_target)
    {
        t_data.SetLinearSteering(Globals.GetSeeringToLocation(t_data.GetPosition(), t_target));
        t_data.SetVelocity(t_data.GetVelocity() + t_data.GetLinearSteering() * Time.deltaTime);

        //if the velocity is greater than the max speed, normalise and cap at max speed.
        if (Globals.Magnitude(t_data.GetVelocity()) > Globals.MAX_SPEED_RANGE)
        {
            t_data.SetVelocity(Globals.Normalise(t_data.GetVelocity()));
            t_data.SetVelocity(t_data.GetVelocity() * Globals.MAX_SPEED_RANGE);
        }
    }
}