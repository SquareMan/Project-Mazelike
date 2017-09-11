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
        public List<ScreenComponent>[] components;
        
        public Camera Camera { get; set; }

        public Screen(Game game, Boolean moveable, Boolean rotatable, Boolean scaleable) : base(game) {
            this.canBeMoved = moveable;
            this.canBeRotated = rotatable;
            this.canBeZoomed = scaleable;

            //Create an array of lists of game components, corresponding to their draw layer
            components = new List<ScreenComponent>[Enum.GetNames(typeof(DrawLayer)).Length];
            for(int i = 0; i < components.Length; i++) {
                components[i] = new List<ScreenComponent>();
            }

            //Create a new camera with origin at the top left corner
            Camera = new Camera(GraphicsDevice.Viewport, Vector2.Zero + new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2));
        }

        /// <summary>
        /// Register a component to be rendered
        /// </summary>
        /// <param name="sc">The ScreenComponent to add</param>
        /// <param name="layer">The layer for the ScreenComponent to be drawn on</param>
        public void AddComponent(ScreenComponent sc) {
            components[(int)sc.Layer].Add(sc);
        }

        /// <summary>
        /// Unregister a previously registered component
        /// </summary>
        /// <param name="sc">The ScreenComponent to be removed</param>
        public void RemoveComponent(ScreenComponent sc) {
            if (sc != null && components[(int)sc.Layer].Contains(sc))
                components[(int)sc.Layer].Remove(sc);
        }

        public override void Update(GameTime gameTime) {
            for (int i = 0; i < Enum.GetNames(typeof(DrawLayer)).Length; i++) {
                foreach (ScreenComponent sc in components[i]) {
                    sc.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            //Draw all objects that belong to this screen according to what they are and their draw layer
            for(int i = 0; i < Enum.GetNames(typeof(DrawLayer)).Length; i++) {
                ((ProjectMazelike)Game).SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState, SamplerState, DepthStencilState, RasterizerState, Effect, Camera.GetTransformMatrix(canBeMoved, canBeRotated, canBeZoomed));
                foreach (ScreenComponent sc in components[i]) {
                    sc.Draw(gameTime, ((ProjectMazelike)Game).SpriteBatch);
                }
                ((ProjectMazelike)Game).SpriteBatch.End();
            }
        }
    }
}
