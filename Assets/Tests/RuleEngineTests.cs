using NUnit.Framework;
using System.Collections.Generic;
using ClubBlackout;

public class RuleEngineTests {
    [Test]
    public void AssignRoles_ShouldCreatePlayers() {
        var gm = new GameObject().AddComponent<GameManager>();
        gm.AssignRoles(new List<RoleType> { RoleType.Dealer, RoleType.PartyAnimal, RoleType.Medic });
        Assert.AreEqual(3, gm.Players.Count);
        Assert.AreEqual(RoleType.Dealer, gm.Players[0].Role.Type);
    }

    [Test]
    public void RoleSpriteDB_AutoPopulate_ShouldMapSprites() {
        #if UNITY_EDITOR
        // Run the editor populate routine (requires editor)
        ClubBlackout.Editor.PopulateRoleSpritesEditor.Populate();
        var db = UnityEditor.AssetDatabase.LoadAssetAtPath<ClubBlackout.RoleSpriteDatabase>("Assets/Resources/role_sprites.asset");
        Assert.IsNotNull(db);
        // At least Dealer should be found (since asset exists in Assets/ named role_dealer.png)
        var sprite = db.GetSprite(RoleType.Dealer);
        Assert.IsNotNull(sprite, "Dealer sprite should be auto-mapped from Assets files");
        #endif
    }
}