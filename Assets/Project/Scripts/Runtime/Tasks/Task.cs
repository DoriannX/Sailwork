using System;
using Project.Scripts.Runtime.Core.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.Scripts.Runtime.Tasks
{
    /// <summary>
    /// This class is responsible for managing the task system.
    /// </summary>
    public class Task : MonoBehaviour, IInteractable
    {
        #region Task State

        [Header("Task State")] public bool done { get; private set; }
        private bool started;
        public bool isAvailable => !done && !started;
        public float completePercentage { get; private set; }

        #endregion

        #region Visual Properties

        [Header("Visual Properties")]
        [field: SerializeField]
        public Sprite icon { get; private set; }

        [SerializeField] private string taskName;

        #endregion

        #region Task Configuration

        [Header("Task Configuration")]
        [field: SerializeField, Range(0, 1)]
        public float fatiguePercentage { get; private set; } = 0.1f;

        [field: SerializeField] [field: Min(0.01f)] public float timeToComplete { get; private set; } = 5f;

        [SerializeField, Min(0)] private float rechargeTime = 1f;

        #endregion

        #region Events
        public static event Action onNewTaskStarted;
        public static event Action onNewTaskCompleted;
        public event Action onComplete;
        public event Action onStarted;
        public event Action onAvailable;

        #endregion

        private void Awake()
        {
            Assert.IsNotNull(icon);
        }

        private void Start()
        {
            onAvailable?.Invoke();
        }
        
        private void Update()
        {
            UpdateTask();
        }

        /// <summary>
        /// This method is used to complete a task and check when the task is done.
        /// </summary>
        public void CompleteTask()
        {
            if (!started)
            {
                onStarted?.Invoke();
                onNewTaskStarted?.Invoke();
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
                onNewTaskCompleted?.Invoke();
                done = true;
                started = false;
            }
        }

        /// <summary>
        /// This method is used to recharge the task when it has be done and make it available again.
        /// </summary>
        private void UpdateTask()
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