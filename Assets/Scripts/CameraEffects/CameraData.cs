using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMovementState
{
    None,
    Kinematic,
    Steering
}

public enum TrackingType
{
    None,
    Tight,
    Follow
}

public enum ZoomDirection
{
    None,
	ZoomIn,
	ZoomOut
}

public class CameraData : MonoBehaviour
{
    private Vector3 m_position;
    private Vector3 m_initialPosition;
    private Vector3 m_velocity;
    private Vector3 m_linearSteering;
    private float m_currentSpeed;
    private float m_initialSpeed;
    private float m_orientation;
    private CameraMovementState m_state = CameraMovementState.None;
    private TrackingType m_trackingType = TrackingType.None;
    private Vector3 m_trackingOffset;
    private ZoomDirection m_zoomDirection = ZoomDirection.None;
    private float m_initialZoom;
    private float m_timeForZoom;
    private float m_zoomAmount;
    private float m_currentZoomSpeed;

    public void InitialiseCamera(Vector3 t_position, float t_speed, float t_orientation = 0.0f)
    {
        SetPosition(t_position);
        SetInitialPosition(t_position);
        SetCurrentSpeed(t_speed);
        SetOrientation(t_orientation);
        SetVelocity(new Vector3(0.0f, 0.0f));
        SetLinearSteering(new Vector3(0.0f, 0.0f));
        SetInitialSpeed(t_speed);
    }

    public void InitialiseMovement(CameraMovementState t_state)
    {
        SetCameraState(t_state);
        SetTrackingType(TrackingType.None);
    }

    public void InitialiseTracking(TrackingType t_type, Vector3 t_offset = new Vector3())
    {
        SetTrackingType(t_type);
        SetTrackingOffset(t_offset);
        SetPosition(GetPosition() + t_offset);
        SetInitialPosition(GetPosition() + t_offset);
        SetCameraState(CameraMovementState.None);
    }

    public void InitialiseZoom(ZoomDirection t_direction, float t_zoomSpeed, float t_zoomAmount = 0.0f, float t_timeToZoom = 0.0f, CameraMovementState t_state = CameraMovementState.None)
    {
        SetZoomDirection(t_direction);
        SetInitialZoom(GetPosition().z);
        SetTrackingType(TrackingType.None);
        SetCameraState(CameraMovementState.None);
        SetTimeForZoom(t_timeToZoom);
        SetZoomAmount(t_zoomAmount);
        SetCurrentZoomSpeed(t_zoomSpeed);

        if(t_state != CameraMovementState.None)
		{
            InitialiseMovement(t_state);
		}
    }

    //getters
    public Vector3 GetPosition() { return m_position; }
    public Vector3 GetInitialPosition() { return m_initialPosition; }
    public Vector3 GetVelocity() { return m_velocity; }
    public float GetCurrentSpeed() { return m_currentSpeed; }
    public float GetOrientation() { return m_orientation; }
    public Vector3 GetLinearSteering() { return m_linearSteering; }
    public CameraMovementState GetCameraState() { return m_state; }
    public TrackingType GetTrackingType() { return m_trackingType; }
    public Vector3 GetTrackingOffset() { return m_trackingOffset; }
    public float GetInitialSpeed() { return m_initialSpeed; }
    public ZoomDirection GetZoomDirection() { return m_zoomDirection; }
    public float GetInitialZoom() { return m_initialZoom; }
    public float GetTimeForZoom() { return m_timeForZoom; }
    public float GetZoomAmount() { return m_zoomAmount; }
    public float GetCurrentZoomSpeed() { return m_currentZoomSpeed; }

    //setters
    public void SetPosition(Vector3 t_position) { m_position = t_position; }
    public void SetInitialPosition(Vector3 t_position) { m_initialPosition = t_position; }
    public void SetVelocity(Vector3 t_velocity) { m_velocity = t_velocity; }
    public void SetVelocity2(Vector2 t_velocity) { m_velocity = t_velocity; }
    public void SetCurrentSpeed(float t_currentSpeed) { m_currentSpeed = t_currentSpeed; }
    public void SetOrientation(float t_orientation) { m_orientation = t_orientation; }
    public void SetLinearSteering(Vector3 t_linearSteer) { m_linearSteering = t_linearSteer; }
    public void SetCameraState(CameraMovementState t_state) { m_state = t_state; }
    public void SetTrackingType(TrackingType t_trackingType) { m_trackingType = t_trackingType; }
    public void SetTrackingOffset(Vector3 t_trackingOffset) { m_trackingOffset = t_trackingOffset; }
    public void SetInitialSpeed(float t_initialSpeed) { m_initialSpeed = t_initialSpeed; }
    public void SetZoomDirection(ZoomDirection t_zoomDirection) { m_zoomDirection = t_zoomDirection; }
    public void SetInitialZoom(float t_initialZoom) { m_initialZoom = t_initialZoom; }
    public void SetTimeForZoom(float t_timeForZoom) { m_timeForZoom = t_timeForZoom; }
    public void SetZoomAmount(float t_zoomAmount) { m_zoomAmount = t_zoomAmount; }
    public void SetCurrentZoomSpeed(float t_zoomSpeed) { m_currentZoomSpeed = t_zoomSpeed; }
}
