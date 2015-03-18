using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcturus.Worlds.Weathers
{
    /// <summary>
    ///  Represents a cloud.
    /// </summary>
    public class Cloud
    {
        float size;
        float opacity;
        bool isActive;
        Color color;
        Vector2 speed;
        Vector2 position;
        Texture2D texture;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="speed"></param>
        /// <param name="position"></param>
        public Cloud(float size, Vector2 speed, Vector2 position)
        {
            this.size = size;
            this.speed = speed;
            this.position = position;
            this.isActive = true;
            this.opacity = 0.3F;
            this.color = new Color(1F, 1F, 1F, opacity);
            this.texture = Utils.LoadTexture2D("Textures\\Weather\\cloud"); 
        }

        /// <summary>
        /// Updating the current cloud.
        /// </summary>
        public void Update()
        {
            position += speed;

            if (position.X > (World.WorldWidth * 16))
                isActive = false; // Out of bounds
        }

        /// <summary>
        /// Draw the current cloud.
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, 
                            position, 
                            null, 
                            color, 
                            0F, 
                            Vector2.Zero,
                            size, 
                            SpriteEffects.None,
                            0F);
        }

        /// <summary>
        /// If the cloud is still active or not.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
        }
    }
}
