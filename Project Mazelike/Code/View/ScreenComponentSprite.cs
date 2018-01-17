using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike.View
{
    internal class ScreenComponentSprite : ScreenComponent
    {
        private readonly Sprite sprite;

        public new Vector2 Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        public ScreenComponentSprite(Sprite sprite, Screen screen, DrawLayer layer, DrawSpace space = DrawSpace.World) :
            base(screen, layer, space)
        {
            this.sprite = sprite;
        }
    }
}