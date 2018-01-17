﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike.View
{
    class ScreenComponentSprite : ScreenComponent
    {
        Sprite sprite;

        public new Vector2 Position
        {
            get { return sprite.Position; }
            set { sprite.Position = value; }
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