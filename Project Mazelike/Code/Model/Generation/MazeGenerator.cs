﻿using System;
using System.Collections.Generic;

namespace ProjectMazelike.Model.Generation
{
    internal class MazeGenerator
    {
        protected Cell currentCell;

        protected Maze maze;

        protected Random rand;
        protected Stack<Cell> stack;

        public virtual Maze GenerateMaze(int width, int height)
        {
            maze = new Maze(width, height);
            stack = new Stack<Cell>();

            //Set an initial current cell and mark as visited
            currentCell = maze.GetCell(0, 0);
            currentCell.Visit();

            //Start main loop
            var unvistedCells = true;
            while (unvistedCells)
            {
                var currentNeighbors = currentCell.GetUnvisitedNeighbors();
                if (currentNeighbors.Count > 0)
                {
                    //We have at LEAST one unvisited neighbors
                    //Pick a random one and enter it
                    var index = rand.Next(currentNeighbors.Count);

                    //Enter the next cell, mark as visited and disable applicable walls
                    EnterCell(currentCell, currentNeighbors[index]);
                    //Add new cell to the stack
                    stack.Push(currentCell);
                }
                else if (stack.Count > 0)
                {
                    //We can start backtracking
                    currentCell = stack.Pop();
                }
                else
                {
                    //There are no unvisited neighbors and the stack is empty
                    //We can exit the loop
                    unvistedCells = false;
                }
            }

            return maze;
        }

        public Maze GetMaze()
        {
            return maze;
        }

        protected virtual void EnterCell(Cell current, Cell next)
        {
            //Connect the two cells
            Cell.Connect(current, next);

            //Mark the cell as visited
            next.Visit();

            //Set our current cell to the be next cell
            currentCell = next;
        }

        public MazeGenerator(int randomSeed = -1)
        {
            if (randomSeed == -1) randomSeed = Environment.TickCount;

            rand = new Random(randomSeed);
        }
    }
}