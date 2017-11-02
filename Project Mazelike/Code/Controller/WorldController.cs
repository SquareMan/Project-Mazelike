using Microsoft.Xna.Framework;
using ProjectMazelike.Model;
using ProjectMazelike.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.Controller {
    class WorldController {
        public static WorldController Instance;

        public World world;
        public Map currentMap;

        ScreenComponentMap scm;

        public WorldController() {
            Instance = this;

            world = new World(SetMap);
            world.OnMapChanged += SetMap;
        }

        public void SetMap(Map newMap) {
            currentMap = newMap;

            ScreenController.gameScreen.RemoveComponent(scm);
            scm = new ScreenComponentMap(newMap, ScreenController.gameScreen, DrawLayer.Background);
            ScreenController.gameScreen.AddComponent(scm);
        }
    }
}
