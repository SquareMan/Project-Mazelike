using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.Controller;
using ProjectMazelike.View;
using ProjectMazelike.View.Scenes;

namespace ProjectMazelike
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class ProjectMazelike : Game
    {
        public delegate void GameStateChangedDelegate(GameState newState);

        public enum GameState
        {
            Startup,
            MainMenu,
            Running,
            Paused
        }

        public static SpriteFont font;

        private GameState _currentState = GameState.Startup;

        private GraphicsDeviceManager graphics;

        public SpriteBatch SpriteBatch;
        private WorldController worldManager;

        public GameState CurrentState
        {
            get => _currentState;
            protected set
            {
                _currentState = value;
                OnGameStateChanged?.Invoke(_currentState);
            }
        }

        public static ProjectMazelike Instance { get; protected set; }
        public event GameStateChangedDelegate OnGameStateChanged;

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            KeyboardController.Initialize();
            ScreenController.Initialize();
            worldManager = new WorldController();

            IsMouseVisible = true;

            base.Initialize();

            CurrentState = GameState.MainMenu;
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            TextureController.LoadTextures(Content);
            font = Content.Load<SpriteFont>("Fonts/Font");

            //Pause Screen Components
            var quitGameButton = new ScreenComponentButton(
                new Vector2(GraphicsDevice.Viewport.Width / 2 - 120,
                    GraphicsDevice.Viewport.Height / 2 - 60),
                240,
                120,
                ScreenController.pauseScreen,
                DrawLayer.UI,
                DrawSpace.Screen);
            quitGameButton.text = "Quit Game";

            //Make button change map the game
            quitGameButton.OnClicked += Exit;
            ScreenController.pauseScreen.AddComponent(quitGameButton);

            var scene = new SceneMainMenu(this);
        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseController.Update(gameTime);
            KeyboardController.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }

        /////////////////////////////////////////////////////////////////////////////////
        //Game State Code
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Start a new game
        /// </summary>
        public void StartGame()
        {
            if (CurrentState != GameState.MainMenu)
                return;
            CurrentState = GameState.Running;
        }

        /// <summary>
        ///     Puase the current game
        /// </summary>
        public void PauseGame()
        {
            if (CurrentState != GameState.Running)
                return;
            CurrentState = GameState.Paused;
        }

        /// <summary>
        ///     Unpause the current game
        /// </summary>
        public void UnpauseGame()
        {
            if (CurrentState != GameState.Paused)
                return;
            CurrentState = GameState.Running;
        }

        /// <summary>
        ///     Runs when the CurrentState is changed
        /// </summary>
        /// <param name="newState">the new GameState</param>
        private void StateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.Startup:
                    break;
                case GameState.MainMenu:
                    break;
                case GameState.Running:
                    break;
                case GameState.Paused:
                    break;
            }
        }

        public ProjectMazelike()
        {
            Instance = this;

            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            OnGameStateChanged += StateChanged;
        }
    }
}