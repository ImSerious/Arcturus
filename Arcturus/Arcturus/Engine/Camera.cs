using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arcturus.Tools;
using Arcturus.Entities;
using Arcturus.Worlds;

namespace Arcturus.Engine
{
    /// <summary>
    /// The camera of the world screen.
    /// </summary>
    public class Camera
    {
        float moveSpeed;
        float rotation;
        float scale;
        Viewport viewport;
        Vector2 position;
        Vector2 origin;
        Vector2 screenCenter;
        Matrix transform;
        Player player;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="player">Player followed by the camera</param>
        public Camera(Player player)
        {
            this.player = player;
            this.scale = 1;
            this.moveSpeed = 15F;
            this.rotation = 0F;
            this.viewport = GameServices.GetService<GraphicsDeviceManager>().GraphicsDevice.Viewport;
        }

        /// <summary>
        /// Updating the current camera.
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public void Update(GameTime gameTime)
        {
            viewport = GameServices.GetService<GraphicsDeviceManager>().GraphicsDevice.Viewport;
            screenCenter = new Vector2(viewport.Width / 2, viewport.Height / 2);
            transform = Matrix.Identity *
                        Matrix.CreateTranslation((int)-position.X, (int)-position.Y, 0) *
                        Matrix.CreateRotationZ(rotation) *
                        Matrix.CreateTranslation(origin.X, origin.Y, 0) *
                        Matrix.CreateScale(new Vector3(scale, scale, scale));

            origin = screenCenter / scale;

            // Move the Camera to the position that it needs to go
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position.X += (player.Position.X - position.X) * moveSpeed * delta;
            position.Y += (player.Position.Y - position.Y) * moveSpeed * delta;

            if (position.X < Arcturus.WindowWidth)
            {
                position = new Vector2(Arcturus.WindowWidth, position.Y);
            }
            else if (position.X > (World.WorldWidth * 16) - Arcturus.WindowWidth)
            {
                position = new Vector2((World.WorldWidth * 16) - Arcturus.WindowWidth, position.Y);
            }

        }

        /// <summary>
        /// Get the current draw zone with the help of the camera position.
        /// </summary>
        /// <returns>The player draw zone</returns>
        public Rectangle GetDrawZone()
        {
            Rectangle drawZone;
            drawZone = new Rectangle((int)Position.X / 16, (int)Position.Y / 16, viewport.Width / Tile.Width, viewport.Height / Tile.Height);
            drawZone.Inflate(2, 2);
            return drawZone;
        }

        /// <summary>
        /// The zoom of the camera.
        /// </summary>
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// The player followed by the camera.
        /// </summary>
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        /// <summary>
        /// Position of the camera.
        /// </summary>
        public Vector2 Position
        {
            get { return position - screenCenter; }
            set { position = value; }
        }

        /// <summary>
        /// The matrix transformation of the camera.
        /// </summary>
        public Matrix Transform
        {
            get { return transform; }
        }
    }
}
