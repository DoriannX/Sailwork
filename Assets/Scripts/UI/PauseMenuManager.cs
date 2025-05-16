using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;
        private CanvasGroup pauseMenu;
        private bool enabled;

        private void Awake()
        {
            pauseMenu = GetComponent<CanvasGroup>();
            TogglePauseMenu(false);
            resumeButton.onClick.AddListener(ResumeGame);
            restartButton.onClick.AddListener(RestartGame);
            quitButton.onClick.AddListener(QuitGame);
            InputManager.onPauseInputTriggered += TogglePauseMenu;
        }

        private void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void ResumeGame()
        {
            TogglePauseMenu(true);
            InputManager.TriggerPauseInput();
        }

        private void TogglePauseMenu()
        {
            TogglePauseMenu(!enabled);
        }

        private void TogglePauseMenu(bool state)
        {
            enabled = state;
            pauseMenu.alpha = state ? 1 : 0;
            pauseMenu.blocksRaycasts = state;
            pauseMenu.interactable = state;
        }

        private void OnDestroy()
        {
            InputManager.onPauseInputTriggered -= TogglePauseMenu;
        }
    }
}