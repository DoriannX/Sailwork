using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Project.Scripts.Runtime.Sound
{
    /// <summary>
    /// This class is responsible for playing sound effects in the game.
    /// </summary>
    public class SfxManager : MonoBehaviour
    {
        public enum Sfx
        {
            Click,
            Ding,
            Snore,
        }

        [SerializeField] private SerializedDictionary<Sfx, AudioClip> soundEffects;
        private Transform sfxManagerTransform;

        private void Awake()
        {
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

            var audioSourceObj = new GameObject("SFX_" + sfx)
            {
                transform =
                {
                    parent = sfxManagerTransform
                }
            };
            
            var audioSource = audioSourceObj.AddComponent<AudioSource>();
            
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