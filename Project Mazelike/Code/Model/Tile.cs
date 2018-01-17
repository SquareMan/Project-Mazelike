using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace ProjectMazelike.Model
{
    internal class Tile
    {
        public static readonly Dictionary<string, Tile> tilePrototypes = new Dictionary<string, Tile>();

        public static readonly Tile tileFloor = new Tile("Floor", TileType.Floor);
        public static readonly Tile tileWall = new Tile("Wall", TileType.Wall);
        public static readonly Tile tileStair = new Tile("Stair", TileType.Floor);
        public readonly Map map;
        public readonly Point position;

        public Entity EntityInTile { get; protected set; }
        public TileType TileType { get; protected set; }

        public string ID { get; protected set; }

        public event Action<Entity> OnTileEntered;

        public static Tile GetTile(string blockID)
        {
            return tilePrototypes[blockID];
        }

        public bool CanEnter()
        {
            if (EntityInTile != null) return false;

            if (TileType == TileType.Wall) return false;

            return true;
        }

        public void EnterTile(Entity entity)
        {
            EntityInTile = entity;
            OnTileEntered?.Invoke(entity);
        }

        public void LeaveTile(Entity entity)
        {
            if (EntityInTile != entity)
            {
                Debug.WriteLine("Entity tried to leave a tile it's not in");
                return;
            }

            EntityInTile = null;
        }

        /// <summary>
        ///     For creating Tile prototypes
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="type"></param>
        protected Tile(string ID, TileType type)
        {
            tilePrototypes.Add(ID, this);
            this.ID = ID;
            TileType = type;
        }

        /// <summary>
        ///     Copy Constructor for creating game tiles
        /// </summary>
        /// <param name="t"></param>
        public Tile(Tile t, Map map, Point position)
        {
            ID = t.ID;
            TileType = t.TileType;
            this.map = map;
            this.position = position;
        }
    }

    internal enum TileType
    {
        Floor,
        Wall
    }
}