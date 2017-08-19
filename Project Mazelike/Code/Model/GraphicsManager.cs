using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class GraphicsManager : DrawableGameComponent {
        public static int cellSize = 32;
        public static int wallSize = 2;
        Texture2D cellTexture;
        Texture2D wallTexture;

        SpriteBatch spriteBatch;

        public GraphicsManager(Game game, SpriteBatch spriteBatch) : base(game) {
            this.spriteBatch = spriteBatch;
        }

        public override void Initialize() {
            base.Initialize();
            SetupTextures();
        }

        protected override void LoadContent() {
            base.LoadContent();
        }

        void SetupTextures() {
            //Create Temporary Texture for a Cell
            Color[] data = new Color[cellSize * cellSize];
            for (int i = 0; i < data.Length; i++) {
                data[i] = Color.White;
            }

            cellTexture = new Texture2D(Game.GraphicsDevice, cellSize, cellSize);
            cellTexture.SetData(data);

            //Create Temporary Texture for a Wall
            data = new Color[wallSize * wallSize];
            for (int i = 0; i < data.Length; i++) {
                data[i] = Color.White;
            }

            wallTexture = new Texture2D(Game.GraphicsDevice, wallSize, wallSize);
            wallTexture.SetData(data);
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.Begin();
            base.Draw(gameTime);
            DrawMaze(GameManager.instance.GetMaze());
            spriteBatch.End();
        }

        public void DrawMaze(Maze maze) {
            //Get the cell array from our maze

            //loop through each cell and draw them
            List<Cell> unvisitedCells = new List<Cell>();
            foreach (Cell cell in maze.GetCellArray()) {
                //Draw rectangle at cell's position
                Rectangle rect = new Rectangle(cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);
                if (cell.Visited) {
                    spriteBatch.Draw(cellTexture, rect, Color.White);
                } else {
                    unvisitedCells.Add(cell);
                }
            }

            //Loop through each cell and draw walls
            foreach (Cell cell in maze.GetCellArray()) {

                Rectangle rect;
                //Draw rectangle at wall positions
                //Since walls are shared between cells, we only need to draw a maximum of two per cell, they must be connected
                //  i.e North/East, North/West, South/East, South/West
                //  This will not draw the walls on the edge of the screen, but they do exist.
                if (cell.WallStatus(Cell.Direction.East)) {
                    rect = new Rectangle(cell.X * cellSize + (cellSize - wallSize / 2), cell.Y * cellSize, wallSize, cellSize);
                    spriteBatch.Draw(wallTexture, rect, Color.Black);
                }
                if (cell.WallStatus(Cell.Direction.South)) {
                    rect = new Rectangle(cell.X * cellSize, cell.Y * cellSize + (cellSize - wallSize / 2), cellSize, wallSize);
                    spriteBatch.Draw(wallTexture, rect, Color.Black);
                }
            }

            //Draw unvisited cells AFTER walls so they get hidden
            foreach (Cell cell in unvisitedCells) {
                Rectangle rect = new Rectangle(cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);
                spriteBatch.Draw(cellTexture, rect, Color.Gray);
            }
        }
    }
}
