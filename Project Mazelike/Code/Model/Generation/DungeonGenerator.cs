namespace ProjectMazelike.Model.Generation
{
    class DungeonGenerator {
        Maze maze;

        public DungeonGenerator(Maze baseMaze) {
            this.maze = baseMaze;
        }

        public Maze GetMaze() {
            return maze;
        }
    }
}
