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
        Player thePlayer;

        public MazeGenerator MazeGenerator { get => ourMazeGenerator; protected set => ourMazeGenerator = value; }

        public Player Player { get => thePlayer; protected set => thePlayer = value; }

        public GameManager(ProjectMazelike game, int mazeWidth, int mazeHeight) {
            //Assign Static Values
            Instance = this;
            GameManager.Game = game;

            this.mazeWidth = mazeWidth;
            this.mazeHeight = mazeHeight;
        }

        public void Initialize(GraphicsDevice graphicsDevice) {

            screen = new Screen(Game);
            Game.Components.Add(screen);

            thePlayer = new Player(new Point(3));
            screen.AddComponent(new ScreenComponentPlayer(thePlayer, DrawLayer.Player));

            MazeGenerator = new MazeGeneratorImperfect(.33f);
            NewMaze();
        }

        public Maze GetMaze() {
            return MazeGenerator.GetMaze();
        }

        public void NewMaze() {
            if (mazeComponent != null)
                screen.RemoveComponent(mazeComponent);

            Maze generatedMaze = MazeGenerator.GenerateMaze(mazeWidth, mazeHeight);

            mazeComponent = new ScreenComponentMaze(generatedMaze, DrawLayer.Background);
            screen.AddComponent(mazeComponent);
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
