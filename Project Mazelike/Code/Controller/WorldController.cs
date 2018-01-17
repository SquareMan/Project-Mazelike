using ProjectMazelike.Model;
using ProjectMazelike.View;

namespace ProjectMazelike.Controller
{
    internal class WorldController
    {
        public static WorldController Instance;
        public Map CurrentMap;

        private ScreenComponentMap _scm;

        public World World;

        public void SetMap(Map newMap)
        {
            CurrentMap = newMap;

            ScreenController.GameScreen.RemoveComponent(_scm);
            _scm = new ScreenComponentMap(newMap, ScreenController.GameScreen, DrawLayer.Background);
            ScreenController.GameScreen.AddComponent(_scm);
        }

        public WorldController()
        {
            Instance = this;

            World = new World(SetMap);
            World.OnMapChanged += SetMap;
        }
    }
}