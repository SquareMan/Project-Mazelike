using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.Generation
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
