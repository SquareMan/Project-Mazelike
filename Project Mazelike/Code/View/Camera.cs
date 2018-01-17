using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike.View
{
    class Camera : ITransformable
    {
        public Boolean canBeMoved;
        public Boolean canBeRotated;
        public Boolean canBeScaled;

        private Rectangle Bounds { get; set; }

        public void MoveCamera(Vector2 direction)
        {
            Position += direction;
        }

        public Camera(Viewport viewport)
        {
            this.Bounds = viewport.Bounds;
            this.Position = Vector2.Zero;
            this.Scale = 1f;
            this.Rotation = 0f;
        }

        public Camera(Viewport viewport, Vector2 position, float rotation = 0f, float scale = 1f)
        {
            this.Bounds = viewport.Bounds;
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
        }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }

        public Matrix TransformMatrix
        {
            get
            {
                return
                    (canBeMoved
                        ? Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0))
                        : Matrix.Identity) *
                    (canBeRotated ? Matrix.CreateRotationZ(Rotation) : Matrix.Identity) *
                    (canBeScaled ? Matrix.CreateScale(Scale) : Matrix.Identity) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            }
        }
    }
}