using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class MouseManager {
        MouseState currentState;
        MouseState lastState;

        public MouseManager() {

        }

        public void Update(GameTime gameTime) {
            currentState = Mouse.GetState();

            if (IsLeftReleased(currentState, lastState)) {
                GameManager.Instance.screen.RemoveComponent(GameManager.Instance.mazeComponent);

                Maze newMaze = GameManager.Instance.MazeGenerator.GenerateMaze(ProjectMazelike.MazeWidth, ProjectMazelike.MazeHeight);
                GameManager.Instance.mazeComponent = new ScreenComponentMaze(newMaze, DrawLayer.Background);
                GameManager.Instance.screen.AddComponent(GameManager.Instance.mazeComponent);
            }
            if(IsRightReleased(currentState, lastState)) {
                GameManager.Instance.CycleGenerator();
            }

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
    }
}
