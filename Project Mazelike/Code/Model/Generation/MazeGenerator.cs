using System;
using System.Collections.Generic;

namespace ProjectMazelike.Model.Generation
{
    internal class MazeGenerator
    {
        protected Cell CurrentCell;

        protected Maze Maze;

        protected Random Rand;
        protected Stack<Cell> Stack;

        public virtual Maze GenerateMaze(int width, int height)
        {
            Maze = new Maze(width, height);
            Stack = new Stack<Cell>();

            //Set an initial current cell and mark as visited
            CurrentCell = Maze.GetCell(0, 0);
            CurrentCell.Visit();

            //Start main loop
            var unvistedCells = true;
            while (unvistedCells)
            {
                var currentNeighbors = CurrentCell.GetUnvisitedNeighbors();
                if (currentNeighbors.Count > 0)
                {
                    //We have at LEAST one unvisited neighbors
                    //Pick a random one and enter it
                    var index = Rand.Next(currentNeighbors.Count);

                    //Enter the next cell, mark as visited and disable applicable walls
                    EnterCell(CurrentCell, currentNeighbors[index]);
                    //Add new cell to the stack
                    Stack.Push(CurrentCell);
                }
                else if (Stack.Count > 0)
                {
                    //We can start backtracking
                    CurrentCell = Stack.Pop();
                }
                else
                {
                    //There are no unvisited neighbors and the stack is empty
                    //We can exit the loop
                    unvistedCells = false;
                }
            }

            return Maze;
        }

        public Maze GetMaze()
        {
            return Maze;
        }

        protected virtual void EnterCell(Cell current, Cell next)
        {
            //Connect the two cells
            Cell.Connect(current, next);

            //Mark the cell as visited
            next.Visit();

            //Set our current cell to the be next cell
            CurrentCell = next;
        }

        public MazeGenerator(int randomSeed = -1)
        {
            if (randomSeed == -1) randomSeed = Environment.TickCount;

            Rand = new Random(randomSeed);
        }
    }
}