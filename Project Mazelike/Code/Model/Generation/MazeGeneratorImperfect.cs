using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.Model.Generation {
    //This generator creates an imperfect maze with loops by
    //finding dead ends and entering a neighbor from them
    class MazeGeneratorImperfect : MazeGenerator {
        public float chance;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="chance">Chance to remove dead end from 0-1. Default = 0.5</param>
        /// <param name="randomSeed">Seed for the random generator. -1 = random seed (default)</param>
        public MazeGeneratorImperfect(float chance = 0.5f, int randomSeed = -1) : base(randomSeed) {
            this.chance = MathHelper.Clamp(chance, 0f, 1f);
        }

        public override Maze GenerateMaze(int width, int height) {
            base.GenerateMaze(width, height);

            //Find dead ends
            List<Cell> deadEnds = new List<Cell>();
            foreach (Cell cell in maze.GetCellArray()) {
                if (cell.GetNumberOfWalls() >= 3) {
                    //This is a dead end. We should now Reverse the maze process, maybe....
                    
                    //Decide if we should remove the dead end
                    if (rand.NextDouble() < chance) {
                        List<Cell> neighbors = cell.GetWalledNeighbors();
                        EnterCell(cell, neighbors[rand.Next(neighbors.Count)]);
                        Debug.WriteLine("Dead End removed");
                    }
                }
            }

            //Now continue removing dead ends until there are none, leaving only loops
            Boolean deadEndsRemain = true;
            while (deadEndsRemain) {
                deadEnds = new List<Cell>();
                deadEndsRemain = false;
                foreach (Cell cell in maze.GetCellArray()) {
                    if (cell.connectedCells.Count == 1) {
                        deadEndsRemain = true;

                        //Reset this cell
                        ResetCell(cell);
                    }
                }
            }

            return maze;
        }

        public void ResetCell(Cell cell) {
            List<Cell> cellsToDisconnect = new List<Cell>();
            foreach (Cell connected in cell.connectedCells) {
                cellsToDisconnect.Add(connected);
            }

            foreach (Cell c in cellsToDisconnect) {
                Cell.Disconnect(cell, c);
            }

            cell.Visit(false);
        }
    }
}
