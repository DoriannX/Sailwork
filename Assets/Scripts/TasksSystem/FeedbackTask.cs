using SailorSystems;
using UnityEngine;
using UnityEngine.EventSystems;

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
            task.onComplete += OnTaskComplete;
            hoverFeedback = GetComponent<HoverFeedback>();
            hoverFeedback.enabled = false;
            SelectionManager.onActorSelected += OnNewSelectedSailor;
        }

        private void OnNewSelectedSailor(bool selected)
        {
            hoverFeedback.enabled = selected;
        }

        private void OnTaskComplete()
        {
            renderer.material.color = Color.blue;
        }

        private void OnTaskAvailable()
        {
            renderer.material.color = Color.green;
        }
    }
}