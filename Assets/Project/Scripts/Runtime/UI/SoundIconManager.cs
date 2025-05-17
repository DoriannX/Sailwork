using UnityEngine;
using UnityEngine.SceneManagement;
using static Project.Scripts.Runtime.UI.SoundIconManager.State;

namespace Project.Scripts.Runtime.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SoundIconManager : MonoBehaviour
    {
        public enum State
        {
            In,
            Idle,
            Out
        }
        private CanvasGroup canvasGroup;
        private State state;
        [SerializeField] private float fadeSpeed = 1f;
        
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
        }

        private void Start()
        {
            state = In;
        }
        
        private void Update()
        {
            Fade(state);
        }

        private void Fade(State fadeIn)
        {
            if (fadeIn == In)
            {
                canvasGroup.alpha += Time.deltaTime * fadeSpeed;
                if (canvasGroup.alpha >= 1)
                {
                    state = Idle;
                    Invoke(nameof(FadeOut), 3f);
                }
            }
            else if (fadeIn == Out)
            {
                canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
                if (canvasGroup.alpha > 0)
                {
                    return;
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        private void FadeOut()
        {
            state = Out;
        }
    }
}
