using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class Player : DrawableGameComponent {
        public Map currentMap;
        public Point position;

        SpriteBatch sb;
        Texture2D texture;

        public Player(Game game) : base(game) {
            position = new Point(3);
        }

        protected override void LoadContent() {
            sb = ((ProjectMazelike)Game).spriteBatch;
            texture = Game.Content.Load<Texture2D>("Graphics\\player");
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            sb.Begin();
            sb.Draw(TextureManager.GetTexture("Player"), position.ToVector2(), null, Color.White);
            sb.End();
        }
    }
}
