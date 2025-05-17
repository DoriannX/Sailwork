using Project.Scripts.Runtime.Core;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// This class is responsible for managing the pause menu.
    /// </summary>
    public class PauseMenuManager : MonoBehaviour
    {
        #region Buttons

        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;

        #endregion
        private CanvasGroup pauseMenu;
#pragma warning disable CS0108, CS0114
        private bool enabled;
#pragma warning restore CS0108, CS0114

        private void Awake()
        {
            Assert.IsNotNull(resumeButton);
            Assert.IsNotNull(restartButton);
            Assert.IsNotNull(quitButton);
            pauseMenu = GetComponent<CanvasGroup>();
            TogglePauseMenu(false);
            InitButtons();
            InputManager.onPauseInputTriggered += TogglePauseMenu;
        }

        /// <summary>
        /// This method is used to add behaviours to the buttons
        /// </summary>
        private void InitButtons()
        {
            resumeButton.onClick.AddListener(ResumeGame);
            restartButton.onClick.AddListener(RestartGame);
            quitButton.onClick.AddListener(QuitGame);
        }

        /// <summary>
        /// This method is used to quit the game.
        /// </summary>
        private void QuitGame()
        {
            Application.Quit();
            //This is used to stop the game in the editor
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        /// <summary>
        /// This method is used to reload the current scene.
        /// </summary>
        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// This method is used to resume the game by calling the InputManager
        /// </summary>
        private void ResumeGame()
        {
            TogglePauseMenu(true);
            InputManager.TriggerPauseInput();
        }
        
        /// <summary>
        /// This is an override function that is called when the game is paused or unpaused.
        /// </summary>
        private void TogglePauseMenu()
        {
            TogglePauseMenu(!enabled);
        }

        /// <summary>
        /// This function is used to toggle the pause menu on or off by changing its alpha value and removing
        /// any interactions with it.
        /// </summary>
        /// <param name="state"></param>
        private void TogglePauseMenu(bool state)
        {
            enabled = state;
            pauseMenu.alpha = state ? 1 : 0;
            pauseMenu.blocksRaycasts = state;
            pauseMenu.interactable = state;
        }

        /// <summary>
        /// Clears the static event
        /// </summary>
        private void OnDestroy()
        {
            InputManager.onPauseInputTriggered -= TogglePauseMenu;
        }
    }
}