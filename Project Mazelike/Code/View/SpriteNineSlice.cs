using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike.View
{
    internal class SpriteNineSlice : Sprite
    {
        private readonly int bottomSlice;
        private readonly int leftSlice;
        private readonly int rightSlice;

        private readonly Rectangle[] sourcePatches;
        private readonly int topSlice;

        public Rectangle[] GetDestinationPatches()
        {
            return CreatePatches(Bounds);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var destPatches = CreatePatches(Bounds);

            for (var i = 0; i < sourcePatches.Length; i++)
                spriteBatch.Draw(texture, destPatches[i], sourcePatches[i], Color.White);
        }

        private Rectangle[] CreatePatches(Rectangle bounds)
        {
            var x = bounds.X;
            var y = bounds.Y;
            var width = bounds.Width;
            var height = bounds.Height;
            var middleWidth = width - leftSlice - rightSlice;
            var middleHeight = height - topSlice - bottomSlice;
            var leftX = x + leftSlice;
            var rightX = x + width - rightSlice;
            var topY = y + topSlice;
            var bottomY = y + height - topSlice;

            return new[]
            {
                new Rectangle(x, y, leftSlice, topSlice), //Top-left
                new Rectangle(leftX, y, middleWidth, topSlice), //Top-middle
                new Rectangle(rightX, y, rightSlice, topSlice), //Top-Right
                new Rectangle(x, topY, leftSlice, middleHeight), // Left-Middle
                new Rectangle(leftX, topY, middleWidth, middleHeight), // Middle
                new Rectangle(rightX, topY, rightSlice, middleHeight), // Right-Middle
                new Rectangle(x, bottomY, leftSlice, bottomSlice), // Bottom-Left
                new Rectangle(leftX, bottomY, middleWidth, bottomSlice), // Bottom-Middle
                new Rectangle(rightX, bottomY, rightSlice, bottomSlice) // Bottom-Right
            };
        }

        /// <summary>
        ///     Creates a new Nine-Sliced Texture
        /// </summary>
        /// <param name="texture">The base texture</param>
        /// <param name="leftSlice">pixels from the left to slice image</param>
        /// <param name="rightSlice">pixels from the right to slice image</param>
        /// <param name="topSlice">pixels from the top to slice image</param>
        /// <param name="bottomSlice">pixels from the bottom to slice image</param>
        public SpriteNineSlice(Texture2D texture, Vector2 position, int width, int height, int leftSlice,
            int rightSlice, int topSlice, int bottomSlice) : base(texture, width, height, position)
        {
            this.leftSlice = leftSlice;
            this.rightSlice = rightSlice;
            this.topSlice = topSlice;
            this.bottomSlice = bottomSlice;
            sourcePatches = CreatePatches(new Rectangle(0, 0, texture.Width, texture.Height));
        }
    }
}