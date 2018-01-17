namespace ProjectMazelike.Model.Generation
{
    internal class DungeonGenerator
    {
        private readonly Maze _maze;

        public Maze GetMaze()
        {
            return _maze;
        }

        public DungeonGenerator(Maze baseMaze)
        {
            _maze = baseMaze;
        }
    }
}