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

        public ScreenComponentPlayer(Player player) {
            this.player = player;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(TextureManager.GetTexture("Player"), new Vector2(10, 3) * GraphicsManager.cellSize, null, Color.White);
        }
    }
}
