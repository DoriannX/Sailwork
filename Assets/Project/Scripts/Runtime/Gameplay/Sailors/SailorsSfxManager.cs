using Project.Scripts.Runtime.Sound;
using Project.Scripts.Runtime.StateMachineSystem.States;
using UnityEngine;
using static Project.Scripts.Runtime.Sound.SfxManager.Sfx;

namespace Project.Scripts.Runtime.Gameplay.Sailors
{
    /// <summary>
    /// This class is responsible for managing the sound effects of the sailors.
    /// </summary>
    public class SailorsSfxManager : MonoBehaviour
    {
        [SerializeField] private SfxManager sfxManager;

        private void Awake()
        {
            TiredState.onStarted += OnTiredStateEnteredAnywhere;
        }

        private void OnDestroy()
        {
            TiredState.onStarted -= OnTiredStateEnteredAnywhere;
        }

        /// <summary>
        /// This method is used to play the sound effect when a sailor enters the tired state.
        /// </summary>
        private void OnTiredStateEnteredAnywhere()
        {
            sfxManager.PlaySfx(Snore, 0.01f);
        }
    }
}