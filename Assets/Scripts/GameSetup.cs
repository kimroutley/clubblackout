using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ClubBlackout {
    public class GameSetup : MonoBehaviour {
        public InputField PlayerCountInput;
        public Dropdown RoleSelectionDropdown;
        public Button AddRoleButton;
        public Button StartGameButton;
        public Text SelectedRolesText;

        private List<RoleType> selectedRoles = new List<RoleType>();

        void Start() {
            if (AddRoleButton != null) AddRoleButton.onClick.AddListener(OnAddRole);
            if (StartGameButton != null) StartGameButton.onClick.AddListener(OnStartGame);
            
            PopulateRoleDropdown();
            UpdateRoleList();
        }

        void OnDestroy() {
            if (AddRoleButton != null) AddRoleButton.onClick.RemoveListener(OnAddRole);
            if (StartGameButton != null) StartGameButton.onClick.RemoveListener(OnStartGame);
        }

        void PopulateRoleDropdown() {
            if (RoleSelectionDropdown == null) return;
            RoleSelectionDropdown.ClearOptions();
            List<string> options = new List<string>();
            foreach (RoleType role in System.Enum.GetValues(typeof(RoleType))) {
                options.Add(role.ToString());
            }
            RoleSelectionDropdown.AddOptions(options);
        }

        void OnAddRole() {
            if (RoleSelectionDropdown == null) return;
            int index = RoleSelectionDropdown.value;
            RoleType selectedRole = (RoleType)index;
            selectedRoles.Add(selectedRole);
            UpdateRoleList();
        }

        void UpdateRoleList() {
            if (SelectedRolesText == null) return;
            string text = "Selected Roles:\n";
            foreach (var role in selectedRoles) {
                text += role.ToString() + "\n";
            }
            SelectedRolesText.text = text;
        }

        void OnStartGame() {
            // Validate: must have at least 1 Dealer and 2 Party Animals/others
            int dealers = selectedRoles.FindAll(r => r == RoleType.Dealer).Count;
            if (dealers == 0 || selectedRoles.Count < 3) {
                Debug.LogWarning("Invalid role configuration. Need at least 1 Dealer and 3+ total players.");
                return;
            }

            // Save selected roles and load game scene
            PlayerPrefs.SetString("SelectedRoles", string.Join(",", selectedRoles));
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }
}