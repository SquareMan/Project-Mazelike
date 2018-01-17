using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.Controller;
using ProjectMazelike.Model.Generation;

namespace ProjectMazelike.View
{
    internal class ScreenComponentMaze : ScreenComponent
    {
        public static int CellSize = 32;
        public static int WallSize = 2;

        private readonly Maze _maze;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //loop through each cell and draw them
            var unvisitedCells = new List<Cell>();
            foreach (var cell in _maze.GetCellArray())
            {
                //Draw rectangle at cell's position
                var rect = new Rectangle(cell.X * CellSize, cell.Y * CellSize, CellSize, CellSize);
                if (cell.Visited)
                    spriteBatch.Draw(TextureController.GetTexture("Cell"), rect, Color.White);
                else
                    unvisitedCells.Add(cell);
            }

            //Loop through each cell and draw walls
            foreach (var cell in _maze.GetCellArray())
            {
                Rectangle rect;
                //Draw rectangle at wall positions
                //Since walls are shared between cells, we only need to draw a maximum of two per cell, they must be connected
                //  i.e North/East, North/West, South/East, South/West
                //  This will not draw the walls on the edge of the screen, but they do exist.
                if (cell.WallStatus(Cell.Direction.East))
                {
                    rect = new Rectangle(cell.X * CellSize + (CellSize - WallSize / 2), cell.Y * CellSize, WallSize,
                        CellSize);
                    spriteBatch.Draw(TextureController.GetTexture("Maze Wall"), rect, Color.Black);
                }

                if (cell.WallStatus(Cell.Direction.South))
                {
                    rect = new Rectangle(cell.X * CellSize, cell.Y * CellSize + (CellSize - WallSize / 2), CellSize,
                        WallSize);
                    spriteBatch.Draw(TextureController.GetTexture("Maze Wall"), rect, Color.Black);
                }
            }

            //Draw unvisited cells AFTER walls so they get hidden
            foreach (var cell in unvisitedCells)
            {
                var rect = new Rectangle(cell.X * CellSize, cell.Y * CellSize, CellSize, CellSize);
                spriteBatch.Draw(TextureController.GetTexture("Cell"), rect, Color.Gray);
            }
        }

        public ScreenComponentMaze(Maze maze, Screen screen, DrawLayer layer) : base(screen, layer)
        {
            _maze = maze;
        }
    }
}