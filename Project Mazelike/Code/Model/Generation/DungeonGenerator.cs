namespace ProjectMazelike.Model.Generation
{
    class DungeonGenerator
    {
        Maze maze;

        public Maze GetMaze()
        {
            return maze;
        }

        public DungeonGenerator(Maze baseMaze)
        {
            this.maze = baseMaze;
        }
    }
}