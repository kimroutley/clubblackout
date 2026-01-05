using System;

namespace ClubBlackout {
    public enum RoleType { Dealer, PartyAnimal, Medic, Bouncer, TeaSpiller, Wallflower }

    [Serializable]
    public class Role {
        public RoleType Type;
        public string DisplayName;
        public bool IsAlive = true;

        public Role(RoleType type, string displayName=null) {
            Type = type;
            DisplayName = displayName ?? type.ToString();
        }
    }
}