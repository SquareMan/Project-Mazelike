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

        public ScreenComponentButton(Point position, Screen screen, DrawLayer layer) : base(screen, layer) {
            this.position = position;

            this.drawInWorldSpace = false;
            this.rotatable = false;

            //TODO: FIXME THIS IS REALLY BAD
            //Area rectangle size currently hardcoded
            rect = new Rectangle(position.X, position.Y, 200, 80);
        }

        public event ClickedDelegate OnClicked;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(TextureManager.GetTexture("Button"),
                             drawInWorldSpace? position.ToVector2() : Vector2.Transform(position.ToVector2(), Matrix.Invert(Screen.Camera.TransformMatrix)),
                             Color.White);
        }

        public override void Update(GameTime gameTime) {
            if (MouseManager.IsLeftReleased() && rect.Intersects(
                new Rectangle(drawInWorldSpace? MouseManager.GetPositionInWorldSpace(Screen.Camera).ToPoint() : MouseManager.currentState.Position, new Point(1)))) {
                OnClicked?.Invoke();
            }
        }
    }
}
