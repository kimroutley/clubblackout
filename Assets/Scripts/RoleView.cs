using UnityEngine;
using UnityEngine.UI;

namespace ClubBlackout {
    [RequireComponent(typeof(CanvasGroup))]
    public class RoleView : MonoBehaviour {
        public Image RoleImage;
        public Text RoleNameText;
        public CanvasGroup CanvasGroup;

        void Reset() {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show(Role role, Sprite sprite) {
            if (RoleImage != null) RoleImage.sprite = sprite;
            if (RoleNameText != null) RoleNameText.text = role.DisplayName;
            if (CanvasGroup != null) CanvasGroup.alpha = 1f; CanvasGroup.blocksRaycasts = true; CanvasGroup.interactable = true;
        }

        public void Hide() {
            if (CanvasGroup != null) CanvasGroup.alpha = 0f; CanvasGroup.blocksRaycasts = false; CanvasGroup.interactable = false;
        }
    }
}