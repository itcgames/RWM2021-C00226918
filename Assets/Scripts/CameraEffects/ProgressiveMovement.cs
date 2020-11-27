using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

///@author Francis Carroll
///@date 09/11/20

public enum CameraMovementState
{
    Kinematic,
    Steering
}

public class ProgressiveMovement : MonoBehaviour
{
    private const float MAX_SPEED_RANGE = 0.08f;
    private const float MAX_ACCELERATION = 0.5f;
    private const float MAX_ARRIVE_RADIUS = 0.1f;
    [SerializeField]
    private bool m_scriptActive = false;
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

    [SerializeField]
    private CameraMovementState m_state;

    private bool m_arrived;

    private CameraData m_cameraData;

    void Start()
    {
        m_cameraData = GetComponent<CameraData>();
        if (m_scriptActive)
		{
            Initialisation();
		}
    }

    void Initialisation()
	{
        m_cameraData.SetPosition(transform.position);
        m_cameraData.SetCurrentSpeed(m_initialSpeed);
        m_cameraData.SetOrientation(transform.rotation.z);

        if (m_cameraData.GetPosition() == m_targetPosition)
        {
            m_arrived = true;
        }
        else
        {
            m_arrived = false;
        }
    }

	private void LateUpdate()
	{
        if(m_scriptActive)
		{
            Movement();
        }
	}

    private void CheckPosition()
    {
		if (Globals.Globals.Magnitude(m_targetPosition - m_cameraData.GetPosition()) < MAX_ARRIVE_RADIUS)
		{
			m_arrived = true;
		}
    }

    private void ArriveAtLocation()
	{
        float l_distance = Globals.Globals.Magnitude(m_targetPosition - m_cameraData.GetPosition());

        //calculate the speed based on the position relative to the target
        if (l_distance < MAX_ARRIVE_RADIUS)
        {
            m_arrived = true;
            m_cameraData.SetCurrentSpeed(0.0f);
        }
        //slow down when within slow down radius
        else if(l_distance >= MAX_ARRIVE_RADIUS && l_distance < m_maxSlowRadius)
        {
            m_cameraData.SetCurrentSpeed(MAX_SPEED_RANGE * (l_distance / (float)(m_maxSlowRadius)));
        }
    }

    /// <summary>
    /// Cap the current speed to the maximum speed
    /// </summary>
    private void CapSpeed()
	{
        if(m_cameraData.GetCurrentSpeed() > m_maxSpeed)
		{
            m_cameraData.SetCurrentSpeed(m_maxSpeed);
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
        m_cameraData.SetLinearSteering(GetSeeringToLocation(m_cameraData.GetPosition(), m_targetPosition));
        m_cameraData.SetVelocity(m_cameraData.GetVelocity() + m_cameraData.GetLinearSteering() * Time.deltaTime);

        //if the velocity is greater than the max speed, normalise and cap at max speed.
        if(Globals.Globals.Magnitude(m_cameraData.GetVelocity()) > m_maxSpeed)
		{
            m_cameraData.SetVelocity(Globals.Globals.Normalise(m_cameraData.GetVelocity()));
            m_cameraData.SetVelocity(m_cameraData.GetVelocity() * m_maxSpeed);
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
            Vector3 orientationVector = Globals.Globals.CreateVector(-Mathf.Sin(m_cameraData.GetOrientation()), Mathf.Cos(m_cameraData.GetOrientation()));
            m_cameraData.SetVelocity(m_cameraData.GetCurrentSpeed() * orientationVector);

            m_cameraData.SetPosition(m_cameraData.GetPosition() + m_cameraData.GetVelocity());
            m_cameraData.SetOrientation(Globals.Globals.GetNewOrientation(m_cameraData.GetOrientation(), m_targetPosition - m_cameraData.GetPosition()));

            transform.position = m_cameraData.GetPosition();
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

    //setters
    public void SetTargetPosition(Vector3 t_position) { m_targetPosition = t_position; }
    public void SetCameraState(CameraMovementState t_state) { m_state = t_state; }
    public void SetArrived(bool t_arrive) { m_arrived = t_arrive; }
    public void SetMaxSpeed(float t_speed) { m_maxSpeed = t_speed; }
    public void SetSpriptActive(bool t_active) { m_scriptActive = t_active; }

    //getters
    public CameraMovementState GetCameraState() { return m_state; }
    public Vector3 GetTargetPosition() { return m_targetPosition; }
    public float GetMaxSpeed() { return m_maxSpeed; }
    public bool GetArrived() { return m_arrived; }
    public bool GetScriptActive() { return m_scriptActive; }
}
