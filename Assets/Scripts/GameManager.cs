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

        void Start() {
            // Placeholder: initialize or load a saved game
            // Attempt to auto-populate role sprite database in editor (no-op at runtime)
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += () => {
                var db = UnityEditor.AssetDatabase.LoadAssetAtPath<RoleSpriteDatabase>("Assets/Resources/role_sprites.asset");
                if (db == null) {
                    // try to create/populate it automatically
                    var populate = typeof(ClubBlackout.Editor.PopulateRoleSpritesEditor);
                    // call the menu command via EditorUtility
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

        public void StartNight() {
            CurrentPhase = Phase.Night;
            // invoke night resolver
        }

        public void StartDay() {
            CurrentPhase = Phase.Day;
            // start discussion timer, show votes UI
        }
    }
}