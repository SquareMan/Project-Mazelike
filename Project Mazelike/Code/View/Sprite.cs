﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike.View
{
    internal class Sprite : ITransformable
    {
        public int Height;

        protected Texture2D Texture;

        public int Width;

        public Rectangle Bounds => new Rectangle((int) Position.X, (int) Position.Y, Width, Height);

        /// <summary>
        ///     Return the texture of this Sprite
        /// </summary>
        /// <returns></returns>
        public Texture2D GetTexture()
        {
            return Texture;
        }

        /// <summary>
        ///     Draw the sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds, Color.White);
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            Position = position;

            Width = texture.Width;
            Height = texture.Height;
        }

        public Sprite(Texture2D texture, int width, int height, Vector2 position, float rotation = 0f, float scale = 1f)
        {
            this.Texture = texture;
            this.Width = width;
            this.Height = height;

            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }

        public Matrix TransformMatrix => Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(Scale);
    }
}