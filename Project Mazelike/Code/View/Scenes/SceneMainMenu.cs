using Microsoft.Xna.Framework;
using ProjectMazelike.Controller;

namespace ProjectMazelike.View.Scenes
{
    internal class SceneMainMenu
    {
        private readonly ScreenComponentButton backButton;

        private readonly ScreenComponentSprite background;
        private ProjectMazelike game;
        private readonly ScreenComponentButton newGameButton;
        private readonly ScreenComponentButton quitGameButton;
        private readonly Screen screen;

        private readonly ScreenComponentButton startGameButton;

        private void OnClicked_NewGame()
        {
            startGameButton.Enabled = true;
            backButton.Enabled = true;

            newGameButton.Enabled = false;
            quitGameButton.Enabled = false;
        }

        private void OnClicked_Back()
        {
            newGameButton.Enabled = true;
            quitGameButton.Enabled = true;
            startGameButton.Enabled = false;
            backButton.Enabled = false;
        }


        public SceneMainMenu(ProjectMazelike game)
        {
            this.game = game;
            screen = ScreenController.GetScreen("Main Menu");

            //Main Components
            background = new ScreenComponentSprite(
                new Sprite(TextureController.GetTexture("Player"), game.GraphicsDevice.Viewport.Width,
                    game.GraphicsDevice.Viewport.Height, Vector2.Zero),
                ScreenController.mainMenuScreen,
                DrawLayer.Background,
                DrawSpace.Screen);
            screen.AddComponent(background);

            newGameButton = new ScreenComponentButton(
                new Vector2(game.GraphicsDevice.Viewport.Width / 2 - 120,
                    game.GraphicsDevice.Viewport.Height / 2 - 140),
                240,
                120,
                ScreenController.mainMenuScreen,
                DrawLayer.UI,
                DrawSpace.Screen);
            newGameButton.text = "New Game";
            newGameButton.OnClicked += OnClicked_NewGame;
            screen.AddComponent(newGameButton);

            quitGameButton = new ScreenComponentButton(
                new Vector2(game.GraphicsDevice.Viewport.Width / 2 - 120,
                    game.GraphicsDevice.Viewport.Height / 2 + 20),
                240,
                120,
                ScreenController.mainMenuScreen,
                DrawLayer.UI,
                DrawSpace.Screen);
            quitGameButton.text = "Quit Game";
            quitGameButton.OnClicked += game.Exit;
            screen.AddComponent(quitGameButton);

            //New Game submenu
            startGameButton = new ScreenComponentButton(
                new Vector2(game.GraphicsDevice.Viewport.Width / 2 - 120,
                    game.GraphicsDevice.Viewport.Height / 2 - 60),
                240,
                120,
                ScreenController.mainMenuScreen,
                DrawLayer.UI,
                DrawSpace.Screen);
            startGameButton.text = "Start";
            startGameButton.OnClicked += game.StartGame;
            startGameButton.Enabled = false;
            screen.AddComponent(startGameButton);

            backButton = new ScreenComponentButton(
                startGameButton.Position + new Vector2(0, 150),
                160,
                80,
                ScreenController.mainMenuScreen,
                DrawLayer.UI,
                DrawSpace.Screen);
            backButton.text = "Back";
            backButton.OnClicked += OnClicked_Back;
            backButton.Enabled = false;
            screen.AddComponent(backButton);
        }
    }
}