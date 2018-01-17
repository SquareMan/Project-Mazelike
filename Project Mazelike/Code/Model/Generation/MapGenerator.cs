using System;
using System.Diagnostics;

namespace ProjectMazelike.Model.Generation
{
    internal class MapGenerator
    {
        private Map map;
        private Maze maze;

        private readonly int seed;

        public Map GenerateMap()
        {
            //Create empty map and maze
            map = new Map(25, 25);
            maze = new MazeGenerator(seed).GenerateMaze(5, 5);
            var rand = new Random(seed);

            //Make map borders based on maze
            for (var x = 0; x < 5; x++)
            for (var y = 0; y < 5; y++)
            {
                if (maze.GetCell(x, y).WallStatus(Cell.Direction.North))
                    for (var i = 0; i < 5; i++)
                        map.SetTile(5 * x + i, 5 * y, Tile.tileWall);

                if (maze.GetCell(x, y).WallStatus(Cell.Direction.East))
                    for (var i = 0; i < 5; i++)
                        map.SetTile(5 * (x + 1) - 1, 5 * y + i, Tile.tileWall);

                if (maze.GetCell(x, y).WallStatus(Cell.Direction.West))
                    for (var i = 0; i < 5; i++)
                        map.SetTile(5 * x, 5 * y + i, Tile.tileWall);

                if (maze.GetCell(x, y).WallStatus(Cell.Direction.South))
                    for (var i = 0; i < 5; i++)
                        map.SetTile(5 * x + i, 5 * (y + 1) - 1, Tile.tileWall);
            }

            for (var i = 0; i < 50; i++)
            {
                //Get a random tile
                var x = rand.Next(25);
                var y = rand.Next(25);
                var t = map.GetTile(x, y);
                if (t.ID != Tile.tileWall.ID)
                {
                    Debug.WriteLine("Entity spawned at ({0},{1})", x, y);
                    var enemy = new Enemy(t);
                    t.EnterTile(enemy);
                    map.Enemies.Add(enemy);
                    enemy.OnDeath += () => { map.Enemies.Remove(enemy); };
                }
            }

            return map;
        }

        public MapGenerator(int seed)
        {
            this.seed = seed;
        }
    }
}