using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SailorSystems
{
    public class TaskQueueFeedback : MonoBehaviour
    {
        [SerializeField] private List<Image> taskQueueImages = new();
        private SailorTaskManager sailorTaskManager;
        
        private void Awake()
        {
            sailorTaskManager = GetComponent<SailorTaskManager>();
        }
        
        private void Update()
        {
            List<Task> tasksList = sailorTaskManager.askedTasks.ToList();
            int minCount = Mathf.Min(taskQueueImages.Count, tasksList.Count);
            
            for (int i = 0; i < minCount; i++)
            {
                taskQueueImages[i].enabled = true;
                taskQueueImages[i].sprite = tasksList[i].icon;
            }
            
            for (int i = minCount; i < taskQueueImages.Count; i++)
            {
                taskQueueImages[i].enabled = false;
            }
        }
    }
}