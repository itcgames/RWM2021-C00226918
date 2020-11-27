using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

///@author Francis Carroll
///@date 27/11/20
public enum TrackingType
{
    Tight,
    Follow
}

public class EntityTracking : MonoBehaviour
{
    private const float MAX_SPEED_RANGE = 0.08f;
    private const float MAX_ACCELERATION = 0.5f;
    private const float MAX_ARRIVE_RADIUS = 0.1f;

    [SerializeField]
    private bool m_scriptActive = false;
    [SerializeField]
    private GameObject m_trackedEntity;

    [SerializeField]
    private TrackingType m_trackingType;

    [Header("'Follow' Tracking")]
    [Range(0, MAX_SPEED_RANGE)]
    [SerializeField]
    private float m_initialFollowSpeed = MAX_SPEED_RANGE / 2.0f;
    [SerializeField]
    private float m_maxSlowRadius = 2.5f;

    private CameraData m_cameraData;


    void Start()
    {
        m_cameraData = GetComponent<CameraData>();
        if (m_scriptActive)
		{
            m_cameraData.SetPosition(transform.position);
            m_cameraData.SetCurrentSpeed(m_initialFollowSpeed);
        }
    }

    void LateUpdate()
    {
        //if the spript is active
        if(m_scriptActive)
		{
            //if the camera is tightly tracking the entity
            if (m_trackingType == TrackingType.Tight)
            {
                m_cameraData.SetPosition(new Vector3(m_trackedEntity.transform.position.x, m_trackedEntity.transform.position.y, m_cameraData.GetPosition().z));
            }
            else if(m_trackingType == TrackingType.Follow)
			{
                Steering();

                Vector3 orientationVector = Globals.Globals.CreateVector(-Mathf.Sin(m_cameraData.GetOrientation()), Mathf.Cos(m_cameraData.GetOrientation()));
                m_cameraData.SetVelocity(m_cameraData.GetCurrentSpeed() * orientationVector);

                m_cameraData.SetPosition(m_cameraData.GetPosition() + m_cameraData.GetVelocity());
                m_cameraData.SetOrientation(Globals.Globals.GetNewOrientation(m_cameraData.GetOrientation(), m_trackedEntity.transform.position - m_cameraData.GetPosition()));

                Arrive();
            }
            transform.position = m_cameraData.GetPosition();
        }
    }

    void Steering()
	{
        m_cameraData.SetLinearSteering(GetSeeringToLocation(m_cameraData.GetPosition(), m_trackedEntity.transform.position));
        m_cameraData.SetVelocity(m_cameraData.GetVelocity() + m_cameraData.GetLinearSteering() * Time.deltaTime);
    }

    void Arrive()
	{
        float l_distance = Globals.Globals.Magnitude(m_trackedEntity.transform.position - m_cameraData.GetPosition());

        //calculate the speed based on the position relative to the target
        if (l_distance < MAX_ARRIVE_RADIUS)
        {
            m_cameraData.SetCurrentSpeed(0.0f);
        }
        //slow down when within slow down radius
        else if (l_distance >= MAX_ARRIVE_RADIUS && l_distance < m_maxSlowRadius)
        {
            m_cameraData.SetCurrentSpeed(m_initialFollowSpeed * (l_distance / (float)(m_maxSlowRadius)));
        }
		else
		{
            m_cameraData.SetCurrentSpeed(m_initialFollowSpeed);

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

    //getters
    public TrackingType GetTrackingState() { return m_trackingType; }
    public GameObject GetTrackedEntity() { return m_trackedEntity;  }
    public bool GetScriptActive() { return m_scriptActive; }

    //setters
    public void SetTrackingState(TrackingType t_trackingState) { m_trackingType = t_trackingState; }
    public void SetTrackedEntity(GameObject t_trackedEntity) { m_trackedEntity = t_trackedEntity; }
    public void SetScriptActive(bool t_active) { m_scriptActive = t_active; }
}
