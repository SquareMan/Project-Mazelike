using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.Controller;

namespace ProjectMazelike.View
{
    internal class ScreenComponentButton : ScreenComponent, IClickable
    {
        private string _text;
        private readonly SpriteNineSlice sprite;
        private Vector2 textPosition;

        public string text
        {
            get => _text;
            set
            {
                _text = value;
                textPosition = (Bounds.Center - (ProjectMazelike.font.MeasureString(_text) / 2).ToPoint()).ToVector2();
            }
        }

        private Rectangle Bounds => new Rectangle((int) Position.X, (int) Position.Y, sprite.width, sprite.height);

        public new Vector2 Position
        {
            get => sprite.Position;
            set
            {
                if (sprite != null)
                    sprite.Position = value;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //sprite.Draw(spriteBatch, Position);
            sprite.Draw(spriteBatch);

            if (text != null) spriteBatch.DrawString(ProjectMazelike.font, text, textPosition, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (MouseController.IsLeftReleased() && Bounds.Intersects(
                    new Rectangle(Screen.GetMousePosition(Space), new Point(1))))
                OnClicked?.Invoke();
        }

        public ScreenComponentButton(Vector2 position, int width, int height, Screen screen, DrawLayer layer,
            DrawSpace space = DrawSpace.World) : base(screen, layer, space)
        {
            sprite = new SpriteNineSlice(TextureController.GetTexture("Button"), position, width, height, 8, 8, 8, 8);

            Position = position;
        }

        public event ClickedDelegate OnClicked;
    }
}