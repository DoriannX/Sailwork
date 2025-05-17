using UnityEngine;

namespace UI
{
    /// <summary>
    /// The Billboard class implements the behaviors needed to keep a GameObject 
    /// oriented towards the user.
    /// </summary>
    public class Billboard : MonoBehaviour
    {
        private enum PivotAxis
        {
            Free,
            X,
            Y
        }

        [SerializeField] private PivotAxis pivotAxis = PivotAxis.Free;
        private Quaternion defaultRotation { get; set; }

        private Transform _mainCameraTransform;
        private Transform mainCameraTransform
        {
            get
            {
                if (_mainCameraTransform == null)
                {
                    _mainCameraTransform = Camera.main.transform;
                }
                return _mainCameraTransform;
            }
        }
        private Transform selfTransform;

        private void Awake()
        {
            selfTransform = transform;
            defaultRotation = gameObject.transform.rotation;
        }

        /// <summary>
        /// The billboard logic is performed in FixedUpdate to update the object
        /// with the player independent of the frame rate.  This allows the object to 
        /// remain correctly rotated even if the frame rate drops.
        /// </summary>
        private void FixedUpdate()
        {
            Vector3 directionToTarget = mainCameraTransform.position - selfTransform.position;

            switch (pivotAxis)
            {
                case PivotAxis.X:
                    directionToTarget.x = selfTransform.position.x;
                    break;

                case PivotAxis.Y:
                    directionToTarget.y = selfTransform.position.y;
                    break;

                case PivotAxis.Free:
                default:
                    break;
            }

            selfTransform.rotation = Quaternion.LookRotation(-directionToTarget) * defaultRotation;
        }
    }
}