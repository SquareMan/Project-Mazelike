﻿using Microsoft.Xna.Framework;
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

        public static void LoadTextures(ContentManager content) {
            textureMap = new Dictionary<string, Texture2D>();

            textureMap.Add("Player", content.Load<Texture2D>("Graphics\\player"));

            //DEBUG MAZE CELL TEXTURE
            SetupTextures();
        }

        static void SetupTextures() {
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
            textureMap.Add("Wall", wallTexture);
        }

        public static Texture2D GetTexture(String name) {
            if(textureMap[name] != null) {
                return textureMap[name];
            }

            Debug.WriteLine(String.Format("Texture with name {0} was attempted to be retrieved but does not exist", name));
            return null;
        }
    }
}