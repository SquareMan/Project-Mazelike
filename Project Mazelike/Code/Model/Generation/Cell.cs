using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectMazelike.Model.Generation
{
    internal class Cell
    {
        public enum Direction
        {
            None,
            North,
            East,
            South,
            West
        }

        public List<Cell> ConnectedCells;

        protected Maze Maze;

        //TODO: Does the cell itself need to care about being 'Visited'?
        //          This might be more of just a MazeGnerator thing
        //          Add an array/list of visited cells instead?

        //protected int x;
        //protected int y;

        public int X { get; private set; }

        public int Y { get; private set; }

        public bool Visited { get; protected set; }


        public Cell(int x, int y, Maze maze)
        {
            X = x;
            Y = y;
            Maze = maze;

            ConnectedCells = new List<Cell>();
        }

        /// <summary>
        ///     Connects two cells together, this removes the walls between them
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Connect(Cell a, Cell b)
        {
            a.ConnectedCells.Add(b);
            b.ConnectedCells.Add(a);
        }

        public static void Disconnect(Cell a, Cell b)
        {
            a.ConnectedCells.Remove(b);
            b.ConnectedCells.Remove(a);
        }

        /// <summary>
        ///     Returns the direction between two adjacent cells
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Direction GetDirection(Cell origin, Cell direction)
        {
            //Check for same case and diagonal case
            if (origin.X == direction.X && origin.Y == direction.Y)
            {
                Debug.WriteLine("Warning: Cell.GetDirection called with same cells or same cell positions");
                return Direction.None;
            }

            if (origin.X != direction.X && origin.Y != direction.Y)
            {
                Debug.WriteLine("Warning: Cell.GetDirection called with diagonal cells");
                return Direction.None;
            }

            if (origin.Y > direction.Y)
                return Direction.North;
            if (origin.X < direction.X)
                return Direction.East;
            if (origin.Y < direction.Y)
                return Direction.South;
            if (origin.X > direction.X)
                return Direction.West;

            Debug.WriteLine("ERROR: Direction not found");
            return Direction.None;
        }

        /// <summary>
        ///     Returns if a wall exists in a given direction
        /// </summary>
        /// <param name="direction">Direction to check for wall</param>
        /// <returns></returns>
        public bool WallStatus(Direction direction)
        {
            //A cell has a wall in a given direction if it does not have a connected cell in the corresponding direction
            //By definition, if a cell is in connectedCells there is no wall between them, so check for lack of a wall
            foreach (var cell in ConnectedCells)
                if (GetDirection(this, cell) == direction)
                    return false;

            //There is a wall in this direction
            return true;
        }

        //TODO: These neighbor methods are very similar. Find a way to combine them?
        public List<Cell> GetWalledNeighbors()
        {
            var neighbors = new List<Cell>();
            //Add North Neighbor
            if (Y - 1 >= 0)
            {
                var cell = Maze.GetCell(X, Y - 1);
                if (WallStatus(Direction.North)) neighbors.Add(cell);
            }

            //Add South Neighbor
            if (Y + 1 < Maze.Height)
            {
                var cell = Maze.GetCell(X, Y + 1);
                if (WallStatus(Direction.South)) neighbors.Add(cell);
            }

            //Add West Neighbor
            if (X - 1 >= 0)
            {
                var cell = Maze.GetCell(X - 1, Y);
                if (WallStatus(Direction.West)) neighbors.Add(cell);
            }

            //Add East Neighbor
            if (X + 1 < Maze.Width)
            {
                var cell = Maze.GetCell(X + 1, Y);
                if (WallStatus(Direction.East)) neighbors.Add(cell);
            }

            return neighbors;
        }

        public List<Cell> GetUnvisitedNeighbors()
        {
            var neighbors = new List<Cell>();
            //Add North Neighbor
            if (Y - 1 >= 0)
            {
                var cell = Maze.GetCell(X, Y - 1);
                if (cell.Visited == false) neighbors.Add(cell);
            }

            //Add South Neighbor
            if (Y + 1 < Maze.Height)
            {
                var cell = Maze.GetCell(X, Y + 1);
                if (cell.Visited == false) neighbors.Add(cell);
            }

            //Add West Neighbor
            if (X - 1 >= 0)
            {
                var cell = Maze.GetCell(X - 1, Y);
                if (cell.Visited == false) neighbors.Add(cell);
            }

            //Add East Neighbor
            if (X + 1 < Maze.Width)
            {
                var cell = Maze.GetCell(X + 1, Y);
                if (cell.Visited == false) neighbors.Add(cell);
            }

            return neighbors;
        }

        public int GetNumberOfWalls()
        {
            return 4 - ConnectedCells.Count;
        }

        /// <summary>
        ///     Marks the cell as visited
        /// </summary>
        /// <param name="flag">If set to false, cell is unvisited instead</param>
        public void Visit(bool flag = true)
        {
            Visited = flag;
        }
    }
}