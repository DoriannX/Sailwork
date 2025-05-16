using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button creditButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private CanvasGroup creditPanel;

        private void Awake()
        {
            ToggleCreditPanel(false);
            startButton.onClick.AddListener(StartGame);
            creditButton.onClick.AddListener(OpenCreditPanel);
            quitButton.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void OpenCreditPanel()
        {
            ToggleCreditPanel(true);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void ToggleCreditPanel(bool state)
        {
            creditPanel.alpha = state ? 1 : 0;
            creditPanel.blocksRaycasts = state;
            creditPanel.interactable = state;
        }
    }
}