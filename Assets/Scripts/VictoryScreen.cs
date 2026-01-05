using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ClubBlackout {
    public class VictoryScreen : MonoBehaviour {
        public Text VictoryTitleText;
        public Text VictorySummaryText;
        public Button PlayAgainButton;
        public Button MainMenuButton;

        void Start() {
            if (PlayAgainButton != null) PlayAgainButton.onClick.AddListener(OnPlayAgain);
            if (MainMenuButton != null) MainMenuButton.onClick.AddListener(OnMainMenu);
        }

        void OnDestroy() {
            if (PlayAgainButton != null) PlayAgainButton.onClick.RemoveListener(OnPlayAgain);
            if (MainMenuButton != null) MainMenuButton.onClick.RemoveListener(OnMainMenu);
        }

        public void ShowVictory(bool partyAnimalsWin, int nightsPlayed) {
            gameObject.SetActive(true);
            if (VictoryTitleText != null) {
                VictoryTitleText.text = partyAnimalsWin ? "Party Animals Win!" : "Dealers Win!";
            }
            if (VictorySummaryText != null) {
                VictorySummaryText.text = string.Format("Game lasted {0} nights.", nightsPlayed);
            }
        }

        void OnPlayAgain() {
            SceneManager.LoadScene("GameSetup");
        }

        void OnMainMenu() {
            SceneManager.LoadScene("MainMenu");
        }
    }
}