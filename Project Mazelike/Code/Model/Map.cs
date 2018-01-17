using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ProjectMazelike.Model
{
    internal class Map
    {
        protected Tile[,] Tiles;

        public int Width => Tiles.GetLength(0);

        public int Height => Tiles.GetLength(1);

        public Player Player { get; set; }
        public List<Enemy> Enemies { get; set; }
        public Point PlayerStart { get; set; }

        public Map(int width, int height)
        {
            Tiles = new Tile[width, height];
            Enemies = new List<Enemy>();

            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                Tiles[x, y] = new Tile(Tile.TileFloor, this, new Point(x, y));
                Tiles[x, y].EnterTile(null);
            }
        }

        public Tile GetTile(int x, int y)
        {
            if (x > Tiles.GetLength(0) - 1 || x < 0 || y > Tiles.GetLength(1) - 1 || y < 0) return null;

            return Tiles[x, y];
        }

        public void SetTile(int x, int y, Tile t)
        {
            Tiles[x, y] = new Tile(t, this, new Point(x, y));
        }
    }
}