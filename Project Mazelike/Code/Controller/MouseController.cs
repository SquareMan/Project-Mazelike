﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.Controller {
    static class MouseController {
        public static float zoomSensitivity = .001f;

        public static MouseState currentState { get; private set; }
        public static MouseState lastState { get; private set; }
        
        private static Action<GameTime> updateFunc;

        public static void Initialize() {
            ProjectMazelike.Instance.OnGameStateChanged += OnGameStateChanged;
        }

        public static void Update(GameTime gameTime) {
            lastState = currentState;
            currentState = Mouse.GetState();

            if(currentState.LeftButton == ButtonState.Pressed) {
                //Drag the camera
                Vector2 delta = (lastState.Position.ToVector2() - currentState.Position.ToVector2())/ScreenController.ActiveScreen.Camera.Scale;

                ScreenController.ActiveScreen.Camera.MoveCamera(delta);
            }

            //Zoom the game camera in and out
            if(ScreenController.ActiveScreen != null)
                ScreenController.ActiveScreen.Camera.Scale += GetScrollWhellAmount(currentState, lastState) * zoomSensitivity;

            updateFunc?.Invoke(gameTime);
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

        static void OnGameStateChanged(ProjectMazelike.GameState newState) {
            switch (newState) {
                case ProjectMazelike.GameState.Startup:
                    updateFunc = null;
                    break;
                case ProjectMazelike.GameState.MainMenu:
                    updateFunc = null;
                    break;
                case ProjectMazelike.GameState.Running:
                    updateFunc = null;
                    break;
                case ProjectMazelike.GameState.Paused:
                    updateFunc = null;
                    break;
            }
        }
    }
}
