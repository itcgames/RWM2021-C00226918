using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Specialized;
using Globals;

public class ProgressiveCameraMovement
{
    private ProgressiveMovement progressiveMovement;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Camera"));
        progressiveMovement = gameGameObject.GetComponent<ProgressiveMovement>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(progressiveMovement.gameObject);
    }

    [UnityTest]
    public IEnumerator CheckCameraMovedTowardsTarget()
    {
        progressiveMovement.SetCameraState(0);
        progressiveMovement.SetCurrentSpeed(2.0f);
        progressiveMovement.SetTargetPosition(new Vector3(14.0f, 10.0f, 0.0f));
        progressiveMovement.SetArrived(false);
        Vector3 targetPos = progressiveMovement.GetTargetPosition();
        Vector3 initialPos = progressiveMovement.GetCameraPosition();
        yield return new WaitForSeconds(0.1f);
        Vector3 currentPos = progressiveMovement.GetCameraPosition();
        float startMagnitude = Globals.Globals.Magnitude(initialPos - targetPos);
        float nextMagnitude = Globals.Globals.Magnitude(currentPos - targetPos);
        Assert.Less(nextMagnitude, startMagnitude);
    }

    [UnityTest]
    public IEnumerator SlowOnArrive()
    {
        progressiveMovement.SetCameraState(0);
        progressiveMovement.SetCurrentSpeed(5.0f);
        progressiveMovement.SetTargetPosition(new Vector3(14.0f, 10.0f, 0.0f));
        progressiveMovement.SetArrived(false);
        float initialSpeed = progressiveMovement.GetCurrentSpeed();
        yield return new WaitForSeconds(0.1f);
        float currentSpeed = progressiveMovement.GetCurrentSpeed();
        Assert.Less(currentSpeed, initialSpeed);
    }
}