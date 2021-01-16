using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections.Specialized;

public class ProgressiveCameraMovement
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
		cam.m_player = null;
	}

	[TearDown]
	public void Teardown()
	{
		Object.Destroy(cameraData.gameObject);
		Object.Destroy(cam.gameObject);
		Object.Destroy(cameraObject);
	}

	[UnityTest]
	public IEnumerator CheckCameraMovedTowardsTargetKinematic()
	{
		cam.m_state = Feature.Move;
		cameraData.InitialiseCamera(cameraObject.transform.position, 10.0f);
		cameraData.InitialiseMovement(CameraMovementState.Kinematic);

		Vector3 targetPos = cam.trackPos;
		Vector3 initialPos = cameraData.GetPosition();

		yield return new WaitForSeconds(0.1f);

		Vector3 currentPos = cameraData.GetPosition();
		float startMagnitude = Globals.Magnitude(initialPos - targetPos);
		float nextMagnitude = Globals.Magnitude(currentPos - targetPos);

		Assert.Less(nextMagnitude, startMagnitude);
	}

	[UnityTest]
	public IEnumerator CheckCameraMovedTowardsTargetSteering()
	{
		cam.m_state = Feature.Move;
		cameraData.InitialiseCamera(cameraObject.transform.position, 10.0f);
		cameraData.InitialiseMovement(CameraMovementState.Steering);

		Vector3 targetPos = cam.trackPos;
		Vector3 initialPos = cameraData.GetPosition();

		yield return new WaitForSeconds(0.1f);

		Vector3 currentPos = cameraData.GetPosition();
		float startMagnitude = Globals.Magnitude(initialPos - targetPos);
		float nextMagnitude = Globals.Magnitude(currentPos - targetPos);

		Assert.Less(nextMagnitude, startMagnitude);
	}

	[UnityTest]
	public IEnumerator CheckSpeedCap()
	{
		cam.m_state = Feature.Move;
		cameraData.InitialiseCamera(cameraObject.transform.position, 10.0f);
		cameraData.InitialiseMovement(CameraMovementState.Steering);

		Globals.MAX_SPEED = 0.08f;

		float MAX_SPEED = Globals.MAX_SPEED;
		float initialSpeed = cameraData.GetCurrentSpeed();

		yield return new WaitForSeconds(0.01f);

		float currentSpeed = cameraData.GetCurrentSpeed();
		Assert.AreEqual(MAX_SPEED, currentSpeed);
	}
}