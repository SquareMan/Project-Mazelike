using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class Cell {
        public Boolean northWall = true;
        public Boolean southWall = true;
        public Boolean westWall = true;
        public Boolean eastWall = true;

        protected int x;
        protected int y;

        protected Maze maze;

        private Boolean visited = false;


        public Cell(int x, int y, Maze maze) {
            this.X = x;
            this.Y = y;
            this.maze = maze;
        }

        public List<Cell> GetNeighbors() {
            List<Cell> neighbors = new List<Cell>();
            //Add North Neighbor
            if (Y - 1 >= 0) {
                neighbors.Add(maze.GetCell(X, Y - 1));
            }
            //Add South Neighbor
            if (Y + 1 < maze.Height) {
                neighbors.Add(maze.GetCell(X, Y + 1));
            }
            //Add West Neighbor
            if (X - 1 >= 0) {
                neighbors.Add(maze.GetCell(X - 1, Y));
            }
            //Add East Neighbor
            if (X + 1 < maze.Width) {
                neighbors.Add(maze.GetCell(X + 1, Y));
            }
            return neighbors;
        }

        public int X { get => x; protected set => x = value; }
        public int Y { get => y; protected set => y = value; }
        public Boolean Visited { get => visited; protected set => visited = value; }
    }
}
