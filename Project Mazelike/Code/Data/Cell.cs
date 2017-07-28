using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //TODO: These neighbor methods are very similar. Find a way to combine them?

        public List<Cell>GetWalledNeighbors() {
            List<Cell> neighbors = new List<Cell>();
            //Add North Neighbor
            if (Y - 1 >= 0) {
                Cell cell = maze.GetCell(X, Y - 1);
                if (northWall) {
                    neighbors.Add(cell);
                }
            }
            //Add South Neighbor
            if (Y + 1 < maze.Height) {
                Cell cell = maze.GetCell(X, Y + 1);
                if (southWall) {
                    neighbors.Add(cell);
                }
            }
            //Add West Neighbor
            if (X - 1 >= 0) {
                Cell cell = maze.GetCell(X - 1, Y);
                if (westWall) {
                    neighbors.Add(cell);
                }
            }
            //Add East Neighbor
            if (X + 1 < maze.Width) {
                Cell cell = maze.GetCell(X + 1, Y);
                if (eastWall) {
                    neighbors.Add(cell);
                }
            }
            return neighbors;
        }

        public List<Cell> GetUnvisitedNeighbors() {
            List<Cell> neighbors = new List<Cell>();
            //Add North Neighbor
            if (Y - 1 >= 0) {
                Cell cell = maze.GetCell(X, Y - 1);
                if (cell.Visited == false) {
                    neighbors.Add(cell);
                }
            }
            //Add South Neighbor
            if (Y + 1 < maze.Height) {
                Cell cell = maze.GetCell(X, Y + 1);
                if (cell.Visited == false) {
                    neighbors.Add(cell);
                }
            }
            //Add West Neighbor
            if (X - 1 >= 0) {
                Cell cell = maze.GetCell(X - 1, Y);
                if (cell.Visited == false) {
                    neighbors.Add(cell);
                }
            }
            //Add East Neighbor
            if (X + 1 < maze.Width) {
                Cell cell = maze.GetCell(X + 1, Y);
                if (cell.Visited == false) {
                    neighbors.Add(cell);
                }
            }
            return neighbors;
        }

        public int GetNumberOfWalls() {
            int walls = 0;
            if(northWall) {
                walls++;
            }
            if(southWall) {
                walls++;
            }
            if(westWall) {
                walls++;
            }
            if(eastWall) {
                walls++;
            }
            return walls;
        }

        public void Visit() {
            Visited = true;
        }

        public int X { get => x; protected set => x = value; }
        public int Y { get => y; protected set => y = value; }
        public Boolean Visited { get => visited; protected set => visited = value; }
    }
}
