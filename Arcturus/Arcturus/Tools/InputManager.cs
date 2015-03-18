using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcturus.Tools
{
    /// <summary>
    /// Represents a manager for all inputs from the player , keyboard and mouse.
    /// </summary>
    public class InputManager
    {
        private int lastScrollValue;
        KeyboardState oldState;
        KeyboardState currentState;
        private ButtonState lastButton;

        /// <summary>
        /// Constructor.
        /// </summary>
        public InputManager() { }

        /// <summary>
        /// Updating the last key pressed.
        /// </summary>
        public void Update()
        {
            oldState = currentState;
            currentState = Keyboard.GetState();
        }

        /// <summary>
        /// Check a single press on a key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>True if the key is pressed.False if not or still down</returns>
        public bool IsKeyPress(Keys key)
        {
            if (currentState.IsKeyDown(key) && !oldState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if a key is down.
        /// </summary>
        /// <param name="key">They key</param>
        /// <returns>True if the key is still pressed.False if released</returns>
        public bool IsKeyDown(Keys key)
        {
            if (currentState.IsKeyDown(key))
            {
                return true;
            }
                return false;
        }

        /// <summary>
        /// Check a single press on the mouse ,left or right.
        /// </summary>
        /// <param name="button">Left or right button of the mouse</param>
        /// <returns>True is left or button is be</returns>
        public bool IsMouseClick(ButtonState button)
        {
            if (button == ButtonState.Pressed && lastButton != button)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if a mouse button is down.
        /// </summary>
        /// <param name="button">Left of right button of the mouse</param>
        /// <returns></returns>
        public bool IsMouseDown(ButtonState button)
        {
            if (button == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if the mouse wheel is scrolling up.
        /// </summary>
        /// <returns>True if up , false if not</returns>
        public bool IsScrollUp()
        {
            int scrollValue = Mouse.GetState().ScrollWheelValue;
            if (scrollValue > lastScrollValue)
            {
                lastScrollValue = scrollValue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if the mouse wheel is scrolling down.
        /// </summary>
        /// <returns>True if down , false if not</returns>
        public bool IsScrollDown()
        {
            int scrollValue = Mouse.GetState().ScrollWheelValue;
            if (scrollValue < lastScrollValue)
            {
                lastScrollValue = scrollValue;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the current mouse position.
        /// </summary>
        public Vector2 MousePosition
        {
            get { return new Vector2(Mouse.GetState().X, Mouse.GetState().Y); }
        }

    }
}
