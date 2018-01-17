using Microsoft.Xna.Framework;

namespace ProjectMazelike.Model
{
    class TileStair : Tile
    {
        protected Tile destination;

        public void SetDestination(Tile destination)
        {
            this.destination = destination;
        }

        protected TileStair(string ID, TileType type) : base(ID, type)
        {
        }

        public TileStair(TileStair t, Map map, Point position) : base(t, map, position)
        {
            destination = t.destination;
        }
    }
}