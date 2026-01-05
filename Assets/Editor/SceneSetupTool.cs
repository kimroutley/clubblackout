#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ClubBlackout.Editor {
    public static class SceneSetupTool {
        [MenuItem("Tools/ClubBlackout/Setup All Scenes")]
        public static void SetupAllScenes() {
            SetupMainMenuScene();
            SetupGameSetupScene();
            SetupGameScene();
            Debug.Log("All scenes created successfully!");
        }

        [MenuItem("Tools/ClubBlackout/Setup MainMenu Scene")]
        public static void SetupMainMenuScene() {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            
            // Create Canvas
            var canvasGO = new GameObject("Canvas");
            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasGO.AddComponent<GraphicRaycaster>();

            // Background
            var bgGO = new GameObject("Background");
            bgGO.transform.SetParent(canvasGO.transform, false);
            var bgImg = bgGO.AddComponent<Image>();
            bgImg.color = new Color(0.1f, 0.1f, 0.15f);
            var bgRect = bgGO.GetComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.sizeDelta = Vector2.zero;

            // Title
            var titleGO = new GameObject("Title");
            titleGO.transform.SetParent(canvasGO.transform, false);
            var titleText = titleGO.AddComponent<Text>();
            titleText.text = "Club Blackout";
            titleText.fontSize = 48;
            titleText.alignment = TextAnchor.UpperCenter;
            titleText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            var titleRect = titleGO.GetComponent<RectTransform>();
            titleRect.anchoredPosition = new Vector2(0, -100);
            titleRect.sizeDelta = new Vector2(600, 100);

            // Buttons
            CreateButton(canvasGO.transform, "NewGameButton", "New Game", new Vector2(0, -250));
            CreateButton(canvasGO.transform, "PlayerGuidesButton", "Player Guides", new Vector2(0, -350));
            CreateButton(canvasGO.transform, "SettingsButton", "Settings", new Vector2(0, -450));
            CreateButton(canvasGO.transform, "ExitButton", "Exit", new Vector2(0, -550));

            // Add MainMenu component
            canvasGO.AddComponent<ClubBlackout.MainMenu>();

            EditorSceneManager.SaveScene(scene, "Assets/Scenes/MainMenu.unity");
            Debug.Log("MainMenu scene created at Assets/Scenes/MainMenu.unity");
        }

        [MenuItem("Tools/ClubBlackout/Setup GameSetup Scene")]
        public static void SetupGameSetupScene() {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            
            var canvasGO = CreateCanvas();
            
            // Title
            CreateText(canvasGO.transform, "Title", "Game Setup - Select Roles", new Vector2(0, -80), 36);
            
            // Role dropdown
            var dropdownGO = CreateDropdown(canvasGO.transform, "RoleDropdown", new Vector2(-150, -200));
            
            // Add role button
            CreateButton(canvasGO.transform, "AddRoleButton", "Add Role", new Vector2(150, -200));
            
            // Selected roles text
            CreateText(canvasGO.transform, "SelectedRolesText", "Selected Roles:\n", new Vector2(0, -350), 20);
            
            // Start game button
            CreateButton(canvasGO.transform, "StartGameButton", "Start Game", new Vector2(0, -550));
            
            canvasGO.AddComponent<ClubBlackout.GameSetup>();

            EditorSceneManager.SaveScene(scene, "Assets/Scenes/GameSetup.unity");
            Debug.Log("GameSetup scene created at Assets/Scenes/GameSetup.unity");
        }

        [MenuItem("Tools/ClubBlackout/Setup Game Scene")]
        public static void SetupGameScene() {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            
            var canvasGO = CreateCanvas();
            
            // Game Manager
            var gmGO = new GameObject("GameManager");
            gmGO.AddComponent<ClubBlackout.GameManager>();
            gmGO.AddComponent<ClubBlackout.NightResolver>();
            
            // Host Tools
            var hostToolsGO = new GameObject("HostTools");
            hostToolsGO.AddComponent<ClubBlackout.HostTools>();
            
            // Vote Controller
            var voteGO = new GameObject("VoteController");
            voteGO.AddComponent<ClubBlackout.VoteController>();
            
            // Victory Screen (hidden by default)
            var victoryGO = new GameObject("VictoryScreen");
            victoryGO.transform.SetParent(canvasGO.transform, false);
            victoryGO.AddComponent<ClubBlackout.VictoryScreen>();
            victoryGO.SetActive(false);
            
            // Pause Menu
            var pauseGO = new GameObject("PauseMenu");
            pauseGO.AddComponent<ClubBlackout.PauseMenu>();

            EditorSceneManager.SaveScene(scene, "Assets/Scenes/Game.unity");
            Debug.Log("Game scene created at Assets/Scenes/Game.unity");
        }

        static GameObject CreateCanvas() {
            var canvasGO = new GameObject("Canvas");
            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvasGO.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            canvasGO.AddComponent<GraphicRaycaster>();
            return canvasGO;
        }

        static GameObject CreateButton(Transform parent, string name, string text, Vector2 position) {
            var btnGO = new GameObject(name);
            btnGO.transform.SetParent(parent, false);
            
            var img = btnGO.AddComponent<Image>();
            img.color = new Color(0.2f, 0.6f, 0.8f);
            
            var btn = btnGO.AddComponent<Button>();
            var colors = btn.colors;
            colors.normalColor = new Color(0.2f, 0.6f, 0.8f);
            colors.highlightedColor = new Color(0.3f, 0.7f, 0.9f);
            colors.pressedColor = new Color(0.1f, 0.5f, 0.7f);
            btn.colors = colors;
            
            var rect = btnGO.GetComponent<RectTransform>();
            rect.anchoredPosition = position;
            rect.sizeDelta = new Vector2(400, 80);
            
            var textGO = new GameObject("Text");
            textGO.transform.SetParent(btnGO.transform, false);
            var txtComponent = textGO.AddComponent<Text>();
            txtComponent.text = text;
            txtComponent.fontSize = 24;
            txtComponent.alignment = TextAnchor.MiddleCenter;
            txtComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txtComponent.color = Color.white;
            
            var textRect = textGO.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.sizeDelta = Vector2.zero;
            
            return btnGO;
        }

        static GameObject CreateText(Transform parent, string name, string text, Vector2 position, int fontSize) {
            var textGO = new GameObject(name);
            textGO.transform.SetParent(parent, false);
            var txtComponent = textGO.AddComponent<Text>();
            txtComponent.text = text;
            txtComponent.fontSize = fontSize;
            txtComponent.alignment = TextAnchor.UpperCenter;
            txtComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txtComponent.color = Color.white;
            
            var rect = textGO.GetComponent<RectTransform>();
            rect.anchoredPosition = position;
            rect.sizeDelta = new Vector2(800, 200);
            
            return textGO;
        }

        static GameObject CreateDropdown(Transform parent, string name, Vector2 position) {
            var dropdownGO = new GameObject(name);
            dropdownGO.transform.SetParent(parent, false);
            
            var img = dropdownGO.AddComponent<Image>();
            img.color = Color.white;
            
            var dropdown = dropdownGO.AddComponent<Dropdown>();
            
            var rect = dropdownGO.GetComponent<RectTransform>();
            rect.anchoredPosition = position;
            rect.sizeDelta = new Vector2(400, 60);
            
            return dropdownGO;
        }
    }
}
#endif