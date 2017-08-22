using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike {
    class ScreenComponentTile : ScreenComponent {
        Tile tile;

        public ScreenComponentTile(Tile tile, DrawLayer layer) : base(layer) {
            this.tile = tile;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(TextureManager.GetTexture(tile.TileType.ToString()), tile.Position.ToVector2() * ScreenComponentMaze.cellSize, Color.White);
        }
    }
}
