using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class ProgressiveMovement : MonoBehaviour
{
    const float MAX_SPEED_RANGE = 0.08f;
    const float MAX_ACCELERATION = 0.5f;
    const float MAX_ARRIVE_RADIUS = 0.1f;

    [Range(0, MAX_SPEED_RANGE)]
    [SerializeField]
    private float m_maxSpeed = MAX_SPEED_RANGE;
    [Range(0, MAX_SPEED_RANGE)]
    [SerializeField]
    private float m_initialSpeed = MAX_SPEED_RANGE / 2.0f;
    [SerializeField]
    private float m_maxSlowRadius = 2.5f;
    [SerializeField]
    private Vector3 m_targetPosition;
    [SerializeField]
    private bool m_slowOnArrive = true;

    public enum CameraMovementState
    {
        Kinematic,
        Steering
    }
    [SerializeField]
    private CameraMovementState m_state;

    private Vector3 m_position;
    private Vector3 m_velocity;
    private Vector3 m_linearSteering;
    private float m_currentSpeed;
    private float m_orientation;
    private bool m_arrived;

    void Start()
    {
        m_position = transform.position;
        m_currentSpeed = m_initialSpeed;
        m_orientation = transform.rotation.z;
        m_arrived = false;
    }

	private void LateUpdate()
	{
        Movement();
	}

    private void CheckPosition()
	{
        if (Globals.Globals.Magnitude(m_position - m_targetPosition) < MAX_ARRIVE_RADIUS)
        {
            m_arrived = true;
        }
    }

    private void ArriveAtLocation()
	{
        float l_distance = Globals.Globals.Magnitude(m_targetPosition - m_position);

        //calculate the speed based on the position relative to the target
        if (l_distance < MAX_ARRIVE_RADIUS)
        {
            m_arrived = true;
            m_currentSpeed = 0.0f;
        }
        //slow down when within slow down radius
        else if(l_distance >= MAX_ARRIVE_RADIUS && l_distance < m_maxSlowRadius)
        {
            m_currentSpeed = MAX_SPEED_RANGE * (l_distance / (float)(m_maxSlowRadius));
        }
    }

    /// <summary>
    /// Cap the current speed to the maximum speed
    /// </summary>
    private void CapSpeed()
	{
        if(m_currentSpeed > m_maxSpeed)
		{
            m_currentSpeed = m_maxSpeed;
		}
	}

    /// <summary>
    /// Gets the linear steering vector to the target loaction
    /// </summary>
    /// <param name="t_vector1"></param>
    /// <param name="t_vector2"></param>
    /// <returns></returns>
    private Vector3 GetSeeringToLocation(Vector3 t_vector1, Vector3 t_vector2)
	{
        Vector3 linearSteering = t_vector1 - t_vector2;
        linearSteering = Globals.Globals.Normalise(linearSteering);
        linearSteering *= MAX_ACCELERATION;
        return linearSteering;
	}

    /// <summary>
    /// Moves the camera using linear steering
    /// </summary>
    private void SteeringMovement()
	{
        m_linearSteering = GetSeeringToLocation(m_position, m_targetPosition);
        m_velocity = m_velocity + m_linearSteering * Time.deltaTime;
        m_velocity.z = 0;

        //if the velocity is greater than the max speed, normalise and cap at max speed.
        if(Globals.Globals.Magnitude(m_velocity) > m_maxSpeed)
		{
            m_velocity = Globals.Globals.Normalise(m_velocity);
            m_velocity = m_velocity * m_maxSpeed;
		}
    }

    /// <summary>
    /// Movement of the camera using kinematic and steering
    /// </summary>
    public void Movement()
	{
        //if the camera has not arrived at the target location
        if (!m_arrived)
        {
            //if the current camera's state is set to steering
            if (m_state == CameraMovementState.Steering)
            {
                SteeringMovement();
            }
            //calculates the orientaion as a vector
            Vector3 orientationVector = Globals.Globals.CreateVector(-Mathf.Sin(m_orientation), Mathf.Cos(m_orientation));
            m_velocity = m_currentSpeed * orientationVector;

            m_position += m_velocity;
            m_orientation = Globals.Globals.GetNewOrientation(m_orientation, m_targetPosition - m_position);

            transform.position = m_position;
        }
		if (m_slowOnArrive)
		{
            ArriveAtLocation();
		}
		else
		{
            CheckPosition();
		}
        CapSpeed();
    }

    /// <summary>
    /// Setters
    /// </summary>
    public void SetTargetPosition(Vector3 t_position) { m_targetPosition = t_position; }
    public void SetInitialSpeed(float t_initialSpeed) { m_initialSpeed = t_initialSpeed; }
    public void SetCurrentSpeed(float t_speed) { m_currentSpeed = t_speed; }
    public void SetCameraState(CameraMovementState t_state) { m_state = t_state; }
    public void SetArrived(bool t_arrive) { m_arrived = t_arrive; }
    public void SetMaxSpeed(float t_speed) { m_maxSpeed = t_speed; }

    /// <summary>
    /// Getters
    /// </summary>
    public CameraMovementState GetCameraState() { return m_state; }
    public Vector3 GetTargetPosition() { return m_targetPosition; }
    public Vector3 GetCameraPosition() { return m_position; }
    public float GetCurrentSpeed() { return m_currentSpeed; }
    public float GetMaxSpeed() { return m_maxSpeed; }
}
