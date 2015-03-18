using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Arcturus.Screens;
using Arcturus.Tools;

namespace Arcturus
{
    /// <summary>
    /// The main class of the game.
    /// </summary>
    public class Arcturus : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;
        /// <summary>
        /// Current version of the game.
        /// </summary>
        public const string Version = "0.3.2.0";
        /// <summary>
        /// The debug state.
        /// </summary>
        public const bool DebugMode = true;
        /// <summary>
        /// The width of the game's window.
        /// </summary>
        public const int WindowWidth = 1700;
        /// <summary>
        /// The height of the game's window.
        /// </summary>
        public const int WindowHeight = 900;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Arcturus()
        { 
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        /// <summary>
        /// Initiliaze all stuff of the game.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            // Adding the content manager as a service
            GameServices.AddService<ContentManager>(Content);
            // Adding the graphics as a service
            GameServices.AddService<GraphicsDeviceManager>(graphics);
            // Adding the graphics device as a service
            GameServices.AddService<GraphicsDevice>(GraphicsDevice);

            // Screen manager
            screenManager = new ScreenManager();
            screenManager.SwitchScreen(screenManager.WorldScreen);

            // Change the properties of the window
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.Title = "Arcturus - Version " + Arcturus.Version;
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();
            
        }

        /// <summary>
        /// Load every content needed.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        /// <summary>
        /// Unload all unused stuff.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Update the current screen.
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        protected override void Update(GameTime gameTime)
        {
            // Update the current screen
            screenManager.CurrentScreen.IsActive = IsActive;
            screenManager.CurrentScreen.Update(gameTime);
        }

        /// <summary>
        /// Draw the current screen.
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // Draw the current screen
            screenManager.CurrentScreen.Draw(spriteBatch);

        }

    }
}
