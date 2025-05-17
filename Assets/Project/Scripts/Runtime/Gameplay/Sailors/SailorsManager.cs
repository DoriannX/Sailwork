using System;
using System.Collections;
using Project.Scripts.Runtime.Core;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.Scripts.Runtime.Gameplay.Sailors
{
    /// <summary>
    /// This class is responsible for spawning sailors in the game to get access to all the sailors without any instances
    /// </summary>
    public class SailorsManager : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private SailorController sailorPrefab;
        [SerializeField] private float spawnCount;
        [SerializeField] private float spawnDelaySecond;
        [SerializeField] private RestPoint restPoint;
        #endregion
        
        #region Events
        public event Action<SailorController> onSailorsSpawned;
        #endregion

        private Transform selfTransform;
        private void Awake()
        {
            Assert.IsNotNull(sailorPrefab);
            Assert.IsNotNull(restPoint);
            selfTransform = transform;
        }

        private void Start()
        {
            StartCoroutine(SpawnSailors());
        }

        /// <summary>
        /// A coroutine is used to spawn the sailors with a delay between each spawn. Also initialize the rest point for each sailor
        /// </summary>
        private IEnumerator SpawnSailors()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                SailorController instancedController = Instantiate(sailorPrefab, selfTransform);
                instancedController.transform.position = restPoint.transform.position;
                instancedController.GetComponent<SailorFatigueManager>().SetRestPointPos(restPoint.transform.position);
                onSailorsSpawned?.Invoke(instancedController);
                yield return new WaitForSeconds(spawnDelaySecond);
            }
        }
    }
}