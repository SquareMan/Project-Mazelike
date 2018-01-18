using Microsoft.Xna.Framework;
using ProjectMazelike.Controller;

namespace ProjectMazelike.View.Scenes
{
    internal class SceneMainMenu
    {
        private readonly ScreenComponentButton _backButton;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly ScreenComponentSprite _background;
        private readonly ScreenComponentButton _newGameButton;
        private readonly ScreenComponentButton _quitGameButton;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly Screen _screen;

        private readonly ScreenComponentButton _startGameButton;

        // ReSharper disable once NotAccessedField.Local
        private ProjectMazelike _game;


        public SceneMainMenu(ProjectMazelike game)
        {
            _game = game;
            _screen = ScreenController.GetScreen("Main Menu");

            //Main Components
            _background = new ScreenComponentSprite(
                new Sprite(TextureController.GetTexture("Player"), game.GraphicsDevice.Viewport.Width,
                    game.GraphicsDevice.Viewport.Height, Vector2.Zero),
                ScreenController.MainMenuScreen,
                DrawLayer.Background,
                DrawSpace.Screen);
            _screen.AddComponent(_background);

            _newGameButton = new ScreenComponentButton(
                new Vector2(game.GraphicsDevice.Viewport.Width / 2 - 120,
                    game.GraphicsDevice.Viewport.Height / 2 - 140),
                240,
                120,
                ScreenController.MainMenuScreen,
                DrawLayer.Ui,
                DrawSpace.Screen);
            _newGameButton.Text = "New Game";
            _newGameButton.OnClicked += OnClicked_NewGame;
            _screen.AddComponent(_newGameButton);

            _quitGameButton = new ScreenComponentButton(
                new Vector2(game.GraphicsDevice.Viewport.Width / 2 - 120,
                    game.GraphicsDevice.Viewport.Height / 2 + 20),
                240,
                120,
                ScreenController.MainMenuScreen,
                DrawLayer.Ui,
                DrawSpace.Screen);
            _quitGameButton.Text = "Quit Game";
            _quitGameButton.OnClicked += game.Exit;
            _screen.AddComponent(_quitGameButton);

            //New Game submenu
            _startGameButton = new ScreenComponentButton(
                new Vector2(game.GraphicsDevice.Viewport.Width / 2 - 120,
                    game.GraphicsDevice.Viewport.Height / 2 - 60),
                240,
                120,
                ScreenController.MainMenuScreen,
                DrawLayer.Ui,
                DrawSpace.Screen);
            _startGameButton.Text = "Start";
            _startGameButton.OnClicked += game.StartGame;
            _startGameButton.Enabled = false;
            _screen.AddComponent(_startGameButton);

            _backButton = new ScreenComponentButton(
                _startGameButton.Position + new Vector2(0, 150),
                160,
                80,
                ScreenController.MainMenuScreen,
                DrawLayer.Ui,
                DrawSpace.Screen);
            _backButton.Text = "Back";
            _backButton.OnClicked += OnClicked_Back;
            _backButton.Enabled = false;
            _screen.AddComponent(_backButton);
        }

        private void OnClicked_NewGame()
        {
            _startGameButton.Enabled = true;
            _backButton.Enabled = true;

            _newGameButton.Enabled = false;
            _quitGameButton.Enabled = false;
        }

        private void OnClicked_Back()
        {
            _newGameButton.Enabled = true;
            _quitGameButton.Enabled = true;
            _startGameButton.Enabled = false;
            _backButton.Enabled = false;
        }
    }
}