using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///@author Francis Carroll
///@date 27/11/20

public class PositionTracking
{
    private static float MAX_ARRIVE_RADIUS = 0.01f;

    public static void Tracking(ref CameraData t_data, Vector3 t_target)
	{
        if (t_data.GetTrackingType() != TrackingType.None)
        {
            //if the camera is tightly tracking the entity
            if (t_data.GetTrackingType() == TrackingType.Tight)
            {
                t_data.SetPosition(new Vector3(t_target.x, t_target.y, t_data.GetPosition().z) - t_data.GetTrackingOffset());
            }
            else if (t_data.GetTrackingType() == TrackingType.Follow)
            {
                Steering(ref t_data, t_target);

                Vector3 orientationVector = new Vector3(0.0f, 0.0f, 0.0f);
                if (t_data.GetOrientation() != 0.0f)
                {
                    //calculates the orientaion as a vector
                    orientationVector = Globals.CreateVector(-Mathf.Sin(t_data.GetOrientation()), Mathf.Cos(t_data.GetOrientation()));
                }
                t_data.SetVelocity(t_data.GetCurrentSpeed() * (orientationVector * Time.deltaTime));

                t_data.SetPosition(t_data.GetPosition() + t_data.GetVelocity());
                t_data.SetOrientation(Globals.GetNewOrientation(t_data.GetOrientation(), t_target - (t_data.GetPosition() - t_data.GetTrackingOffset())));

                SlowOnArrival(ref t_data, t_target);
            }
        }
    }

    private static void Steering(ref CameraData t_data, Vector3 t_target)
	{
        t_data.SetLinearSteering(Globals.GetSeeringToLocation(t_data.GetPosition() + t_data.GetTrackingOffset(), t_target));
        t_data.SetVelocity(t_data.GetVelocity() + t_data.GetLinearSteering() * Time.deltaTime);

        //if the velocity is greater than the max speed, normalise and cap at max speed.
        if (Globals.Magnitude(t_data.GetVelocity()) > Globals.MAX_SPEED_RANGE)
        {
            t_data.SetVelocity(Globals.Normalise(t_data.GetVelocity()));
            t_data.SetVelocity(t_data.GetVelocity() * Globals.MAX_SPEED_RANGE);
        }
    }

    private static void SlowOnArrival(ref CameraData t_data, Vector3 t_target)
	{
        float l_distance = Globals.Magnitude(t_target - (t_data.GetPosition() - t_data.GetTrackingOffset()));

        //calculate the speed based on the position relative to the target
        if (l_distance < MAX_ARRIVE_RADIUS)
        {
            t_data.SetCurrentSpeed(0.0f);
        }
        //slow down when within slow down radius
        else if (l_distance >= MAX_ARRIVE_RADIUS && l_distance < Globals.MAX_SLOW_RADIUS)
        {
            t_data.SetCurrentSpeed(t_data.GetInitialSpeed() * (l_distance / (float)(Globals.MAX_SLOW_RADIUS)));
        }
        else
        {
            t_data.SetCurrentSpeed(t_data.GetInitialSpeed());

        }
    }
}

//public class EntityTracking : MonoBehaviour
//{
//    private const float MAX_SPEED_RANGE = 0.08f;
//    private const float MAX_ACCELERATION = 0.5f;
//    private const float MAX_ARRIVE_RADIUS = 0.1f;

//    [SerializeField]
//    private bool m_scriptActive = false;
//    [SerializeField]
//    private GameObject m_trackedEntity;

//    [SerializeField]
//    private TrackingType m_trackingType;

//    [SerializeField]
//    private Vector3 m_trackingOffset;

//    [Header("'Follow' Tracking")]
//    [Range(0, MAX_SPEED_RANGE)]
//    [SerializeField]
//    private float m_initialFollowSpeed = MAX_SPEED_RANGE / 2.0f;
//    [SerializeField]
//    private float m_maxSlowRadius = 2.5f;

//    private CameraData m_cameraData;



//    void LateUpdate()
//    {
//        Tracking();
//    }

//    public void Tracking()
//	{
//        //if the spript is active
//        if (m_scriptActive)
//        {
//            //if the camera is tightly tracking the entity
//            if (m_trackingType == TrackingType.Tight)
//            {
//                m_cameraData.SetPosition(new Vector3(m_trackedEntity.transform.position.x, m_trackedEntity.transform.position.y, m_cameraData.GetPosition().z) - m_trackingOffset);
//            }
//            else if (m_trackingType == TrackingType.Follow)
//            {
//                Steering();

//                Vector3 orientationVector = Globals.Globals.CreateVector(-Mathf.Sin(m_cameraData.GetOrientation()), Mathf.Cos(m_cameraData.GetOrientation()));
//                m_cameraData.SetVelocity(m_cameraData.GetCurrentSpeed() * orientationVector);

//                m_cameraData.SetPosition(m_cameraData.GetPosition() + m_cameraData.GetVelocity());
//                m_cameraData.SetOrientation(Globals.Globals.GetNewOrientation(m_cameraData.GetOrientation(), m_trackedEntity.transform.position - (m_cameraData.GetPosition() - m_trackingOffset)));

//                Arrive();
//            }
//            transform.position = m_cameraData.GetPosition();
//        }
//    }

//    void Steering()
//	{
//        m_cameraData.SetLinearSteering(GetSeeringToLocation(m_cameraData.GetPosition() + m_trackingOffset, m_trackedEntity.transform.position));
//        m_cameraData.SetVelocity(m_cameraData.GetVelocity() + m_cameraData.GetLinearSteering() * Time.deltaTime);
//    }

//    void Arrive()
//	{
//        float l_distance = Globals.Globals.Magnitude(m_trackedEntity.transform.position - (m_cameraData.GetPosition() - m_trackingOffset));

//        //calculate the speed based on the position relative to the target
//        if (l_distance < MAX_ARRIVE_RADIUS)
//        {
//            m_cameraData.SetCurrentSpeed(0.0f);
//        }
//        //slow down when within slow down radius
//        else if (l_distance >= MAX_ARRIVE_RADIUS && l_distance < m_maxSlowRadius)
//        {
//            m_cameraData.SetCurrentSpeed(m_initialFollowSpeed * (l_distance / (float)(m_maxSlowRadius)));
//        }
//		else
//		{
//            m_cameraData.SetCurrentSpeed(m_initialFollowSpeed);

//        }
            
//    }

//    /// <summary>
//    /// Gets the linear steering vector to the target loaction
//    /// </summary>
//    /// <param name="t_vector1"></param>
//    /// <param name="t_vector2"></param>
//    /// <returns></returns>
//    private Vector3 GetSeeringToLocation(Vector3 t_vector1, Vector3 t_vector2)
//    {
//        Vector3 linearSteering = t_vector1 - t_vector2;
//        linearSteering = Globals.Globals.Normalise(linearSteering);
//        linearSteering *= MAX_ACCELERATION;
//        return linearSteering;
//    }

//    //getters
//    public TrackingType GetTrackingState() { return m_trackingType; }
//    public GameObject GetTrackedEntity() { return m_trackedEntity;  }
//    public bool GetScriptActive() { return m_scriptActive; }
//    public Vector3 GetTrackingOffset() { return m_trackingOffset; }

//    //setters
//    public void SetTrackingState(TrackingType t_trackingState) { m_trackingType = t_trackingState; }
//    public void SetTrackedEntity(GameObject t_trackedEntity) { m_trackedEntity = t_trackedEntity; }
//    public void SetScriptActive(bool t_active) { m_scriptActive = t_active; }
//    public void SetTrackingOffset(Vector3 t_trackingOffset) { m_trackingOffset = t_trackingOffset; }
//}
