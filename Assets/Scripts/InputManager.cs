using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private SelectionManager selectionManager;
        
    private void Awake()
    {
        selectionManager = GetComponent<SelectionManager>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            selectionManager.OnClick();
        }
    }
}