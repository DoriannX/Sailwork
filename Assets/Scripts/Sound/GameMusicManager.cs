using SailorSystems.Sound;
using UnityEngine;
using static SailorSystems.Sound.SoundManager.Sound;

namespace Sound
{
    [RequireComponent(typeof(SoundManager))]
    public class GameMusicManager : MonoBehaviour
    {
        private SoundManager soundManager;
        private int currentMusicIndex = 1;
        private readonly SoundManager.Sound[] ambientTracks = { AmbientMusic1, AmbientMusic2, AmbientMusic3 };

        private void Awake()
        {
            soundManager = GetComponent<SoundManager>();
        }

        private void Start()
        {
            PlayNextTrack();
        }

        private void PlayNextTrack()
        {
            SoundManager.Sound nextTrack = ambientTracks[currentMusicIndex - 1];
            AudioClip clip = soundManager.PlaySound(nextTrack);
            
            if (clip != null)
            {
                StartCoroutine(FadeAudioSource(2.0f, .05f));
                
                float playTime = clip.length - 3.0f;
                Invoke(nameof(StartFadeOut), playTime);
                Invoke(nameof(PlayNextTrack), clip.length);
            }
            
            currentMusicIndex = currentMusicIndex % ambientTracks.Length + 1;
        }
        
        private void StartFadeOut()
        {
            StartCoroutine(FadeAudioSource(1.0f, 0.0f)); 
        }
        
        private System.Collections.IEnumerator FadeAudioSource(float duration, float targetVolume)
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