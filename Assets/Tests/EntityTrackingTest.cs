using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EntityTrackingTest
{
	private CameraS cam;
	private CameraData cameraData;
	private GameObject cameraObject;
	private GameObject player;

	[SetUp]
	public void Setup()
	{
		cameraObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Camera"));
		player = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/TestPlayer"));
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
	public IEnumerator TightEntityTracking()
	{
		cam.m_state = Feature.Track;
		cameraData.InitialiseCamera(cameraObject.transform.position, 10.0f);
		cameraData.InitialiseTracking(TrackingType.Tight);

		cam.m_player = player;
		cam.m_player.transform.position = new Vector3(10.0f, 20.0f, 0.0f);

		yield return new WaitForSeconds(0.1f);

		Assert.AreEqual(cam.m_player.transform.position.x, cameraData.GetPosition().x);
		Assert.AreEqual(cam.m_player.transform.position.y, cameraData.GetPosition().y);
	}

	[UnityTest]
	public IEnumerator LooselyTrackEntity()
	{
		cam.m_state = Feature.Track;
		cameraData.InitialiseCamera(cameraObject.transform.position, 10.0f);
		cameraData.InitialiseTracking(TrackingType.Follow);

		cam.m_player = player;
		cam.m_player.transform.position = new Vector3(10.0f, 20.0f, 0.0f);

		Vector3 initialPos = cameraData.GetPosition();
		yield return new WaitForSeconds(0.1f);

		float startMagnitude = Globals.Magnitude(initialPos - cam.m_player.transform.position);
		float nextMagnitude = Globals.Magnitude(cameraData.GetPosition() - cam.m_player.transform.position);
		Assert.Less(nextMagnitude, startMagnitude);
	}

	[UnityTest]
	public IEnumerator LooselyTrackEntityWOffset()
	{
		cam.m_state = Feature.Track;
		cameraData.InitialiseCamera(cameraObject.transform.position, 10.0f);
		cameraData.InitialiseTracking(TrackingType.Follow, new Vector3(2.0f,0.0f,0.0f));

		cam.m_player = player;
		cam.m_player.transform.position = new Vector3(10.0f, 20.0f, 0.0f);

		Vector3 initialPos = cameraData.GetPosition();

		yield return new WaitForSeconds(0.1f);

		float startMagnitude = Globals.Magnitude(initialPos - cam.m_player.transform.position);
		float nextMagnitude = Globals.Magnitude(cameraData.GetPosition() - cam.m_player.transform.position);

		Assert.Less(nextMagnitude, startMagnitude);
	}

	[UnityTest]
	public IEnumerator TightlyTrackEntityWOffset()
	{
		Vector3 trackOffset = new Vector3(2.0f, 0.0f, 0.0f);
		cam.m_state = Feature.Track;
		cameraData.InitialiseCamera(cameraObject.transform.position, 10.0f);
		cameraData.InitialiseTracking(TrackingType.Tight, trackOffset);

		cam.m_player = player;
		cam.m_player.transform.position = new Vector3(10.0f, 20.0f, 0.0f);

		Vector3 initialPos = cameraData.GetPosition();

		yield return new WaitForSeconds(0.1f);

		Assert.AreEqual(cam.m_player.transform.position.x - trackOffset.x, cameraData.GetPosition().x);
		Assert.AreEqual(cam.m_player.transform.position.y - trackOffset.y, cameraData.GetPosition().y);
	}
}
