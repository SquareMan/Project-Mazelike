using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProjectMazelike {
    struct Room {
        public Tile[,] tiles;

        public Room(string fileName) {
            tiles = new Tile[1, 1];

            FileStream stream = new FileStream("Content/XML/" + fileName + ".xml", System.IO.FileMode.Open);
            XmlReader reader = XmlReader.Create(stream);
            while (reader.Read()) {
                if (reader.NodeType == XmlNodeType.Element) {
                    //Initialize the map based on specified size
                    if (reader.Name == "room") {
                        tiles = new Tile[int.Parse(reader.GetAttribute("width")), int.Parse(reader.GetAttribute("height"))];
                        for (int x = 0; x < tiles.GetLength(0); x++) {
                            for (int y = 0; y < tiles.GetLength(1); y++) {
                                tiles[x, y] = Tile.TileFloor;
                            }
                        }
                    }

                    //Load tile information
                    if (reader.Name == "tile") {
                        int x = int.Parse(reader.GetAttribute("x"));
                        int y = int.Parse(reader.GetAttribute("y"));

                        tiles[x, y] = Tile.GetTile(reader.GetAttribute("type"));
                    }

                    //Set the players starting point
                    //if (reader.Name == "player") {
                    //    int x = int.Parse(reader.GetAttribute("x"));
                    //    int y = int.Parse(reader.GetAttribute("y"));
                    //    testMap.PlayerStart = new Point(x, y);
                    //}
                }
            }
            stream.Close();
            reader.Close();
        }
    }
}
