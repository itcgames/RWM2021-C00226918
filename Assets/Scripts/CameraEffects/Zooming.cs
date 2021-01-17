﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zooming
{
    public static void ZoomInOutAmount(ref CameraData t_data, float t_zoomAmount)
	{
		CapZoom(ref t_data);

		if (t_data.GetZoomDirection() == ZoomDirection.ZoomIn)
		{
			if (t_data.GetPosition().z <= t_data.GetInitialZoom() + t_zoomAmount)
			{
				t_data.SetVelocity(new Vector3(t_data.GetVelocity().x, t_data.GetVelocity().y, t_data.GetCurrentSpeed() * Globals.VELOCITY_SLOW));
			}
		}
		else if (t_data.GetZoomDirection() == ZoomDirection.ZoomOut)
		{
			if (t_data.GetPosition().z >= t_data.GetInitialZoom() - t_zoomAmount)
			{
				t_data.SetVelocity(new Vector3(t_data.GetVelocity().x, t_data.GetVelocity().y, -t_data.GetCurrentSpeed() * Globals.VELOCITY_SLOW));
			}
		}

		ZoomMovement(ref t_data);
	}

	public static void ZoomInOutTime(ref CameraData t_data, float t_zoomAmount, float t_timeInSeconds)
	{
		CapZoom(ref t_data);

		float l_difference = (t_data.GetInitialZoom() + t_zoomAmount) - t_data.GetPosition().z;

		if (t_data.GetZoomDirection() == ZoomDirection.ZoomIn)
		{
			if (t_data.GetPosition().z <= t_data.GetInitialZoom() + t_zoomAmount)
			{
				t_data.SetCurrentSpeed(l_difference / t_timeInSeconds);
				t_data.SetVelocity(new Vector3(t_data.GetVelocity().x, t_data.GetVelocity().y, t_data.GetCurrentSpeed()));
			}
		}
		else if (t_data.GetZoomDirection() == ZoomDirection.ZoomOut)
		{
			if (t_data.GetPosition().z >= t_data.GetInitialZoom() - t_zoomAmount)
			{
				t_data.SetCurrentSpeed(l_difference / t_timeInSeconds);
				t_data.SetVelocity(new Vector3(t_data.GetVelocity().x, t_data.GetVelocity().y, -t_data.GetCurrentSpeed()));
			}
		}

		ZoomMovement(ref t_data);
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