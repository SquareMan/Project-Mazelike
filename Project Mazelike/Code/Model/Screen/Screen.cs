using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike {
    class Screen : DrawableGameComponent {
        //SpriteBatch Information
        public BlendState BlendState { get; set; }
        public SamplerState SamplerState { get; set; }
        public DepthStencilState DepthStencilState { get; set; }
        public RasterizerState RasterizerState { get; set; }
        public Effect Effect { get; set; }

        public Boolean canBeMoved;
        public Boolean canBeRotated;
        public Boolean canBeZoomed;

        //Index is in order of draw layers, stores a list of every ScreenComponent on that layer
        //public List<ScreenComponent>[] components;
        public List<ScreenComponent>[] worldSpaceComponents;
        public List<ScreenComponent>[] screenSpaceComponents;

        public Camera Camera { get; set; }

        public Screen(Game game, Boolean moveable, Boolean rotatable, Boolean scaleable) : base(game) {
            this.canBeMoved = moveable;
            this.canBeRotated = rotatable;
            this.canBeZoomed = scaleable;

            //Create arrays of lists of game components, corresponding to their draw layer, for both world space and screen space
            worldSpaceComponents = new List<ScreenComponent>[Enum.GetNames(typeof(DrawLayer)).Length];
            for(int i = 0; i < worldSpaceComponents.Length; i++) {
                worldSpaceComponents[i] = new List<ScreenComponent>();
            }
            screenSpaceComponents = new List<ScreenComponent>[Enum.GetNames(typeof(DrawLayer)).Length];
            for (int i = 0; i < screenSpaceComponents.Length; i++) {
                screenSpaceComponents[i] = new List<ScreenComponent>();
            }

            //Create a new camera with origin at the top left corner
            Camera = new Camera(GraphicsDevice.Viewport, Vector2.Zero + new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2));
        }

        /// <summary>
        /// Register a component to be rendered
        /// </summary>
        /// <param name="sc">The ScreenComponent to add</param>
        /// <param name="screenSpace">Set to true if drawing in screen space is desired instead of world space</param>
        public void AddComponent(ScreenComponent sc) {
            if (sc.Space == DrawSpace.Screen) {
                screenSpaceComponents[(int)sc.Layer].Add(sc);
            } else {
                worldSpaceComponents[(int)sc.Layer].Add(sc);
            }
        }

        /// <summary>
        /// Unregister a previously registered component
        /// </summary>
        /// <param name="sc">The ScreenComponent to be removed</param>
        public void RemoveComponent(ScreenComponent sc) {
            if (sc != null) {
                if (screenSpaceComponents[(int)sc.Layer].Contains(sc)) {
                    screenSpaceComponents[(int)sc.Layer].Remove(sc);
                } else if(worldSpaceComponents[(int)sc.Layer].Contains(sc)) {
                    worldSpaceComponents[(int)sc.Layer].Remove(sc);
                }
            }
        }

        public Point GetMousePosition(DrawSpace space) {
            if (space == DrawSpace.World) {
                return Vector2.Transform(MouseManager.currentState.Position.ToVector2(), Matrix.Invert(Camera.GetTransformMatrix(canBeMoved, canBeRotated, canBeZoomed))).ToPoint();
            } else {
                return MouseManager.currentState.Position;
            }
        }

        public override void Update(GameTime gameTime) {
            for (int i = 0; i < Enum.GetNames(typeof(DrawLayer)).Length; i++) {
                foreach (ScreenComponent sc in worldSpaceComponents[i]) {
                    sc.Update(gameTime);
                }

                foreach (ScreenComponent sc in screenSpaceComponents[i]) {
                    sc.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            //Draw all objects that belong to this screen according to what they are, their draw layer, and whether they should be in world space or screen space
            for(int i = 0; i < Enum.GetNames(typeof(DrawLayer)).Length; i++) {
                ((ProjectMazelike)Game).SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState, SamplerState, DepthStencilState, RasterizerState, Effect, Camera.GetTransformMatrix(canBeMoved, canBeRotated, canBeZoomed));
                foreach (ScreenComponent sc in worldSpaceComponents[i]) {
                    sc.Draw(gameTime, ((ProjectMazelike)Game).SpriteBatch);
                }
                ((ProjectMazelike)Game).SpriteBatch.End();

                ((ProjectMazelike)Game).SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState, SamplerState, DepthStencilState, RasterizerState, Effect);
                foreach (ScreenComponent sc in screenSpaceComponents[i]) {
                    sc.Draw(gameTime, ((ProjectMazelike)Game).SpriteBatch);
                }
                ((ProjectMazelike)Game).SpriteBatch.End();
            }
        }
    }
}
