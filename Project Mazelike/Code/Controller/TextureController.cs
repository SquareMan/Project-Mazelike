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
        private static Dictionary<string, Texture2D> _textureMap;

        private static readonly string MissingTexture = "MISSINGTEXTURE";

        public static void LoadTextures(ContentManager content)
        {
            _textureMap = new Dictionary<string, Texture2D>();
            _textureMap.Add(MissingTexture, content.Load<Texture2D>("Graphics\\Tiles\\MissingTile"));
            _textureMap.Add("Player", content.Load<Texture2D>("Graphics\\Player"));
            _textureMap.Add("Enemy", content.Load<Texture2D>("Graphics\\Enemy"));
            _textureMap.Add("Button", content.Load<Texture2D>("Graphics\\UI\\Button"));
            LoadTileTextures(content);

            //DEBUG MAZE CELL TEXTURE
            SetupMazeTextures();
        }

        private static void LoadTileTextures(ContentManager content)
        {
            foreach (var name in Tile.TilePrototypes.Keys)
                try
                {
                    _textureMap.Add(name, content.Load<Texture2D>("Graphics\\Tiles\\" + name));
                }
                catch
                {
                    Debug.WriteLine("Texture for Tile: " + name + " Does not exist");
                }
        }

        private static void SetupMazeTextures()
        {
            //Create Temporary Texture for a Cell
            var data = new Color[ScreenComponentMaze.CellSize * ScreenComponentMaze.CellSize];
            for (var i = 0; i < data.Length; i++) data[i] = Color.White;

            var cellTexture = new Texture2D(ProjectMazelike.Instance.GraphicsDevice, ScreenComponentMaze.CellSize,
                ScreenComponentMaze.CellSize);
            cellTexture.SetData(data);
            _textureMap.Add("Cell", cellTexture);

            //Create Temporary Texture for a Wall
            data = new Color[ScreenComponentMaze.WallSize * ScreenComponentMaze.WallSize];
            for (var i = 0; i < data.Length; i++) data[i] = Color.White;

            var wallTexture = new Texture2D(ProjectMazelike.Instance.GraphicsDevice, ScreenComponentMaze.WallSize,
                ScreenComponentMaze.WallSize);
            wallTexture.SetData(data);
            _textureMap.Add("Maze Wall", wallTexture);
        }

        public static Texture2D GetTexture(string name)
        {
            if (_textureMap.Keys.Contains(name)) return _textureMap[name];

            Debug.WriteLine(string.Format("Texture with name {0} was attempted to be retrieved but does not exist",
                name));
            return _textureMap[MissingTexture];
        }
    }
}