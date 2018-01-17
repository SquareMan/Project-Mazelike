using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ProjectMazelike.Controller
{
    internal static class KeyboardController
    {
        private static readonly float RotationSpeed = MathHelper.Pi / 32;
        
        private static Action<GameTime> _updateFunc;
        public static KeyboardState CurrentState { get; private set; }
        public static KeyboardState LastState { get; private set; }

        public static void Initialize()
        {
            ProjectMazelike.Instance.OnGameStateChanged += OnGameStateChanged;
        }

        public static void Update(GameTime gameTime)
        {
            //Store the state from the previous frame and get the new one
            LastState = CurrentState;
            CurrentState = Keyboard.GetState();

            //Run the appropriate update method
            _updateFunc?.Invoke(gameTime);

            //Rotate ActiveScreen if possible
            if (CurrentState.IsKeyDown(Keys.E))
                ScreenController.ActiveScreen.Camera.Rotation += RotationSpeed;
            if (CurrentState.IsKeyDown(Keys.Q))
                ScreenController.ActiveScreen.Camera.Rotation -= RotationSpeed;
        }

        private static void Update_GameRunning(GameTime gameTime)
        {
            //Player movement
            if (IsButtonReleased(Keys.Right))
                WorldController.Instance.World.Player.Move(Vector2.UnitX);
            if (IsButtonReleased(Keys.Left))
                WorldController.Instance.World.Player.Move(-Vector2.UnitX);
            if (IsButtonReleased(Keys.Down))
                WorldController.Instance.World.Player.Move(Vector2.UnitY);
            if (IsButtonReleased(Keys.Up))
                WorldController.Instance.World.Player.Move(-Vector2.UnitY);

            //Pause the game
            if (IsButtonReleased(Keys.Escape)) ProjectMazelike.Instance.PauseGame();
        }

        private static void Update_GamePaused(GameTime gameTime)
        {
            //Unpause the game
            if (IsButtonReleased(Keys.Escape)) ProjectMazelike.Instance.UnpauseGame();
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
                    _updateFunc = Update_GameRunning;
                    break;
                case ProjectMazelike.GameState.Paused:
                    _updateFunc = Update_GamePaused;
                    break;
            }
        }

        public static bool IsButtonReleased(Keys key)
        {
            return CurrentState.IsKeyUp(key) && LastState.IsKeyDown(key);
        }
    }
}