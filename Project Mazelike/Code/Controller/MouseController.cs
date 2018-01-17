using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ProjectMazelike.Controller
{
    internal static class MouseController
    {
        public static float ZoomSensitivity = .001f;

        private static Action<GameTime> _updateFunc;

        public static MouseState CurrentState { get; private set; }
        public static MouseState LastState { get; private set; }

        public static void Initialize()
        {
            ProjectMazelike.Instance.OnGameStateChanged += OnGameStateChanged;
        }

        public static void Update(GameTime gameTime)
        {
            LastState = CurrentState;
            CurrentState = Mouse.GetState();

            if (CurrentState.LeftButton == ButtonState.Pressed)
            {
                //Drag the camera
                var delta = (LastState.Position.ToVector2() - CurrentState.Position.ToVector2()) /
                            ScreenController.ActiveScreen.Camera.Scale;

                ScreenController.ActiveScreen.Camera.MoveCamera(delta);
            }

            //Zoom the game camera in and out
            if (ScreenController.ActiveScreen != null)
                ScreenController.ActiveScreen.Camera.Scale +=
                    GetScrollWhellAmount(CurrentState, LastState) * ZoomSensitivity;

            _updateFunc?.Invoke(gameTime);
        }

        public static bool IsLeftReleased()
        {
            if (LastState.LeftButton == ButtonState.Pressed && CurrentState.LeftButton == ButtonState.Released)
                return true;

            return false;
        }

        public static bool IsRightReleased()
        {
            if (LastState.RightButton == ButtonState.Pressed &&
                CurrentState.RightButton == ButtonState.Released) return true;

            return false;
        }

        public static int GetScrollWhellAmount(MouseState currentState, MouseState lastState)
        {
            return currentState.ScrollWheelValue - lastState.ScrollWheelValue;
        }

        private static void OnGameStateChanged(ProjectMazelike.GameState newState)
        {
            switch (newState)
            {
                case ProjectMazelike.GameState.Startup:
                    _updateFunc = null;
                    break;
                case ProjectMazelike.GameState.MainMenu:
                    _updateFunc = null;
                    break;
                case ProjectMazelike.GameState.Running:
                    _updateFunc = null;
                    break;
                case ProjectMazelike.GameState.Paused:
                    _updateFunc = null;
                    break;
            }
        }
    }
}