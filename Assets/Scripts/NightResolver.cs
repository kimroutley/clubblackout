using System.Collections.Generic;
using UnityEngine;

namespace ClubBlackout {
    public class NightAction {
        public RoleType Actor;
        public string TargetPlayerId;
        public string ActionType; // "protect", "kill", "check", "inspect", "roofi", "send_home"
    }

    // Responsible for resolving all night actions in deterministic order
    public class NightResolver : MonoBehaviour {
        public GameManager GameManager;
        private List<NightAction> pendingActions = new List<NightAction>();
        private bool soberUsedThisNight = false;
        private HashSet<string> roofiTargets = new HashSet<string>();
        private HashSet<string> protectedTargets = new HashSet<string>();
        private List<string> killTargets = new List<string>();

        public void StartNight() {
            pendingActions.Clear();
            soberUsedThisNight = false;
            roofiTargets.Clear();
            protectedTargets.Clear();
            killTargets.Clear();
        }

        public void AddAction(RoleType actor, string targetId, string actionType) {
            pendingActions.Add(new NightAction { Actor = actor, TargetPlayerId = targetId, ActionType = actionType });
        }

        public void ResolveNight(List<Player> players) {
            // Night order: Sober -> Roofi -> MedicProtect -> BouncerCheck -> ManagerInspect -> DealersChoose -> ResolveKills -> DeathTriggers
            Debug.Log("Resolving night for " + players.Count + " players");

            // 1. Sober
            var soberAction = pendingActions.Find(a => a.Actor == RoleType.Sober);
            if (soberAction != null) {
                soberUsedThisNight = true;
                protectedTargets.Add(soberAction.TargetPlayerId);
                Debug.Log("Sober sent player " + soberAction.TargetPlayerId + " home. No kills this night.");
            }

            // 2. Roofi
            var roofiAction = pendingActions.Find(a => a.Actor == RoleType.Roofi);
            if (roofiAction != null) {
                roofiTargets.Add(roofiAction.TargetPlayerId);
                Debug.Log("Roofi paralyzed player " + roofiAction.TargetPlayerId);
            }

            // 3. Medic Protect
            var medicAction = pendingActions.Find(a => a.Actor == RoleType.Medic && a.ActionType == "protect");
            if (medicAction != null) {
                protectedTargets.Add(medicAction.TargetPlayerId);
                Debug.Log("Medic protected player " + medicAction.TargetPlayerId);
            }

            // 4. Bouncer Check (handled by host/UI, not in resolver)
            // 5. Manager Inspect (handled by host/UI, not in resolver)

            // 6. Dealers Choose
            if (!soberUsedThisNight) {
                var dealerActions = pendingActions.FindAll(a => a.Actor == RoleType.Dealer && a.ActionType == "kill");
                if (dealerActions.Count > 0) {
                    // All dealers must agree on a target; take the first for now
                    killTargets.Add(dealerActions[0].TargetPlayerId);
                }
            }

            // 7. Resolve Kills
            foreach (var targetId in killTargets) {
                if (protectedTargets.Contains(targetId)) {
                    Debug.Log("Player " + targetId + " was protected and survived.");
                    continue;
                }
                var player = players.Find(p => p.Id == targetId);
                if (player != null && player.Role.IsAlive) {
                    // Check for Minor (needs Bouncer ID first)
                    // Check for SeasonedDrinker (has extra lives)
                    // Check for AllyCat (9 lives)
                    // For MVP, just kill them
                    player.Role.IsAlive = false;
                    Debug.Log("Player " + targetId + " (" + player.Role.DisplayName + ") was killed.");
                    ProcessDeathTriggers(player, players);
                }
            }

            pendingActions.Clear();
        }

        void ProcessDeathTriggers(Player deadPlayer, List<Player> allPlayers) {
            // Tea-Spiller: expose one player
            if (deadPlayer.Role.Type == RoleType.TeaSpiller) {
                Debug.Log("Tea-Spiller died! They can now expose one player's alignment.");
                // TODO: trigger UI prompt for Tea-Spiller to choose a player to expose
            }

            // Predator: take one voter down
            if (deadPlayer.Role.Type == RoleType.Predator) {
                Debug.Log("Predator died! They can choose one voter to kill.");
                // TODO: trigger UI prompt
            }

            // Drama Queen: swap two cards
            if (deadPlayer.Role.Type == RoleType.DramaQueen) {
                Debug.Log("Drama Queen died! They can swap two cards later.");
                // TODO: mark for later night activation
            }

            // Second Wind: offer conversion to Dealers
            if (deadPlayer.Role.Type == RoleType.SecondWind) {
                Debug.Log("Second Wind died! Dealers can choose to convert them.");
                // TODO: trigger dealer decision UI
            }
        }

        public bool CheckVictory(List<Player> players) {
            int dealersAlive = 0;
            int partyAnimalsAlive = 0;

            foreach (var p in players) {
                if (!p.Role.IsAlive) continue;
                if (p.Role.Type == RoleType.Dealer) dealersAlive++;
                else partyAnimalsAlive++;
            }

            if (dealersAlive == 0) {
                Debug.Log("Victory: Party Animals win!");
                ShowVictory(true);
                return true;
            }
            if (dealersAlive >= partyAnimalsAlive) {
                Debug.Log("Victory: Dealers win!");
                ShowVictory(false);
                return true;
            }
            return false;
        }

        void ShowVictory(bool partyAnimalsWin) {
            if (GameManager != null && GameManager.VictoryScreen != null) {
                GameManager.CurrentPhase = GameManager.Phase.Finished;
                GameManager.VictoryScreen.ShowVictory(partyAnimalsWin, GameManager.NightsPlayed);
            }
        }
    }
}