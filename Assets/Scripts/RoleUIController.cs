using UnityEngine;

namespace ClubBlackout {
    public class RoleUIController : MonoBehaviour {
        public RoleSpriteDatabase SpriteDB;
        public RoleView RoleViewPrefab;

        public RoleView SpawnRoleView(Role role) {
            var view = Instantiate(RoleViewPrefab, transform);
            var sprite = SpriteDB != null ? SpriteDB.GetSprite(role.Type) : null;
            view.Show(role, sprite);
            return view;
        }
    }
}