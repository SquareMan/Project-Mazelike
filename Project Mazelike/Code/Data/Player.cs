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

        public void Move(int deltaX, int deltaY) {
            if (currentMap.CanEnter(position.X + deltaX, position.Y + deltaY))
                position += new Point(deltaX, deltaY);
        }

        public void SetMap(Map newMap) {
            currentMap = newMap;
        }
    }
}
