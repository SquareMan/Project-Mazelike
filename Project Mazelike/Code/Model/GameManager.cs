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

        protected Game game;
        
        MazeGenerator ourMazeGenerator;
        Player thePlayer;

        public MazeGenerator MazeGenerator { get => ourMazeGenerator; protected set => ourMazeGenerator = value; }
        public Player Player { get => thePlayer; protected set => thePlayer = value; }

        public GameManager(Game game, int mazeWidth, int mazeHeight) {
            GameManager.instance = this;
            this.game = game;

            this.mazeWidth = mazeWidth;
            this.mazeHeight = mazeHeight;

            thePlayer = new Player(game);
            game.Components.Add(thePlayer);
        }

        public void Initialize(GraphicsDevice graphicsDevice) {
            MazeGenerator = new MazeGeneratorImperfect(.33f);
            MazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
        }

        public Maze GetMaze() {
            return MazeGenerator.GetMaze();
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
    }
}
