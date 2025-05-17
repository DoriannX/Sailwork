using UnityEngine;
using UnityEngine.Assertions;

namespace Project.Scripts.Runtime.SelectionSystem
{
    /// <summary>
    /// this class is responsible for showing the arrow object on top of the selected sailor
    /// </summary>
    [RequireComponent(typeof(SelectionManager))]
    public class SelectionFeedback : MonoBehaviour
    {
        [SerializeField] private GameObject selectedSailorArrowPrefab;
        private GameObject selectedSailorArrow;
        private SelectionManager selectionManager;

        private void Awake()
        {
            Assert.IsNotNull(selectedSailorArrowPrefab);
            selectionManager = GetComponent<SelectionManager>();
            selectedSailorArrow = Instantiate(selectedSailorArrowPrefab, Vector3.zero, Quaternion.identity);
            selectedSailorArrow.SetActive(false);
        }

        private void Start()
        {
            selectionManager.onNewSelectedSailor += ToggleSelectedArrow;
        }

        private void OnDestroy()
        {
            selectionManager.onNewSelectedSailor -= ToggleSelectedArrow;
        }

        /// <summary>
        /// This method is used to toggle the arrow based on the selected sailor
        /// </summary>
        private void ToggleSelectedArrow()
        {
            if (selectionManager.selectedSailor == null)
            {
                selectedSailorArrow.SetActive(false);
                return;
            }

            selectedSailorArrow.SetActive(true);
            selectedSailorArrow.transform.parent = selectionManager.selectedSailor.transform;
            selectedSailorArrow.transform.localPosition = Vector3.zero;
        }
    }
}