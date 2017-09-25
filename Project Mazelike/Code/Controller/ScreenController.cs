using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.Controller {
    static class ScreenController {
        public static Screen ActiveScreen { get; private set; }

        public static Screen gameScreen;
        public static Screen pauseScreen;
        public static Screen mainMenuScreen;

        private static Dictionary<String, Screen> screens = new Dictionary<string, Screen>();

        public static void Initialize() {
            ProjectMazelike.Instance.OnGameStateChanged += OnGameStateChanged;

            //Setup screens
            gameScreen = AddScreen("Game", true, false, true);
            gameScreen.SamplerState = SamplerState.PointClamp;
            pauseScreen = AddScreen("Pause", false, false, false);
            pauseScreen.SamplerState = SamplerState.PointClamp;
            mainMenuScreen = AddScreen("Main Menu", false, false, false);
            mainMenuScreen.SamplerState = SamplerState.PointClamp;
        }

        public static void SetActiveScreen(String name) {
            foreach (string n in screens.Keys) {
                screens[n].Visible = false;
                screens[n].Enabled = false;
            }

            if (screens.Keys.Contains(name)) {
                ActiveScreen = screens[name];
                ActiveScreen.Visible = true;
                ActiveScreen.Enabled = true;
            } else {
                Debug.WriteLine(String.Format("Screen with name {0} was attempted to be set as active but does not exist", name));
            }
        }

        public static Screen AddScreen(String name, Boolean moveable = true, Boolean rotatable = true, Boolean scaleable = true) {
            Screen newScreen = new Screen(ProjectMazelike.Instance, moveable, rotatable, scaleable);
            newScreen.Visible = false;
            newScreen.Enabled = false;
            screens.Add(name, newScreen);
            ProjectMazelike.Instance.Components.Add(newScreen);
            return newScreen;
        }

        public static Screen GetScreen(String name) {
            if (screens.Keys.Contains(name)) {
                return screens[name];
            }

            Debug.WriteLine(String.Format("Screen with name {0} was attempted to be retrieved but does not exist", name));
            return null;
        }

        static void OnGameStateChanged(ProjectMazelike.GameState newState) {
            switch (newState) {
                case ProjectMazelike.GameState.Startup:
                    break;
                case ProjectMazelike.GameState.MainMenu:
                    SetActiveScreen("Main Menu");
                    break;
                case ProjectMazelike.GameState.Running:
                    SetActiveScreen("Game");
                    break;
                case ProjectMazelike.GameState.Paused:
                    SetActiveScreen("Pause");
                    break;
            }
        }
    }
}
