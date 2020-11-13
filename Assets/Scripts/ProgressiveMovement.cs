using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class ProgressiveMovement : MonoBehaviour
{
    const float MAX_SPEED_RANGE = 10.0f;
    const float MAX_ACCELERATION = 0.5f;

    [Range(1, MAX_SPEED_RANGE)]
    [SerializeField]
    private int MAX_SPEED;
    [Range(0, MAX_SPEED_RANGE)]
    [SerializeField]
    private float m_initialSpeed;
    [SerializeField]
    private Vector3 m_targetPosition;

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
    private bool m_arrived = false;

    void Start()
    {
        m_position = transform.position;
        m_currentSpeed = m_initialSpeed / 100.0f;
        m_orientation = transform.rotation.z;
        m_arrived = false;
    }

	private void LateUpdate()
	{
        Movement();
	}

    /// <summary>
    /// Simple chack that stops wmoving the camera when its within range of traget location
    /// </summary>
    private void CheckPosition()
	{
        if(Globals.Globals.Magnitude(m_position - m_targetPosition) < 0.1f)
		{
            m_arrived = true;
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
        if(Globals.Globals.Magnitude(m_velocity) > MAX_SPEED)
		{
            m_velocity = Globals.Globals.Normalise(m_velocity);
            m_velocity = m_velocity * MAX_SPEED;
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
        CheckPosition();
    }

    /// <summary>
    /// Setters
    /// </summary>
    public void SetTargetPosition(Vector3 t_position) { m_targetPosition = t_position; }
    public void SetInitialSpeed(float t_initialSpeed) { m_initialSpeed = t_initialSpeed; }
    public void SetCurrentSpeed(float t_speed) { m_currentSpeed = t_speed; }
    public void SetCameraState(CameraMovementState t_state) { m_state = t_state; }
    public void SetArrived(bool t_arrive) { m_arrived = t_arrive; }

    /// <summary>
    /// Getters
    /// </summary>
    public CameraMovementState GetCameraState() { return m_state; }
    public Vector3 GetTargetPosition() { return m_targetPosition; }
    public Vector3 GetCameraPosition() { return m_position; }
}
