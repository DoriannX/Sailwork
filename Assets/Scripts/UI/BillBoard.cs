using UnityEngine;

namespace UI
{
    public enum PivotAxis
    {
        Free,
        X,
        Y
    }

    /// <summary>
    /// The Billboard class implements the behaviors needed to keep a GameObject 
    /// oriented towards the user.
    /// </summary>
    public class Billboard : MonoBehaviour
    {
        public PivotAxis PivotAxis = PivotAxis.Free;
        public Quaternion DefaultRotation { get; private set; }

        private void Awake()
        {
            DefaultRotation = gameObject.transform.rotation;
        }

        /// <summary>
        /// The billboard logic is performed in FixedUpdate to update the object
        /// with the player independent of the frame rate.  This allows the object to 
        /// remain correctly rotated even if the frame rate drops.
        /// </summary>
        private void FixedUpdate()
        {
            Vector3 directionToTarget = Camera.main.transform.position - gameObject.transform.position;

            switch (PivotAxis)
            {
                case PivotAxis.X:
                    directionToTarget.x = gameObject.transform.position.x;
                    break;

                case PivotAxis.Y:
                    directionToTarget.y = gameObject.transform.position.y;
                    break;

                case PivotAxis.Free:
                default:
                    break;
            }
            gameObject.transform.rotation = Quaternion.LookRotation(-directionToTarget) * DefaultRotation;
        }
    }
}