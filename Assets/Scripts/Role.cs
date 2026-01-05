using System;

namespace ClubBlackout {
    public enum RoleType { 
        Dealer, PartyAnimal, Medic, Bouncer, TeaSpiller, Wallflower,
        MessyBitch, Clinger, Whore, Creep, Roofi, Lightweight, 
        Predator, Minor, Sober, SeasonedDrinker, SecondWind, 
        DramaQueen, ClubManager, SilverFox, AllyCat
    }

    [Serializable]
    public class Role {
        public RoleType Type;
        public string DisplayName;
        public string Description;
        public bool IsAlive = true;

        public Role(RoleType type, string displayName=null, string description=null) {
            Type = type;
            DisplayName = displayName ?? type.ToString();
            Description = description ?? "";
        }
    }
}