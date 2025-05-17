using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Sound
{
    /// <summary>
    /// This class is responsible for playing sound effects in the game.
    /// </summary>
    public class SfxManager : MonoBehaviour
    {
        public enum Sfx
        {
            Click,
        }

        [SerializeField] private SerializedDictionary<Sfx, AudioClip> soundEffects;
        [SerializeField] private AudioSource sfxSourcePrefab;
        private Transform sfxManagerTransform;

        private void Awake()
        {
            Assert.IsNotNull(sfxSourcePrefab);
            sfxManagerTransform = transform;
        }

        /// <summary>
        /// This method is used to instantiate an audio source and play the sound effect
        /// </summary>
        /// <param name="sfx"> the sfx that has to be played</param>
        /// <param name="volume"> the volume of the sfx </param>
        public void PlaySfx(Sfx sfx, float? volume = null)
        {
            if (!soundEffects.TryGetValue(sfx, out AudioClip clip))
            {
                return;
            }

            AudioSource audioSource = Instantiate(sfxSourcePrefab, sfxManagerTransform);
            if (volume.HasValue)
            {
                audioSource.volume = volume.Value;
            }

            audioSource.clip = clip;
            audioSource.Play();

            float clipDuration = clip.length;
            Destroy(audioSource.gameObject, clipDuration);
        }
    }
}