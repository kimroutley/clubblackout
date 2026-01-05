using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ClubBlackout {
    public class MainMenu : MonoBehaviour {
        public Button NewGameButton;
        public Button SettingsButton;
        public Button PlayerGuidesButton;
        public Button ExitButton;

        void Start() {
            if (NewGameButton != null) NewGameButton.onClick.AddListener(OnNewGame);
            if (SettingsButton != null) SettingsButton.onClick.AddListener(OnSettings);
            if (PlayerGuidesButton != null) PlayerGuidesButton.onClick.AddListener(OnPlayerGuides);
            if (ExitButton != null) ExitButton.onClick.AddListener(OnExit);
        }

        void OnDestroy() {
            if (NewGameButton != null) NewGameButton.onClick.RemoveListener(OnNewGame);
            if (SettingsButton != null) SettingsButton.onClick.RemoveListener(OnSettings);
            if (PlayerGuidesButton != null) PlayerGuidesButton.onClick.RemoveListener(OnPlayerGuides);
            if (ExitButton != null) ExitButton.onClick.RemoveListener(OnExit);
        }

        void OnNewGame() {
            // Load game setup scene or start role selection
            SceneManager.LoadScene("GameSetup");
        }

        void OnSettings() {
            var settingsPanel = GameObject.Find("SettingsPanel");
            if (settingsPanel != null) settingsPanel.SetActive(true);
        }

        void OnPlayerGuides() {
            var guidesPanel = GameObject.Find("PlayerGuidesPanel");
            if (guidesPanel != null) guidesPanel.SetActive(true);
        }

        void OnExit() {
            Application.Quit();
        }
    }
}