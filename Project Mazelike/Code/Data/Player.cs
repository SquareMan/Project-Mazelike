using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class Player {
        public Map currentMap;
        public Point position;

        public Player(Point position) {
            this.position = position;
        }

        public void SetMap(Map newMap) {
            currentMap = newMap;
        }
    }
}
