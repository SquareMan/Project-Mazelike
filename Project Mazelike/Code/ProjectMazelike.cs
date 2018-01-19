using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.Controller;
using ProjectMazelike.Model;
using ProjectMazelike.View;
using ProjectMazelike.View.Scenes;

namespace ProjectMazelike
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class ProjectMazelike : Game
    {
        public delegate void GameStateChangedDelegate(GameState previousState, GameState newState);

        public enum GameState
        {
            Startup,
            MainMenu,
            Running,
            Paused
        }

        public static SpriteFont Font;

        private GameState _currentState = GameState.Startup;

        //These objects are currently not accessed here but need to exist
        // ReSharper disable once NotAccessedField.Local
        private GraphicsDeviceManager _graphics;

        // ReSharper disable once NotAccessedField.Local
        private WorldController _worldManager;

        public SpriteBatch SpriteBatch;

        public GameState CurrentState
        {
            get => _currentState;
            protected set
            {
                OnGameStateChanged?.Invoke(_currentState, value);
                _currentState = value;
            }
        }

        public static ProjectMazelike Instance { get; protected set; }
        public event GameStateChangedDelegate OnGameStateChanged;

        public ProjectMazelike()
        {
            Instance = this;

            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            OnGameStateChanged += StateChanged;
        }

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
            _worldManager = new WorldController();

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
            Font = Content.Load<SpriteFont>("Fonts/Font");

            //Pause Screen Components
            var quitGameButton = new ScreenComponentButton(
                new Vector2(GraphicsDevice.Viewport.Width / 2 - 120,
                    GraphicsDevice.Viewport.Height / 2 - 60),
                240,
                120,
                ScreenController.PauseScreen,
                DrawLayer.Ui,
                DrawSpace.Screen) {Text = "Quit Game"};

            //Make button change map the game
            quitGameButton.OnClicked += Exit;
            ScreenController.PauseScreen.AddComponent(quitGameButton);

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
        /// <param name="previousState">the previous GameState</param>
        /// <param name="newState">the new GameState</param>
        private void StateChanged(GameState previousState, GameState newState)
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
    }
}