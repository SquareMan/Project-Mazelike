using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.Controller;
using ProjectMazelike.Model;

namespace ProjectMazelike.View
{
    internal class ScreenComponentMap : ScreenComponent
    {
        private readonly Map _map;

        public ScreenComponentMap(Map map, Screen screen, DrawLayer layer) : base(screen, layer)
        {
            _map = map;
        }

        public override void Update(GameTime gameTime)
        {
            Screen.Camera.Position = _map.Player.Position.ToVector2() * ScreenComponentMaze.CellSize +
                                     new Vector2(ScreenComponentMaze.CellSize / 2);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (var x = 0; x < _map.Width; x++)
            for (var y = 0; y < _map.Height; y++)
            {
                spriteBatch.Draw(TextureController.GetTexture(_map.GetTile(x, y).Id),
                    new Vector2(x, y) * ScreenComponentMaze.CellSize + Position, Color.White);

                if (_map.GetTile(x, y).EntityInTile?.GetType() == typeof(Enemy))
                    spriteBatch.Draw(TextureController.GetTexture("Enemy"),
                        new Vector2(x, y) * ScreenComponentMaze.CellSize + Position, Color.White);
            }

            if (_map.Player != null)
                spriteBatch.Draw(TextureController.GetTexture("Player"),
                    _map.Player.Position.ToVector2() * ScreenComponentMaze.CellSize, null, Color.White);
        }
    }
}