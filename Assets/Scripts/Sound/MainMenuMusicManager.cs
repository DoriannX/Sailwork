using System;
using SailorSystems.Sound;
using UnityEngine;
using static SailorSystems.Sound.SoundManager.Sound;

namespace Sound
{
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
            soundManager.PlaySoundLoop(MenuMusic);
        }
    }
}