using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.Model {
    class TileStair : Tile {
        protected TileStair(string ID, TileType type) : base(ID, type) {

        }

        public TileStair(TileStair t, Map map, Point position) : base(t, map, position) {
            destination = t.destination;
        }

        protected Tile destination;

        public void SetDestination(Tile destination) {
            this.destination = destination;
        }
    }
}
