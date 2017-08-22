using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class Tile {
        public TileType TileType { get; protected set; }
        public Point Position { get; set; }

        public delegate void TileChangedDelegate();
        public event TileChangedDelegate OnTileChanged;

        public Tile(Point position, TileType type) {
            this.Position = position;
            this.TileType = type;
        }
    }

    enum TileType { Floor, Wall }
}
