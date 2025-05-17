using System.Collections.Generic;
using Gameplay.Sailors;
using UnityEngine;
using UnityEngine.Assertions;

namespace Tasks
{
    /// <summary>
    /// This class is responsible for managing the tasks in the game.
    /// </summary>
    public class TaskManager : MonoBehaviour
    {
        [SerializeField] private List<Task> tasks = new();
        [SerializeField] private SailorsManager sailorsManager;

        private void Awake()
        {
            Assert.IsNotNull(sailorsManager);
            Assert.IsNotNull(tasks);
            Assert.IsTrue(tasks.Count > 0);
            sailorsManager.onSailorsSpawned += BindEvent;
        }

        /// <summary>
        /// This method is used to bind the event task added to a sailor when a new sailor is spawned.
        /// </summary>
        private void BindEvent(SailorController sailorController)
        {
            sailorController.taskManager.onTaskAsked += GiveNearestTask;
        }

        /// <summary>
        /// This method is used to answer the task asked by the sailor
        /// and add the nearest task to the sailor task queue.
        /// </summary>
        private void GiveNearestTask(SailorTaskManager sailorTaskManager)
        {
            sailorTaskManager.AddTask(GetNearestTaskPosition(sailorTaskManager.transform.position));
        }

        /// <summary>
        /// This method is used to get the nearest task position to the target position
        /// </summary>
        /// <returns> the nearest task </returns>
        private Task GetNearestTaskPosition(Vector3 targetPosition)
        {
            Vector3 nearest = Vector3.positiveInfinity;
            Task nearestTask = null;
            foreach (Task task in tasks)
            {
                bool isCloserThanCurrentNearest = Vector3.Distance(targetPosition, task.transform.position) <
                                                  Vector3.Distance(nearest, targetPosition);
                if (!task.isAvailable || !isCloserThanCurrentNearest)
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