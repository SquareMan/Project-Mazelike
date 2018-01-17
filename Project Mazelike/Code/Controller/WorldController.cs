using ProjectMazelike.Model;
using ProjectMazelike.View;

namespace ProjectMazelike.Controller
{
    internal class WorldController
    {
        public static WorldController Instance;

        private ScreenComponentMap _scm;
        public Map CurrentMap;

        public World World;

        public WorldController()
        {
            Instance = this;

            World = new World(SetMap);
            World.OnMapChanged += SetMap;
        }

        public void SetMap(Map newMap)
        {
            CurrentMap = newMap;

            ScreenController.GameScreen.RemoveComponent(_scm);
            _scm = new ScreenComponentMap(newMap, ScreenController.GameScreen, DrawLayer.Background);
            ScreenController.GameScreen.AddComponent(_scm);
        }
    }
}