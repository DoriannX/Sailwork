using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace SailorSystems
{
    public class SailorMovement : MonoBehaviour
    {
        [SerializeField] private float minMopNextPositionDistance;
        [SerializeField] private float maxMopNextPositionDistance;
        bool reachedTarget = true;
        private Transform sailorTransform;
        private Vector3 targetPosition;
        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            sailorTransform = transform;
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void GoTo(Vector3 position)
        {
            reachedTarget = false;
            targetPosition = position;
            navMeshAgent.SetDestination(targetPosition);
        }

        private void Update()
        {
            if (!reachedTarget && Vector3.Distance(sailorTransform.position, targetPosition) <= 0.6f)
            {
                reachedTarget = true;
            }
        }

        public void Mop()
        {
            if (reachedTarget)
            {
                GoTo(GetRandomPos());
            }
        }

        private Vector3 GetRandomPos(int maxAttempts = 10)
        {
            for (int i = 0; i < maxAttempts; i++)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized * Random.Range(minMopNextPositionDistance, maxMopNextPositionDistance);
                Vector3 randomPoint = sailorTransform.position + new Vector3(randomDirection.x, 0, randomDirection.y);

                if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }

            return sailorTransform.position;
        }


        public void Stop()
        {
            if (navMeshAgent != null)
            {
                reachedTarget = true;
                navMeshAgent.isStopped = true;
                navMeshAgent.ResetPath();
            }
        }
    }
}