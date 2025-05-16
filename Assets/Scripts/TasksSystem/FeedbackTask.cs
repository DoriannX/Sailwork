using SailorSystems;
using UnityEngine;

namespace TasksSystem
{
    public class FeedbackTask : MonoBehaviour
    {
        private Task task;
        private Renderer renderer;
        private HoverFeedback hoverFeedback;

        private void Awake()
        {
            renderer = GetComponentInChildren<Renderer>();
            task = GetComponent<Task>();

            task.onAvailable += OnTaskAvailable;
            task.onStarted += OnTaskStarted;
            task.onComplete += OnTaskComplete;
            hoverFeedback = GetComponent<HoverFeedback>();
            ToggleHover(false);
            SelectionManager.onActorSelected += OnNewSelectedSailor;
        }

        private void OnTaskStarted()
        {
            ToggleHover(false);
            renderer.material.color = Color.yellow;
        }

        private void OnDestroy()
        {
            SelectionManager.onActorSelected -= OnNewSelectedSailor;
        }

        private void OnNewSelectedSailor(bool selected)
        {
            ToggleHover(selected && task.isAvailable);
        }

        private void ToggleHover(bool state)
        {
            hoverFeedback.SetActive(state);
        }

        private void OnTaskComplete()
        {
            ToggleHover(false);
            renderer.material.color = Color.blue;
        }

        private void OnTaskAvailable()
        {
            renderer.material.color = Color.green;
        }
    }
}