namespace ProjectMazelike.Model.Generation
{
    internal class Maze
    {
        protected Cell[,] cells;

        public int Width { get; protected set; }

        public int Height { get; protected set; }

        public Cell GetCell(int x, int y)
        {
            return cells[x, y];
        }

        public Cell[,] GetCellArray()
        {
            return cells;
        }

        public Maze(int width, int height)
        {
            Width = width;
            Height = height;

            cells = new Cell[width, height];
            for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
                cells[i, j] = new Cell(i, j, this);
        }
    }
}