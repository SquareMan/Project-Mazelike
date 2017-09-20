using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.Model;
using ProjectMazelike.Controller;

namespace ProjectMazelike.View {
    class ScreenComponentMap : ScreenComponent {
        Map map;

        public ScreenComponentMap(Map map, Screen screen, DrawLayer layer) : base(screen, layer) {
            this.map = map;
        }

        public override void Update(GameTime gameTime) {
            Screen.Camera.Position = ((map.Player.position.ToVector2() * ScreenComponentMaze.cellSize) + new Vector2(ScreenComponentMaze.cellSize / 2));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            for (int x = 0; x < map.Tiles.GetLength(0); x++) {
                for (int y = 0; y < map.Tiles.GetLength(1); y++) {
                    spriteBatch.Draw(TextureController.GetTexture(map.Tiles[x,y].ID), (new Vector2(x,y) * ScreenComponentMaze.cellSize) + Position, Color.White);

                    if(map.Tiles[x,y].EntityInTile?.GetType() == typeof(Enemy)) {
                        spriteBatch.Draw(TextureController.GetTexture("Enemy"), (new Vector2(x, y) * ScreenComponentMaze.cellSize) + Position, Color.White);
                    }
                }
            }

            if(map.Player != null)
                spriteBatch.Draw(TextureController.GetTexture("Player"), map.Player.position.ToVector2() * ScreenComponentMaze.cellSize, null, Color.White);
        }
    }
}
