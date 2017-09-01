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
        public Point PlayerStart { get; protected set; }

        public Map(int width, int height) {
            Tiles = new Tile[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Tiles[x, y] = new Tile(new Point(x, y), TileType.Floor);
                }
            }
        }

        /// <summary>
        /// Extremely experimental map loading
        /// </summary>
        /// <param name="fileName">name of the XML file for the map</param>
        /// <returns></returns>
        public static Map TestMap(string fileName) {
            Map testMap = null;

            XmlReader reader = XmlReader.Create(new System.IO.FileStream("Content/XML/" + fileName + ".xml", System.IO.FileMode.Open));
            while(reader.Read()) {
                if(reader.NodeType == XmlNodeType.Element) {
                    //Initialize the map based on specified size
                    if (reader.Name == "room") {
                        testMap = new Map(int.Parse(reader.GetAttribute("width")), int.Parse(reader.GetAttribute("height")));
                    }

                    //Load tile information
                    if(reader.Name == "tile") {
                        int x = int.Parse(reader.GetAttribute("x"));
                        int y = int.Parse(reader.GetAttribute("y"));
                        TileType type = (TileType)Enum.Parse(typeof(TileType), reader.GetAttribute("type"));

                        testMap.Tiles[x, y].SetTileType(type);
                    }

                    //Set the players starting point
                    if(reader.Name == "player") {
                        int x = int.Parse(reader.GetAttribute("x"));
                        int y = int.Parse(reader.GetAttribute("y"));
                        testMap.PlayerStart = new Point(x, y);
                    }
                }
            }

            return testMap;
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
