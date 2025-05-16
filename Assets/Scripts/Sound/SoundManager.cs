using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace SailorSystems.Sound
{
    public class SoundManager : MonoBehaviour
    {
        public enum Sound
        {
            MenuMusic,
            AmbientMusic1,
            AmbientMusic2,
            AmbientMusic3,
        }
        private AudioSource audioSource;
        [SerializeField] private SerializedDictionary<Sound, AudioClip> sounds;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public AudioClip PlaySound(Sound sound)
        {
            if (!sounds.TryGetValue(sound, out AudioClip clip))
            {
                return null;
            }
            audioSource.clip = clip;
            audioSource.loop = false;
            audioSource.Play();
            return clip;
        }
        
        public void PlaySoundLoop( Sound sound)
        {
            PlaySound(sound);
            audioSource.loop = true;
        }

        public AudioSource GetAudioSource()
        {
            return audioSource;
        }
    }
}