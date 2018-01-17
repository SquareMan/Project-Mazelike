using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.Controller;

namespace ProjectMazelike.View
{
    internal class ScreenComponentButton : ScreenComponent, IClickable
    {
        private string _text;
        private readonly SpriteNineSlice _sprite;
        private Vector2 _textPosition;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                _textPosition = (Bounds.Center - (ProjectMazelike.Font.MeasureString(_text) / 2).ToPoint()).ToVector2();
            }
        }

        private Rectangle Bounds => new Rectangle((int) Position.X, (int) Position.Y, _sprite.Width, _sprite.Height);

        public new Vector2 Position
        {
            get => _sprite.Position;
            set
            {
                if (_sprite != null)
                    _sprite.Position = value;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //sprite.Draw(spriteBatch, Position);
            _sprite.Draw(spriteBatch);

            if (Text != null) spriteBatch.DrawString(ProjectMazelike.Font, Text, _textPosition, Color.White);
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
            _sprite = new SpriteNineSlice(TextureController.GetTexture("Button"), position, width, height, 8, 8, 8, 8);

            Position = position;
        }

        public event ClickedDelegate OnClicked;
    }
}