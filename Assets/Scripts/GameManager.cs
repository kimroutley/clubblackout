using System.Collections.Generic;
using UnityEngine;

namespace ClubBlackout {
    public class Player {
        public string Id;
        public string Name;
        public Role Role;
        public Player(string id, string name, Role role) { Id = id; Name = name; Role = role; }
    }

    public class GameManager : MonoBehaviour {
        public List<Player> Players = new List<Player>();

        // Basic flow state
        public enum Phase { Setup, Night, Day, Finished }
        public Phase CurrentPhase = Phase.Setup;

        // Pass-and-play support
        public RoleUIController RoleUIController;
        public HostTools HostTools;
        public bool PassAndPlayMode = true;
        private int currentPrivateIndex = -1;
        private RoleView currentRoleView = null;

        void Start() {
            // Placeholder: initialize or load a saved game
            // Attempt to auto-populate role sprite database in editor (no-op at runtime)
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += () => {
                var db = UnityEditor.AssetDatabase.LoadAssetAtPath<RoleSpriteDatabase>("Assets/Resources/role_sprites.asset");
                if (db == null) {
                    var populate = typeof(ClubBlackout.Editor.PopulateRoleSpritesEditor);
                    UnityEditor.Menu.SetChecked("Tools/ClubBlackout/Populate Role Sprite DB (auto)", true);
                    UnityEditor.EditorApplication.ExecuteMenuItem("Tools/ClubBlackout/Populate Role Sprite DB (auto)");
                }
            };
            #endif
        }

        public void AssignRoles(List<RoleType> roles) {
            // Placeholder deterministic assignment for now
            Players.Clear();
            for (int i = 0; i < roles.Count; i++) {
                Players.Add(new Player(i.ToString(), "Player " + (i+1), new Role(roles[i])));
            }
        }

        public void DealRolesAndStartPassAndPlay(List<RoleType> roles) {
            AssignRoles(roles);
            currentPrivateIndex = -1;
            ShowNextPrivateRole();
        }

        public void ShowNextPrivateRole() {
            // Destroy previous view
            if (currentRoleView != null) Destroy(currentRoleView.gameObject);

            currentPrivateIndex++;
            if (currentPrivateIndex >= Players.Count) {
                EndPassAndPlayPrivateViews();
                return;
            }

            var role = Players[currentPrivateIndex].Role;
            if (RoleUIController != null) {
                currentRoleView = RoleUIController.SpawnRoleView(role);
            }
        }

        public void EndPassAndPlayPrivateViews() {
            if (currentRoleView != null) Destroy(currentRoleView.gameObject);
            currentRoleView = null;
            currentPrivateIndex = -1;
            StartNight();
        }

        public void StartNight() {
            CurrentPhase = Phase.Night;
            if (HostTools != null) HostTools.StartNightTimer();
            // TODO: invoke night resolver
        }

        public void StartDay() {
            CurrentPhase = Phase.Day;
            if (HostTools != null) HostTools.StartDayTimer();
            // TODO: start discussion timer, show votes UI
        }

        // Host manual overrides
        public void HostAdvanceToDay() {
            StartDay();
        }
        public void HostAdvanceToNight() {
            StartNight();
        }
    }
}