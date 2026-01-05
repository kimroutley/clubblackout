using UnityEngine;
using UnityEngine.UI;

namespace ClubBlackout {
    public class PauseMenu : MonoBehaviour {
        public GameObject PausePanel;
        public Button ResumeButton;
        public Button SettingsButton;
        public Button MainMenuButton;

        private bool isPaused = false;

        void Start() {
            if (PausePanel != null) PausePanel.SetActive(false);
            if (ResumeButton != null) ResumeButton.onClick.AddListener(OnResume);
            if (SettingsButton != null) SettingsButton.onClick.AddListener(OnSettings);
            if (MainMenuButton != null) MainMenuButton.onClick.AddListener(OnMainMenu);
        }

        void OnDestroy() {
            if (ResumeButton != null) ResumeButton.onClick.RemoveListener(OnResume);
            if (SettingsButton != null) SettingsButton.onClick.RemoveListener(OnSettings);
            if (MainMenuButton != null) MainMenuButton.onClick.RemoveListener(OnMainMenu);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                TogglePause();
            }
        }

        public void TogglePause() {
            isPaused = !isPaused;
            if (PausePanel != null) PausePanel.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }

        void OnResume() {
            TogglePause();
        }

        void OnSettings() {
            var settingsPanel = GameObject.Find("SettingsPanel");
            if (settingsPanel != null) settingsPanel.SetActive(true);
        }

        void OnMainMenu() {
            Time.timeScale = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}