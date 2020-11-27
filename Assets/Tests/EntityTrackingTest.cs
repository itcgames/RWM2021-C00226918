using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EntityTrackingTest
{
    private EntityTracking entityTracking;
    private CameraData cameraData;
    private GameObject Player;

    [SetUp]
    public void Setup()
    {
        GameObject cameraObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Camera"));
        entityTracking = cameraObject.GetComponent<EntityTracking>();
        cameraData = cameraObject.GetComponent<CameraData>();
        Player = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/TestPlayer"));
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(entityTracking.gameObject);
        Object.Destroy(cameraData.gameObject);
    }

    [UnityTest]
    public IEnumerator TightEntityTracking()
	{
        entityTracking.SetScriptActive(true);
        entityTracking.SetTrackingState(TrackingType.Tight);
        entityTracking.SetTrackedEntity(Player);
        Player.transform.position = new Vector3(10.0f,20.0f,0.0f);
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(Player.transform.position, cameraData.GetPosition());
    }
}
