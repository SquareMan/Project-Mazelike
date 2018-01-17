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
        public static Screen GameScreen;
        public static Screen PauseScreen;
        public static Screen MainMenuScreen;

        private static readonly Dictionary<string, Screen> Screens = new Dictionary<string, Screen>();
        public static Screen ActiveScreen { get; private set; }

        public static void Initialize()
        {
            ProjectMazelike.Instance.OnGameStateChanged += OnGameStateChanged;

            //Setup screens
            GameScreen = AddScreen("Game", true, false, true);
            GameScreen.SamplerState = SamplerState.PointClamp;
            GameScreen.ClearColor = Color.Black;
            PauseScreen = AddScreen("Pause", false, false, false);
            PauseScreen.SamplerState = SamplerState.PointClamp;
            MainMenuScreen = AddScreen("Main Menu", false, false, false);
            MainMenuScreen.SamplerState = SamplerState.PointClamp;
        }

        public static void SetActiveScreen(string name)
        {
            foreach (var n in Screens.Keys)
            {
                Screens[n].Visible = false;
                Screens[n].Enabled = false;
            }

            if (Screens.Keys.Contains(name))
            {
                ActiveScreen = Screens[name];
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
            Screens.Add(name, newScreen);
            ProjectMazelike.Instance.Components.Add(newScreen);
            return newScreen;
        }

        public static Screen GetScreen(string name)
        {
            if (Screens.Keys.Contains(name)) return Screens[name];

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