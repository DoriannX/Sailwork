using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private void Awake()
    {
        InputManager.onPauseInputTriggered += ToggleTimeScale;
        Time.timeScale = 1;
    }
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
    
    private void OnDestroy()
    {
        InputManager.onPauseInputTriggered -= ToggleTimeScale;
    }
}