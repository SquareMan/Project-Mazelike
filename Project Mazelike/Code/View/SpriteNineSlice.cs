using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.View {
    class SpriteNineSlice {
        public Vector2 position;
        public int width;
        public int height;

        public Rectangle Bounds {
            get {
                return new Rectangle((int)position.X, (int)position.Y, width, height);
            }
        }

        int leftSlice;
        int rightSlice;
        int topSlice;
        int bottomSlice;

        Texture2D texture;
        Rectangle[] sourcePatches;

        /// <summary>
        /// Creates a new Nine-Sliced Texture
        /// </summary>
        /// <param name="texture">The base texture</param>
        /// <param name="leftSlice">pixels from the left to slice image</param>
        /// <param name="rightSlice">pixels from the right to slice image</param>
        /// <param name="topSlice">pixels from the top to slice image</param>
        /// <param name="bottomSlice">pixels from the bottom to slice image</param>
        public SpriteNineSlice(Texture2D texture, Vector2 position, int width, int height, int leftSlice, int rightSlice, int topSlice, int bottomSlice) {
            this.texture = texture;
            this.position = position;
            this.width = width;
            this.height = height;

            this.leftSlice = leftSlice;
            this.rightSlice = rightSlice;
            this.topSlice = topSlice;
            this.bottomSlice = bottomSlice;
            this.sourcePatches = CreatePatches(new Rectangle(0, 0, texture.Width, texture.Height));
        }

        public Texture2D GetTexture() {
            return texture;
        }

        public Rectangle[] GetDestinationPatches() {
            return CreatePatches(Bounds);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position) {
            Rectangle[] destPatches = CreatePatches(Bounds);

            for (int i = 0; i < sourcePatches.Length; i++) {
                spriteBatch.Draw(texture, destPatches[i], sourcePatches[i], Color.White);
            }
        }

        Rectangle[] CreatePatches(Rectangle bounds) {
            int x            = bounds.X;
            int y            = bounds.Y;
            int width        = bounds.Width;
            int height       = bounds.Height;
            int middleWidth  = width - leftSlice - rightSlice;
            int middleHeight = height - topSlice - bottomSlice;
            int leftX        = x + leftSlice;
            int rightX       = x + width - rightSlice;
            int topY         = y + topSlice;
            int bottomY      = y + height - topSlice;

            return new Rectangle[] {
                new Rectangle(x,         y,        leftSlice,   topSlice),     //Top-left
                new Rectangle(leftX,     y,        middleWidth, topSlice),     //Top-middle
                new Rectangle(rightX,    y,        rightSlice,  topSlice),     //Top-Right
                new Rectangle(x,         topY,     leftSlice,   middleHeight), // Left-Middle
                new Rectangle(leftX,     topY,     middleWidth, middleHeight), // Middle
                new Rectangle(rightX,    topY,     rightSlice,  middleHeight), // Right-Middle
                new Rectangle(x,         bottomY,  leftSlice,   bottomSlice),  // Bottom-Left
                new Rectangle(leftX,     bottomY,  middleWidth, bottomSlice),  // Bottom-Middle
                new Rectangle(rightX,    bottomY,  rightSlice,  bottomSlice)   // Bottom-Right
            };
        }
    }
}
