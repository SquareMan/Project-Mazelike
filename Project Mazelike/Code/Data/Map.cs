using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class Map {
        public Tile[,] Tiles { get; set; }

        public Map(int width, int height) {
            Tiles = new Tile[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Tiles[x, y] = new Tile(new Point(x,y), TileType.Floor);
                }
            }
        }
    }
}
