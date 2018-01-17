using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike.View
{
    internal class Camera : ITransformable
    {
        public bool CanBeMoved;
        public bool CanBeRotated;
        public bool CanBeScaled;

        private Rectangle Bounds { get; }

        public void MoveCamera(Vector2 direction)
        {
            Position += direction;
        }

        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
            Position = Vector2.Zero;
            Scale = 1f;
            Rotation = 0f;
        }

        public Camera(Viewport viewport, Vector2 position, float rotation = 0f, float scale = 1f)
        {
            Bounds = viewport.Bounds;
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }

        public Matrix TransformMatrix => (CanBeMoved
                                             ? Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0))
                                             : Matrix.Identity) *
                                         (CanBeRotated ? Matrix.CreateRotationZ(Rotation) : Matrix.Identity) *
                                         (CanBeScaled ? Matrix.CreateScale(Scale) : Matrix.Identity) *
                                         Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f,
                                             0));
    }
}