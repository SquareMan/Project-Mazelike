using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class MazeGenerator {

        protected Maze maze;

        public MazeGenerator() {

        }

        public void GenerateMaze(int width, int height) {
            maze = new Maze(width, height);
        }

        public Maze GetMaze() {
            return maze;
        }

    }
}
