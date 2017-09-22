using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike.View {
    class Sprite : ITransformable {
        public Sprite(Texture2D texture, Vector2 position) {
            this.texture = texture;
            this.Position = position;

            width = texture.Width;
            height = texture.Height;
        }

        public Sprite(Texture2D texture, int width, int height, Vector2 position, float rotation = 1f, float scale = 1f) {
            this.texture = texture;
            this.width = width;
            this.height = height;
            
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
        }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }

        public int width;
        public int height;

        public Matrix TransformMatrix {
            get {
                return  Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateScale(Scale);
            }
        }

        public Rectangle Bounds {
            get {
                return new Rectangle((int)Position.X, (int)Position.Y, width, height);
            }
        }

        protected Texture2D texture;

        /// <summary>
        /// Return the texture of this Sprite
        /// </summary>
        /// <returns></returns>
        public Texture2D GetTexture() {
            return texture;
        }

        /// <summary>
        /// Draw the sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, Bounds, Color.White);
        }
    }
}
