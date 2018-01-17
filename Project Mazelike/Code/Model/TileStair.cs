using Microsoft.Xna.Framework;

namespace ProjectMazelike.Model
{
    internal class TileStair : Tile
    {
        protected Tile Destination;

        protected TileStair(string id, TileType type) : base(id, type)
        {
        }

        public TileStair(TileStair t, Map map, Point position) : base(t, map, position)
        {
            Destination = t.Destination;
        }

        public void SetDestination(Tile destination)
        {
            Destination = destination;
        }
    }
}