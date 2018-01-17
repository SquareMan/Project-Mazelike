namespace ProjectMazelike.Model.Generation
{
    internal class Maze
    {
        protected Cell[,] Cells;

        public int Width { get; protected set; }

        public int Height { get; protected set; }

        public Cell GetCell(int x, int y)
        {
            return Cells[x, y];
        }

        public Cell[,] GetCellArray()
        {
            return Cells;
        }

        public Maze(int width, int height)
        {
            Width = width;
            Height = height;

            Cells = new Cell[width, height];
            for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
                Cells[i, j] = new Cell(i, j, this);
        }
    }
}