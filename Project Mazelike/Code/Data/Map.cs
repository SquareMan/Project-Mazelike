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

        public Boolean CanEnter(int x, int y) {
            if (x < 0 || x >= Tiles.GetLength(0) || y < 0 || y >= Tiles.GetLength(1)) {
                //Coordinate is outside of the map
                return false;
            }
            if (Tiles[x, y] == null) {
                //Tile at coordinate doesnt exist
                return false;
            }

            if (Tiles[x, y].TileType == TileType.Wall)
                return false;

            return true;
        }
    }
}
