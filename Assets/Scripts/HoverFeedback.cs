using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SailorSystems
{
    public class HoverFeedback : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform rendererTransform;
        public bool enabled { get; private set; } = true;

        public void SetActive(bool state)
        {
            if (!state)
            {
                OnPointerExit(null);
            }

            enabled = state;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (enabled)
            {
                rendererTransform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.OutBounce);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (enabled)
            {
                rendererTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
            }
        }

        private void OnDestroy()
        {
            rendererTransform.DOKill();
        }
    }
}