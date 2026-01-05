using System.Collections.Generic;
using UnityEngine;

namespace ClubBlackout {
    // Responsible for resolving all night actions in deterministic order
    public class NightResolver : MonoBehaviour {
        public void ResolveNight(List<Player> players) {
            // Suggested order (simplified): Sober -> Roofi -> Medic/Protect -> Bouncer -> Manager -> Dealers choose -> Resolve kills
            // This is a placeholder; full implementation will follow the game spec
            Debug.Log("Resolving night for " + players.Count + " players");
        }
    }
}