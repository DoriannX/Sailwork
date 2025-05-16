using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SailorSystems
{
    public class SailorsManager : MonoBehaviour
    {
        [SerializeField] private SailorController sailorPrefab;
        [SerializeField] private float spawnCount;
        [SerializeField] private float spawnDelaySecond;
        [SerializeField] private Transform spawnParent;
        private RestPoint restPoint;
        public event Action<SailorController> onSailorsSpawned;
        private void Awake()
        {
            restPoint = GetComponent<RestPoint>();
        }

        private void Start()
        {
            StartCoroutine(SpawnSailors());
        }

        private IEnumerator SpawnSailors()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                SailorController instancedController = Instantiate(sailorPrefab, spawnParent);
                instancedController.GetComponent<SailorFatigueManager>().SetRestPointPos(restPoint.transform.position);
                onSailorsSpawned?.Invoke(instancedController);
                yield return new WaitForSeconds(spawnDelaySecond);
            }
        }
    }
}