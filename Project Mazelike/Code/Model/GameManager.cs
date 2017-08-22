using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class GameManager {
        public Boolean DEBUGDrawMaze = false;

        public static GameManager Instance;
        public static ProjectMazelike Game;

        public int mazeWidth;
        public int mazeHeight;
        
        public Screen Screen { get; set; }
        public ScreenComponentMaze mazeComponent;
        MazeGenerator ourMazeGenerator;
        Player thePlayer;

        public MazeGenerator MazeGenerator { get => ourMazeGenerator; protected set => ourMazeGenerator = value; }

        public Player Player { get => thePlayer; protected set => thePlayer = value; }


        Map testMap;
        Dictionary<Tile, ScreenComponent> tileToScreenComponentMap;

        public GameManager(ProjectMazelike game, int mazeWidth, int mazeHeight) {
            //Assign Static Values
            Instance = this;
            GameManager.Game = game;

            this.mazeWidth = mazeWidth;
            this.mazeHeight = mazeHeight;

            tileToScreenComponentMap = new Dictionary<Tile, ScreenComponent>();
        }

        public void Initialize(GraphicsDevice graphicsDevice) {
            Screen = new Screen(Game);
            Screen.SamplerState = SamplerState.PointClamp;
            Game.Components.Add(Screen);

            thePlayer = new Player(new Point(3));
            Screen.AddComponent(new ScreenComponentPlayer(thePlayer, DrawLayer.Player));

            MazeGenerator = new MazeGeneratorImperfect(.33f);
            NewMaze();

            testMap = new Map(ProjectMazelike.MazeWidth, ProjectMazelike.MazeHeight);
            foreach(Tile t in testMap.Tiles) {
                tileToScreenComponentMap.Add(t, new ScreenComponentTile(t, DrawLayer.Background));
                Screen.AddComponent(tileToScreenComponentMap[t]);
            }
        }

        public Maze GetMaze() {
            return MazeGenerator.GetMaze();
        }
        
        public void NewMaze() {
            if (mazeComponent != null)
                Screen.RemoveComponent(mazeComponent);

            Maze generatedMaze = MazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            
            if (DEBUGDrawMaze) {
                mazeComponent = new ScreenComponentMaze(generatedMaze, DrawLayer.Background);
                Screen.AddComponent(mazeComponent);
            }
        }

        //DEBUG
        //TODO: REMOVE ME
        public void CycleGenerator() {
            Screen.RemoveComponent(mazeComponent);
            Maze newMaze;

            if(MazeGenerator.GetType() == typeof(MazeGenerator)) {
                MazeGenerator = new MazeGeneratorImperfect(.33f);
                newMaze = MazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            } else {
                MazeGenerator = new MazeGenerator();
                newMaze = MazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            }

            if (DEBUGDrawMaze) {
                mazeComponent = new ScreenComponentMaze(newMaze, DrawLayer.Background);
                Screen.AddComponent(mazeComponent);
            }
        }
    }
}
