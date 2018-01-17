namespace ProjectMazelike.Model.Generation
{
    internal class DungeonGenerator
    {
        private readonly Maze maze;

        public Maze GetMaze()
        {
            return maze;
        }

        public DungeonGenerator(Maze baseMaze)
        {
            maze = baseMaze;
        }
    }
}