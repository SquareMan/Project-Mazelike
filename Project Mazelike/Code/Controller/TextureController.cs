using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.Model;
using ProjectMazelike.View;

namespace ProjectMazelike.Controller
{
    internal static class TextureController
    {
        private static Dictionary<string, Texture2D> textureMap;

        private static readonly string missingTexture = "MISSINGTEXTURE";

        public static void LoadTextures(ContentManager content)
        {
            textureMap = new Dictionary<string, Texture2D>();
            textureMap.Add(missingTexture, content.Load<Texture2D>("Graphics\\Tiles\\MissingTile"));
            textureMap.Add("Player", content.Load<Texture2D>("Graphics\\Player"));
            textureMap.Add("Enemy", content.Load<Texture2D>("Graphics\\Enemy"));
            textureMap.Add("Button", content.Load<Texture2D>("Graphics\\UI\\Button"));
            LoadTileTextures(content);

            //DEBUG MAZE CELL TEXTURE
            SetupMazeTextures();
        }

        private static void LoadTileTextures(ContentManager content)
        {
            foreach (var name in Tile.tilePrototypes.Keys)
                try
                {
                    textureMap.Add(name, content.Load<Texture2D>("Graphics\\Tiles\\" + name));
                }
                catch
                {
                    Debug.WriteLine("Texture for Tile: " + name + " Does not exist");
                }
        }

        private static void SetupMazeTextures()
        {
            //Create Temporary Texture for a Cell
            var data = new Color[ScreenComponentMaze.cellSize * ScreenComponentMaze.cellSize];
            for (var i = 0; i < data.Length; i++) data[i] = Color.White;

            var cellTexture = new Texture2D(ProjectMazelike.Instance.GraphicsDevice, ScreenComponentMaze.cellSize,
                ScreenComponentMaze.cellSize);
            cellTexture.SetData(data);
            textureMap.Add("Cell", cellTexture);

            //Create Temporary Texture for a Wall
            data = new Color[ScreenComponentMaze.wallSize * ScreenComponentMaze.wallSize];
            for (var i = 0; i < data.Length; i++) data[i] = Color.White;

            var wallTexture = new Texture2D(ProjectMazelike.Instance.GraphicsDevice, ScreenComponentMaze.wallSize,
                ScreenComponentMaze.wallSize);
            wallTexture.SetData(data);
            textureMap.Add("Maze Wall", wallTexture);
        }

        public static Texture2D GetTexture(string name)
        {
            if (textureMap.Keys.Contains(name)) return textureMap[name];

            Debug.WriteLine(string.Format("Texture with name {0} was attempted to be retrieved but does not exist",
                name));
            return textureMap[missingTexture];
        }
    }
}