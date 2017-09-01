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
        Rectangle bounds;
        TextureNineSlice texture;

        public ScreenComponentButton(Point position, int width, int height, Screen screen, DrawLayer layer) : base(screen, layer) {
            this.Position = position;

            this.drawInWorldSpace = false;
            this.rotatable = false;

            this.bounds = new Rectangle(position.X, position.Y, width, height);
        }

        public event ClickedDelegate OnClicked;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            if(texture == null) {
                texture = new TextureNineSlice(TextureManager.GetTexture("Button"), bounds.Width, bounds.Height, 8, 8, 8, 8);
            }
            
            texture.Draw(spriteBatch, Position.ToVector2());
        }

        public override void Update(GameTime gameTime) {
            if (MouseManager.IsLeftReleased() && bounds.Intersects(
                new Rectangle(drawInWorldSpace? MouseManager.GetPositionInWorldSpace(Screen).ToPoint() : MouseManager.currentState.Position, new Point(1)))) {
                OnClicked?.Invoke();
            }
        }
    }
}
