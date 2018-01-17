using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace ProjectMazelike.Model.Generation
{
    //This generator creates an imperfect maze with loops by
    //finding dead ends and entering a neighbor from them
    internal class MazeGeneratorImperfect : MazeGenerator
    {
        public float chance;

        public override Maze GenerateMaze(int width, int height)
        {
            base.GenerateMaze(width, height);

            //Find dead ends
            var deadEnds = new List<Cell>();
            foreach (var cell in maze.GetCellArray())
                if (cell.GetNumberOfWalls() >= 3)
                    if (rand.NextDouble() < chance)
                    {
                        var neighbors = cell.GetWalledNeighbors();
                        EnterCell(cell, neighbors[rand.Next(neighbors.Count)]);
                        Debug.WriteLine("Dead End removed");
                    }

            //Now continue removing dead ends until there are none, leaving only loops
            var deadEndsRemain = true;
            while (deadEndsRemain)
            {
                deadEnds = new List<Cell>();
                deadEndsRemain = false;
                foreach (var cell in maze.GetCellArray())
                    if (cell.connectedCells.Count == 1)
                    {
                        deadEndsRemain = true;

                        //Reset this cell
                        ResetCell(cell);
                    }
            }

            return maze;
        }

        public void ResetCell(Cell cell)
        {
            var cellsToDisconnect = new List<Cell>();
            foreach (var connected in cell.connectedCells) cellsToDisconnect.Add(connected);

            foreach (var c in cellsToDisconnect) Cell.Disconnect(cell, c);

            cell.Visit(false);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="chance">Chance to remove dead end from 0-1. Default = 0.5</param>
        /// <param name="randomSeed">Seed for the random generator. -1 = random seed (default)</param>
        public MazeGeneratorImperfect(float chance = 0.5f, int randomSeed = -1) : base(randomSeed)
        {
            this.chance = MathHelper.Clamp(chance, 0f, 1f);
        }
    }
}