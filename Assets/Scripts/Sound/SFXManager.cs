using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Sound
{
    public class SFXManager : MonoBehaviour
    {
        public enum SFX
        {
            Click,
        }

        [SerializeField] private SerializedDictionary<SFX, AudioClip> soundEffects;
        [SerializeField] private AudioSource sfxSourcePrefab;

        public void PlaySFX(SFX sfx, Vector3 position = default)
        {
            if (!soundEffects.TryGetValue(sfx, out AudioClip clip))
            {
                return;
            }

            AudioSource audioSource = Instantiate(sfxSourcePrefab, position, Quaternion.identity);
            audioSource.clip = clip;
            audioSource.Play();

            float clipDuration = clip.length;
            Destroy(audioSource.gameObject, clipDuration);
        }

        public void PlaySFXWithVolume(SFX sfx, float volume, Vector3 position = default)
        {
            if (!soundEffects.TryGetValue(sfx, out AudioClip clip))
            {
                return;
            }

            AudioSource audioSource = Instantiate(sfxSourcePrefab, position, Quaternion.identity);
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();

            float clipDuration = clip.length;
            Destroy(audioSource.gameObject, clipDuration);
        }
    }
}