using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class ProgressiveMovement : MonoBehaviour
{
    const float MAX_SPEED_RANGE = 10.0f;
    const float MAX_ACCELERATION = 0.5f;

    [Range(1, MAX_SPEED_RANGE)]
    public int MAX_SPEED;
    [Range(0, MAX_SPEED_RANGE)]
    public float initialSpeed;
    public Vector3 target;

    public enum CameraMovementState
    {
        Kinematic,
        Steering
    } public CameraMovementState m_state;

    private Vector3 m_position;
    private Vector3 m_velocity;
    private Vector3 m_linearSteering;
    private float m_currentSpeed;
    private float m_orientation;
    private bool m_arrived;

    void Start()
    {
        m_position = transform.position;
        m_currentSpeed = initialSpeed / 100.0f;
        m_orientation = transform.rotation.z;
        m_arrived = false;
    }


    void Update()
    {
		if (!m_arrived)
		{
            m_position += m_velocity;
            m_orientation = Globals.Globals.GetNewOrientation(m_orientation, target - m_position);

            transform.position = m_position;
        }
    }

	private void LateUpdate()
	{
        CheckPosition();
        if (!m_arrived)
		{
            if (m_state == CameraMovementState.Steering)
            {
                SteeringMovement();
            }
            Vector3 orientationVector = Globals.Globals.CreateVector(-Mathf.Sin(m_orientation), Mathf.Cos(m_orientation));
            m_velocity = m_currentSpeed * orientationVector;
        }
	}

    private void CheckPosition()
	{
        if(Globals.Globals.Magnitude(m_position - target) < 0.1f)
		{
            m_arrived = true;
		}
	}

    private Vector3 GetSeeringToLocation(Vector3 t_vector1, Vector3 t_vector2)
	{
        Vector3 linearSteering = t_vector1 - t_vector2;
        linearSteering = Globals.Globals.Normalise(linearSteering);
        linearSteering *= MAX_ACCELERATION;
        return linearSteering;
	}

    private void SteeringMovement()
	{
        m_linearSteering = GetSeeringToLocation(m_position, target);
        m_velocity = m_velocity + m_linearSteering * Time.deltaTime;
        m_velocity.z = 0;


        if(Globals.Globals.Magnitude(m_velocity) > MAX_SPEED)
		{
            m_velocity = Globals.Globals.Normalise(m_velocity);
            m_velocity = m_velocity * MAX_SPEED;
		}
    }
}
