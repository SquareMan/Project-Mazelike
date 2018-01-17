using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike.View
{
    internal class ScreenComponentSprite : ScreenComponent
    {
        private readonly Sprite _sprite;

        public new Vector2 Position
        {
            get => _sprite.Position;
            set => _sprite.Position = value;
        }

        public ScreenComponentSprite(Sprite sprite, Screen screen, DrawLayer layer, DrawSpace space = DrawSpace.World) :
            base(screen, layer, space)
        {
            _sprite = sprite;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }
    }
}