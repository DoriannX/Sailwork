using System;
using Interfaces;
using UnityEngine;

namespace SailorSystems
{
    public class Task : MonoBehaviour, IInteractable
    {
        public bool done { get; private set; }
        private bool started;
        public bool isAvailable => !done && !started;
        [SerializeField] private string taskName;

        [field: SerializeField, Range(0, 1)] public float fatiguePercentage { get; private set; } = 0.1f;

        [SerializeField, Min(0.01f)] private float timeToComplete = 5f;
        [SerializeField, Min(0)] private float rechargeTime = 1f;

        public float completePercentage { get; private set; }

        public event Action onComplete;
        public event Action onStarted;
        public event Action onAvailable;

        private void Start()
        {
            onAvailable?.Invoke();
        }

        public void CompleteTask()
        {
            if (!started)
            {
                onStarted?.Invoke();
                started = true;
            }

            if (done)
            {
                return;
            }

            completePercentage += Time.deltaTime / timeToComplete;
            if (completePercentage >= 1)
            {
                onComplete?.Invoke();
                done = true;
                started = false;
            }
        }

        private void Update()
        {
            if (!done)
            {
                return;
            }

            completePercentage -= Time.deltaTime / rechargeTime;
            if (!(completePercentage <= 0))
            {
                return;
            }

            completePercentage = 0;
            done = false;
            onAvailable?.Invoke();
        }
    }
}