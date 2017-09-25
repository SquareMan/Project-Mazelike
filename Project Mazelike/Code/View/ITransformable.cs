using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.View {
    interface ITransformable {
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        float Scale { get; set; }

        Matrix TransformMatrix { get; }
    }
}
