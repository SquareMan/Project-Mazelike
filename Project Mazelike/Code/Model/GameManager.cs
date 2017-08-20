using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class GameManager {
        public static GameManager Instance;
        public static ProjectMazelike Game;

        public int mazeWidth;
        public int mazeHeight;
        
        public Screen screen;
        public ScreenComponentMaze mazeComponent;
        MazeGenerator ourMazeGenerator;
        //Maze generatedMaze;
        Player thePlayer;

        public MazeGenerator MazeGenerator { get => ourMazeGenerator; protected set => ourMazeGenerator = value; }
        //public Maze GeneratedMaze { get => generatedMaze; protected set => generatedMaze = value; }
        public Player Player { get => thePlayer; protected set => thePlayer = value; }

        public GameManager(ProjectMazelike game, int mazeWidth, int mazeHeight) {
            //Assign Static Values
            Instance = this;
            GameManager.Game = game;

            this.mazeWidth = mazeWidth;
            this.mazeHeight = mazeHeight;

            screen = new Screen(game);
            game.Components.Add(screen);

            thePlayer = new Player(new Point(3));
            screen.AddComponent(new ScreenComponentPlayer(thePlayer, DrawLayer.Player));
        }

        public void Initialize(GraphicsDevice graphicsDevice) {
            MazeGenerator = new MazeGeneratorImperfect(.33f);
            Maze generatedMaze = MazeGenerator.GenerateMaze(mazeWidth, mazeHeight);

            mazeComponent = new ScreenComponentMaze(generatedMaze, DrawLayer.Background);
            screen.AddComponent(mazeComponent);
        }

        public Maze GetMaze() {
            return MazeGenerator.GetMaze();
        }

        //DEBUG
        //TODO: REMOVE ME
        public void CycleGenerator() {
            screen.RemoveComponent(mazeComponent);
            Maze newMaze;

            if(MazeGenerator.GetType() == typeof(MazeGenerator)) {
                MazeGenerator = new MazeGeneratorImperfect(.33f);
                newMaze = MazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            } else {
                MazeGenerator = new MazeGenerator();
                newMaze = MazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            }

            mazeComponent = new ScreenComponentMaze(newMaze, DrawLayer.Background);
            screen.AddComponent(mazeComponent);
        }
    }
}
