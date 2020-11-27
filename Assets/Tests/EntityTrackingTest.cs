using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EntityTrackingTest
    {
        private EntityTracking entityTracking;

        [SetUp]
        public void Setup()
        {
            GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Camera"));
            entityTracking = gameGameObject.GetComponent<EntityTracking>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(entityTracking.gameObject);
        }
    }
}
