using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace ProjectMazelike {
    class Screen : DrawableGameComponent {

        //Index is in order of draw layers, stores a list of every ScreenComponent on that layer
        public List<ScreenComponent>[] components;

        public Screen(Game game) : base(game) {
            //Create an array of lists of game components, corresponding to their draw layer
            components = new List<ScreenComponent>[Enum.GetNames(typeof(DrawLayer)).Length];
            for(int i = 0; i < components.Length; i++) {
                components[i] = new List<ScreenComponent>();
            }
        }

        /// <summary>
        /// Register a component to be rendered
        /// </summary>
        /// <param name="sc">The ScreenComponent to add</param>
        /// <param name="layer">The layer for the ScreenComponent to be drawn on</param>
        public void AddComponent(ScreenComponent sc) {
            components[(int)sc.Layer].Add(sc);
        }

        public void RemoveComponent(ScreenComponent sc) {
            components[(int)sc.Layer].Add(sc);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            //Draw all objects that belong to this screen according to what they are and their draw layer

            for(int i = 0; i < Enum.GetNames(typeof(DrawLayer)).Length; i++) {
                ((ProjectMazelike)Game).SpriteBatch.Begin();
                foreach (ScreenComponent sc in components[i]) {
                    sc.Draw(gameTime, ((ProjectMazelike)Game).SpriteBatch);
                }
                ((ProjectMazelike)Game).SpriteBatch.End();
            }
        }
    }
}
