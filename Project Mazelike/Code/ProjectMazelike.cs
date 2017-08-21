using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace ProjectMazelike {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ProjectMazelike : Game {
        public static readonly int MazeWidth = 8;
        public static readonly int MazeHeight = 8;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameManager gameManager;
        MouseManager mouseManager;
        KeyboardManager keyboardManager;

        public SpriteBatch SpriteBatch { get => spriteBatch; private set => spriteBatch = value; }

        public ProjectMazelike() {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            gameManager = new GameManager(this, MazeWidth, MazeHeight);
            mouseManager = new MouseManager();
            keyboardManager = new KeyboardManager();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: Add your initialization logic here
            gameManager.Initialize(GraphicsDevice);

            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            TextureManager.LoadTextures(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mouseManager.Update(gameTime);
            keyboardManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
