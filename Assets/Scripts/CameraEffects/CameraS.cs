using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Feature
{
    Move, 
    Track,
    ZoomAmount,
    ZoomTime
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
        m_state = Feature.ZoomTime;
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
        else if (m_state == Feature.ZoomAmount || m_state == Feature.ZoomTime)
        {
            m_cameraData.InitialiseZoom(ZoomDirection.ZoomOut, 5.0f,10.0f);
        }
    }

	// Update is called once per frame
	void LateUpdate()
    {
        switch(m_state)
		{
            case Feature.Move:
                ProgressiveMovement.Move(ref m_cameraData, new Vector3(14.0f, 10.0f, 0.0f));
                break;
            case Feature.Track:
            {
                if (m_player != null)
                {
                    PositionTracking.Tracking(ref m_cameraData, m_player.transform.position);
                }
                break;
            }
            case Feature.ZoomAmount:
                Zooming.ZoomInOutAmount(ref m_cameraData);
                break;
            case Feature.ZoomTime:
                Zooming.ZoomInOutTime(ref m_cameraData);
                break;
            default:
                break;
        }
        transform.position = m_cameraData.GetPosition();
    }
}
