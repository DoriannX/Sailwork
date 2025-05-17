using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// This class is responsible for managing the main menu.
    /// </summary>
    public class MainMenuManager : MonoBehaviour
    {
        #region Buttons

        [Header("Buttons")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button creditButton;
        [SerializeField] private Button quitButton;

        #endregion
        [Space]
        [SerializeField] private CanvasGroup creditPanel;

        private void Awake()
        {
            Assert.IsNotNull(startButton);
            Assert.IsNotNull(creditButton);
            Assert.IsNotNull(quitButton);
            Assert.IsNotNull(creditPanel);
            ToggleCreditPanel(false);
            InitButtons();
        }

        /// <summary>
        /// This method is used to add behaviours to the buttons
        /// </summary>
        private void InitButtons()
        {
            startButton.onClick.AddListener(StartGame);
            creditButton.onClick.AddListener(() => ToggleCreditPanel(true));
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
        /// This method is used to go to the next scene : game scene
        /// </summary>
        private static void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        /// <summary>
        /// This method is used to toggle on or off the canvas panel by changing its alpha value and removing
        /// any interactions with it
        /// </summary>
        private void ToggleCreditPanel(bool state)
        {
            creditPanel.alpha = state ? 1 : 0;
            creditPanel.blocksRaycasts = state;
            creditPanel.interactable = state;
        }
    }
}