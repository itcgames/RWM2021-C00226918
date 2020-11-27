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
        cameraObject.GetComponent<ProgressiveMovement>().SetScriptActive(false);
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
        entityTracking.SetTrackedEntity(Player);
        entityTracking.SetScriptActive(true);
        entityTracking.SetTrackingState(TrackingType.Tight);
        Player.transform.position = new Vector3(10.0f,20.0f,0.0f);
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(Player.transform.position.x, cameraData.GetPosition().x);
        Assert.AreEqual(Player.transform.position.y, cameraData.GetPosition().y);
    }

	[UnityTest]
	public IEnumerator LooselyTrackEntity()
	{
		entityTracking.SetScriptActive(true);
		entityTracking.SetTrackingState(TrackingType.Follow);
		entityTracking.SetTrackedEntity(Player);
		cameraData.SetCurrentSpeed(0.04f);
		Player.transform.position = new Vector3(10.0f, 20.0f, 0.0f);
		Vector3 initialPos = cameraData.GetPosition();
		yield return new WaitForSeconds(0.1f);
		float startMagnitude = Globals.Globals.Magnitude(initialPos - Player.transform.position);
		float nextMagnitude = Globals.Globals.Magnitude(cameraData.GetPosition() - Player.transform.position);
		Assert.Less(nextMagnitude, startMagnitude);
	}
}
