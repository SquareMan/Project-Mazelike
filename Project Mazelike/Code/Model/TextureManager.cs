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

        public static void LoadTextures(ContentManager content) {
            textureMap = new Dictionary<string, Texture2D>();

            textureMap.Add("Player", content.Load<Texture2D>("Graphics\\player"));
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
