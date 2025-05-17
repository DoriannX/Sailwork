using System;
using Project.Scripts.Runtime.Core.Interfaces;
using Project.Scripts.Runtime.Gameplay.Sailors;
using Project.Scripts.Runtime.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace Project.Scripts.Runtime.SelectionSystem
{
    /// <summary>
    /// This class is responsible for managing the selection of sailors and tasks in the game.
    /// </summary>
    public class SelectionManager : MonoBehaviour
    {
        #region SerializedFields

        [SerializeField] private Camera mainCamera;

        #endregion

        #region Variables

        public SailorController selectedSailor { get; private set; }
        private SailorController previouslySelectedSailor;
        public event Action onNewSelectedSailor;
        public static event Action<bool> onActorSelected;

        #endregion

        private void Awake()
        {
            Assert.IsNotNull(mainCamera);
        }

        /// <summary>
        /// This function is used to select a new sailor. If the sailor is already selected, it will be deselected.
        /// It also deselect the previously selected sailor.
        /// </summary>
        /// <param name="sailorController"></param>
        private void SelectNewSailor(SailorController sailorController)
        {
            if (selectedSailor == sailorController)
            {
                sailorController = null;
            }

            previouslySelectedSailor = selectedSailor;
            previouslySelectedSailor?.sailorSelectionManager?.Select(false);
            selectedSailor = sailorController;
            selectedSailor?.sailorSelectionManager?.Select(true);
            onActorSelected?.Invoke(sailorController != null);

            onNewSelectedSailor?.Invoke();
        }

        /// <summary>
        /// This method is called when the player clicks on the screen. It checks if the click is on a sailor or a task.
        /// </summary>
        public void OnClick()
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Debug.DrawRay(mainCamera.transform.position, ray.direction * 100, Color.red, 2f);

            if (!Physics.Raycast(ray, out RaycastHit hit))
            {
                SelectNewSailor(null);
                return;
            }


            var interactable = hit.collider.GetComponent<IInteractable>();
            switch (interactable)
            {
                case SailorController sailor:
                    // Select the sailor if clicked
                    SelectNewSailor(sailor);
                    break;
                case Task task when selectedSailor != null:
                {
                    // If a task is clicked and a sailor is selected, assign the task to the sailor
                    var sailorTaskManager = selectedSailor.GetComponent<SailorTaskManager>();
                    sailorTaskManager?.AddTask(task);
                    // Deselect the sailor after assigning the task
                    SelectNewSailor(null);
                    break;
                }
                default:
                    // Deselect if clicked object is not a sailor or task
                    SelectNewSailor(null);
                    break;
            }
        }
    }
}

