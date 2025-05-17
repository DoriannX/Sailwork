using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.Runtime.SelectionSystem
{
    /// <summary>
    /// This class is responsible for providing hover feedback on the selected object.
    /// </summary>
    public class HoverFeedback : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform rendererTransform;
#pragma warning disable CS0108, CS0114
        private bool enabled = true;
#pragma warning restore CS0108, CS0114

        private void Awake()
        {
            Assert.IsNotNull(rendererTransform);
        }

        /// <summary>
        /// This method is used to call the exit hover event when the script is disabled or enabling it
        /// </summary>
        public void SetActive(bool state)
        {
            if (!state)
            {
                OnPointerExit(null);
            }

            enabled = state;
        }

        /// <summary>
        /// This method is called when the pointer enters the object to expand the renderer of the object
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (enabled)
            {
                rendererTransform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.OutBounce);
            }
        }

        /// <summary>
        /// This method is called when the pointer exits the object to return the renderer of the object to its original size
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (enabled)
            {
                rendererTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
            }
        }

        /// <summary>
        /// This method is used to destroy DoTween when the object is destroyed as it is not automatically destroyed
        /// </summary>
        private void OnDestroy()
        {
            rendererTransform.DOKill();
        }
    }
}