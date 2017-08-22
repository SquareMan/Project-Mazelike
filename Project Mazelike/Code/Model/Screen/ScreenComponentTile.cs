using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike {
    class ScreenComponentTile : ScreenComponent {
        Point position;

        public ScreenComponentTile(Point position, DrawLayer layer) : base(layer) {
            this.position = position;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(TextureManager.GetTexture("Floor"), position.ToVector2() * ScreenComponentMaze.cellSize, Color.White);
        }
    }
}
