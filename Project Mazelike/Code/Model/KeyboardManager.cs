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
            if (currentState.IsKeyDown(Keys.Right))
                GameManager.Instance.screen.Camera.MoveCamera(Vector2.UnitX * scrollSpeed);
            if (currentState.IsKeyDown(Keys.Left))
                GameManager.Instance.screen.Camera.MoveCamera(-Vector2.UnitX * scrollSpeed);
            if (currentState.IsKeyDown(Keys.Down))
                GameManager.Instance.screen.Camera.MoveCamera(Vector2.UnitY * scrollSpeed);
            if (currentState.IsKeyDown(Keys.Up))
                GameManager.Instance.screen.Camera.MoveCamera(-Vector2.UnitY * scrollSpeed);

            if(GameManager.Instance.screen.canBeRotated) {
                if (currentState.IsKeyDown(Keys.E))
                    GameManager.Instance.screen.Camera.Rotation += rotationSpeed;
                if (currentState.IsKeyDown(Keys.Q))
                    GameManager.Instance.screen.Camera.Rotation -= rotationSpeed;
            }

            lastState = currentState;
        }

        public static Boolean IsButtonReleased(KeyboardState currentState, KeyboardState lastState, Keys key) {
            return currentState.IsKeyUp(key) && lastState.IsKeyDown(key);
        }
    }
}
