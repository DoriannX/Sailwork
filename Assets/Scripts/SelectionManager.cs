using System;
using Interfaces;
using SailorSystems;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    public SailorController selectedSailor { get; private set; }
    public SailorController previouslySelectedSailor { get; private set; }
    public event Action onNewSelectedSailor;
    public static event Action<bool> onActorSelected;
    Camera currentCamera;

    private void Awake()
    {
        currentCamera = Camera.main;
    }

    private void SelectNewSailor(SailorController sailorController)
    {
        if (selectedSailor == sailorController)
        {
            sailorController = null;
        }

        previouslySelectedSailor = selectedSailor;
        previouslySelectedSailor?.Select(false);
        selectedSailor = sailorController;
        selectedSailor?.Select(true);
        onActorSelected?.Invoke(sailorController != null);

        onNewSelectedSailor?.Invoke();
    }

    public void OnClick()
    {
        Ray ray = currentCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(currentCamera.transform.position, ray.direction * 100, Color.red, 2f);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable is SailorController sailor)
            {
                SelectNewSailor(sailor);
            }
            else if (interactable is Task task && selectedSailor != null)
            {
                SailorTaskManager sailorTaskManager = selectedSailor.GetComponent<SailorTaskManager>();
                if (sailorTaskManager != null)
                {
                    sailorTaskManager.AddTask(task);
                }

                SelectNewSailor(null);
            }
            else
            {
                SelectNewSailor(null);
            }
        }
        else
        {
            SelectNewSailor(null);
        }
    }
}