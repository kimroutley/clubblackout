using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ClubBlackout {
    public class VoteController : MonoBehaviour {
        public GameManager GameManager;
        public Text VotePromptText;
        public Transform VoteButtonContainer;
        public Button VoteButtonPrefab;

        private Dictionary<string, int> votes = new Dictionary<string, int>();
        private int votesSubmitted = 0;

        public void StartVoting() {
            votes.Clear();
            votesSubmitted = 0;
            SpawnVoteButtons();
            if (VotePromptText != null) VotePromptText.text = "Vote to eliminate a player:";
        }

        void SpawnVoteButtons() {
            // Clear existing buttons
            foreach (Transform child in VoteButtonContainer) Destroy(child.gameObject);

            // Create vote button for each living player
            foreach (var player in GameManager.Players) {
                if (!player.Role.IsAlive) continue;
                var btn = Instantiate(VoteButtonPrefab, VoteButtonContainer);
                var btnText = btn.GetComponentInChildren<Text>();
                if (btnText != null) btnText.text = player.Name;
                
                string playerId = player.Id;
                btn.onClick.AddListener(() => OnVoteClicked(playerId));
            }
        }

        void OnVoteClicked(string playerId) {
            if (!votes.ContainsKey(playerId)) votes[playerId] = 0;
            votes[playerId]++;
            votesSubmitted++;

            Debug.Log("Vote cast for player " + playerId + ". Total votes for this player: " + votes[playerId]);

            // Check if all players voted
            int alivePlayers = GameManager.Players.FindAll(p => p.Role.IsAlive).Count;
            if (votesSubmitted >= alivePlayers) {
                ResolveVotes();
            }
        }

        void ResolveVotes() {
            // Find player with most votes
            string eliminatedId = null;
            int maxVotes = 0;
            foreach (var kvp in votes) {
                if (kvp.Value > maxVotes) {
                    maxVotes = kvp.Value;
                    eliminatedId = kvp.Key;
                }
            }

            if (eliminatedId != null) {
                var player = GameManager.Players.Find(p => p.Id == eliminatedId);
                if (player != null) {
                    player.Role.IsAlive = false;
                    Debug.Log("Player " + player.Name + " (" + player.Role.DisplayName + ") was voted out with " + maxVotes + " votes.");
                    // TODO: trigger death effects (Predator, Tea-Spiller, etc.)
                }
            }

            // Check victory
            var nightResolver = FindObjectOfType<NightResolver>();
            if (nightResolver != null && nightResolver.CheckVictory(GameManager.Players)) {
                GameManager.CurrentPhase = GameManager.Phase.Finished;
            } else {
                GameManager.StartNight();
            }
        }
    }
}