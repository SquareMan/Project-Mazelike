using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    static class KeyboardManager {
        static KeyboardState currentState;
        static KeyboardState lastState;

        static float scrollSpeed = 4;
        static float rotationSpeed = MathHelper.Pi / 32;

        public static void Update(GameTime gameTime) {
            currentState = Keyboard.GetState();

            //Scroll the camera
            /*
            if (currentState.IsKeyDown(Keys.Right))
                GameManager.Instance.Screen.Camera.MoveCamera(Vector2.UnitX * scrollSpeed);
            if (currentState.IsKeyDown(Keys.Left))
                GameManager.Instance.Screen.Camera.MoveCamera(-Vector2.UnitX * scrollSpeed);
            if (currentState.IsKeyDown(Keys.Down))
                GameManager.Instance.Screen.Camera.MoveCamera(Vector2.UnitY * scrollSpeed);
            if (currentState.IsKeyDown(Keys.Up))
                GameManager.Instance.Screen.Camera.MoveCamera(-Vector2.UnitY * scrollSpeed);*/

            if (IsButtonReleased(currentState, lastState, Keys.Right))
                GameManager.Instance.Player.Move(1, 0);
            if (IsButtonReleased(currentState, lastState, Keys.Left))
                GameManager.Instance.Player.Move(-1, 0);
            if (IsButtonReleased(currentState, lastState, Keys.Down))
                GameManager.Instance.Player.Move(0, 1);
            if (IsButtonReleased(currentState, lastState, Keys.Up))
                GameManager.Instance.Player.Move(0, -1);

            if (IsButtonReleased(currentState, lastState, Keys.T)) {
                if(GameManager.Instance.screenManager.ActiveScreen == GameManager.Instance.screenManager.GetScreen("Game")) {
                    GameManager.Instance.screenManager.SetActiveScreen("Pause");
                } else {
                    GameManager.Instance.screenManager.SetActiveScreen("Game");
                }
            }

            if (GameManager.Instance.screenManager.ActiveScreen.canBeRotated) {
                if (currentState.IsKeyDown(Keys.E))
                    GameManager.Instance.screenManager.ActiveScreen.Camera.Rotation += rotationSpeed;
                if (currentState.IsKeyDown(Keys.Q))
                    GameManager.Instance.screenManager.ActiveScreen.Camera.Rotation -= rotationSpeed;
            }

            lastState = currentState;
        }

        public static Boolean IsButtonReleased(KeyboardState currentState, KeyboardState lastState, Keys key) {
            return currentState.IsKeyUp(key) && lastState.IsKeyDown(key);
        }
    }
}
