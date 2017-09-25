using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using ProjectMazelike.Controller;

namespace ProjectMazelike.View {
    class ScreenComponentButton : ScreenComponent, IClickable {
        public ScreenComponentButton(Vector2 position, int width, int height, Screen screen, DrawLayer layer, DrawSpace space = DrawSpace.World) : base(screen, layer, space) {
            sprite = new SpriteNineSlice(TextureController.GetTexture("Button"), position, width, height, 8, 8, 8, 8);

            this.Position = position;
        }

        private string _text;
        private Vector2 textPosition;
        public string text {
            get {
                return _text;
            }
            set {
                _text = value;
                textPosition = (Bounds.Center - (ProjectMazelike.font.MeasureString(_text) / 2).ToPoint()).ToVector2();
            }
        }
        
        Rectangle Bounds {
            get {
                return new Rectangle((int)Position.X, (int)Position.Y, sprite.width, sprite.height);
            }
        }
        SpriteNineSlice sprite;
        
        new public Vector2 Position {
            get {
                return sprite.Position;
            }
            set {
                if (sprite != null)
                    sprite.Position = value;
            }
        }

        public event ClickedDelegate OnClicked;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            //sprite.Draw(spriteBatch, Position);
            sprite.Draw(spriteBatch);

            if(text != null) {
                spriteBatch.DrawString(ProjectMazelike.font, text, textPosition, Color.White);
            }
        }

        public override void Update(GameTime gameTime) {
            if (MouseController.IsLeftReleased() && Bounds.Intersects(
                new Rectangle(Screen.GetMousePosition(Space), new Point(1)))) {
                OnClicked?.Invoke();
            }
        }
    }
}
