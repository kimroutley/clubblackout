using UnityEngine;
using UnityEngine.UI;

namespace ClubBlackout {
    public class PassAndPlayController : MonoBehaviour {
        public GameManager GameManager;
        public Button NextButton;
        public Text InfoText;

        void Start() {
            if (NextButton != null) NextButton.onClick.AddListener(OnNextClicked);
        }

        void OnDestroy() {
            if (NextButton != null) NextButton.onClick.RemoveListener(OnNextClicked);
        }

        public void OnNextClicked() {
            GameManager.ShowNextPrivateRole();
            UpdateInfo();
        }

        public void UpdateInfo() {
            if (GameManager == null || GameManager.Players == null) return;
            int idx = Mathf.Max(0, GameManager.Players.Count > 0 ? Mathf.Min(GameManager.Players.Count, GameManager.Players.Count) : 0);
            if (InfoText != null) InfoText.text = "Pass device to next player and press " + (GameManager.Players.Count > 0 ? "Next" : "Done");
        }
    }
}