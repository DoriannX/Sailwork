using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SailorSystems
{
    public class SailorHoverFeedback : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform rendererTransform;

        public void OnPointerEnter(PointerEventData eventData)
        {
            rendererTransform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.InBounce);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            rendererTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
        }
    }
}