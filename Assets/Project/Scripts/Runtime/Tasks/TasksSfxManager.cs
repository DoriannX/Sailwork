using Project.Scripts.Runtime.Sound;
using UnityEngine;
using UnityEngine.Assertions;
using static Project.Scripts.Runtime.Sound.SfxManager.Sfx;

namespace Project.Scripts.Runtime.Tasks
{
    /// <summary>
    /// This class is responsible for managing the sound effects of the task system.
    /// </summary>
    public class TasksSfxManager : MonoBehaviour
    {
        [SerializeField] private SfxManager sfxManager;
        
        private void Awake()
        {
            Assert.IsNotNull(sfxManager);
            Task.onNewTaskStarted += OnTaskStarted;
            Task.onNewTaskCompleted += OnTaskCompleted;
        }
        
        private void OnDestroy()
        {
            Task.onNewTaskStarted -= OnTaskStarted;
            Task.onNewTaskCompleted -= OnTaskCompleted;
        }
        
        /// <summary>
        /// This method is used to play the sound effect when a task is started.
        /// </summary>
        private void OnTaskStarted()
        {
            sfxManager.PlaySfx(Click, 0.01f);
        }

        /// <summary>
        /// This method is used to play the sound effect when a task is completed.
        /// </summary>
        private void OnTaskCompleted()
        {
            sfxManager.PlaySfx(Ding, 0.05f);
        }
    }
}