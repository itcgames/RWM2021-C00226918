using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zooming
{
    public static void ZoomInOutAmount(ref CameraData t_data, float t_zoomAmount)
	{
		CapZoom(ref t_data);

		if (t_data.GetZoomDirection() == ZoomDirection.ZoomIn)
		{
			if (t_data.GetPosition().z <= t_data.GetInitialZoom() + t_zoomAmount)
			{
				t_data.SetVelocity(new Vector3(t_data.GetVelocity().x, t_data.GetVelocity().y, t_data.GetCurrentSpeed() * Globals.VELOCITY_SLOW));
			}
		}
		else if (t_data.GetZoomDirection() == ZoomDirection.ZoomOut)
		{
			if (t_data.GetPosition().z >= t_data.GetInitialZoom() - t_zoomAmount)
			{
				t_data.SetVelocity(new Vector3(t_data.GetVelocity().x, t_data.GetVelocity().y, -t_data.GetCurrentSpeed() * Globals.VELOCITY_SLOW));
			}
		}

		ZoomMovement(ref t_data);
	}

	private static void ZoomMovement(ref CameraData t_data)
	{
		t_data.SetPosition(t_data.GetPosition() + (t_data.GetVelocity() * Time.deltaTime));
	}

	private static void CapZoom(ref CameraData t_data)
	{
		if(t_data.GetPosition().z > Globals.MAX_ZOOM)
		{
			t_data.SetPosition(new Vector3(t_data.GetPosition().x, t_data.GetPosition().y, Globals.MAX_ZOOM));
		}
		else if (t_data.GetPosition().z < -Globals.MAX_ZOOM)
		{
			t_data.SetPosition(new Vector3(t_data.GetPosition().x, t_data.GetPosition().y, -Globals.MAX_ZOOM));
		}
	}
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public enum ZoomDirection
//{
//    ZoomIn,
//    ZoomOut
//}

//public class Zoom : MonoBehaviour
//{
//    const int MAX_ZOOM = 20;

//    [SerializeField]
//    private bool m_scriptActive = false;
//    [SerializeField]
//    [Range(0, MAX_ZOOM)]
//    private float m_zoomAmount;
//    [SerializeField]
//    private float m_initialSpeed;
//    [SerializeField]
//    private ZoomDirection m_zoomDirection = ZoomDirection.ZoomIn;

//    private CameraData m_cameraData;
//    private float m_initialZoom;

//    void Start()
//    {
//        Initialise();
//    }

//    public void Initialise()
//    {
//        m_cameraData = GetComponent<CameraData>();
//        if (m_scriptActive)
//        {
//            m_cameraData.SetPosition(transform.position);
//            m_cameraData.SetCurrentZoomSpeed(m_initialSpeed);
//            m_initialZoom = m_cameraData.GetPosition().z;
//        }
//    }

//    void LateUpdate()
//    {
//        if (m_scriptActive)
//        {
//            ZoomInOutAmount();
//        }
//    }

//    void ZoomInOutAmount()
//    {
//        if (m_zoomDirection == ZoomDirection.ZoomIn)
//        {
//            if (m_cameraData.GetPosition().z <= m_initialZoom + m_zoomAmount)
//            {
//                m_cameraData.SetVelocity(new Vector3(m_cameraData.GetVelocity().x, m_cameraData.GetVelocity().y, m_cameraData.GetCurrentZoomSpeed()));
//                ZoomMovement();
//            }
//        }
//        else if (m_zoomDirection == ZoomDirection.ZoomOut)
//        {
//            if (m_cameraData.GetPosition().z >= m_initialZoom - m_zoomAmount)
//            {
//                m_cameraData.SetVelocity(new Vector3(m_cameraData.GetVelocity().x, m_cameraData.GetVelocity().y, -m_cameraData.GetCurrentZoomSpeed()));
//                ZoomMovement();
//            }
//        }
//    }

//    void ZoomMovement()
//    {
//        m_cameraData.SetPosition(m_cameraData.GetPosition() + m_cameraData.GetVelocity());

//        transform.position = m_cameraData.GetPosition();
//    }

//    //setters
//    public void SetScriptActive(bool t_scriptActive) { m_scriptActive = t_scriptActive; }
//    public void SetZoomDirection(ZoomDirection t_zoomDirection) { m_zoomDirection = t_zoomDirection; }
//    public void SetZoomAmount(float t_zoomAmount) { m_zoomAmount = t_zoomAmount; }

//    //getters
//    public bool GetScriptActive() { return m_scriptActive; }
//    public ZoomDirection GetZoomDirection() { return m_zoomDirection; }
//    public float GetZoomAmount() { return m_zoomAmount; }
//}