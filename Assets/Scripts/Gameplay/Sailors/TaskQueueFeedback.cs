using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Sailors
{
    /// <summary>
    /// This class is responsible for displaying the task queue feedback for sailors.
    /// </summary>
    [RequireComponent(typeof(SailorTaskManager))]
    public class TaskQueueFeedback : MonoBehaviour
    {
        [SerializeField] private List<Image> taskQueueImages = new();
        private SailorTaskManager sailorTaskManager;
        
        private void Awake()
        {
            Assert.IsNotNull(taskQueueImages);
            Assert.IsTrue(taskQueueImages.Count > 0);
            sailorTaskManager = GetComponent<SailorTaskManager>();
        }
        
        private void Update()
        {
            UpdateTaskList();
        }

        /// <summary>
        /// This method is used to handle the task queue feedback for sailors.
        /// It converts the queue to a list and displays the tasks in the UI.
        /// </summary>
        private void UpdateTaskList()
        {
            List<Task> tasksList = sailorTaskManager.askedTasks.ToList();
            int visibleTaskCount = Mathf.Min(taskQueueImages.Count, tasksList.Count);
            
            for (var i = 0; i < visibleTaskCount; i++)
            {
                ToggleTask(taskQueueImages[i], tasksList[i]);
            }
            
            for (int i = visibleTaskCount; i < taskQueueImages.Count; i++)
            {
                ToggleTask(taskQueueImages[i]);
            }
        }

        /// <summary>
        /// This method is used to toggle the task image in the UI.
        /// </summary>
        /// <param name="taskImage"> the image to toggle</param>
        /// <param name="task"> the task to display </param>
        private static void ToggleTask(Image taskImage, Task task = null)
        {
            taskImage.enabled = task != null;
            taskImage.sprite = task?.icon;
        }
    }
}