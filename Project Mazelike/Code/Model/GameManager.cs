using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class GameManager {
        public static GameManager instance;

        public int mazeWidth;
        public int mazeHeight;
        
        MazeGenerator ourMazeGenerator;

        public int cellSize = 20;
        public int wallSize = 2;
        Texture2D cellTexture;
        Texture2D wallTexture;

        public MazeGenerator MazeGenerator { get => ourMazeGenerator; protected set => ourMazeGenerator = value; }

        public GameManager(int mazeWidth, int mazeHeight) {
            GameManager.instance = this;

            this.mazeWidth = mazeWidth;
            this.mazeHeight = mazeHeight;
        }

        public void Initialize(GraphicsDevice graphicsDevice) {
            MazeGenerator = new MazeGeneratorImperfect(.33f);
            MazeGenerator.GenerateMaze(mazeWidth, mazeHeight);

            SetupTextures(graphicsDevice);
        }

        void SetupTextures(GraphicsDevice graphicsDevice) {
            //Create Temporary Texture for a Cell
            Color[] data = new Color[cellSize * cellSize];
            for (int i = 0; i < data.Length; i++) {
                data[i] = Color.White;
            }

            cellTexture = new Texture2D(graphicsDevice, cellSize, cellSize);
            cellTexture.SetData(data);

            //Create Temporary Texture for a Wall
            data = new Color[wallSize * wallSize];
            for (int i = 0; i < data.Length; i++) {
                data[i] = Color.White;
            }

            wallTexture = new Texture2D(graphicsDevice, wallSize, wallSize);
            wallTexture.SetData(data);
        }

        //DEBUG
        //TODO: REMOVE ME
        public void CycleGenerator() {
            if(MazeGenerator.GetType() == typeof(MazeGenerator)) {
                MazeGenerator = new MazeGeneratorImperfect(.33f);
                MazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            } else {
                MazeGenerator = new MazeGenerator();
                MazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            }
        }

        public void DrawMaze(SpriteBatch spriteBatch) {
            //Get the cell array from our maze
            Maze ourMaze = MazeGenerator.GetMaze();

            //loop through each cell and draw them
            List<Cell> unvisitedCells = new List<Cell>();
            foreach (Cell cell in ourMaze.GetCellArray()) {
                //Draw rectangle at cell's position
                Rectangle rect = new Rectangle(cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);
                if (cell.Visited) {
                    spriteBatch.Draw(cellTexture, rect, Color.White);
                } else {
                    unvisitedCells.Add(cell);
                }
            }

            //Loop through each cell and draw walls
            foreach (Cell cell in ourMaze.GetCellArray()) {

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

            foreach (Cell cell in unvisitedCells) {
                Rectangle rect = new Rectangle(cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);
                spriteBatch.Draw(cellTexture, rect, Color.Gray);
            }
        }
    }
}
