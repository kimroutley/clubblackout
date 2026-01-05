# Club-Blackout (Unity)

Pass-and-play mobile implementation of the Club Blackout social-deduction party game (inspired by Werewolf). This repo contains a Unity 2D scaffold, core rule engine scripts, and test harness.

## Quick start
1. Open Unity (2021 LTS recommended) and choose *Open Project*, point to this folder.
2. Let Unity import the `Assets/` folder. Create Android build support if not installed.
3. Provide your PNG assets from your Google Drive link (or upload them to this repo) into `Assets/Art/` (create the folder if needed). Recommended file names: `role_medic.png`, `role_bouncer.png`, etc.

Assets link provided by you: https://drive.google.com/drive/folders/15VgMrhvHTc1yjZTTi1yUkE0kbSb_OAZC?usp=sharing

## What I added
- Initial C# scripts: `GameManager`, `Role`, `NightResolver`, `HostTools` (skeletons)
- `Assets/Resources/rules.json` — machine-readable rule spec for the MVP
- Simple NUnit test skeleton: `Assets/Tests/RuleEngineTests.cs`
- `.gitignore` (Unity template)

## Notes & next steps
- I will wait for your PNGs and audio files to replace placeholders.
- I added Firebase placeholders; supply your `google-services.json` later to enable Crashlytics/Analytics.
- To create a remote GitHub repo and push, either give me the remote repo URL, or I can create one and ask you for permission to push.

---

If you'd like, I can push the initial commit to a GitHub repository for you (requires permission/remote URL).