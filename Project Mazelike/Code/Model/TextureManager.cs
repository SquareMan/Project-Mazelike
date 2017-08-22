using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    static class TextureManager {
        static Dictionary<String, Texture2D> textureMap;

        private static String missingTexture = "MISSINGTEXTURE";

        public static void LoadTextures(ContentManager content) {
            textureMap = new Dictionary<string, Texture2D>();
            textureMap.Add(missingTexture, content.Load<Texture2D>("Graphics\\Tiles\\MissingTile"));
            textureMap.Add("Player", content.Load<Texture2D>("Graphics\\player"));
            //textureMap.Add("Floor", content.Load<Texture2D>("Graphics\\Tiles\\Floor"));
            LoadTileTextures(content);

            //DEBUG MAZE CELL TEXTURE
            SetupMazeTextures();
        }

        static void LoadTileTextures(ContentManager content) {
            foreach (string name in Enum.GetNames(typeof(TileType))) {
                textureMap.Add(name, content.Load<Texture2D>("Graphics\\Tiles\\" + name));
            }
        }

        static void SetupMazeTextures() {
            //Create Temporary Texture for a Cell
            Color[] data = new Color[ScreenComponentMaze.cellSize * ScreenComponentMaze.cellSize];
            for (int i = 0; i < data.Length; i++) {
                data[i] = Color.White;
            }

            Texture2D cellTexture = new Texture2D(GameManager.Game.GraphicsDevice, ScreenComponentMaze.cellSize, ScreenComponentMaze.cellSize);
            cellTexture.SetData(data);
            textureMap.Add("Cell", cellTexture);

            //Create Temporary Texture for a Wall
            data = new Color[ScreenComponentMaze.wallSize * ScreenComponentMaze.wallSize];
            for (int i = 0; i < data.Length; i++) {
                data[i] = Color.White;
            }

            Texture2D wallTexture = new Texture2D(GameManager.Game.GraphicsDevice, ScreenComponentMaze.wallSize, ScreenComponentMaze.wallSize);
            wallTexture.SetData(data);
            textureMap.Add("Maze Wall", wallTexture);
        }

        public static Texture2D GetTexture(String name) {
            if (textureMap.Keys.Contains(name)) {
                return textureMap[name];
            }

            Debug.WriteLine(String.Format("Texture with name {0} was attempted to be retrieved but does not exist", name));
            return textureMap[missingTexture];
        }
    }
}
