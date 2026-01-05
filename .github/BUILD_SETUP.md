# GitHub Actions Build Setup

This repository uses GitHub Actions to automatically build Android APKs.

## Setup Instructions

### 1. Activate Unity License
You need to add Unity credentials as GitHub Secrets:

1. Go to your repo: https://github.com/kimroutley/clubblackout/settings/secrets/actions
2. Click "New repository secret" and add these three secrets:

   - **UNITY_EMAIL**: Your Unity account email
   - **UNITY_PASSWORD**: Your Unity account password  
   - **UNITY_LICENSE**: Your Unity license file contents

### 2. Get Unity License File

Run this locally to get your license:
```bash
docker run -it --rm \
  -e UNITY_EMAIL=your@email.com \
  -e UNITY_PASSWORD=yourpassword \
  game-ci/unity-activator:latest
```

Or use Unity Personal License (free):
- Go to https://github.com/game-ci/unity-actions
- Follow the "Activation" guide to generate a license file
- Copy the entire license file contents and paste as UNITY_LICENSE secret

### 3. Android Keystore (Optional, for signed builds)

For debug builds, you can skip this. For release builds:

1. Create a keystore:
```bash
keytool -genkey -v -keystore user.keystore -alias myalias -keyalg RSA -keysize 2048 -validity 10000
```

2. Convert to base64:
```bash
base64 user.keystore > keystore.base64.txt
```

3. Add these secrets to GitHub:
   - **ANDROID_KEYSTORE_BASE64**: Contents of keystore.base64.txt
   - **ANDROID_KEYSTORE_PASS**: Keystore password you set
   - **ANDROID_KEYALIAS_NAME**: Alias name (e.g., myalias)
   - **ANDROID_KEYALIAS_PASS**: Alias password

### 4. Trigger Build

The workflow runs automatically on:
- Push to main branch
- Pull requests
- Manual trigger (go to Actions tab → Build Android APK → Run workflow)

### 5. Download APK

After build completes:
1. Go to Actions tab: https://github.com/kimroutley/clubblackout/actions
2. Click on the latest workflow run
3. Download "ClubBlackout-Android" artifact
4. Extract the .apk file and install on device

## Quick Start (No Unity License Setup)

If you just want to build without setting up Unity licensing, you can:
1. Open the project in Unity on your local machine
2. Use Tools → ClubBlackout → Build Android APK
3. APK will be in Builds/Android/ClubBlackout.apk

The GitHub Actions workflow is optional for automated cloud builds.