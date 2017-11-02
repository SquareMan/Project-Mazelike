using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.Controller {
    static class KeyboardController {
        public static KeyboardState currentState { get; private set; }
        public static KeyboardState lastState { get; private set; }

        static float rotationSpeed = MathHelper.Pi / 32;

        delegate void UpdateFunc();
        static UpdateFunc Update_CurrentFunc;

        public static void Initialize() {
            ProjectMazelike.Instance.OnGameStateChanged += OnGameStateChanged;
        }

        public static void Update(GameTime gameTime) {
            //Store the state from the previous frame and get the new one
            lastState = currentState;
            currentState = Keyboard.GetState();

            //Run the appropriate update method
            Update_CurrentFunc?.Invoke();

            //Rotate ActiveScreen if possible
            if (currentState.IsKeyDown(Keys.E))
                ScreenController.ActiveScreen.Camera.Rotation += rotationSpeed;
            if (currentState.IsKeyDown(Keys.Q))
                ScreenController.ActiveScreen.Camera.Rotation -= rotationSpeed;
        }

        static void Update_GameRunning() {
            //Player movement
            if (IsButtonReleased(Keys.Right))
                WorldController.Instance.world.player.Move(Vector2.UnitX);
            if (IsButtonReleased(Keys.Left))
                WorldController.Instance.world.player.Move(-Vector2.UnitX);
            if (IsButtonReleased(Keys.Down))
                WorldController.Instance.world.player.Move(Vector2.UnitY);
            if (IsButtonReleased(Keys.Up))
                WorldController.Instance.world.player.Move(-Vector2.UnitY);
            
            //Pause the game
            if (IsButtonReleased(Keys.Escape)) {
                ProjectMazelike.Instance.PauseGame();
            }
        }

        static void Update_GamePaused() {
            //Unpause the game
            if (IsButtonReleased(Keys.Escape)) {
                ProjectMazelike.Instance.UnpauseGame();
            }
        }

        static void OnGameStateChanged(ProjectMazelike.GameState newState) {
            switch (newState) {
                case ProjectMazelike.GameState.Startup:
                    Update_CurrentFunc = null;
                    break;
                case ProjectMazelike.GameState.MainMenu:
                    Update_CurrentFunc = null;
                    break;
                case ProjectMazelike.GameState.Running:
                    Update_CurrentFunc = Update_GameRunning;
                    break;
                case ProjectMazelike.GameState.Paused:
                    Update_CurrentFunc = Update_GamePaused;
                    break;
            }
        }

        public static Boolean IsButtonReleased(Keys key) {
            return currentState.IsKeyUp(key) && lastState.IsKeyDown(key);
        }
    }
}
