using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class Maze {
        protected Cell[,] cells;

        public Maze(int width, int height) {
            cells = new Cell[width, height];
        }
    }
}
