using Project.Scripts.Runtime.SelectionSystem;
using Project.Scripts.Runtime.Sound;
using UnityEngine;
using static Project.Scripts.Runtime.Sound.SfxManager.Sfx;

namespace Project.Scripts.Runtime.Tasks
{
    /// <summary>
    /// This class is responsible for showing the feedback of the task.
    /// </summary>
    [RequireComponent(typeof(Task))]
    [RequireComponent(typeof(HoverFeedback))]
    public class FeedbackTask : MonoBehaviour
    {
        private Task task;
#pragma warning disable CS0108, CS0114
        private Renderer renderer;
#pragma warning restore CS0108, CS0114
        private HoverFeedback hoverFeedback;

        private void Awake()
        {
            InitComponents();
            InitEvents();
            ToggleHover(false);
            SelectionManager.onActorSelected += OnNewSelectedSailor;
        }

        private void OnDestroy()
        {
            SelectionManager.onActorSelected -= OnNewSelectedSailor;
        }

        private void InitEvents()
        {
            task.onAvailable += OnTaskAvailable;
            task.onStarted += OnTaskStarted;
            task.onComplete += OnTaskComplete;
        }

        private void InitComponents()
        {
            hoverFeedback = GetComponent<HoverFeedback>();
            renderer = GetComponentInChildren<Renderer>();
            task = GetComponent<Task>();
        }

        /// <summary>
        /// This method is used to show the feedback when the task is started.
        /// </summary>
        private void OnTaskStarted()
        {
            ToggleHover(false);
            renderer.material.color = Color.yellow;
        }

        /// <summary>
        /// This method is used to show the hover feedback when the sailor is selected.
        /// </summary>
        private void OnNewSelectedSailor(bool selected)
        {
            ToggleHover(selected && task.isAvailable);
        }

        /// <summary>
        /// This method is used to show or hide the hover feedback.
        /// </summary>
        private void ToggleHover(bool state)
        {
            hoverFeedback.SetActive(state);
        }

        /// <summary>
        /// This method is used to show the feedback when the task is completed.
        /// </summary>
        private void OnTaskComplete()
        {
            ToggleHover(false);
            renderer.material.color = Color.blue;
        }

        /// <summary>
        /// This method is used to show the feedback when the task is available.
        /// </summary>
        private void OnTaskAvailable()
        {
            renderer.material.color = Color.green;
        }
    }
}