﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    abstract class ScreenComponent {

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
