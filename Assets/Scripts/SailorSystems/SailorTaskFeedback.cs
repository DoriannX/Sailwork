using UnityEngine;
using UnityEngine.UI;

namespace SailorSystems
{
    public class SailorTaskFeedback : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image fill;
        private SailorTaskManager taskManager;
        private Task currentTask;

        private void Awake()
        {
            taskManager = GetComponent<SailorTaskManager>();
            taskManager.onTaskStarted += OnTaskStarted;
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

        private void OnTaskStarted(Task task)
        {
            ToggleCanvasGroup(true);
            currentTask = task;
            task.onComplete += OnTaskComplete;
        }

        private void OnTaskComplete()
        {
            ToggleCanvasGroup(false);
            currentTask = null;
        }

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