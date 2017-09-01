using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    static class KeyboardManager {
        public static KeyboardState currentState { get; private set; }
        public static KeyboardState lastState { get; private set; }

        static float rotationSpeed = MathHelper.Pi / 32;

        public static void Update(GameTime gameTime) {
            //Store the state from the previous frame and get the new one
            lastState = currentState;
            currentState = Keyboard.GetState();

            //Player movement
            if (IsButtonReleased(Keys.Right))
                GameManager.Instance.Player.Move(1, 0);
            if (IsButtonReleased(Keys.Left))
                GameManager.Instance.Player.Move(-1, 0);
            if (IsButtonReleased(Keys.Down))
                GameManager.Instance.Player.Move(0, 1);
            if (IsButtonReleased(Keys.Up))
                GameManager.Instance.Player.Move(0, -1);

            //Test for Screen Switching
            if (IsButtonReleased(Keys.T)) {
                if(GameManager.Instance.screenManager.ActiveScreen == GameManager.Instance.screenManager.GetScreen("Game")) {
                    GameManager.Instance.screenManager.SetActiveScreen("Pause");
                } else {
                    GameManager.Instance.screenManager.SetActiveScreen("Game");
                }
            }

            //Rotate ActiveScreen if possible
            if (currentState.IsKeyDown(Keys.E))
                GameManager.Instance.screenManager.ActiveScreen.Camera.Rotation += rotationSpeed;
            if (currentState.IsKeyDown(Keys.Q))
                GameManager.Instance.screenManager.ActiveScreen.Camera.Rotation -= rotationSpeed;
        }

        public static Boolean IsButtonReleased(Keys key) {
            return currentState.IsKeyUp(key) && lastState.IsKeyDown(key);
        }
    }
}
