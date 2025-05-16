using UnityEngine;

namespace UI
{
    public class TooltipManager : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            ToggleCanvasGroup(false);
        }

        public void ToggleCanvasGroup(bool state)
        {
            canvasGroup.alpha = state ? 1 : 0;
            canvasGroup.blocksRaycasts = state;
            canvasGroup.interactable = state;
        }
    }
}