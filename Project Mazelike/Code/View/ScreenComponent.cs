﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.View {
    abstract class ScreenComponent {
        public Screen Screen { get; protected set; }
        public Vector2 Position { get; protected set; }

        public DrawLayer Layer { get; set; }
        public DrawSpace Space { get; set; }

        public delegate void ClickedDelegate();

        protected ScreenComponent(Screen screen, DrawLayer layer, DrawSpace space = DrawSpace.World) {
            this.Screen = screen;
            this.Layer = layer;
            this.Space = space;
        }

        //protected void DrawWithTransformation(SpriteBatch spriteBatch, Texture2D texture) {
        //    spriteBatch.Draw(
        //        texture,
        //        drawInWorldSpace ? Position : Vector2.Transform(Position, Matrix.Invert(Screen.Camera.GetTransformMatrix(Screen.canBeMoved, Screen.canBeRotated, Screen.canBeZoomed))),
        //        null,
        //        Color.White,
        //        rotatable ? 0f : -Screen.Camera.Rotation,
        //        Vector2.One,
        //        Screen.Camera.Scale,
        //        SpriteEffects.None,
        //        1f
        //    );
        //}

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public virtual void Update(GameTime gameTime) {

        }
    }

    public enum DrawLayer { Background, Player, Foreground, UI }
    public enum DrawSpace { World, Screen};
}