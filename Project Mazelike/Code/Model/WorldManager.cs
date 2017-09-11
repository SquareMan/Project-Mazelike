using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class WorldManager {
        public static WorldManager Instance;

        public World world;
        public Player player;

        ScreenComponentMap scm;

        public WorldManager() {
            Instance = this;

            player = new Player(new Point(2));
            world = new World(player);
        }

        public void SetMap(Map newMap) {
            //world.SetMap(newMap);

            ScreenManager.gameScreen.RemoveComponent(scm);
            scm = new ScreenComponentMap(newMap, ScreenManager.gameScreen, DrawLayer.Background);
            ScreenManager.gameScreen.AddComponent(scm);
        }

        public void GenerateWorld() {

        }
    }
}
