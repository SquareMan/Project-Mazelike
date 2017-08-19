using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class Cell {

        public enum Direction { NONE, North, East, South, West };

        public List<Cell> connectedCells;

        protected int x;
        protected int y;

        protected Maze maze;

        //TODO: Does the cell itself need to care about being 'Visited'?
        //          This might be more of just a MazeGnerator thing
        //          Add an array/list of visited cells instead?
        private Boolean visited = false;


        public Cell(int x, int y, Maze maze) {
            this.X = x;
            this.Y = y;
            this.maze = maze;

            connectedCells = new List<Cell>();
        }

        /// <summary>
        /// Connects two cells together, this removes the walls between them
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Connect(Cell a, Cell b) {
            a.connectedCells.Add(b);
            b.connectedCells.Add(a);
        }

        public static void Disconnect(Cell a, Cell b) {
            a.connectedCells.Remove(b);
            b.connectedCells.Remove(a);
        }

        /// <summary>
        /// Returns the direction between two adjacent cells
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Direction GetDirection(Cell origin, Cell direction) {
            //Check for same case and diagonal case
            if(origin.X == direction.X && origin.Y == direction.Y) {
                Debug.WriteLine("Warning: Cell.GetDirection called with same cells or same cell positions");
                return Direction.NONE;
            } else if (origin.X != direction.X && origin.Y != direction.Y) {
                Debug.WriteLine("Warning: Cell.GetDirection called with diagonal cells");
                return Direction.NONE;
            }

            if(origin.Y > direction.Y) {
                return Direction.North;
            } else if (origin.X < direction.X) {
                return Direction.East;
            } else if (origin.Y < direction.Y) {
                return Direction.South;
            } else if (origin.X > direction.X) {
                return Direction.West;
            }

            Debug.WriteLine("ERROR: Direction not found");
            return Direction.NONE;
        }

        /// <summary>
        /// Returns if a wall exists in a given direction
        /// </summary>
        /// <param name="direction">Direction to check for wall</param>
        /// <returns></returns>
        public Boolean WallStatus(Direction direction) {
            //A cell has a wall in a given direction if it does not have a connected cell in the corresponding direction
            //By definition, if a cell is in connectedCells there is no wall between them, so check for lack of a wall
            foreach (Cell cell in connectedCells) {
                if(Cell.GetDirection(this, cell) == direction) {
                    //This cell does not have wall in the requested direction
                    return false;
                }
            }

            //There is a wall in this direction
            return true;
        }

        //TODO: These neighbor methods are very similar. Find a way to combine them?
        public List<Cell>GetWalledNeighbors() {
            List<Cell> neighbors = new List<Cell>();
            //Add North Neighbor
            if (Y - 1 >= 0) {
                Cell cell = maze.GetCell(X, Y - 1);
                if (WallStatus(Direction.North)) {
                    neighbors.Add(cell);
                }
            }
            //Add South Neighbor
            if (Y + 1 < maze.Height) {
                Cell cell = maze.GetCell(X, Y + 1);
                if (WallStatus(Direction.South)) {
                    neighbors.Add(cell);
                }
            }
            //Add West Neighbor
            if (X - 1 >= 0) {
                Cell cell = maze.GetCell(X - 1, Y);
                if (WallStatus(Direction.West)) {
                    neighbors.Add(cell);
                }
            }
            //Add East Neighbor
            if (X + 1 < maze.Width) {
                Cell cell = maze.GetCell(X + 1, Y);
                if (WallStatus(Direction.East)) {
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
            return 4 - connectedCells.Count;
        }

        /// <summary>
        /// Marks the cell as visited
        /// </summary>
        /// <param name="flag">If set to false, cell is unvisited instead</param>
        public void Visit(Boolean flag = true) {
            Visited = flag;
        }

        public int X { get => x; protected set => x = value; }
        public int Y { get => y; protected set => y = value; }
        public Boolean Visited { get => visited; protected set => visited = value; }
    }
}
