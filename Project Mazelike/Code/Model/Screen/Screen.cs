using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace ProjectMazelike {
    class Screen : DrawableGameComponent {
        public enum DrawLayer { Background, Player, Foreground }

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
        /// <param name="gc"></param>
        /// <param name="layer"></param>
        public void AddComponent(ScreenComponent sc, DrawLayer layer) {
            components[(int)layer].Add(sc);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            //Draw all objects that belong to this screen according to what they are and their draw layer

            for(int i = 0; i < Enum.GetNames(typeof(DrawLayer)).Length; i++) {
                ((ProjectMazelike)Game).spriteBatch.Begin();
                foreach (ScreenComponent sc in components[i]) {
                    sc.Draw(gameTime, ((ProjectMazelike)Game).spriteBatch);
                }
                ((ProjectMazelike)Game).spriteBatch.End();
            }
        }
    }
}
