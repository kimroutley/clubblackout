using NUnit.Framework;
using UnityEngine;
using ClubBlackout;
using System.Collections.Generic;

public class PassAndPlayTests {
    [Test]
    public void DealRolesAndStartPassAndPlay_ShouldSetIndexAndShowFirst() {
        var gmGO = new GameObject("GM");
        var gm = gmGO.AddComponent<GameManager>();
        // using dummy RoleUIController to avoid NullRef
        var uiGO = new GameObject("UI");
        var ui = uiGO.AddComponent<RoleUIController>();
        gm.RoleUIController = ui;

        gm.DealRolesAndStartPassAndPlay(new List<RoleType> { RoleType.Dealer, RoleType.PartyAnimal, RoleType.Medic });
        // internal index is private; we check that after one ShowNextPrivateRole call the flow will progress without exceptions
        gm.ShowNextPrivateRole();
        gm.ShowNextPrivateRole();
        gm.ShowNextPrivateRole(); // should iterate past players and end

        Assert.AreEqual(GameManager.Phase.Night, gm.CurrentPhase);
    }
}