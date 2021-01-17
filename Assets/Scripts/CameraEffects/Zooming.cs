using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zooming
{
    public static void ZoomInOutAmount(ref CameraData t_data)
	{
		CapZoom(ref t_data);

		if (t_data.GetZoomDirection() == ZoomDirection.ZoomIn)
		{
			if (t_data.GetPosition().z <= t_data.GetInitialZoom() + t_data.GetZoomAmount())
			{
				t_data.SetVelocity(new Vector3(t_data.GetVelocity().x, t_data.GetVelocity().y, t_data.GetCurrentSpeed() * Globals.VELOCITY_SLOW));
				ZoomMovement(ref t_data);
			}
		}
		else if (t_data.GetZoomDirection() == ZoomDirection.ZoomOut)
		{
			if (t_data.GetPosition().z >= t_data.GetInitialZoom() - t_data.GetZoomAmount())
			{
				t_data.SetVelocity(new Vector3(t_data.GetVelocity().x, t_data.GetVelocity().y, -t_data.GetCurrentSpeed() * Globals.VELOCITY_SLOW));
				ZoomMovement(ref t_data);
			}
		}
	}

	public static void ZoomInOutTime(ref CameraData t_data)
	{
		CapZoom(ref t_data);

		if (t_data.GetZoomDirection() == ZoomDirection.ZoomIn)
		{
			if (t_data.GetPosition().z <= (t_data.GetInitialZoom() + t_data.GetZoomAmount()))
			{
				if (t_data.GetTimeForZoom() != 0)
				{
					t_data.SetCurrentZoomSpeed(t_data.GetZoomAmount() / t_data.GetTimeForZoom());
				}
				t_data.SetVelocity(new Vector3(t_data.GetVelocity().x, t_data.GetVelocity().y, t_data.GetCurrentZoomSpeed()));
				ZoomMovement(ref t_data);
			}
		}
		if (t_data.GetZoomDirection() == ZoomDirection.ZoomOut)
		{
			if (t_data.GetPosition().z >= (t_data.GetInitialZoom() - t_data.GetZoomAmount()))
			{
				if (t_data.GetTimeForZoom() != 0)
				{
					t_data.SetCurrentZoomSpeed(t_data.GetZoomAmount() / t_data.GetTimeForZoom());
				}
				t_data.SetVelocity(new Vector3(t_data.GetVelocity().x, t_data.GetVelocity().y, -t_data.GetCurrentZoomSpeed()));
				ZoomMovement(ref t_data);
			}
		}
	}

	public static void ZoomInOutPosition(ref CameraData t_data, Vector3 t_position)
	{
		float distance = Globals.Magnitude(t_data.GetPosition() - t_position);
		float time = distance / t_data.GetInitialSpeed();

		t_data.SetTimeForZoom(time);

		ZoomInOutTime(ref t_data);

		ProgressiveMovement.Move(ref t_data, t_position);
	}

	private static void ZoomMovement(ref CameraData t_data)
	{
		t_data.SetPosition(t_data.GetPosition() + (t_data.GetVelocity() * Time.deltaTime));
	}

	private static void CapZoom(ref CameraData t_data)
	{
		if(t_data.GetPosition().z > Globals.MAX_ZOOM)
		{
			t_data.SetPosition(new Vector3(t_data.GetPosition().x, t_data.GetPosition().y, Globals.MAX_ZOOM));
		}
		else if (t_data.GetPosition().z < -Globals.MAX_ZOOM)
		{
			t_data.SetPosition(new Vector3(t_data.GetPosition().x, t_data.GetPosition().y, -Globals.MAX_ZOOM));
		}
	}
}