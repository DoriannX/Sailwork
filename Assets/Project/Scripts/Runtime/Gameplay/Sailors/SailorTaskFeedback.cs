using Project.Scripts.Runtime.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Project.Scripts.Runtime.Gameplay.Sailors
{
    /// <summary>
    /// This class is responsible for displaying the task feedback on the UI
    /// </summary>
    [RequireComponent(typeof(SailorTaskManager))]
    public class SailorTaskFeedback : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image fill;
        [SerializeField] private TextMeshProUGUI taskProgressText;
        private SailorTaskManager taskManager;
        private Task currentTask;

        private void Awake()
        {
            Assert.IsNotNull(canvasGroup);
            Assert.IsNotNull(fill);
            Assert.IsNotNull(taskProgressText);
            InitializeTaskManager();
        }

        private void Start()
        {
            ToggleCanvasGroup(false);
        }

        private void Update()
        {
            UpdateTaskProgress();
        }

        /// <summary>
        /// This method is used to update the task progress fill and text
        /// </summary>
        private void UpdateTaskProgress()
        {
            if (currentTask == null)
            {
                return;
            }
            fill.fillAmount = currentTask.completePercentage;
           taskProgressText.text = $"{(1 - currentTask.completePercentage) * currentTask.timeToComplete:F1}s";
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