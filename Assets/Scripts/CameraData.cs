using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraData : MonoBehaviour
{
    private Vector3 m_position;
    private Vector3 m_velocity;
    private Vector3 m_linearSteering;
    private float m_currentSpeed;
    private float m_orientation;

	//getters
	public Vector3 GetPosition() { return m_position; }
    public Vector3 GetVelocity() { return m_velocity; }
    public float GetCurrentSpeed() { return m_currentSpeed; }
    public float GetOrientation() { return m_orientation; }
    public Vector3 GetLinearSteering() { return m_linearSteering; }

    //setters
    public void SetPosition(Vector3 t_position) { m_position = t_position; }
    public void SetVelocity(Vector3 t_velocity) { m_velocity = t_velocity; }
    public void SetCurrentSpeed(float t_currentSpeed) { m_currentSpeed = t_currentSpeed; }
    public void SetOrientation(float t_orientation) { m_orientation = t_orientation; }
    public void SetLinearSteering(Vector3 t_linearSteer) { m_linearSteering = t_linearSteer; }
}
