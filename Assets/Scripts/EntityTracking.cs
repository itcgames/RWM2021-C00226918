using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTracking : MonoBehaviour
{
    [SerializeField]
    private bool m_scriptActive = false;
    [SerializeField]
    private GameObject m_trackedEntity;
    public bool m_trackTightly = true;

    private CameraData m_cameraData;


    void Start()
    {
        if(m_scriptActive)
		{
            m_cameraData = GetComponent<CameraData>();
            m_cameraData.SetPosition(transform.position);
        }
    }

    void LateUpdate()
    {
        //if the spript is active
        if(m_scriptActive)
		{
            //if the camera is tightly tracking the entity
            if (m_trackTightly)
            {
                m_cameraData.SetPosition(new Vector3(m_trackedEntity.transform.position.x, m_trackedEntity.transform.position.y, m_cameraData.GetPosition().z));
            }
            transform.position = m_cameraData.GetPosition();
        }
    }

    //getters
    public bool GetTrackedTightly() { return m_trackTightly; }
    public GameObject GetTrackedEntity() { return m_trackedEntity;  }
    public bool GetScriptActive() { return m_scriptActive; }

    //setters
    public void SetTrackedTightly(bool t_trackedTightly) { m_trackTightly = t_trackedTightly; }
    public void SetTrackedEntity(GameObject t_trackedEntity) { m_trackedEntity = t_trackedEntity; }
    public void SetScriptActive(bool t_active) { m_scriptActive = t_active; }
}
