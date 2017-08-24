using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    static class MouseManager {
        public static float zoomSensitivity = .001f;

        static MouseState currentState;
        static MouseState lastState;

        public static void Update(GameTime gameTime) {
            currentState = Mouse.GetState();

            if(currentState.LeftButton == ButtonState.Pressed) {
                //Drag the camera
                Vector2 delta = (lastState.Position.ToVector2() - currentState.Position.ToVector2())/GameManager.Instance.screenManager.ActiveScreen.Camera.Scale;

                GameManager.Instance.screenManager.ActiveScreen.Camera.MoveCamera(delta);
            }

            //Zoom the game camera in and out
            GameManager.Instance.screenManager.ActiveScreen.Camera.Scale += GetScrollWhellAmount(currentState, lastState) * zoomSensitivity;

            lastState = currentState;
        }

        public static Boolean IsLeftReleased(MouseState currentState, MouseState lastState) {
            if (lastState.LeftButton == ButtonState.Pressed && currentState.LeftButton == ButtonState.Released) {
                return true;
            }
            return false;
        }

        public static Boolean IsRightReleased(MouseState currentState, MouseState lastState) {
            if (lastState.RightButton == ButtonState.Pressed && currentState.RightButton == ButtonState.Released) {
                return true;
            }
            return false;
        }

        public static int GetScrollWhellAmount(MouseState currentState, MouseState lastState) {
            return currentState.ScrollWheelValue - lastState.ScrollWheelValue;
        }
    }
}
