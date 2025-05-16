using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private SelectionManager selectionManager;
    public static event Action onPauseInputTriggered;

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            selectionManager.OnClick();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onPauseInputTriggered?.Invoke();
        }
    }
    
    public static void TriggerPauseInput()
    {
        onPauseInputTriggered?.Invoke();
    }
}