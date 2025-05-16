using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SailorSystems
{
    public class SailorTaskManager : MonoBehaviour
    {
        private Queue<Task> askedTasks = new();

        public event Action<SailorTaskManager> onTaskAsked;
        public event Action<Task> onTaskStarted;
        
        public event Action<float> onTaskCompleted; 
        
        public bool IsTaskAvailable()
        {
            if (askedTasks == null || askedTasks.Count <= 0)
                return false;
                
            while (askedTasks.Count > 0)
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
            }
            
            return askedTasks.Count > 0;
        }
        public bool isWorking { get; private set; } = false;

        public void AskNearestTask()
        {
            onTaskAsked?.Invoke(this);
        }

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
            if (task == null || !task.isAvailable || askedTasks.Contains(task))
            {
                return;
            }
            askedTasks.Enqueue(task);
        }

        public Task GetNextTask()
        {
            return askedTasks.Peek();
        }

        public void Work()
        {
            if (!isWorking)
            {
                return;
            }
            Task nextTask = GetNextTask();
            if (nextTask != null)
            {
                nextTask.CompleteTask();
                if (nextTask.done)
                {
                    onTaskCompleted?.Invoke(askedTasks.Dequeue().fatiguePercentage);
                    isWorking = false;
                }
            }
        }

        public void StartTask()
        {
            isWorking = true;
            onTaskStarted?.Invoke(GetNextTask());
        }

        public void ClearTasks()
        {
            askedTasks.Clear();
        }
    }
}