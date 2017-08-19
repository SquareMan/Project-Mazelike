using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike {
    class ScreenComponentPlayer : ScreenComponent {
        Player player;

        public ScreenComponentPlayer(Player player, DrawLayer layer) {
            this.player = player;
            Layer = layer;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(TextureManager.GetTexture("Player"), player.position.ToVector2() * ScreenComponentMaze.cellSize, null, Color.White);
        }
    }
}
