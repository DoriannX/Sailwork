using System;
using UnityEngine;

namespace SailorSystems
{
    public class SailorSelectionFeedback : MonoBehaviour
    {
        [SerializeField] private Color selectedColor;
        SelectionManager selectionManager;

        private void Awake()
        {
            selectionManager = GetComponent<SelectionManager>();
        }

        private void Start()
        {
            selectionManager.onNewSelectedSailor += ChangeSailorColor;
        }

        private void ChangeSailorColor()
        {
            selectionManager.previouslySelectedSailor?.ResetColor();
            if(selectionManager.selectedSailor == null)
            {
                return;
            }
            selectionManager.selectedSailor.sailorColor = selectedColor;
        }
    }
}