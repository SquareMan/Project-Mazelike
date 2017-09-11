using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class World {
        public World(Player player) {
            this.player = player;

            Map map1 = new Map(Map.TestRoomArray());
            Overworld.Add(map1);
            Map map2 = new Map(Map.TestRoomArray());
            Overworld.Add(map2);

            SetMap(map1);

            map1.Tiles[9, 5] = new Tile(Tile.tileStair);
            map1.Tiles[9, 5].OnTileEntered += (entity) => {
                if (entity == player) {
                    SetMap(map2);
                }
            };

            map2.Tiles[0, 5] = new Tile(Tile.tileStair);
            map2.Tiles[0,5].OnTileEntered += (entity) => {
                if (entity == player) {
                    SetMap(map1);
                }
            };
        }

        List<Map> Overworld = new List<Map>();

        Map currentMap;
        Player player;

        public void SetMap(Map newMap) {
            player.SetMap(newMap);
            WorldManager.Instance.SetMap(newMap);

            currentMap = newMap;
        }
    }
}
