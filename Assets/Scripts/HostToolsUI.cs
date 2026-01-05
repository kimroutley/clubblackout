using UnityEngine;
using UnityEngine.UI;

namespace ClubBlackout {
    public class HostToolsUI : MonoBehaviour {
        public HostTools HostTools;
        public Button AdvanceToNightButton;
        public Button AdvanceToDayButton;

        void Start() {
            if (AdvanceToNightButton != null) AdvanceToNightButton.onClick.AddListener(OnNightClicked);
            if (AdvanceToDayButton != null) AdvanceToDayButton.onClick.AddListener(OnDayClicked);
        }

        void OnDestroy() {
            if (AdvanceToNightButton != null) AdvanceToNightButton.onClick.RemoveListener(OnNightClicked);
            if (AdvanceToDayButton != null) AdvanceToDayButton.onClick.RemoveListener(OnDayClicked);
        }

        public void OnNightClicked() {
            if (HostTools != null) HostTools.HostAdvanceToNight();
        }

        public void OnDayClicked() {
            if (HostTools != null) HostTools.HostAdvanceToDay();
        }
    }
}