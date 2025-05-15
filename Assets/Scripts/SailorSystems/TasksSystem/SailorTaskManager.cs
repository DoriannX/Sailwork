using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SailorSystems
{
    public class SailorTaskManager : MonoBehaviour
    {
        public bool isWorking { get; private set; } = false;
        private List<Task> tasks = new();
        private Transform sailorTransform;

        private void Start()
        {
            sailorTransform = transform;
        }

        public Vector3 GetNearestTaskPosition()
        {
            Vector3 nearest = Vector3.positiveInfinity;
            bool found = false;
            foreach (var task in tasks)
            {
                if (!task.done && Vector3.Distance(sailorTransform.position, task.transform.position) <
                    Vector3.Distance(nearest, task.transform.position))
                {
                    nearest = task.transform.position;
                    found = true;
                }
            }
            return found ? nearest : Vector3.zero;
        }

        public bool IsTaskAvailable()
        {
            return tasks.Any(task => !task.done);
        }
    }
}