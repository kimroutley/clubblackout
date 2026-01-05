using UnityEngine;

namespace ClubBlackout {
    [CreateAssetMenu(fileName = "RoleSpriteDatabase", menuName = "ClubBlackout/Role Sprite Database")]
    public class RoleSpriteDatabase : ScriptableObject {
        [System.Serializable]
        public class Entry { public RoleType role; public Sprite sprite; }
        public Entry[] entries;

        public Sprite GetSprite(RoleType role) {
            if (entries == null) return null;
            for (int i = 0; i < entries.Length; i++) {
                if (entries[i] != null && entries[i].role == role) return entries[i].sprite;
            }
            return null;
        }
    }
}