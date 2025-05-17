using System;
using UnityEngine;
using static Sound.SoundManager.Sound;

namespace Sound
{
    /// <summary>
    /// This class is responsible for managing the main menu music.
    /// </summary>
    [RequireComponent(typeof(SoundManager))]
    public class MainMenuMusicManager : MonoBehaviour
    {
        private SoundManager soundManager;

        private void Awake()
        {
            soundManager = GetComponent<SoundManager>();
        }

        private void Start()
        {
            soundManager.PlaySound(MenuMusic, true);
        }
    }
}