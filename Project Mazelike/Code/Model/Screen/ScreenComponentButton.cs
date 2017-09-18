using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace ProjectMazelike {
    class ScreenComponentButton : ScreenComponent, IClickable {
        private string _text;
        public string text {
            get {
                return _text;
            }
            set {
                _text = value;
                textPosition = (bounds.Center - (ProjectMazelike.font.MeasureString(_text) / 2).ToPoint()).ToVector2();
            }
        }
        private Vector2 textPosition;

        Rectangle bounds;
        SpriteNineSlice sprite;

        public ScreenComponentButton(Vector2 position, int width, int height, Screen screen, DrawLayer layer, DrawSpace space = DrawSpace.World) : base(screen, layer, space) {
            this.Position = position;

            this.bounds = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public event ClickedDelegate OnClicked;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            if(sprite == null) {
                sprite = new SpriteNineSlice(TextureManager.GetTexture("Button"), Position, bounds.Width, bounds.Height, 8, 8, 8, 8);
            }
            
            sprite.Draw(spriteBatch, Position);

            if(text != null) {
                spriteBatch.DrawString(ProjectMazelike.font, text, textPosition, Color.White);
            }
        }

        public override void Update(GameTime gameTime) {
            if (MouseManager.IsLeftReleased() && bounds.Intersects(
                new Rectangle(Screen.GetMousePosition(Space), new Point(1)))) {
                OnClicked?.Invoke();
            }
        }
    }
}
