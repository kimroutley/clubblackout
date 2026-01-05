using NUnit.Framework;
using UnityEngine;
using ClubBlackout;
using System.Collections.Generic;

public class GameplayTests {
    [Test]
    public void NightResolver_DealerKillsTarget_TargetDies() {
        var go = new GameObject("NightResolver");
        var resolver = go.AddComponent<NightResolver>();
        
        var players = new List<Player> {
            new Player("0", "P1", new Role(RoleType.Dealer)),
            new Player("1", "P2", new Role(RoleType.PartyAnimal)),
            new Player("2", "P3", new Role(RoleType.Medic))
        };

        resolver.StartNight();
        resolver.AddAction(RoleType.Dealer, "1", "kill");
        resolver.ResolveNight(players);

        Assert.IsFalse(players[1].Role.IsAlive, "Target should be killed");
    }

    [Test]
    public void NightResolver_MedicProtectsTarget_TargetSurvives() {
        var go = new GameObject("NightResolver");
        var resolver = go.AddComponent<NightResolver>();
        
        var players = new List<Player> {
            new Player("0", "P1", new Role(RoleType.Dealer)),
            new Player("1", "P2", new Role(RoleType.PartyAnimal)),
            new Player("2", "P3", new Role(RoleType.Medic))
        };

        resolver.StartNight();
        resolver.AddAction(RoleType.Medic, "1", "protect");
        resolver.AddAction(RoleType.Dealer, "1", "kill");
        resolver.ResolveNight(players);

        Assert.IsTrue(players[1].Role.IsAlive, "Target should survive with Medic protection");
    }

    [Test]
    public void VictoryCheck_AllDealersDead_PartyAnimalsWin() {
        var go = new GameObject("NightResolver");
        var resolver = go.AddComponent<NightResolver>();
        
        var players = new List<Player> {
            new Player("0", "P1", new Role(RoleType.Dealer) { IsAlive = false }),
            new Player("1", "P2", new Role(RoleType.PartyAnimal)),
            new Player("2", "P3", new Role(RoleType.Medic))
        };

        bool victory = resolver.CheckVictory(players);
        Assert.IsTrue(victory, "Party Animals should win when all dealers are dead");
    }

    [Test]
    public void VictoryCheck_DealersReachParity_DealersWin() {
        var go = new GameObject("NightResolver");
        var resolver = go.AddComponent<NightResolver>();
        
        var players = new List<Player> {
            new Player("0", "P1", new Role(RoleType.Dealer)),
            new Player("1", "P2", new Role(RoleType.PartyAnimal) { IsAlive = false }),
            new Player("2", "P3", new Role(RoleType.Medic) { IsAlive = false })
        };

        bool victory = resolver.CheckVictory(players);
        Assert.IsTrue(victory, "Dealers should win when they reach parity");
    }
}