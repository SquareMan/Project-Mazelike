using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Diagnostics;

namespace ProjectMazelike {
    class Map {
        public Tile[,] Tiles { get; set; }
        public Player Player { get; set; }
        public List<Enemy> Enemies { get; set; }
        public Point PlayerStart { get; protected set; }

        public Map(int width, int height) {
            Tiles = new Tile[width, height];
            Enemies = new List<Enemy>();

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Tiles[x, y] = Tile.tileFloor;
                }
            }
        }

        public Map(Room[,] rooms) {
            int totalWidth = rooms.GetLength(0) * 10;
            int totalHeight = rooms.GetLength(1) * 10;
            Tiles = new Tile[totalWidth, totalHeight];
            Enemies = new List<Enemy>();

            for (int i = 0; i < rooms.GetLength(0); i++) {
                for (int j = 0; j < rooms.GetLength(1); j++) {
                    for (int x = 0; x < rooms[i,j].tiles.GetLength(0); x++) {
                        for (int y = 0; y < rooms[i,j].tiles.GetLength(1); y++) {
                            Tiles[(10 * i) + x, (10 * j) + y] = rooms[i, j].tiles[x, y];

                            IEntity entity = rooms[i, j].tiles[x, y].EntityInTile;
                            if (entity != null) {
                                Tiles[(10 * i) + x, (10 * j) + y].EnterTile(entity);

                                if (typeof(Enemy) == entity.GetType())
                                    Enemies.Add((Enemy)entity);
                            }
                        }
                    }
                }
            }

            PlayerStart = new Point(2, 2);
        }


        static Random rand = new Random();
        public static Room[,] TestRoomArray() {
            Room[,] rooms = new Room[2,2];

            for (int i = 0; i < 2; i++) {
                for (int j = 0; j < 2; j++) {
                    if (rand.Next() % 2 == 0) {
                        rooms[i, j] = new Room("RoomExample");
                    } else {
                        rooms[i, j] = new Room("RoomExample2");
                    }
                }
            }

            return rooms;
        }

        public Tile GetTile(int x, int y) {
            if (x > Tiles.GetLength(0) || x < 0 || y > Tiles.GetLength(1) || y < 0) {
                //Requested tile is out of range
                return null;
            }

            return Tiles[x, y];
        }
    }
}
