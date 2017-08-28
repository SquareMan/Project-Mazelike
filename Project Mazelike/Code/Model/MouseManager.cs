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

        public static MouseState currentState { get; private set; }
        public static MouseState lastState { get; private set; }

        public static void Update(GameTime gameTime) {
            lastState = currentState;
            currentState = Mouse.GetState();

            if(currentState.LeftButton == ButtonState.Pressed) {
                //Drag the camera
                Vector2 delta = (lastState.Position.ToVector2() - currentState.Position.ToVector2())/GameManager.Instance.screenManager.ActiveScreen.Camera.Scale;

                GameManager.Instance.screenManager.ActiveScreen.Camera.MoveCamera(delta);
            }

            //Zoom the game camera in and out
            //if(GameManager.Instance.screenManager.ActiveScreen.canBeZoomed)
                GameManager.Instance.screenManager.ActiveScreen.Camera.Scale += GetScrollWhellAmount(currentState, lastState) * zoomSensitivity;
        }

        public static Vector2 GetPositionInWorldSpace(Screen screen) {
            return Vector2.Transform(currentState.Position.ToVector2(), Matrix.Invert(screen.Camera.GetTransformMatrix(screen.canBeMoved, screen.canBeRotated, screen.canBeZoomed)));
        }

        public static Boolean IsLeftReleased() {
            if (lastState.LeftButton == ButtonState.Pressed && currentState.LeftButton == ButtonState.Released) {
                return true;
            }
            return false;
        }

        public static Boolean IsRightReleased() {
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
