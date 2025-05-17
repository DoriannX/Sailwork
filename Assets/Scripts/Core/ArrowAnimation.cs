using DG.Tweening;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// This classes handles the animation of a 3D arrow that shows the selected sailor
    /// </summary>
    public class ArrowAnimation : MonoBehaviour
    {
        [SerializeField] private float rotationDuration = 1.0f;
        [SerializeField] private float rotationAmount = 360f;
        [SerializeField] private Ease rotationEase = Ease.Linear;

        private void OnEnable()
        {
            StartAnimation();
        }
        
        private void OnDisable()
        {
            ResetDoTween();
        }

        /// <summary>
        /// I am using DOTween to animate easily the rotation of the arrow and calling it one time with infinite loops to avoid
        /// using bool variables and update methods for clearness
        /// </summary>
        private void StartAnimation()
        {
            transform.DOLocalRotate(new Vector3(0, 0, rotationAmount), rotationDuration, RotateMode.FastBeyond360)
                .SetEase(rotationEase)
                .SetLoops(-1, LoopType.Restart);
        }

        /// <summary>
        /// This method is used to destroy DoTween due to DoTween not being able to destroy itself after Disable
        /// </summary>
        private void ResetDoTween()
        {
            transform.DOKill();
            transform.localRotation = Quaternion.identity;
        }
    }
}