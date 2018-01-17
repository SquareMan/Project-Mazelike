using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike.View
{
    abstract class ScreenComponent : ITransformable
    {
        public delegate void ClickedDelegate();

        public Screen Screen { get; protected set; }
        public bool Enabled { get; set; } = true;

        public DrawLayer Layer { get; set; }
        public DrawSpace Space { get; set; }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public virtual void Update(GameTime gameTime)
        {
        }

        protected ScreenComponent(Screen screen, DrawLayer layer, DrawSpace space = DrawSpace.World)
        {
            this.Screen = screen;
            this.Layer = layer;
            this.Space = space;
        }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }

        public Matrix TransformMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                       Matrix.CreateRotationZ(Rotation) *
                       Matrix.CreateScale(Scale);
            }
        }
    }

    public enum DrawLayer
    {
        Background,
        Player,
        Foreground,
        UI
    }

    public enum DrawSpace
    {
        World,
        Screen
    };
}