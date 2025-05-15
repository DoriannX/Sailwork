using System;
using System.Collections.Generic;
using UnityEngine;

namespace SailorSystems
{
    public class SailorsManager : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private SailorController sailorPrefab;
        public List<SailorController> sailorsController { get; } = new();
        private Transform managerTransform;
        private void Awake()
        {
            managerTransform = transform;
            foreach (var spawnPoint in spawnPoints)
            {
                SailorController instancedController = Instantiate(sailorPrefab, managerTransform);
                instancedController.transform.position = spawnPoint.position;
                instancedController.transform.rotation = spawnPoint.rotation;
                sailorsController.Add(instancedController);
            }
        }
    }
}