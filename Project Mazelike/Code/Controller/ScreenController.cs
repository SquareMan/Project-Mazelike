using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.View;

namespace ProjectMazelike.Controller
{
    internal static class ScreenController
    {
        public static Screen gameScreen;
        public static Screen pauseScreen;
        public static Screen mainMenuScreen;

        private static readonly Dictionary<string, Screen> screens = new Dictionary<string, Screen>();
        public static Screen ActiveScreen { get; private set; }

        public static void Initialize()
        {
            ProjectMazelike.Instance.OnGameStateChanged += OnGameStateChanged;

            //Setup screens
            gameScreen = AddScreen("Game", true, false, true);
            gameScreen.SamplerState = SamplerState.PointClamp;
            gameScreen.clearColor = Color.Black;
            pauseScreen = AddScreen("Pause", false, false, false);
            pauseScreen.SamplerState = SamplerState.PointClamp;
            mainMenuScreen = AddScreen("Main Menu", false, false, false);
            mainMenuScreen.SamplerState = SamplerState.PointClamp;
        }

        public static void SetActiveScreen(string name)
        {
            foreach (var n in screens.Keys)
            {
                screens[n].Visible = false;
                screens[n].Enabled = false;
            }

            if (screens.Keys.Contains(name))
            {
                ActiveScreen = screens[name];
                ActiveScreen.Visible = true;
                ActiveScreen.Enabled = true;
            }
            else
            {
                Debug.WriteLine(
                    string.Format("Screen with name {0} was attempted to be set as active but does not exist", name));
            }
        }

        public static Screen AddScreen(string name, bool moveable = true, bool rotatable = true,
            bool scaleable = true)
        {
            var newScreen = new Screen(ProjectMazelike.Instance, moveable, rotatable, scaleable);
            newScreen.Visible = false;
            newScreen.Enabled = false;
            screens.Add(name, newScreen);
            ProjectMazelike.Instance.Components.Add(newScreen);
            return newScreen;
        }

        public static Screen GetScreen(string name)
        {
            if (screens.Keys.Contains(name)) return screens[name];

            Debug.WriteLine(
                string.Format("Screen with name {0} was attempted to be retrieved but does not exist", name));
            return null;
        }

        private static void OnGameStateChanged(ProjectMazelike.GameState newState)
        {
            switch (newState)
            {
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