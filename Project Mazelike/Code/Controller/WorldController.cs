using ProjectMazelike.Model;
using ProjectMazelike.View;

namespace ProjectMazelike.Controller
{
    class WorldController
    {
        public static WorldController Instance;
        public Map currentMap;

        ScreenComponentMap scm;

        public World world;

        public void SetMap(Map newMap)
        {
            currentMap = newMap;

            ScreenController.gameScreen.RemoveComponent(scm);
            scm = new ScreenComponentMap(newMap, ScreenController.gameScreen, DrawLayer.Background);
            ScreenController.gameScreen.AddComponent(scm);
        }

        public WorldController()
        {
            Instance = this;

            world = new World(SetMap);
            world.OnMapChanged += SetMap;
        }
    }
}