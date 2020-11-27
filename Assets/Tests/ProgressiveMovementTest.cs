using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections.Specialized;
using Globals;

public class ProgressiveCameraMovement
{
    private ProgressiveMovement progressiveMovement;
    private CameraData cameraData;

    [SetUp]
    public void Setup()
    {
        GameObject cameraObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Camera"));
        progressiveMovement = cameraObject.GetComponent<ProgressiveMovement>();
        cameraData = cameraObject.GetComponent<CameraData>();
        cameraObject.GetComponent<EntityTracking>().SetScriptActive(false);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(progressiveMovement.gameObject);
        Object.Destroy(cameraData.gameObject);
    }

    [UnityTest]
    public IEnumerator CheckCameraMovedTowardsTargetKinematic()
    {
        progressiveMovement.SetCameraState(CameraMovementState.Kinematic);
        progressiveMovement.SetScriptActive(true);
        progressiveMovement.SetTargetPosition(new Vector3(14.0f, 10.0f, 0.0f));
        progressiveMovement.SetArrived(false);
        cameraData.SetCurrentSpeed(2.0f);
        Vector3 targetPos = progressiveMovement.GetTargetPosition();
        Vector3 initialPos = cameraData.GetPosition();
        yield return new WaitForSeconds(0.1f);
        Vector3 currentPos = cameraData.GetPosition();
        float startMagnitude = Globals.Globals.Magnitude(initialPos - targetPos);
        float nextMagnitude = Globals.Globals.Magnitude(currentPos - targetPos);
        Assert.Less(nextMagnitude, startMagnitude);
    }

    [UnityTest]
    public IEnumerator CheckCameraMovedTowardsTargetSteering()
    {
        progressiveMovement.SetCameraState(CameraMovementState.Steering);
        progressiveMovement.SetScriptActive(true);
        progressiveMovement.SetTargetPosition(new Vector3(14.0f, 10.0f, 0.0f));
        progressiveMovement.SetArrived(false);
        cameraData.SetCurrentSpeed(2.0f);
        Vector3 targetPos = progressiveMovement.GetTargetPosition();
        Vector3 initialPos = cameraData.GetPosition();
        yield return new WaitForSeconds(0.1f);
        Vector3 currentPos = cameraData.GetPosition();
        float startMagnitude = Globals.Globals.Magnitude(initialPos - targetPos);
        float nextMagnitude = Globals.Globals.Magnitude(currentPos - targetPos);
        Assert.Less(nextMagnitude, startMagnitude);
    }

    [UnityTest]
    public IEnumerator SlowOnArriveKinematic()
    {
        progressiveMovement.SetCameraState(CameraMovementState.Kinematic);
        progressiveMovement.SetScriptActive(true);
        cameraData.SetCurrentSpeed(5.0f);
        progressiveMovement.SetTargetPosition(new Vector3(14.0f, 10.0f, 0.0f));
        progressiveMovement.SetArrived(false);
        float initialSpeed = cameraData.GetCurrentSpeed();
        yield return new WaitForSeconds(0.1f);
        float currentSpeed = cameraData.GetCurrentSpeed();
        Assert.Less(currentSpeed, initialSpeed);
    }

    [UnityTest]
    public IEnumerator SlowOnArriveSteering()
    {
        progressiveMovement.SetScriptActive(true);
        progressiveMovement.SetCameraState(CameraMovementState.Steering);
        cameraData.SetCurrentSpeed(5.0f);
        progressiveMovement.SetTargetPosition(new Vector3(14.0f, 10.0f, 0.0f));
        progressiveMovement.SetArrived(false);
        float initialSpeed = cameraData.GetCurrentSpeed();
        yield return new WaitForSeconds(0.1f);
        float currentSpeed = cameraData.GetCurrentSpeed();
        Assert.Less(currentSpeed, initialSpeed);
    }

    [UnityTest]
    public IEnumerator CheckSpeedCap()
    {
        progressiveMovement.SetCameraState(CameraMovementState.Kinematic);
        progressiveMovement.SetScriptActive(true);
        cameraData.SetCurrentSpeed(3.0f);
        progressiveMovement.SetTargetPosition(new Vector3(14.0f, 10.0f, 0.0f));
        progressiveMovement.SetArrived(false);
        progressiveMovement.SetMaxSpeed(1.0f);
        float MAX_SPEED = progressiveMovement.GetMaxSpeed();
        float initialSpeed = cameraData.GetCurrentSpeed();
        yield return new WaitForSeconds(0.01f);
        float currentSpeed = cameraData.GetCurrentSpeed();
        Assert.AreEqual(MAX_SPEED, currentSpeed);
    }
 }