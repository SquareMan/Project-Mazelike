using System;
using System.Diagnostics;

namespace ProjectMazelike.Model.Generation
{
    internal class MapGenerator
    {
        private Map _map;
        private Maze _maze;

        private readonly int _seed;

        public Map GenerateMap()
        {
            //Create empty map and maze
            _map = new Map(25, 25);
            _maze = new MazeGenerator(_seed).GenerateMaze(5, 5);
            var rand = new Random(_seed);

            //Make map borders based on maze
            for (var x = 0; x < 5; x++)
            for (var y = 0; y < 5; y++)
            {
                if (_maze.GetCell(x, y).WallStatus(Cell.Direction.North))
                    for (var i = 0; i < 5; i++)
                        _map.SetTile(5 * x + i, 5 * y, Tile.TileWall);

                if (_maze.GetCell(x, y).WallStatus(Cell.Direction.East))
                    for (var i = 0; i < 5; i++)
                        _map.SetTile(5 * (x + 1) - 1, 5 * y + i, Tile.TileWall);

                if (_maze.GetCell(x, y).WallStatus(Cell.Direction.West))
                    for (var i = 0; i < 5; i++)
                        _map.SetTile(5 * x, 5 * y + i, Tile.TileWall);

                if (_maze.GetCell(x, y).WallStatus(Cell.Direction.South))
                    for (var i = 0; i < 5; i++)
                        _map.SetTile(5 * x + i, 5 * (y + 1) - 1, Tile.TileWall);
            }

            for (var i = 0; i < 50; i++)
            {
                //Get a random tile
                var x = rand.Next(25);
                var y = rand.Next(25);
                var t = _map.GetTile(x, y);
                if (t.Id != Tile.TileWall.Id)
                {
                    Debug.WriteLine("Entity spawned at ({0},{1})", x, y);
                    var enemy = new Enemy(t);
                    t.EnterTile(enemy);
                    _map.Enemies.Add(enemy);
                    enemy.OnDeath += () => { _map.Enemies.Remove(enemy); };
                }
            }

            return _map;
        }

        public MapGenerator(int seed)
        {
            _seed = seed;
        }
    }
}