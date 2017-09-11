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
        public Point PlayerStart { get; protected set; }

        public Map(int width, int height) {
            Tiles = new Tile[width, height];

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

            for (int i = 0; i < rooms.GetLength(0); i++) {
                for (int j = 0; j < rooms.GetLength(1); j++) {
                    for (int x = 0; x < rooms[i,j].tiles.GetLength(0); x++) {
                        for (int y = 0; y < rooms[i,j].tiles.GetLength(1); y++) {
                            Tiles[(10 * i) + x, (10 * j) + y] = rooms[i, j].tiles[x, y];
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

        public Boolean CanEnter(int x, int y) {
            if (x < 0 || x >= Tiles.GetLength(0) || y < 0 || y >= Tiles.GetLength(1)) {
                //Coordinate is outside of the map
                return false;
            }
            if (Tiles[x, y] == null) {
                //Tile at coordinate doesnt exist
                return false;
            }

            if (Tiles[x, y].TileType == TileType.Wall)
                return false;

            return true;
        }
    }

    
    struct MapData : IXmlSerializable {
        public MapData Identity { get {
                return new MapData();
            }
        }

        Tile[,] tiles;

        public MapData(Map map) {
            tiles = map.Tiles;
        }

        public XmlSchema GetSchema() {
            return null;
        }

        public void ReadXml(XmlReader reader) {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer) {
            throw new NotImplementedException();
        }
    }
}
