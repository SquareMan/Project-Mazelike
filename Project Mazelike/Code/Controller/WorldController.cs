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
        public Player player;

        ScreenComponentMap scm;

        public WorldController() {
            Instance = this;

            player = new Player(null);
            world = new World(player);
        }

        public void SetMap(Map newMap) {
            ScreenController.gameScreen.RemoveComponent(scm);
            scm = new ScreenComponentMap(newMap, ScreenController.gameScreen, DrawLayer.Background);
            ScreenController.gameScreen.AddComponent(scm);
        }

        public void GenerateWorld() {

        }
    }
}
