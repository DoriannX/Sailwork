using UnityEngine;

namespace SailorSystems
{
    public class SailorFatigueManager : MonoBehaviour
    {
        [SerializeField] private float _fatiguePercentage;
        public bool isResting { get; private set; }

        public float fatiguePercentage
        {
            get
            {
                return _fatiguePercentage = Mathf.Clamp01(_fatiguePercentage);
            }
            private set => _fatiguePercentage = Mathf.Clamp01(value);
        }

        public Vector3 restPointPos { get; private set; }

        public void AddFatigue(float fatigue)
        {
            fatiguePercentage += fatigue;
        }

        public void Rest(float amount)
        {
            isResting = true;
            fatiguePercentage -= amount;
            if (fatiguePercentage <= 0)
            {
                isResting = false;
            }
        }
        
        public void SetRestPointPos(Vector3 restPointPos)
        {
            this.restPointPos = restPointPos;
        }
    }
}