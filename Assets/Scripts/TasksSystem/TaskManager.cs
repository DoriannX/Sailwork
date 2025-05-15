using System;
using System.Collections.Generic;
using UnityEngine;

namespace SailorSystems
{
    public class TaskManager : MonoBehaviour
    {
        [SerializeField] private List<Task> tasks = new();
        private Transform sailorTransform;
        private SailorsManager sailorsManager;

        private void Awake()
        {
            sailorsManager = GetComponent<SailorsManager>();
        }

        private void Start()
        {
            sailorTransform = transform;
            foreach (SailorController sailorController in sailorsManager.sailorsController)
            {
                sailorController.taskManager.onTaskAsked += GiveNearestTask;
            }
        }

        private void GiveNearestTask(SailorTaskManager sailorTaskManager)
        {
            sailorTaskManager.AddTask(GetNearestTaskPosition(sailorTaskManager.transform.position));
        }

        public Task GetNearestTaskPosition(Vector3 targetPosition)
        {
            Vector3 nearest = Vector3.positiveInfinity;
            Task nearestTask = null;
            foreach (var task in tasks)
            {
                if (!task.isAvailable || !(Vector3.Distance(targetPosition, task.transform.position) <
                                   Vector3.Distance(nearest, targetPosition)))
                {
                    continue;
                }
                nearest = task.transform.position;
                nearestTask = task;
            }
            return nearestTask;
        }
    }
}