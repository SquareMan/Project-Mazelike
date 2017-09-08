﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMazelike {
    class ScreenComponentMap : ScreenComponent {
        Map map;

        public ScreenComponentMap(Map map, Screen screen, DrawLayer layer) : base(screen, layer) {
            this.map = map;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            for (int x = 0; x < map.Tiles.GetLength(0); x++) {
                for (int y = 0; y < map.Tiles.GetLength(1); y++) {
                    spriteBatch.Draw(TextureManager.GetTexture(map.Tiles[x,y].TileType.ToString()), (new Vector2(x,y) * ScreenComponentMaze.cellSize) + Position, Color.White);
                }
            }

            spriteBatch.Draw(TextureManager.GetTexture("Player"), map.Player.position.ToVector2() * ScreenComponentMaze.cellSize, null, Color.White);
        }
    }
}