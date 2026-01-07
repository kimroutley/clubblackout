using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ClubBlackout {
    public class PlayerGuideViewer : MonoBehaviour {
        public Image GuideImage;
        public Text TitleText;
        public Text DescriptionText;
        private List<PlayerGuideData> guides = new List<PlayerGuideData>();
        private int currentIndex = 0;

        void Start() {
            LoadGuides();
            ShowGuide(0);
        }

        void LoadGuides() {
            var json = Resources.Load<TextAsset>("player_guides");
            if (json != null) {
                var data = JsonUtility.FromJson<PlayerGuidesRoot>(json.text);
                if (data != null && data.guides != null) guides = data.guides;
            }
        }

        public void ShowGuide(int index) {
            if (guides == null || guides.Count == 0) return;
            currentIndex = Mathf.Clamp(index, 0, guides.Count - 1);
            var guide = guides[currentIndex];
            
            if (TitleText != null) TitleText.text = guide.title;
            if (DescriptionText != null) DescriptionText.text = guide.description;
            
            // Load sprite from Assets root (not Resources, so use direct path or Resources.Load if moved)
            var sprite = Resources.Load<Sprite>(System.IO.Path.GetFileNameWithoutExtension(guide.image));
            if (sprite != null && GuideImage != null) GuideImage.sprite = sprite;
        }

        public void NextGuide() {
            ShowGuide(currentIndex + 1);
        }

        public void PreviousGuide() {
            ShowGuide(currentIndex - 1);
        }
    }
}