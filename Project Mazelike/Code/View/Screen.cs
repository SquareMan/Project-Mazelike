using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMazelike.Controller;

namespace ProjectMazelike.View
{
    internal class Screen : DrawableGameComponent
    {
        public Color ClearColor = Color.CornflowerBlue;
        public List<ScreenComponent>[] ScreenSpaceComponents;

        //Index is in order of draw layers, stores a list of every ScreenComponent on that layer
        //public List<ScreenComponent>[] components;
        public List<ScreenComponent>[] WorldSpaceComponents;

        //SpriteBatch Information
        public BlendState BlendState { get; set; }
        public SamplerState SamplerState { get; set; }
        public DepthStencilState DepthStencilState { get; set; }
        public RasterizerState RasterizerState { get; set; }
        public Effect Effect { get; set; }

        public Camera Camera { get; set; }

        /// <summary>
        ///     Create a new Screen
        /// </summary>
        /// <param name="game">The game this screen belongs to</param>
        /// <param name="moveable">Whether or not the Camera can be panned</param>
        /// <param name="rotatable">Whether or not the Camera can be rotated</param>
        /// <param name="scaleable">Whether or not the Camera can be zoomed</param>
        public Screen(Game game, bool moveable, bool rotatable, bool scaleable) : base(game)
        {
            //this.canBeMoved = moveable;
            //this.canBeRotated = rotatable;
            //this.canBeZoomed = scaleable;

            //Create arrays of lists of game components, corresponding to their draw layer, for both world space and screen space
            WorldSpaceComponents = new List<ScreenComponent>[Enum.GetNames(typeof(DrawLayer)).Length];
            for (var i = 0; i < WorldSpaceComponents.Length; i++) WorldSpaceComponents[i] = new List<ScreenComponent>();

            ScreenSpaceComponents = new List<ScreenComponent>[Enum.GetNames(typeof(DrawLayer)).Length];
            for (var i = 0; i < ScreenSpaceComponents.Length; i++)
                ScreenSpaceComponents[i] = new List<ScreenComponent>();

            //Create a new camera with origin at the top left corner
            Camera = new Camera(GraphicsDevice.Viewport,
                Vector2.Zero + new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));
            Camera.CanBeMoved = moveable;
            Camera.CanBeRotated = rotatable;
            Camera.CanBeScaled = scaleable;
        }

        /// <summary>
        ///     Register a component to be rendered
        /// </summary>
        /// <param name="sc">The ScreenComponent to add</param>
        /// <param name="screenSpace">Set to true if drawing in screen space is desired instead of world space</param>
        public void AddComponent(ScreenComponent sc)
        {
            if (sc.Space == DrawSpace.Screen)
                ScreenSpaceComponents[(int) sc.Layer].Add(sc);
            else
                WorldSpaceComponents[(int) sc.Layer].Add(sc);
        }

        /// <summary>
        ///     Unregister a previously registered component
        /// </summary>
        /// <param name="sc">The ScreenComponent to be removed</param>
        public void RemoveComponent(ScreenComponent sc)
        {
            if (sc != null)
                if (ScreenSpaceComponents[(int) sc.Layer].Contains(sc))
                    ScreenSpaceComponents[(int) sc.Layer].Remove(sc);
                else if (WorldSpaceComponents[(int) sc.Layer].Contains(sc))
                    WorldSpaceComponents[(int) sc.Layer].Remove(sc);
        }

        /// <summary>
        ///     Return the current mouse position in either world or screen space
        /// </summary>
        /// <param name="space">World or Screen space</param>
        /// <returns></returns>
        public Point GetMousePosition(DrawSpace space)
        {
            if (space == DrawSpace.World)
                return Vector2.Transform(MouseController.CurrentState.Position.ToVector2(),
                    Matrix.Invert(Camera.TransformMatrix)).ToPoint();
            return MouseController.CurrentState.Position;
        }

        /// <summary>
        ///     Update all Screen Components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            for (var i = 0; i < Enum.GetNames(typeof(DrawLayer)).Length; i++)
            {
                //Update WorldSpace ScreenComponents
                foreach (var sc in WorldSpaceComponents[i])
                    if (sc.Enabled)
                        sc.Update(gameTime);

                //Update ScreenSpace ScreenComponents
                foreach (var sc in ScreenSpaceComponents[i])
                    if (sc.Enabled)
                        sc.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        ///     Draw all Screen Components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.Clear(ClearColor);

            for (var i = 0; i < Enum.GetNames(typeof(DrawLayer)).Length; i++)
            {
                //Draw WorldSpace Screen Components
                ((ProjectMazelike) Game).SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState, SamplerState,
                    DepthStencilState, RasterizerState, Effect, Camera.TransformMatrix);
                foreach (var sc in WorldSpaceComponents[i])
                    if (sc.Enabled)
                        sc.Draw(gameTime, ((ProjectMazelike) Game).SpriteBatch);

                ((ProjectMazelike) Game).SpriteBatch.End();

                //Draw ScreenSpace Screen Components
                ((ProjectMazelike) Game).SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState, SamplerState,
                    DepthStencilState, RasterizerState, Effect);
                foreach (var sc in ScreenSpaceComponents[i])
                    if (sc.Enabled)
                        sc.Draw(gameTime, ((ProjectMazelike) Game).SpriteBatch);

                ((ProjectMazelike) Game).SpriteBatch.DrawString(ProjectMazelike.Font,
                    "FPS: " + (int) (1000 / gameTime.ElapsedGameTime.TotalMilliseconds + .5f), new Vector2(5, 5),
                    Color.White);
                ((ProjectMazelike) Game).SpriteBatch.End();
            }
        }
    }
}