using System.Collections;
using UnityEngine;
using static Sound.SoundManager.Sound;

namespace Sound
{
    /// <summary>
    /// This class is responsible for managing the game music.
    /// </summary>
    [RequireComponent(typeof(SoundManager))]
    public class GameMusicManager : MonoBehaviour
    {
        private SoundManager soundManager;
        private int currentMusicIndex;
        private readonly SoundManager.Sound[] ambientTracks = { AmbientMusic1, AmbientMusic2, AmbientMusic3 };

        private void Awake()
        {
            soundManager = GetComponent<SoundManager>();
        }

        private void Start()
        {
            PlayNextTrack();
        }

        /// <summary>
        /// This method fade in the next track in the ambient tracks array and fades out the previous track.
        /// </summary>
        private void PlayNextTrack()
        {
            SoundManager.Sound nextTrack = ambientTracks[currentMusicIndex];
            AudioClip clip = soundManager.PlaySound(nextTrack);
            
            if (clip != null)
            {
                StartCoroutine(FadeAudioSource(2.0f, .05f));
                
                float playTime = clip.length - 3.0f;
                Invoke(nameof(StartFadeOut), playTime);
                Invoke(nameof(PlayNextTrack), clip.length);
            }
            
            // Increment the index to play the next track
            currentMusicIndex = currentMusicIndex % ambientTracks.Length + 1;
        }
        
        private void StartFadeOut()
        {
            StartCoroutine(FadeAudioSource(1.0f, 0.0f)); 
        }
        
        /// <summary>
        /// this method is used to fade the audio source volume from the current volume to the target volume over a specified duration.
        /// </summary>
        private IEnumerator FadeAudioSource(float duration, float targetVolume)
        {
            AudioSource audioSource = soundManager.GetAudioSource();
            float currentTime = 0;
            float start = audioSource.volume;
            
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
            
            audioSource.volume = targetVolume;
        }
    }
}