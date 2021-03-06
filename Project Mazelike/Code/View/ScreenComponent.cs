﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike.View
{
    internal abstract class ScreenComponent : ITransformable
    {
        public delegate void ClickedDelegate();

        public Screen Screen { get; protected set; }
        public bool Enabled { get; set; } = true;

        public DrawLayer Layer { get; set; }
        public DrawSpace Space { get; set; }

        protected ScreenComponent(Screen screen, DrawLayer layer, DrawSpace space = DrawSpace.World)
        {
            Screen = screen;
            Layer = layer;
            Space = space;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public virtual void Update(GameTime gameTime)
        {
        }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }

        public Matrix TransformMatrix => Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(Scale);
    }

    public enum DrawLayer
    {
        Background,
        Player,
        Foreground,
        Ui
    }

    public enum DrawSpace
    {
        World,
        Screen
    }
}