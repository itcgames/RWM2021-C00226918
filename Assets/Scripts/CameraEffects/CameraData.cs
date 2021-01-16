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

public class CameraData : MonoBehaviour
{
    private Vector3 m_position;
    private Vector3 m_velocity;
    private Vector3 m_linearSteering;
    private float m_currentSpeed;
    private float m_initialSpeed;
    private float m_orientation;
    private CameraMovementState m_state = CameraMovementState.None;
    private TrackingType m_trackingType = TrackingType.None;
    private Vector3 m_trackingOffset;

    public void InitialiseCamera(Vector3 t_position, float t_speed, float t_orientation = 0.0f)
    {
        SetPosition(t_position);
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
        SetCameraState(CameraMovementState.None);
    }

    //getters
    public Vector3 GetPosition() { return m_position; }
    public Vector3 GetVelocity() { return m_velocity; }
    public float GetCurrentSpeed() { return m_currentSpeed; }
    public float GetOrientation() { return m_orientation; }
    public Vector3 GetLinearSteering() { return m_linearSteering; }
    public CameraMovementState GetCameraState() { return m_state; }
    public TrackingType GetTrackingType() { return m_trackingType; }
    public Vector3 GetTrackingOffset() { return m_trackingOffset; }
    public float GetInitialSpeed() { return m_initialSpeed; }

    //setters
    public void SetPosition(Vector3 t_position) { m_position = t_position; }
    public void SetVelocity(Vector3 t_velocity) { m_velocity = t_velocity; }
    public void SetCurrentSpeed(float t_currentSpeed) { m_currentSpeed = t_currentSpeed; }
    public void SetOrientation(float t_orientation) { m_orientation = t_orientation; }
    public void SetLinearSteering(Vector3 t_linearSteer) { m_linearSteering = t_linearSteer; }
    public void SetCameraState(CameraMovementState t_state) { m_state = t_state; }
    public void SetTrackingType(TrackingType t_trackingType) { m_trackingType = t_trackingType; }
    public void SetTrackingOffset(Vector3 t_trackingOffset) { m_trackingOffset = t_trackingOffset; }
    public void SetInitialSpeed(float t_initialSpeed) { m_initialSpeed = t_initialSpeed; }
}
