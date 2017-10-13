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

        public Map(Room[,] rooms) {
            int totalWidth = rooms.GetLength(0) * 10;
            int totalHeight = rooms.GetLength(1) * 10;
            tiles = new Tile[totalWidth, totalHeight];
            Enemies = new List<Enemy>();

            for (int i = 0; i < rooms.GetLength(0); i++) {
                for (int j = 0; j < rooms.GetLength(1); j++) {
                    for (int x = 0; x < rooms[i,j].tiles.GetLength(0); x++) {
                        for (int y = 0; y < rooms[i,j].tiles.GetLength(1); y++) {
                            tiles[(10 * i) + x, (10 * j) + y] = rooms[i, j].tiles[x, y];

                            Entity entity = rooms[i, j].tiles[x, y].EntityInTile;
                            if (entity != null) {
                                tiles[(10 * i) + x, (10 * j) + y].EnterTile(entity);

                                if (typeof(Enemy) == entity.GetType()) {
                                    Enemies.Add((Enemy)entity);
                                    entity.OnDeath += () => { Enemies.Remove((Enemy)entity); };
                                }
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
    }
}
