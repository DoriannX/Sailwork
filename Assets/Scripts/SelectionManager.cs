using System;
using SailorSystems;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    public SailorController selectedSailor { get; private set; }
    public SailorController previouslySelectedSailor { get; private set; }
    public event Action onNewSelectedSailor;
    Camera currentCamera;

    private void Awake()
    {
        currentCamera = Camera.main;
    }

    private void SelectNewSailor(SailorController sailorController)
    {
        previouslySelectedSailor = selectedSailor;
        previouslySelectedSailor?.Select(false);
        selectedSailor = sailorController;
        selectedSailor?.Select(true);
        onNewSelectedSailor?.Invoke();
    }

    public void OnClick()
    {
        Ray ray = currentCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(currentCamera.transform.position, ray.direction * 100, Color.red, 2f);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            SailorController sailor = hit.collider.GetComponent<SailorController>();
            if (sailor == null || sailor == selectedSailor)
            {
                SelectNewSailor(null);
            }
            else
            {
                SelectNewSailor(sailor);
            }
        }
        else
        {
            SelectNewSailor(null);
        }
    }
}