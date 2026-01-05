using UnityEngine;
using System.Collections.Generic;

namespace ClubBlackout {
    public class RoleDescriptionLoader : MonoBehaviour {
        private static Dictionary<string, string> roleDescriptions = null;

        public static void LoadDescriptions() {
            if (roleDescriptions != null) return;
            roleDescriptions = new Dictionary<string, string>();

            var json = Resources.Load<TextAsset>("player_guides");
            if (json != null) {
                var data = JsonUtility.FromJson<PlayerGuidesRoot>(json.text);
                if (data != null && data.roleDescriptions != null) {
                    roleDescriptions = data.roleDescriptions;
                }
            }
        }

        public static string GetDescription(RoleType role) {
            LoadDescriptions();
            if (roleDescriptions != null && roleDescriptions.ContainsKey(role.ToString())) {
                return roleDescriptions[role.ToString()];
            }
            return "No description available.";
        }
    }

    [System.Serializable]
    public class PlayerGuidesRoot {
        public List<PlayerGuideData> guides;
        public Dictionary<string, string> roleDescriptions;
    }

    [System.Serializable]
    public class PlayerGuideData {
        public string title;
        public string image;
        public string description;
    }
}