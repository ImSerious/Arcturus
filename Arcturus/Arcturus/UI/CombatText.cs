using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcturus.UI
{
    /// <summary>
    /// Represents a combat text.   
    /// </summary>
    public class CombatText
    {
        string message;
        int opacity;
        byte fadingRate;
        bool isActive;
        Vector2 position;
        Color color;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message of the combat text</param>
        /// <param name="position">Initial position</param>
        /// <param name="color">Color of the text</param>
        public CombatText(string message,Vector2 position,Color color)
        {
            this.message = message;
            this.position = position;
            this.color = color;
            this.isActive = true;
            this.opacity = 255;
            this.fadingRate = 4;
        }

        /// <summary>
        /// Update the opacity and the position of the text.
        /// </summary>
        public void Update()
        {
            position.Y -= fadingRate / 4;
            opacity -= fadingRate;
            color.A = (byte)opacity;

            if (opacity <= 0)
            {
                isActive = false;
            }
        }

        /// <summary>
        /// Draw the text.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // TODO : draw string
        }

        /// <summary>
        /// State of the text.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
        }
    }
}
