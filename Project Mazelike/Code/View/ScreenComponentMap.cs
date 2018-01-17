using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.Controller;
using ProjectMazelike.Model;

namespace ProjectMazelike.View
{
    internal class ScreenComponentMap : ScreenComponent
    {
        private readonly Map map;

        public override void Update(GameTime gameTime)
        {
            Screen.Camera.Position = map.Player.position.ToVector2() * ScreenComponentMaze.cellSize +
                                     new Vector2(ScreenComponentMaze.cellSize / 2);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (var x = 0; x < map.Width; x++)
            for (var y = 0; y < map.Height; y++)
            {
                spriteBatch.Draw(TextureController.GetTexture(map.GetTile(x, y).ID),
                    new Vector2(x, y) * ScreenComponentMaze.cellSize + Position, Color.White);

                if (map.GetTile(x, y).EntityInTile?.GetType() == typeof(Enemy))
                    spriteBatch.Draw(TextureController.GetTexture("Enemy"),
                        new Vector2(x, y) * ScreenComponentMaze.cellSize + Position, Color.White);
            }

            if (map.Player != null)
                spriteBatch.Draw(TextureController.GetTexture("Player"),
                    map.Player.position.ToVector2() * ScreenComponentMaze.cellSize, null, Color.White);
        }

        public ScreenComponentMap(Map map, Screen screen, DrawLayer layer) : base(screen, layer)
        {
            this.map = map;
        }
    }
}