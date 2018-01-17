﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.Controller;
using ProjectMazelike.Model.Generation;

namespace ProjectMazelike.View
{
    class ScreenComponentMaze : ScreenComponent
    {
        public static int cellSize = 32;
        public static int wallSize = 2;

        Maze maze;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //loop through each cell and draw them
            List<Cell> unvisitedCells = new List<Cell>();
            foreach (Cell cell in maze.GetCellArray())
            {
                //Draw rectangle at cell's position
                Rectangle rect = new Rectangle(cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);
                if (cell.Visited)
                {
                    spriteBatch.Draw(TextureController.GetTexture("Cell"), rect, Color.White);
                }
                else
                {
                    unvisitedCells.Add(cell);
                }
            }

            //Loop through each cell and draw walls
            foreach (Cell cell in maze.GetCellArray())
            {
                Rectangle rect;
                //Draw rectangle at wall positions
                //Since walls are shared between cells, we only need to draw a maximum of two per cell, they must be connected
                //  i.e North/East, North/West, South/East, South/West
                //  This will not draw the walls on the edge of the screen, but they do exist.
                if (cell.WallStatus(Cell.Direction.East))
                {
                    rect = new Rectangle(cell.X * cellSize + (cellSize - wallSize / 2), cell.Y * cellSize, wallSize,
                        cellSize);
                    spriteBatch.Draw(TextureController.GetTexture("Maze Wall"), rect, Color.Black);
                }

                if (cell.WallStatus(Cell.Direction.South))
                {
                    rect = new Rectangle(cell.X * cellSize, cell.Y * cellSize + (cellSize - wallSize / 2), cellSize,
                        wallSize);
                    spriteBatch.Draw(TextureController.GetTexture("Maze Wall"), rect, Color.Black);
                }
            }

            //Draw unvisited cells AFTER walls so they get hidden
            foreach (Cell cell in unvisitedCells)
            {
                Rectangle rect = new Rectangle(cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);
                spriteBatch.Draw(TextureController.GetTexture("Cell"), rect, Color.Gray);
            }
        }

        public ScreenComponentMaze(Maze maze, Screen screen, DrawLayer layer) : base(screen, layer)
        {
            this.maze = maze;
        }
    }
}