using UnityEngine;
using UnityEngine.UI;

namespace ClubBlackout {
    public class SettingsPanel : MonoBehaviour {
        public Slider VolumeSlider;
        public Toggle AnalyticsToggle;
        public Button CloseButton;

        void Start() {
            if (VolumeSlider != null) {
                VolumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.8f);
                VolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            }
            if (AnalyticsToggle != null) {
                AnalyticsToggle.isOn = PlayerPrefs.GetInt("Analytics", 1) == 1;
                AnalyticsToggle.onValueChanged.AddListener(OnAnalyticsChanged);
            }
            if (CloseButton != null) CloseButton.onClick.AddListener(OnClose);
        }

        void OnDestroy() {
            if (VolumeSlider != null) VolumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
            if (AnalyticsToggle != null) AnalyticsToggle.onValueChanged.RemoveListener(OnAnalyticsChanged);
            if (CloseButton != null) CloseButton.onClick.RemoveListener(OnClose);
        }

        void OnVolumeChanged(float value) {
            PlayerPrefs.SetFloat("Volume", value);
            AudioListener.volume = value;
        }

        void OnAnalyticsChanged(bool value) {
            PlayerPrefs.SetInt("Analytics", value ? 1 : 0);
            // TODO: enable/disable Firebase Analytics
        }

        void OnClose() {
            gameObject.SetActive(false);
        }
    }
}