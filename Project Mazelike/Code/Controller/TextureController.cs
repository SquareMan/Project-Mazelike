using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.Model;
using ProjectMazelike.View;

namespace ProjectMazelike.Controller {
    static class TextureController {
        static Dictionary<String, Texture2D> textureMap;

        private static String missingTexture = "MISSINGTEXTURE";

        public static void LoadTextures(ContentManager content) {
            textureMap = new Dictionary<string, Texture2D>();
            textureMap.Add(missingTexture, content.Load<Texture2D>("Graphics\\Tiles\\MissingTile"));
            textureMap.Add("Player", content.Load<Texture2D>("Graphics\\Player"));
            textureMap.Add("Enemy", content.Load<Texture2D>("Graphics\\Enemy"));
            textureMap.Add("Button", content.Load<Texture2D>("Graphics\\UI\\Button"));
            LoadTileTextures(content);

            //DEBUG MAZE CELL TEXTURE
            SetupMazeTextures();
        }

        static void LoadTileTextures(ContentManager content) {
            foreach (string name in Tile.tilePrototypes.Keys) {
                try {
                    textureMap.Add(name, content.Load<Texture2D>("Graphics\\Tiles\\" + name));
                } catch {
                    Debug.WriteLine("Texture for Tile: " + name + " Does not exist");
                }
            }
        }

        static void SetupMazeTextures() {
            //Create Temporary Texture for a Cell
            Color[] data = new Color[ScreenComponentMaze.cellSize * ScreenComponentMaze.cellSize];
            for (int i = 0; i < data.Length; i++) {
                data[i] = Color.White;
            }

            Texture2D cellTexture = new Texture2D(ProjectMazelike.Instance.GraphicsDevice, ScreenComponentMaze.cellSize, ScreenComponentMaze.cellSize);
            cellTexture.SetData(data);
            textureMap.Add("Cell", cellTexture);

            //Create Temporary Texture for a Wall
            data = new Color[ScreenComponentMaze.wallSize * ScreenComponentMaze.wallSize];
            for (int i = 0; i < data.Length; i++) {
                data[i] = Color.White;
            }

            Texture2D wallTexture = new Texture2D(ProjectMazelike.Instance.GraphicsDevice, ScreenComponentMaze.wallSize, ScreenComponentMaze.wallSize);
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
