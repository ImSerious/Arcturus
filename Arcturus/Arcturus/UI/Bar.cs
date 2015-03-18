using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Arcturus.Tools;

namespace Arcturus.UI
{
    /// <summary>
    /// Represents a bar of a value , like HP , MP or EXP.
    /// </summary>
    public class Bar
    {
        int currentValue;
        int maxValue;
        int barWidth;
        int barHeight;
        Color color;
        Vector2 position;
        Texture2D texture;
        Rectangle background;
        Rectangle foreground;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="currentValue">Current value of the bar</param>
        /// <param name="maxValue">Maximum value of the bar</param>
        /// <param name="barWidth">Bar width</param>
        /// <param name="barHeight">Bar height</param>
        /// <param name="color">Bar foreground color</param>
        public Bar(int currentValue, int maxValue, int barWidth,int barHeight, Color color)
        {
            this.currentValue = currentValue;
            this.maxValue = maxValue;
            this.color = color;
            this.barWidth = barWidth;
            this.barHeight = barHeight;
            this.texture = new Texture2D(GameServices.GetService<GraphicsDevice>(), barWidth, barHeight);
        }

        /// <summary>
        /// Draw the bar.
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            var text = currentValue + "/" + maxValue;
            var barValue = (currentValue * barWidth) / maxValue;
            // Calculate the rectangles
            background = new Rectangle((int)Position.X, (int)Position.Y, barWidth, barHeight);
            foreground = new Rectangle((int)Position.X, (int)Position.Y, barValue, barHeight);
            // Draw all
            spriteBatch.Draw(texture, background, Color.Black);
            spriteBatch.Draw(texture, foreground, color);
        }

        /// <summary>
        /// Current value of the bar.
        /// </summary>
        public int CurrentValue
        {
            get { return currentValue; }
            set { currentValue = value; }
        }

        /// <summary>
        /// Maximum value of the bar
        /// </summary>
        public int MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        /// <summary>
        /// Current position of the bar
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
    }
}
