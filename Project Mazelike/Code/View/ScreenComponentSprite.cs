using ProjectMazelike.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike.View {
    class ScreenComponentSprite : ScreenComponent {
        public ScreenComponentSprite(Sprite sprite, Screen screen, DrawLayer layer, DrawSpace space = DrawSpace.World) : base(screen, layer, space) {
            this.sprite = sprite;
        }

        public new Vector2 Position {
            get {
                return sprite.Position;
            }
            set {
                sprite.Position = value;
            }
        }

        Sprite sprite;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            sprite.Draw(spriteBatch);
        }
    }
}
