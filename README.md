# Club-Blackout (Unity)

Pass-and-play mobile implementation of the Club Blackout social-deduction party game (inspired by Werewolf). This repo contains a Unity 2D scaffold, core rule engine scripts, and test harness.

## Quick start
1. Open Unity (2021 LTS recommended) and choose *Open Project*, point to this folder.
2. Let Unity import the `Assets/` folder. Create Android build support if not installed.
3. **Setup scenes automatically**: Menu → Tools → ClubBlackout → Setup All Scenes
4. **Populate role sprites**: Menu → Tools → ClubBlackout → Populate Role Sprite DB (auto)
5. **Build Settings**: File → Build Settings → Add scenes (MainMenu, GameSetup, Game) → Switch to Android → Build

Assets link provided by you: https://drive.google.com/drive/folders/15VgMrhvHTc1yjZTTi1yUkE0kbSb_OAZC?usp=sharing

## What I added
- Initial C# scripts: `GameManager`, `Role`, `NightResolver`, `VoteController`, `HostTools` 
- Pass-and-play flow with private role screens
- Night action resolver (deterministic ordering: Sober → Roofi → Medic → Bouncer → Dealers → Kill resolution)
- Day voting and elimination with victory checks
- UI: MainMenu, GameSetup, SettingsPanel, PauseMenu, VictoryScreen
- `Assets/Resources/rules.json` — machine-readable rule spec for all 21 roles
- `Assets/Resources/player_guides.json` — role descriptions from game booklet
- Simple NUnit tests: `Assets/Tests/*.cs`
- `.gitignore` (Unity template)
- **Editor Tools**: SceneSetupTool to auto-create Unity scenes, PopulateRoleSpritesEditor to map PNG assets

## Game Features

### Implemented ✅
- Pass-and-play multiplayer (single device, human host)
- 21 unique roles with descriptions
- Host Tools (timers, manual phase control)
- Night action resolver (deterministic order)
- Day voting and elimination
- Victory condition checking (Party Animals vs Dealers)
- Main menu, settings, pause menu, victory screen
- Role selection and game setup
- Unit tests for core mechanics

### Roles (MVP Subset)
- Dealer, PartyAnimal, Medic, Bouncer, TeaSpiller, Wallflower

### Full Role List
All 21 roles implemented: Dealer, PartyAnimal, Medic, Bouncer, TeaSpiller, Wallflower, MessyBitch, Clinger, Whore, Creep, Roofi, Lightweight, Predator, Minor, Sober, SeasonedDrinker, SecondWind, DramaQueen, ClubManager, SilverFox, AllyCat

See [Assets/Resources/player_guides.json](Assets/Resources/player_guides.json) for descriptions.

## Testing

### In Unity Editor
1. Open MainMenu scene (Assets/Scenes/MainMenu.unity)
2. Press Play
3. Navigate: New Game → Select roles (add at least 1 Dealer + 2 others) → Start Game

### Unit Tests
- Window → General → Test Runner
- Run All Tests

### Android Device
1. File → Build Settings → Build
2. Install APK: `adb install <path_to_apk>`
3. Launch and test

## Build Configuration

### Android
- Package: `com.clubblackout.app`
- Min API: 21 (Android 5.0 Lollipop)
- Target API: 33+ (Android 13+)
- Orientation: Portrait

### Firebase (Optional)
- Add `google-services.json` to project root to enable Analytics/Crashlytics

## Project Structure
```
Assets/
├── Scenes/          # Unity scenes (auto-created by SceneSetupTool)
├── Scripts/         # C# game logic
├── Resources/       # rules.json, player_guides.json
├── Editor/          # Unity Editor automation tools
├── Tests/           # NUnit tests
├── role_*.png       # 21 role card images
├── background.png   # Background artwork
└── Player guide *.png  # Game booklet pages
```

## Notes & next steps
- I will wait for your PNGs and audio files to replace placeholders.
- I added Firebase placeholders; supply your `google-services.json` later to enable Crashlytics/Analytics.
- To create a remote GitHub repo and push, either give me the remote repo URL, or I can create one and ask you for permission to push.

## Development
- Add new roles: Update RoleType enum in Role.cs, add sprite as role_<name>.png, run Tools → Populate Role Sprite DB
- Modify rules: Edit Assets/Resources/rules.json and player_guides.json
- UI customization: Edit scene files or run SceneSetupTool again

## License
MIT License (see LICENSE file)

---

If you'd like, I can push the initial commit to a GitHub repository for you (requires permission/remote URL).