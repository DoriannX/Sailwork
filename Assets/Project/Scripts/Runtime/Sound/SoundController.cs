using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Project.Scripts.Runtime.Sound
{
    /// <summary>
    /// This class is responsible for playing all the sounds in the game.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundController : MonoBehaviour
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

        /// <summary>
        /// This method is used to play a sound and chose whether to loop it or not.
        /// </summary>
        /// <param name="sound"> the sound that has to be played </param>
        /// <param name="loop"></param>
        /// <returns> the audio clip used to play the sound </returns>
        public AudioClip PlaySound(Sound sound, bool loop = false)
        {
            if (!sounds.TryGetValue(sound, out AudioClip clip))
            {
                return null;
            }
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
            return clip;
        }

        public AudioSource GetAudioSource()
        {
            return audioSource;
        }
    }
}