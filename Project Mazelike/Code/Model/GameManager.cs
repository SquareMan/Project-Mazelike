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

        public ScreenManager screenManager;
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

            screenManager = new ScreenManager();

            tileToScreenComponentMap = new Dictionary<Tile, ScreenComponent>();
        }

        public void Initialize(GraphicsDevice graphicsDevice) {
            screenManager.AddScreen("Game");
            screenManager.AddScreen("Pause");
            screenManager.SetActiveScreen("Game");

            screenManager.GetScreen("Game").SamplerState = SamplerState.PointClamp;

            screenManager.GetScreen("Pause").canBeRotated = true;

            thePlayer = new Player(new Point(3));
            screenManager.GetScreen("Game").AddComponent(new ScreenComponentPlayer(thePlayer, DrawLayer.Player));

            MazeGenerator = new MazeGeneratorImperfect(.33f);
            NewMaze();

            screenManager.GetScreen("Pause").AddComponent(new ScreenComponentMaze(MazeGenerator.GetMaze(), DrawLayer.Background));
            ScreenComponentButton button = new ScreenComponentButton(new Point(Game.GraphicsDevice.Viewport.Width / 2 - 100, Game.GraphicsDevice.Viewport.Height / 2 - 40), DrawLayer.Background);
            button.OnClicked += () => { Game.Exit(); };
            screenManager.GetScreen("Pause").AddComponent(button);

            testMap = new Map(ProjectMazelike.MazeWidth, ProjectMazelike.MazeHeight);
            foreach(Tile t in testMap.Tiles) {
                tileToScreenComponentMap.Add(t, new ScreenComponentTile(t, DrawLayer.Background));
                screenManager.GetScreen("Game").AddComponent(tileToScreenComponentMap[t]);
            }
            thePlayer.SetMap(testMap);
            testMap.Tiles[3, 3].SetTileType(TileType.Wall);
        }

        public Maze GetMaze() {
            return MazeGenerator.GetMaze();
        }
        
        public void NewMaze() {
            MazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
        }
    }
}
