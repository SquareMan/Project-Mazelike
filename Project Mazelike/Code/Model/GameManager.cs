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
            MazeGenerator = new MazeGeneratorImperfect(.33f);
            NewMaze();

            thePlayer = new Player(new Point(3));
            
            //Setup screens
            Screen gameScreen = screenManager.AddScreen("Game", true, false, true);
            gameScreen.SamplerState = SamplerState.PointClamp;

            Screen pauseScreen = screenManager.AddScreen("Pause", true, false, true);
            pauseScreen.SamplerState = SamplerState.PointClamp;

            screenManager.SetActiveScreen("Game");

            //Game Screen Components
            //testMap = Map.TestMap("RoomExample");
            //foreach(Tile t in testMap.Tiles) {
            //    tileToScreenComponentMap.Add(t, new ScreenComponentTile(t, gameScreen, DrawLayer.Background));
            //    screenManager.GetScreen("Game").AddComponent(tileToScreenComponentMap[t]);
            //}
            //thePlayer.SetMap(testMap);
            ChangeMap("RoomExample");

            gameScreen.AddComponent(new ScreenComponentPlayer(thePlayer, gameScreen, DrawLayer.Player));

            //Pause Screen Components
            pauseScreen.AddComponent(new ScreenComponentMaze(MazeGenerator.GetMaze(), pauseScreen, DrawLayer.Background));

            ScreenComponentButton button = new ScreenComponentButton(
                                           new Point(Game.GraphicsDevice.Viewport.Width / 2 - 120,
                                                     Game.GraphicsDevice.Viewport.Height / 2 - 60),
                                           240,
                                           120,
                                           pauseScreen,
                                           DrawLayer.UI);

            //Make button change map the game
            button.OnClicked += () => { Game.Exit(); };
            pauseScreen.AddComponent(button);

            ScreenComponentButton mapButton = new ScreenComponentButton(
                                           new Point(Game.GraphicsDevice.Viewport.Width - 125,
                                                     5),
                                           120,
                                           80,
                                           pauseScreen,
                                           DrawLayer.UI);

            //Make button close the game
            mapButton.OnClicked += () => { ChangeMap("RoomExample2"); };
            gameScreen.AddComponent(mapButton);
        }

        public void ChangeMap(string map) {
            testMap = Map.TestMap(map);
            foreach (Tile t in testMap.Tiles) {
                tileToScreenComponentMap[t] = new ScreenComponentTile(t, screenManager.GetScreen("Game"), DrawLayer.Background);
                screenManager.GetScreen("Game").AddComponent(tileToScreenComponentMap[t]);
            }
            thePlayer.SetMap(testMap);
        }

        public Maze GetMaze() {
            return MazeGenerator.GetMaze();
        }
        
        public void NewMaze() {
            MazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
        }
    }
}
