using UnityEngine;

public class SelectionFeedback : MonoBehaviour
{
    [SerializeField] private GameObject selectedSailorImage;
    SelectionManager selectionManager;

    private void Awake()
    {
        selectionManager = GetComponent<SelectionManager>();
        selectedSailorImage.SetActive(false);
    }

    private void Start()
    {
        selectionManager.onNewSelectedSailor += ToggleSelectedArrow;
    }

    private void OnDestroy()
    {
        selectionManager.onNewSelectedSailor -= ToggleSelectedArrow;
    }

    private void ToggleSelectedArrow()
    {
        if (selectionManager.selectedSailor == null)
        {
            selectedSailorImage.SetActive(false);
            return;
        }

        selectedSailorImage.SetActive(true);
        selectedSailorImage.transform.parent = selectionManager.selectedSailor.transform;
        selectedSailorImage.transform.localPosition = Vector3.zero;
    }
}