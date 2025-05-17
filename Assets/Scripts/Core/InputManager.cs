using System;
using SelectionSystem;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace Core
{
    /// <summary>
    /// This class is used to handle the new input system for clear separation of concerns and better readability.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private SelectionManager selectionManager;
        public static event Action onPauseInputTriggered;

        private void Awake()
        {
            Assert.IsNotNull(selectionManager);
        }

        /// <summary>
        /// Handles click input events and delegates the action to the SelectionManager when the input is performed
        /// for better separation of concerns 
        /// </summary>
        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                selectionManager.OnClick();
            }
        }

        /// <summary>
        /// Handles pause input events and calls an event to be able to have multiple listeners like time manager and
        /// pause manager
        /// </summary>
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onPauseInputTriggered?.Invoke();
            }
        }

        /// <summary>
        /// This mehod is used to trigger the pause input event from other classes like TimeManager and PauseManager
        /// For exemple, a button in the UI can call this method to trigger the pause input event
        /// </summary>
        public static void TriggerPauseInput()
        {
            onPauseInputTriggered?.Invoke();
        }
    }
}