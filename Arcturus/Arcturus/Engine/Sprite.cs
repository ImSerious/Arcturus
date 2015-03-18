using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Arcturus
{
    /// <summary>
    /// Represents a 2D sprite 
    /// </summary>
    public class Sprite
    {
        int frames;
        int currentFrame;
        int heightFrame;
        double animationElapsed;
        float size;
        float layer;
        float rotation;
        float animationSpeed;
        bool isAnimated;
        Texture2D texture;
        SpriteEffects spriteEffect;
        Rectangle sourceRectangle;
        Vector2 position;
        Color color;

        /// <summary>
        /// Empty constructor , for cloning purpose.
        /// </summary>
        public Sprite() { }   

        /// <summary>
        /// Simple constructor for non-animated sprite.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="position">Initial position</param>
        /// <param name="color">Color of the sprite</param>
        public Sprite(Texture2D texture, Vector2 position, Color color)
        {
            this.texture = texture;
            this.position = position;
            this.heightFrame = texture.Height;
            this.color = color;
            this.size = 1F;
        }

        /// <summary>
        /// Complete constructor for non-animated sprite.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="position">Initial position</param>
        /// <param name="size">Size of the sprite</param>
        /// <param name="layer">Layer of the sprite</param>
        /// <param name="color">Color of the sprite</param>
        public Sprite(Texture2D texture, Vector2 position, float size, float layer, Color color)
        {
            this.texture = texture;
            this.position = position;
            this.heightFrame = texture.Height;
            this.size = size;
            this.layer = layer;
            this.color = color;
        }

        /// <summary>
        /// Simple constructor for animated sprite.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="frames">Number of frames of the animation</param>
        /// <param name="position">Initial position</param>
        /// <param name="color">Color of the sprite</param>
        public Sprite(Texture2D texture, int frames, Vector2 position, Color color)
        {
            this.texture = texture;
            this.heightFrame = texture.Height / frames;
            this.frames = frames;
            this.position = position;
            this.color = color;

        }

        /// <summary>
        /// Complete constructor for animated sprite.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="frames">Number of frames of the animation</param>
        /// <param name="position">Initial position</param>
        /// <param name="size">Size of the sprite</param>
        /// <param name="layer">Layer of the sprite</param>
        /// <param name="color">Color of the sprite</param>
        public Sprite(Texture2D texture, int frames, Vector2 position,float size , float layer, Color color)
        {
            this.texture = texture;
            this.heightFrame = texture.Height / frames;
            this.position = position;
            this.size = size;
            this.layer = layer;
            this.color = color;
        }

        /// <summary>
        /// Draw the sprite.
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            sourceRectangle = new Rectangle(0, heightFrame * currentFrame, texture.Width, heightFrame); // Get the currentFrame on the texture
            spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, Vector2.Zero, size, spriteEffect, layer);

        }

        /// <summary>
        /// Animate the sprite.
        /// </summary>
        /// <param name="gameTime">Current time of the game</param>
        public void Animate(GameTime gameTime)
        {
            animationElapsed += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (animationElapsed > animationSpeed)
            {
                currentFrame++;

                if (currentFrame >= frames)
                {
                    currentFrame = 0; // Reinit the animation
                }
                animationElapsed = 0; // Reinit the count
            }
        }

        /// <summary>
        /// Clone the current sprite.
        /// </summary>
        /// <returns></returns>
        public Sprite Clone()
        {
            Sprite sprite = new Sprite();
            sprite.color = this.color;
            sprite.frames = this.frames;
            sprite.size = this.size;
            sprite.spriteEffect = this.spriteEffect;
            sprite.rotation = this.rotation;
            sprite.texture = this.texture;
            return sprite;
        }

        /// <summary>
        /// Rotation rate of the sprite.
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// Size of the sprite.
        /// </summary>
        public float Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// If animated or not.
        /// </summary>
        public bool IsAnimated
        {
            get { return isAnimated; }
            set { isAnimated = value; }
        }

        /// <summary>
        /// Texture of the sprite.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        /// <summary>
        /// Current position of the sprite.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Hitbox of the sprite.
        /// </summary>
        public Rectangle Hitbox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * size), (int)(texture.Height * size)); }
        }

        /// <summary>
        /// Center of the sprite.
        /// </summary>
        public Vector2 Center
        {
            get { return new Vector2(texture.Width / 2, texture.Height / (2 * frames)); }
        }

        /// <summary>
        /// Color of the sprite.
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// Animation speed of the sprite.
        /// </summary>
        public float AnimationSpeed
        {
            get { return animationSpeed; }
            set { animationSpeed = value; }
        }

        /// <summary>
        /// Effect on the sprite.
        /// </summary>
        public SpriteEffects SpriteEffect
        {
            get { return spriteEffect; }
            set { spriteEffect = value; }
        }
    }
}
