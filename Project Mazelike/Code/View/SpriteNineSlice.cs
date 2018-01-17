using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike.View
{
    internal class SpriteNineSlice : Sprite
    {
        private readonly int _bottomSlice;
        private readonly int _leftSlice;
        private readonly int _rightSlice;

        private readonly Rectangle[] _sourcePatches;
        private readonly int _topSlice;

        public Rectangle[] GetDestinationPatches()
        {
            return CreatePatches(Bounds);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var destPatches = CreatePatches(Bounds);

            for (var i = 0; i < _sourcePatches.Length; i++)
                spriteBatch.Draw(Texture, destPatches[i], _sourcePatches[i], Color.White);
        }

        private Rectangle[] CreatePatches(Rectangle bounds)
        {
            var x = bounds.X;
            var y = bounds.Y;
            var width = bounds.Width;
            var height = bounds.Height;
            var middleWidth = width - _leftSlice - _rightSlice;
            var middleHeight = height - _topSlice - _bottomSlice;
            var leftX = x + _leftSlice;
            var rightX = x + width - _rightSlice;
            var topY = y + _topSlice;
            var bottomY = y + height - _topSlice;

            return new[]
            {
                new Rectangle(x, y, _leftSlice, _topSlice), //Top-left
                new Rectangle(leftX, y, middleWidth, _topSlice), //Top-middle
                new Rectangle(rightX, y, _rightSlice, _topSlice), //Top-Right
                new Rectangle(x, topY, _leftSlice, middleHeight), // Left-Middle
                new Rectangle(leftX, topY, middleWidth, middleHeight), // Middle
                new Rectangle(rightX, topY, _rightSlice, middleHeight), // Right-Middle
                new Rectangle(x, bottomY, _leftSlice, _bottomSlice), // Bottom-Left
                new Rectangle(leftX, bottomY, middleWidth, _bottomSlice), // Bottom-Middle
                new Rectangle(rightX, bottomY, _rightSlice, _bottomSlice) // Bottom-Right
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
            _leftSlice = leftSlice;
            _rightSlice = rightSlice;
            _topSlice = topSlice;
            _bottomSlice = bottomSlice;
            _sourcePatches = CreatePatches(new Rectangle(0, 0, texture.Width, texture.Height));
        }
    }
}