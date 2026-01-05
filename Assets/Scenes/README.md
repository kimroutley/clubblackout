# Unity Scene Setup Instructions

This project requires Unity 2021 LTS or newer with Android build support.

## Automatic Scene Setup

1. Open Unity and load this project
2. Go to menu: **Tools → ClubBlackout → Setup All Scenes**
3. This will create three scenes in `Assets/Scenes/`:
   - `MainMenu.unity` - Main menu with New Game, Settings, Player Guides, Exit
   - `GameSetup.unity` - Role selection and game configuration
   - `Game.unity` - Main gameplay scene with pass-and-play flow

## Manual Scene Setup (if automatic fails)

### MainMenu Scene
- Canvas with MainMenu.cs component
- Buttons: NewGameButton, PlayerGuidesButton, SettingsButton, ExitButton
- Background image (load from Assets/background.png)

### GameSetup Scene
- Canvas with GameSetup.cs component
- Dropdown for role selection
- Button to add roles
- Text to display selected roles
- Start Game button

### Game Scene
- GameManager with references to:
  - RoleUIController
  - HostTools
  - VictoryScreen
- NightResolver
- VoteController with vote button prefab
- PauseMenu
- VictoryScreen (initially hidden)

## Build Settings
Add scenes in this order:
1. MainMenu
2. GameSetup
3. Game

## Android Build Configuration
- Package Name: `com.clubblackout.app`
- Min API Level: 21 (Lollipop)
- Target API Level: 33+
- Orientation: Portrait or Auto-Rotation
- Install Location: Auto

## Asset Configuration
1. Select all role PNG files in Assets/
2. Set Texture Type: Sprite (2D and UI)
3. Set Max Size: 2048
4. Apply

## Running the Editor Tool
After opening the project in Unity:
- Menu → Tools → ClubBlackout → Populate Role Sprite DB (auto) - to map role sprites
- Menu → Tools → ClubBlackout → Setup All Scenes - to create scene files

## Testing
- Press Play in Unity Editor from MainMenu scene
- Navigate: Main Menu → New Game → Select roles → Start Game
- Test pass-and-play flow, night actions, voting, victory conditions