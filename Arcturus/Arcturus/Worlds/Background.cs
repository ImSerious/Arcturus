using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Arcturus.Worlds
{
    /// <summary>
    /// Represents a background of a biome.
    /// </summary>
    public class Background
    {
        Texture2D texture;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texture">Texture of the background</param>
        public Background(Texture2D texture)
        {
            this.texture = texture;
        }

        /// <summary>
        /// Draw the background.
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
        }
    }
}
