using UnityEngine;
using static Project.Scripts.Runtime.Sound.SoundController.Sound;

namespace Project.Scripts.Runtime.Sound
{
    /// <summary>
    /// This class is responsible for managing the main menu music.
    /// </summary>
    [RequireComponent(typeof(SoundController))]
    public class MainMenuMusicManager : MonoBehaviour
    {
        private SoundController soundController;

        private void Awake()
        {
            soundController = GetComponent<SoundController>();
        }

        private void Start()
        {
            soundController.PlaySound(MenuMusic, true);
        }
    }
}