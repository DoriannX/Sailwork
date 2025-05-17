using NUnit.Framework;
using Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Sailors
{
    /// <summary>
    /// This class is responsible for displaying the task feedback on the UI
    /// </summary>
    [RequireComponent(typeof(SailorTaskManager))]
    public class SailorTaskFeedback : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image fill;
        private SailorTaskManager taskManager;
        private Task currentTask;

        private void Awake()
        {
            Assert.IsNotNull(canvasGroup);
            Assert.IsNotNull(fill);
            InitializeTaskManager();
        }

        private void Start()
        {
            ToggleCanvasGroup(false);
        }

        private void Update()
        {
            if (currentTask != null)
            {
                fill.fillAmount = currentTask.completePercentage;
            }
        }
        
        private void InitializeTaskManager()
        {
            taskManager = GetComponent<SailorTaskManager>();
            taskManager.onTaskStarted += OnTaskStarted;
        }

        /// <summary>
        /// This method is used to turn on the feedback when the task started event is called 
        /// </summary>
        /// <param name="task"> The task that is started </param>
        private void OnTaskStarted(Task task)
        {
            ToggleCanvasGroup(true);
            currentTask = task;
            task.onComplete += OnTaskComplete;
        }

        /// <summary>
        /// This methos is used to turn off the feedback when the task completed event is called
        /// </summary>
        private void OnTaskComplete()
        {
            ToggleCanvasGroup(false);
            currentTask = null;
        }

        /// <summary>
        /// This method is used to reset and turn on/off the feedback
        /// </summary>
        /// <param name="state"> the state in which it will have to be </param>
        private void ToggleCanvasGroup(bool state)
        {
            if (!state)
            {
                fill.fillAmount = 0;
            }
            canvasGroup.alpha = state ? 1 : 0;
            canvasGroup.blocksRaycasts = state;
            canvasGroup.interactable = state;
        }
    }
}