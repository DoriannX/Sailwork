using System.Collections.Generic;
using System.Linq;
using Gameplay.Sailors;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// This class is responsible for managing the rest point for sailors.
    /// </summary>
    public class RestPoint : MonoBehaviour
    {
        [SerializeField, Min(0.01f)] private float timeToRest = 5f;
        private readonly List<SailorFatigueManager> sailorsFatigueManager = new();

        private void Update()
        {
            RestTiredSailors();
        }
        private void OnTriggerEnter(Collider other)
        {
            GetDetectedSailor(other);
        }

        /// <summary>
        /// This method is used to get the fatigue manager of the detected sailor and check at the same time
        /// if it's really a sailor
        /// </summary>
        /// <param name="other"> The detected collider </param>
        private void GetDetectedSailor(Collider other)
        {
            SailorFatigueManager sailorFatigueManager = other.GetComponent<SailorFatigueManager>();
            if (sailorFatigueManager != null && sailorFatigueManager.fatiguePercentage >= 1)
            {
                sailorsFatigueManager.Add(sailorFatigueManager);
            }
        }

        /// <summary>
        /// This function is used to rest the sailors that are tired and check when they are done resting then
        /// remove them from the list
        /// </summary>
        private void RestTiredSailors()
        {
            bool isSailorsTired = sailorsFatigueManager.Count > 0;
            if (!isSailorsTired)
            {
                return;
            }

            foreach (SailorFatigueManager sailorFatigueManager in sailorsFatigueManager.ToList())
            {
                sailorFatigueManager.Rest(amount : Time.deltaTime / timeToRest);
                bool isSailorTired = sailorFatigueManager.fatiguePercentage > 0;
                if (!isSailorTired)
                {
                    sailorsFatigueManager.Remove(sailorFatigueManager);
                }
            }
        }
    }
}
