using System;
using System.Collections.Generic;
using Project.Scripts.Runtime.Tasks;
using UnityEngine;

namespace Project.Scripts.Runtime.Gameplay.Sailors
{
    /// <summary>
    /// This class is responsible for managing the tasks of a sailor.
    /// </summary>
    public class SailorTaskManager : MonoBehaviour
    {
        #region Public Properties and Events

        public Queue<Task> askedTasks { get; } = new();
        public bool isWorking { get; private set; }

        #region Events

        public event Action<SailorTaskManager> onTaskAsked;
        public event Action<Task> onTaskStarted;
        public event Action<float> onTaskCompleted;

        #endregion

        #endregion

        /// <summary>
        /// This method is used to get the next available task and sort the non available tasks.
        /// </summary>
        /// <returns> if there's an available task </returns>
        public bool IsTaskAvailable()
        {
            bool isAskedTasks = askedTasks.Count > 0;
            if (askedTasks == null || !isAskedTasks)
                return false;

            while (isAskedTasks)
            {
                Task nextTask = askedTasks.Peek();
                if (nextTask != null && !nextTask.isAvailable)
                {
                    askedTasks.Dequeue();
                }
                else
                {
                    break;
                }
                isAskedTasks = askedTasks.Count > 0;
            }

            return isAskedTasks;
        }

        /// <summary>
        /// This method is used to ask the nearest task to the task manager to avoid cyclic reference.
        /// </summary>
        public void AskNearestTask()
        {
            onTaskAsked?.Invoke(this);
        }

        /// <summary>
        /// This method is used to check if the sailor is at the next task position.
        /// </summary>
        /// <returns></returns>
        public bool IsAtTask()
        {
            if (!IsTaskAvailable())
            {
                return false;
            }

            Task nextTask = GetNextTask();
            float distance = Vector3.Distance(transform.position, nextTask.transform.position);
            return distance <= 0.6f;
        }
        public void AddTask(Task task)
        {
            bool isTaskValid = task != null && task.isAvailable && !askedTasks.Contains(task);
            if (!isTaskValid)
            {
                return;
            }

            askedTasks.Enqueue(task);
        }

        public Task GetNextTask()
        {
            return askedTasks.Peek();
        }

        /// <summary>
        /// This method is used to complete the next task and check if the task is done.
        /// </summary>
        public void Work()
        {
            if (!isWorking)
            {
                return;
            }

            Task nextTask = GetNextTask();
            if (nextTask == null)
            {
                return;
            }
            nextTask.CompleteTask();
            if (!nextTask.done)
            {
                return;
            }
            onTaskCompleted?.Invoke(askedTasks.Dequeue().fatiguePercentage);
            isWorking = false;
        }

        public void StartTask()
        {
            isWorking = true;
            onTaskStarted?.Invoke(GetNextTask());
        }
    }
}