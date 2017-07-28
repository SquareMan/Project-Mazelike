using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class MazeGenerator {

        protected Maze maze;
        protected Cell currentCell;
        protected Stack<Cell> stack;

        protected Random rand;

        public MazeGenerator(int randomSeed = -1) {
            if(randomSeed == -1) {
                randomSeed = Environment.TickCount;
            }
            rand = new Random(randomSeed);
        }

        public virtual void GenerateMaze(int width, int height) {
            maze = new Maze(width, height);
            stack = new Stack<Cell>();

            //Set an initial current cell and mark as visited
            currentCell = maze.GetCell(0, 0);
            currentCell.Visit();

            //Start main loop
            Boolean unvistedCells = true;
            while(unvistedCells) {
                List<Cell> currentNeighbors = currentCell.GetUnvisitedNeighbors();
                if (currentNeighbors.Count > 0) {
                    //We have at LEAST one unvisited neighbors
                    //Pick a random one and enter it
                    int index = rand.Next(currentNeighbors.Count);

                    //Enter the next cell, mark as visited and disable applicable walls
                    EnterCell(currentCell, currentNeighbors[index]);
                    //Add new cell to the stack
                    stack.Push(currentCell);
                } else if (stack.Count > 0) {
                    //We can start backtracking
                    currentCell = stack.Pop();
                } else {
                    //There are no unvisited neighbors and the stack is empty
                    //We can exit the loop
                    unvistedCells = false;
                }                
            }

        }

        public Maze GetMaze() {
            return maze;
        }
        
        protected void EnterCell(Cell current, Cell next) {
            //Find the direction

            //Remove the walls between current and next
            if(current.X != next.X) {
                //We are moving horizontally
                if(next.X > current.X) {
                    //We moved to the Right
                    current.eastWall = false;
                    next.westWall = false;
                } else if(next.X < current.X) {
                    //We moved to the Left
                    current.westWall = false;
                    next.eastWall = false;
                }
            } else if (current.Y != next.Y) {
                //We are moving vertically
                if(next.Y > current.Y) {
                    //We are moving down
                    current.southWall = false;
                    next.northWall = false;
                } else if(next.Y < current.Y) {
                    //We are moving up
                    current.northWall = false;
                    next.southWall = false;
                }
            }
            next.Visit();

            //Set our current cell to the be next cell
            this.currentCell = next;
        }
    }
}
