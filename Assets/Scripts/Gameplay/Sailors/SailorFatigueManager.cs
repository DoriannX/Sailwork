using UnityEngine;

namespace Gameplay.Sailors
{
    /// <summary>
    /// This class is responsible for managing the fatigue of sailors.
    /// </summary>
    public class SailorFatigueManager : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] private float _fatiguePercentage;
        public bool isResting { get; private set; }
        public Vector3 restPointPos { get; private set; }
        
        /// <summary>
        /// This property is used to clamp the value before setting and returning it.
        /// </summary>
        public float fatiguePercentage
        {
            get
            {
                return _fatiguePercentage = Mathf.Clamp01(_fatiguePercentage);
            }
            private set => _fatiguePercentage = Mathf.Clamp01(value);
        }
        
        public void SetRestPointPos(Vector3 restPointPos)
        {
            this.restPointPos = restPointPos;
        }

        public void AddFatigue(float fatigue)
        {
            fatiguePercentage += fatigue;
        }

        /// <summary>
        /// This method is used to remove fatigue from the sailor and check if the sailor is done resting.
        /// </summary>
        /// <param name="amount"> the amount of fatigue to remove to the sailor </param>
        public void Rest(float amount)
        {
            isResting = true;
            fatiguePercentage -= amount;
            bool isTired = fatiguePercentage > 0;
            if (!isTired)
            {
                isResting = false;
            }
        }
    }
}