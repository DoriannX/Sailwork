using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Project.Scripts.Runtime.Gameplay.Sailors
{
    /// <summary>
    /// This class is responsible for the movement of the sailor.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class SailorMovement : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private float minMopNextPositionDistance;
        [SerializeField] private float maxMopNextPositionDistance;
        #endregion
        
        #region Private Fields
        private bool reachedTarget = true;
        private Transform sailorTransform;
        private Vector3 targetPosition;
        private NavMeshAgent navMeshAgent;
        #endregion

        private void Awake()
        {
            sailorTransform = transform;
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            CheckReachedTarget();
        }

        /// <summary>
        /// This function is used to use the NavMeshAgent to move the sailor to a specific position.
        /// </summary>
        /// <param name="position"></param>
        public void GoTo(Vector3 position)
        {
            reachedTarget = false;
            targetPosition = position;
            navMeshAgent.SetDestination(targetPosition);
        }
        private void CheckReachedTarget()
        {
            bool isArrivingAtTarget = Vector3.Distance(sailorTransform.position, targetPosition) <= 0.6f;
            if (!reachedTarget && isArrivingAtTarget)
            {
                reachedTarget = true;
            }
        }

        /// <summary>
        /// This function is used to move the sailor to a random position to simulate mopping.
        /// </summary>
        public void Mop()
        {
            if (reachedTarget)
            {
                GoTo(GetRandomPos());
            }
        }

        /// <summary>
        /// This function is used to get a random position around the sailor on the navmesh.
        /// </summary>
        /// <returns> the random position found </returns>
        private Vector3 GetRandomPos(int maxAttempts = 10)
        {
            for (var i = 0; i < maxAttempts; i++)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized * Random.Range(minMopNextPositionDistance, maxMopNextPositionDistance);
                Vector3 randomPoint = sailorTransform.position + new Vector3(randomDirection.x, 0, randomDirection.y);

                bool isPositionOnNavMesh = NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1f, NavMesh.AllAreas);
                if (isPositionOnNavMesh)
                {
                    return hit.position;
                }
            }

            return sailorTransform.position;
        }
        
        public void Stop()
        {
            if (navMeshAgent == null)
            {
                return;
            }
            reachedTarget = true;
            navMeshAgent.isStopped = true;
            navMeshAgent.ResetPath();
        }
    }
}