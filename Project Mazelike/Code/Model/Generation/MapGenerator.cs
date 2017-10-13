using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.Model.Generation {
    class MapGenerator {
        public MapGenerator(int seed) {
            this.seed = seed;
        }

        Map map;
        Maze maze;

        int seed;

        public Map GenerateMap() {
            //Create empty map and maze
            map = new Map(25, 25);
            maze = new MazeGenerator(seed).GenerateMaze(5,5);
            Random rand = new Random(seed);

            //Make map borders based on maze
            for (int x = 0; x < 5; x++) {
                for (int y = 0; y < 5; y++) {
                    if ( maze.GetCell(x,y).WallStatus(Cell.Direction.North)) {
                        for (int i = 0; i < 5; i++) {
                            map.SetTile(5 * x + i, 5 * y, Tile.tileWall);
                        }
                    }
                    if (maze.GetCell(x, y).WallStatus(Cell.Direction.East)) {
                        for (int i = 0; i < 5; i++) {
                            map.SetTile(5 * (x + 1) - 1, 5 * y + i, Tile.tileWall);
                        }
                    }
                    if (maze.GetCell(x, y).WallStatus(Cell.Direction.West)) {
                        for (int i = 0; i < 5; i++) {
                            map.SetTile(5 * x, 5 * y + i, Tile.tileWall);
                        }
                    }
                    if (maze.GetCell(x, y).WallStatus(Cell.Direction.South)) {
                        for (int i = 0; i < 5; i++) {
                            map.SetTile(5 * x + i, 5 * (y + 1) - 1, Tile.tileWall);
                        }

                    }
                }
            }

            for (int i = 0; i < 50; i++) {
                //Get a random tile
                int x = rand.Next(25);
                int y = rand.Next(25);
                Tile t = map.GetTile(x, y);
                if(t.ID != Tile.tileWall.ID) {
                    Debug.WriteLine(String.Format("Entity spawned at ({0},{1})", x, y));
                    Enemy enemy = new Enemy(t);
                    t.EnterTile(enemy);
                    map.Enemies.Add(enemy);
                    enemy.OnDeath += () => { map.Enemies.Remove(enemy); };
                }
            }

            return map;
        }
    }
}
