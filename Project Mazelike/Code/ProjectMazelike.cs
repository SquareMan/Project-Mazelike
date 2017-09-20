﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMazelike.Controller;
using ProjectMazelike.View;
using System;
using System.Diagnostics;

namespace ProjectMazelike {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ProjectMazelike : Game {
        public static ProjectMazelike Instance { get; protected set; }

        public static SpriteFont font;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        WorldController worldManager;

        public SpriteBatch SpriteBatch { get => spriteBatch; private set => spriteBatch = value; }

        public ProjectMazelike() {
            Instance = this;

            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ScreenController.Initialize();
            worldManager = new WorldController();

            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            TextureController.LoadTextures(Content);
            font = Content.Load<SpriteFont>("Fonts/Font");

            //Pause Screen Components
            ScreenComponentButton button = new ScreenComponentButton(
                                           new Vector2(GraphicsDevice.Viewport.Width / 2 - 120,
                                                       GraphicsDevice.Viewport.Height / 2 - 60),
                                           240,
                                           120,
                                           ScreenController.pauseScreen,
                                           DrawLayer.UI,
                                           DrawSpace.Screen);
            button.text = "Quit Game";

            //Make button change map the game
            button.OnClicked += () => { Exit(); };
            ScreenController.pauseScreen.AddComponent(button);
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
            MouseController.Update(gameTime);
            KeyboardController.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
