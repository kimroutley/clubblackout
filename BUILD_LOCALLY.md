# Building Club Blackout APK Locally

GitHub Actions free runners don't have enough disk space for Unity Android builds. Here's how to build on your PC instead:

## Prerequisites

1. **Install Unity Hub**: https://unity.com/download
2. **Install Unity 2021 LTS** (or later) with Android Build Support
   - In Unity Hub, go to Installs → Add
   - Select Unity 2021.3 LTS
   - Check "Android Build Support" including:
     - Android SDK & NDK Tools
     - OpenJDK

## Build Steps

### Option 1: Using Unity Editor (Easiest)

1. Open Unity Hub
2. Click "Add" → "Add project from disk"
3. Navigate to: `C:\Users\kimro\Documents\Codex\Club-Blackout\My project`
4. Click on the project to open it in Unity
5. Wait for Unity to import all assets (first time takes 5-10 minutes)
6. Go to **File → Build Settings**
7. Select **Android** and click "Switch Platform"
8. Click **Build** and choose where to save the APK
9. Transfer the APK to your Pixel 10 Pro and install

### Option 2: Using Command Line

Open PowerShell and run:

```powershell
cd "C:\Users\kimro\Documents\Codex\Club-Blackout"

# Find Unity installation path (adjust version as needed)
$unityPath = "C:\Program Files\Unity\Hub\Editor\2021.3.XX\Editor\Unity.exe"

# Build the APK
& $unityPath -quit -batchmode -projectPath "My project" -executeMethod BuildScript.BuildAndroidAPK -logFile build.log
```

The APK will be in: `Builds\Android\ClubBlackout.apk`

### Option 3: Use the BuildScript in Unity

1. Open the project in Unity
2. Go to **Tools → ClubBlackout → Build Android APK**
3. Wait for the build to complete
4. Find the APK in `Builds/Android/ClubBlackout.apk`

## Installing on Pixel 10 Pro

1. Connect your phone to PC via USB
2. Enable USB file transfer on your phone
3. Copy the APK to your phone's Downloads folder
4. On your phone:
   - Go to Settings → Security → Install unknown apps
   - Enable for "Files" or your file manager
   - Open the APK from Downloads and tap Install

## Troubleshooting

**"Build failed"**: Check `build.log` for errors
**"Scenes not found"**: Run **Tools → ClubBlackout → Setup All Scenes** first
**APK won't install**: Make sure to enable "Install unknown apps" for your file manager
