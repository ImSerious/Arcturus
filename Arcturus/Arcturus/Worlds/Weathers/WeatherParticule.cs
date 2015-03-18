using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Arcturus.Worlds.Weathers
{
    /// <summary>
    /// Represents a particule of a weather.Rain drop ,Snow flakes , etc ...
    /// </summary>
    public class WeatherParticule
    {
        bool isActive;
        float size;
        float rotation;
        float lifeTime;
        Vector2 position;
        Vector2 trajectory;
        Texture2D texture;
        public const float MinimumSize = 0.5F;
        public const float MaximumSize = 1F;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texture">Texture of the particule</param>
        /// <param name="position">Initial position of the particule</param>
        /// <param name="trajectory">Trajectory of the particule</param>
        /// <param name="size">Size of the particule</param>
        public WeatherParticule(Texture2D texture)
        {
            this.isActive = true;
            this.texture = texture;
            this.lifeTime = 7F;
            this.size = 1F;
        }

        /// <summary>
        /// Updating the position of the particule.
        /// </summary>
        /// <param name="windSpeed">Speed of the wind</param>
        public void Update(float windSpeed)
        {
            lifeTime -= 1F / 60; // Approximatly one second
            position += trajectory  * size * windSpeed;

            if (lifeTime <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// Draw the particule.
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, 
                            position, 
                            null, 
                            Color.White, 
                            rotation, 
                            Utils.ConvertVector(texture.Bounds.Center), 
                            size, 
                            SpriteEffects.None, 
                            1F);
        }

        /// <summary>
        /// When the particule hit a tile.
        /// </summary>
        public void Die()
        {
            isActive = false;
        }

        /// <summary>
        /// Clone the current object.
        /// </summary>
        /// <returns>A clone of the current instance.</returns>
        public WeatherParticule Clone()
        {
            WeatherParticule particule = new WeatherParticule(texture);
            particule.IsActive = true;
            return particule;
        }

        /// <summary>
        /// If particule is active or not.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// Size of the particule.
        /// </summary>
        public float Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// Rotation of the particule.
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// Hitbox of the particule.
        /// </summary>
        public Rectangle Hitbox
        {
            get { return new Rectangle((int)Position.X,(int)Position.Y,texture.Bounds.Width,texture.Bounds.Height); }
        }

        /// <summary>
        /// The trajectory of the particule.
        /// </summary>
        public Vector2 Trajectory
        {
            get { return trajectory; }
            set { trajectory = value; }
        }

        /// <summary>
        /// Position of the particule.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

    }
}
