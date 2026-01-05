using UnityEngine;
using UnityEngine.UI;

namespace ClubBlackout {
    public class BackgroundController : MonoBehaviour {
        public Image BackgroundImage;
        
        void Start() {
            LoadBackground();
        }

        void LoadBackground() {
            var bgSprite = Resources.Load<Sprite>("background");
            if (bgSprite != null && BackgroundImage != null) {
                BackgroundImage.sprite = bgSprite;
            }
        }
    }
}