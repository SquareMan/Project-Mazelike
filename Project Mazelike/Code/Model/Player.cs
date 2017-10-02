using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.Model {
    class Player : Entity {
        public Player(Tile tile) : base(tile) {
        }

        public void SetMap(Map newMap) {
            if (currentMap != null) {
                currentMap.Player = null;
            }

            if(currentTile != null) {
                currentTile.LeaveTile(this);
            }

            newMap.Player = this;
            currentTile = newMap.Tiles[newMap.PlayerStart.X, newMap.PlayerStart.Y];
            currentTile.EnterTile(this);
        }
    }
}
