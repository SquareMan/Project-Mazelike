using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ProjectMazelike.Model.Generation;

namespace ProjectMazelike.Model
{
    internal class World
    {
        private Map currentMap;

        private readonly List<Map> Overworld = new List<Map>();
        public Player player;

        private readonly int worldSeed;

        public event Action<Map> OnMapChanged;

        public void SetMap(Map newMap)
        {
            player.SetMap(newMap);

            currentMap = newMap;
            OnMapChanged?.Invoke(newMap);
        }

        public World(Action<Map> callback)
        {
            worldSeed = Environment.TickCount;
            player = new Player(null);
            OnMapChanged += callback;

            var map1 = new MapGenerator(worldSeed + 1).GenerateMap();
            map1.PlayerStart = new Point(8, 5);
            Overworld.Add(map1);

            var map2 = new MapGenerator(worldSeed + 2).GenerateMap();
            map2.PlayerStart = new Point(2, 2);
            Overworld.Add(map2);

            SetMap(map1);

            map1.SetTile(9, 5, Tile.tileStair);
            map1.GetTile(9, 5).OnTileEntered += entity =>
            {
                if (entity == player) SetMap(map2);
            };

            map2.SetTile(0, 5, Tile.tileStair);
            map2.GetTile(0, 5).OnTileEntered += entity =>
            {
                if (entity == player) SetMap(map1);
            };
        }
    }
}