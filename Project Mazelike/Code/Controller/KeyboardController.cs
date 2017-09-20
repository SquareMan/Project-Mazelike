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

        public static void Update(GameTime gameTime) {
            //Store the state from the previous frame and get the new one
            lastState = currentState;
            currentState = Keyboard.GetState();

            //Player movement
            if (IsButtonReleased(Keys.Right))
                WorldController.Instance.player.Move(Vector2.UnitX);
            if (IsButtonReleased(Keys.Left))
                WorldController.Instance.player.Move(-Vector2.UnitX);
            if (IsButtonReleased(Keys.Down))
                WorldController.Instance.player.Move(Vector2.UnitY);
            if (IsButtonReleased(Keys.Up))
                WorldController.Instance.player.Move(-Vector2.UnitY);

            //Test for Screen Switching
            if (IsButtonReleased(Keys.Escape)) {
                if(ScreenController.ActiveScreen == ScreenController.GetScreen("Game")) {
                    ScreenController.SetActiveScreen("Pause");
                } else {
                    ScreenController.SetActiveScreen("Game");
                }
            }

            //Rotate ActiveScreen if possible
            if (currentState.IsKeyDown(Keys.E))
                ScreenController.ActiveScreen.Camera.Rotation += rotationSpeed;
            if (currentState.IsKeyDown(Keys.Q))
                ScreenController.ActiveScreen.Camera.Rotation -= rotationSpeed;
        }

        public static Boolean IsButtonReleased(Keys key) {
            return currentState.IsKeyUp(key) && lastState.IsKeyDown(key);
        }
    }
}
