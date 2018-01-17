using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ProjectMazelike.Model
{
    internal class Map
    {
        protected Tile[,] tiles;

        public int Width => tiles.GetLength(0);

        public int Height => tiles.GetLength(1);

        public Player Player { get; set; }
        public List<Enemy> Enemies { get; set; }
        public Point PlayerStart { get; set; }

        public Tile GetTile(int x, int y)
        {
            if (x > tiles.GetLength(0) - 1 || x < 0 || y > tiles.GetLength(1) - 1 || y < 0) return null;

            return tiles[x, y];
        }

        public void SetTile(int x, int y, Tile t)
        {
            tiles[x, y] = new Tile(t, this, new Point(x, y));
        }

        public Map(int width, int height)
        {
            tiles = new Tile[width, height];
            Enemies = new List<Enemy>();

            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(Tile.tileFloor, this, new Point(x, y));
                tiles[x, y].EnterTile(null);
            }
        }
    }
}