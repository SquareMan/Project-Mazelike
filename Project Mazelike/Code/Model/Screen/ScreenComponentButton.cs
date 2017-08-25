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
        Point position;
        Rectangle rect;

        public ScreenComponentButton(Point position, DrawLayer layer) : base(layer) {
            this.position = position;
            rect = new Rectangle(position.X, position.Y, 200, 80);
        }

        public event ClickedDelegate OnClicked;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(TextureManager.GetTexture("Button"), position.ToVector2(), Color.White);
        }

        public override void Update(GameTime gameTime) {
            if (MouseManager.IsLeftReleased() && rect.Intersects(new Rectangle(MouseManager.currentState.Position, new Point(1)))) {
                OnClicked?.Invoke();
            }
        }
    }
}
