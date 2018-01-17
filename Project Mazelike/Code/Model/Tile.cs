using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace ProjectMazelike.Model
{
    internal class Tile
    {
        public static readonly Dictionary<string, Tile> TilePrototypes = new Dictionary<string, Tile>();

        public static readonly Tile TileFloor = new Tile("Floor", TileType.Floor);
        public static readonly Tile TileWall = new Tile("Wall", TileType.Wall);
        public static readonly Tile TileStair = new Tile("Stair", TileType.Floor);
        public readonly Map Map;
        public readonly Point Position;

        public Entity EntityInTile { get; protected set; }
        public TileType TileType { get; protected set; }

        public string Id { get; protected set; }

        public event Action<Entity> OnTileEntered;

        public static Tile GetTile(string blockId)
        {
            return TilePrototypes[blockId];
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
        /// <param name="id"></param>
        /// <param name="type"></param>
        protected Tile(string id, TileType type)
        {
            TilePrototypes.Add(id, this);
            this.Id = id;
            TileType = type;
        }

        /// <summary>
        ///     Copy Constructor for creating game tiles
        /// </summary>
        /// <param name="t"></param>
        public Tile(Tile t, Map map, Point position)
        {
            Id = t.Id;
            TileType = t.TileType;
            this.Map = map;
            this.Position = position;
        }
    }

    internal enum TileType
    {
        Floor,
        Wall
    }
}