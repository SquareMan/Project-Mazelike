﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    abstract class ScreenComponent {
        public DrawLayer Layer { get; set; }

        public delegate void ClickedDelegate();

        protected ScreenComponent(DrawLayer layer) {
            this.Layer = layer;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public virtual void Update(GameTime gameTime) {

        }
    }

    public enum DrawLayer { Background, Player, Foreground }
}
