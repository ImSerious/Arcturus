using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcturus.Worlds.Objects
{
    /// <summary>
    /// Represents an object in the world.
    /// </summary>
    public abstract class WorldObject
    {
        /// <summary>
        /// If the object is active or not.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// If the object can be break or not.
        /// </summary>
        public bool IsBreakable { get; set; }
        /// <summary>
        /// Sprite of the object.
        /// </summary>
        public Sprite Sprite { get; set; }
        /// <summary>
        /// Object's world position.
        /// </summary>
        public Vector2 WorldPosition { get; set; }

        /// <summary>
        /// Loading content for the world object.
        /// </summary>
        public virtual void LoadContent() { }

        /// <summary>
        /// Updating the world object.
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public virtual void Update(GameTime gameTime) { }

        /// <summary>
        /// Draw the world object.
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }

        /// <summary>
        /// Hitbox of the object.
        /// </summary>
        public Rectangle Hitbox
        {
            get { return Sprite.Hitbox; }
        }
    }
}
