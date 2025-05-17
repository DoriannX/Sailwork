using UnityEngine;

namespace Core
{
    /// <summary>
    /// This class is responsible for managing the time scale of the game.
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        private void Awake()
        {
            InitializeTime();
        }
        private void OnDestroy()
        {
            InputManager.onPauseInputTriggered -= ToggleTimeScale;
        }
        private void InitializeTime()
        {
            InputManager.onPauseInputTriggered += ToggleTimeScale;
            Time.timeScale = 1;
        }

        /// <summary>
        /// This method is used to check whether the game is paused or not and in either case,
        /// toggle on or off the time scale.
        /// </summary>
        private void ToggleTimeScale()
        {
            if (Mathf.Approximately(Time.timeScale, 1))
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    
        
    }
}