using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Feature
{
    Move, 
    Track
}

public class CameraS : MonoBehaviour
{
    private CameraData m_cameraData;

    [SerializeField]
    public GameObject m_player;

    public Feature m_state;
    public Vector3 trackPos;

    void Start()
    {
        m_state = Feature.Track;
        trackPos = new Vector3(14.0f, 10.0f, 0.0f);
        m_cameraData = GetComponent<CameraData>();

        m_cameraData.InitialiseCamera(transform.position, 10.0f);
        if (m_state == Feature.Move)
        {
            m_cameraData.InitialiseMovement(CameraMovementState.Steering);
        }
        else if (m_state == Feature.Track)
        {
            m_cameraData.InitialiseTracking(TrackingType.Follow, new Vector3(2.0f, 0.0f));
        }
	}

    // Update is called once per frame
    void LateUpdate()
    {
        if (m_state == Feature.Move)
        {
            ProgressiveMovement.Move(ref m_cameraData, new Vector3(14.0f, 10.0f, 0.0f));
        }
        else if (m_state == Feature.Track)
		{
            if(m_player != null)
			{
                PositionTracking.Tracking(ref m_cameraData, m_player.transform.position);
            }
        }

        transform.position = m_cameraData.GetPosition();
    }
}
