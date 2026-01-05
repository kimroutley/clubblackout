#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace ClubBlackout.Editor {
    public static class PopulateRoleSpritesEditor {
        [MenuItem("Tools/ClubBlackout/Populate Role Sprite DB (auto)")]
        public static void Populate() {
            // Find or create asset
            const string assetPath = "Assets/Resources/role_sprites.asset";
            var db = AssetDatabase.LoadAssetAtPath<ClubBlackout.RoleSpriteDatabase>(assetPath);
            if (db == null) {
                db = ScriptableObject.CreateInstance<ClubBlackout.RoleSpriteDatabase>();
                db.entries = new ClubBlackout.RoleSpriteDatabase.Entry[System.Enum.GetValues(typeof(ClubBlackout.RoleType)).Length];
                AssetDatabase.CreateAsset(db, assetPath);
                AssetDatabase.SaveAssets();
                Debug.Log("Created RoleSpriteDatabase at " + assetPath);
            }

            foreach (ClubBlackout.RoleType rt in System.Enum.GetValues(typeof(ClubBlackout.RoleType))) {
                // try to find a sprite asset with name role_<lower>
                string search = "role_" + rt.ToString().ToLower();
                var guids = AssetDatabase.FindAssets(search + " t:Texture2D");
                Sprite sprite = null;
                if (guids != null && guids.Length > 0) {
                    var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                    if (sprite == null) {
                        // maybe imported as texture: try to load as Sprite via Texture2D and create Sprite
                        var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                        if (tex != null) {
                            sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                        }
                    }
                }

                int idx = (int)rt;
                if (db.entries == null || db.entries.Length != System.Enum.GetValues(typeof(ClubBlackout.RoleType)).Length)
                    db.entries = new ClubBlackout.RoleSpriteDatabase.Entry[System.Enum.GetValues(typeof(ClubBlackout.RoleType)).Length];

                if (db.entries[idx] == null) db.entries[idx] = new ClubBlackout.RoleSpriteDatabase.Entry();
                db.entries[idx].role = rt;
                db.entries[idx].sprite = sprite;

                Debug.LogFormat("Mapped role {0} -> sprite {1}", rt, sprite != null ? sprite.name : "(none)");
            }

            EditorUtility.SetDirty(db);
            AssetDatabase.SaveAssets();
            Debug.Log("Role sprite DB population complete.");
        }
    }
}
#endif