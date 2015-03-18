using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Arcturus.Tools;

namespace Arcturus.Screens
{
    /// <summary>
    /// Represents a screen of the game.
    /// </summary>
    public abstract class Screen
    {
        /// <summary>
        /// Tell if the game is the active windows.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Reference to the screen manager ,for switching screens
        /// </summary>
        public ScreenManager ScreenManager { get; set; }
        /// <summary>
        /// Input manager of all screens.
        /// </summary>
        public InputManager InputManager { get; set; }
        /// <summary>
        /// Where to load the content of the screen.
        /// </summary>
        public abstract void LoadContent();
        /// <summary>
        /// Where to initialize all screen elements.
        /// </summary>
        public abstract void Initialize();
        /// <summary>
        /// Update the screen.
        /// </summary>
        /// <param name="gameTime">The current time of the game</param>
        public abstract void Update(GameTime gameTime);
        /// <summary>
        /// Draw the screen
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw stuff</param>
        public abstract void Draw(SpriteBatch spriteBatch);
        /// <summary>
        /// Get the inputs of the player , mouse and keyboard.
        /// </summary>
        public abstract void GetInputs(GameTime gameTime);
        /// <summary>
        /// Draw all debug informations.
        /// </summary>
        public virtual void DrawDebug() { }
    }
}
