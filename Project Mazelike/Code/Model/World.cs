using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ProjectMazelike.Model.Generation;

namespace ProjectMazelike.Model
{
    internal class World
    {
        private Map _currentMap;

        private readonly List<Map> _overworld = new List<Map>();
        public Player Player;

        private readonly int _worldSeed;

        public event Action<Map> OnMapChanged;

        public void SetMap(Map newMap)
        {
            Player.SetMap(newMap);

            _currentMap = newMap;
            OnMapChanged?.Invoke(newMap);
        }

        public World(Action<Map> callback)
        {
            _worldSeed = Environment.TickCount;
            Player = new Player(null);
            OnMapChanged += callback;

            var map1 = new MapGenerator(_worldSeed + 1).GenerateMap();
            map1.PlayerStart = new Point(8, 5);
            _overworld.Add(map1);

            var map2 = new MapGenerator(_worldSeed + 2).GenerateMap();
            map2.PlayerStart = new Point(2, 2);
            _overworld.Add(map2);

            SetMap(map1);

            map1.SetTile(9, 5, Tile.TileStair);
            map1.GetTile(9, 5).OnTileEntered += entity =>
            {
                if (entity == Player) SetMap(map2);
            };

            map2.SetTile(0, 5, Tile.TileStair);
            map2.GetTile(0, 5).OnTileEntered += entity =>
            {
                if (entity == Player) SetMap(map1);
            };
        }
    }
}