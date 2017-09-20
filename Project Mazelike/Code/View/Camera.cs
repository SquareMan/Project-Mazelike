using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.View {
    class Camera {

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }

        private Rectangle Bounds { get; set; }

        public Matrix TransformMatrix {
            get {
                return
                    Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(Scale) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            }
        }

        public Matrix GetTransformMatrix(Boolean position = true, Boolean rotation = true, Boolean scale = true) {
            Matrix transform = Matrix.Identity;

            //Do applicable transformations
            if(position) {
                transform *= Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0));
            }
            if(rotation) {
                transform *= Matrix.CreateRotationZ(Rotation);
            }
            if(scale) {
                transform *= Matrix.CreateScale(Scale);
            }

            //Center the camera around (0,0)
            transform *= Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));

            return transform;
        }

        public Camera(Viewport viewport) {
            this.Bounds = viewport.Bounds;
            this.Position = Vector2.Zero;
            this.Scale = 1f;
            this.Rotation = 0f;
        }

        public Camera(Viewport viewport, Vector2 position, float rotation = 0f, float scale = 1f) {
            this.Bounds = viewport.Bounds;
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
        }

        public void MoveCamera(Vector2 direction) {
            Position += direction;
        }
    }
}
