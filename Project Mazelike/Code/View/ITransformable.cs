using Microsoft.Xna.Framework;

namespace ProjectMazelike.View
{
    internal interface ITransformable
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        float Scale { get; set; }

        Matrix TransformMatrix { get; }
    }
}