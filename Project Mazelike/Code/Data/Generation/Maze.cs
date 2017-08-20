using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class Maze {

        protected Cell[,] cells;
        private int width;
        private int height;

        public int Width { get => width; protected set => width = value; }
        public int Height { get => height; protected set => height = value; }

        public Maze(int width, int height) {
            this.width = width;
            this.height = height;

            cells = new Cell[width, height];
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    cells[i, j] = new Cell(i, j, this);
                }
            }
        }

        public Cell GetCell(int x, int y) {
            return cells[x, y];
        }

        public Cell[,] GetCellArray() {
            return cells;
        }
    }
}
