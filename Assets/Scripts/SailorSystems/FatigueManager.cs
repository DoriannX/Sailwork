using UnityEngine;

namespace SailorSystems
{
    public class FatigueManager : MonoBehaviour
    {
        private float _fatiguePercentage = 0;

        public float FatiguePercentage
        {
            get
            {
                return _fatiguePercentage = Mathf.Clamp01(_fatiguePercentage);
            }
            private set => _fatiguePercentage = Mathf.Clamp01(value);
        }
        
        public void AddFatigue(float fatigue)
        {
            FatiguePercentage += fatigue;
        }
    }
}