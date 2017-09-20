using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.Model {
    class Tile {
        public delegate void TileEnteredDelegate(IEntity entity);
        public event TileEnteredDelegate OnTileEntered;

        public IEntity EntityInTile { get; protected set; }
        public TileType TileType { get; protected set; }

        public string ID { get; protected set; }

        /// <summary>
        /// For creating Tile prototypes
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="type"></param>
        protected Tile(string ID, TileType type) {
            tilePrototypes.Add(ID, this);
            this.ID = ID;
            this.TileType = type;
        }

        public static readonly Dictionary<string, Tile> tilePrototypes = new Dictionary<string, Tile>();

        public static readonly Tile tileFloor = new Tile("Floor", TileType.Floor);
        public static readonly Tile tileWall = new Tile("Wall", TileType.Wall);
        public static readonly Tile tileStair = new Tile("Stair", TileType.Floor);

        /// <summary>
        /// Copy Constructor for creating game tiles
        /// </summary>
        /// <param name="t"></param>
        public Tile(Tile t) {
            this.ID = t.ID;
            this.TileType = t.TileType;
        }

        public static Tile GetTile(string blockID) {
            return tilePrototypes[blockID];
        }

        public bool CanEnter() {
            if(EntityInTile != null) {
                return false;
            }
            if(TileType == TileType.Wall) {
                return false;
            }

            return true;
        }

        public void EnterTile(IEntity entity) {
            EntityInTile = entity;
            OnTileEntered?.Invoke(entity);
        }

        public void LeaveTile(IEntity entity) {
            if(EntityInTile != entity) {
                Debug.WriteLine("Entity tried to leave a tile it's not in");
                return;
            }

            EntityInTile = null;
        }
    }

    enum TileType { Floor, Wall }
}
