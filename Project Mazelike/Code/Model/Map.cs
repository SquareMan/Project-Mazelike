using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ProjectMazelike.Model {
    class Map {
        public Map(int width, int height) {
            tiles = new Tile[width, height];
            Enemies = new List<Enemy>();

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    tiles[x, y] = new Tile(Tile.tileFloor, this, new Point(x,y));
                    tiles[x, y].EnterTile(null);
                }
            }
        }

        public int Width {
            get {
                return tiles.GetLength(0);
            }
        }

        public int Height {
            get {
                return tiles.GetLength(1);
            }
        }

        protected Tile[,] tiles;
        public Player Player { get; set; }
        public List<Enemy> Enemies { get; set; }
        public Point PlayerStart { get; set; }

        public Tile GetTile(int x, int y) {
            if (x > tiles.GetLength(0) - 1 || x < 0 || y > tiles.GetLength(1) - 1 || y < 0) {
                //Requested tile is out of range
                return null;
            }

            return tiles[x, y];
        }

        public void SetTile(int x, int y, Tile t) {
            tiles[x, y] = new Tile(t, this, new Point(x, y));
        }
    }
}
