using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Zoom
{
    private CameraS cam;
    private CameraData cameraData;
    private GameObject cameraObject;

    [SetUp]
    public void Setup()
    {
        cameraObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Camera"));
        cameraData = cameraObject.GetComponent<CameraData>();
        cam = cameraObject.GetComponent<CameraS>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(cameraData.gameObject);
        Object.Destroy(cam.gameObject);
        Object.Destroy(cameraObject);
    }

    [UnityTest]
    public IEnumerator TestZoomIn()
    {
        cam.m_state = Feature.ZoomAmount;
        cameraData.InitialiseCamera(cameraObject.transform.position, 10.0f);
        cameraData.InitialiseZoom(ZoomDirection.ZoomIn);

        float initialZoom = cameraData.GetPosition().z;

        yield return new WaitForSeconds(0.1f);

        float currentZoom = cameraData.GetPosition().z;

        Assert.Less(initialZoom, currentZoom);
    }

    [UnityTest]
    public IEnumerator TestZoomOut()
    {
        cam.m_state = Feature.ZoomAmount;
        cameraData.InitialiseCamera(cameraObject.transform.position, 10.0f);
        cameraData.InitialiseZoom(ZoomDirection.ZoomOut);

        float initialZoom = cameraData.GetPosition().z;

        yield return new WaitForSeconds(0.1f);

        float currentZoom = cameraData.GetPosition().z;

        Assert.Greater(initialZoom, currentZoom);
    }
}