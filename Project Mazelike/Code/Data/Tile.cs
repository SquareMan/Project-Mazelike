using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class Tile {
        public static readonly Dictionary<string, Tile> GameTiles = new Dictionary<string, Tile>();

        public static readonly Tile TileFloor = new Tile("Floor", TileType.Floor);
        public static readonly Tile TileWall = new Tile("Wall", TileType.Wall);

        public TileType TileType { get; protected set; }

        protected Tile(string ID, TileType type) {
            GameTiles.Add(ID, this);
            this.TileType = type;
        }

        public static Tile GetTile(string blockID) {
            return GameTiles[blockID];
        }
    }

    enum TileType { Floor, Wall }
}
