using Microsoft.Xna.Framework;
using Arcturus.Entities;

namespace Arcturus.Worlds.Objects
{
    /// <summary>
    /// Represents a projectile.
    /// </summary>
    public class Projectile : WorldObject 
    {
        int damage;
        float knockback;
        float speed;
        float lifeTime;
        bool canCrossTile;
        bool canBounce;
        bool isFriendly;
        bool isAffectedByGravity;
        Entity owner;
        Vector2 trajectory;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="damage">Damage of the projectile</param>
        /// <param name="speed">Speed of the projectile</param>
        /// <param name="isFriendly">Friendly or not,will harm enemies or player</param>
        /// <param name="position">Initial position of the projectile</param>
        /// <param name="targetPosition">Target position of the projectile</param>
        /// <param name="sprite">Sprite of the projectile</param>
        public Projectile(int damage, int speed, bool isFriendly, Vector2 position, Vector2 targetPosition, Sprite sprite)
        {
            this.damage = damage;
            this.Sprite = sprite;
            this.IsActive = true;
            this.canCrossTile = false;
            this.isAffectedByGravity = false;
            this.lifeTime = 10;
            this.speed = speed;
            this.WorldPosition = position;
            this.isFriendly = isFriendly;
            this.trajectory = Utils.GetTrajectory(position, targetPosition);
        }

        /// <summary>
        /// Updating the current projectile.
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public override void Update(GameTime gameTime)
        {
            // Makes the projectile move
            WorldPosition += trajectory;

            // Reduce the lifetime
            lifeTime -= 1F / 60; // Approximatly one second

            // If the projectile if affected by gravity
            // make it fall
            if (isAffectedByGravity)
            {
                trajectory += new Vector2(0, World.Gravity);
            }

            // If lifetime is over
            // kill the projectile
            if (lifeTime <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// When the particule meets a obstacle or running out of time.
        /// </summary>
        public void Die()
        {
            IsActive = false;
        }

        /// <summary>
        /// Damage of the projectile.
        /// </summary>
        public int Damage
        {
            get { return damage; }
        }

        /// <summary>
        /// Knockback of the projectile.
        /// </summary>
        public float Knockback
        {
            get { return knockback; }
        }

        /// <summary>
        /// Is from a player or a monster.
        /// </summary>
        public bool IsFriendly
        {
            get { return isFriendly; }
        }

        /// <summary>
        /// If can cross tile or not.
        /// </summary>
        public bool CanCrossTile
        {
            get { return canCrossTile; }
        }

        /// <summary>
        /// Owner of the projectile.
        /// </summary>
        public Entity Owner
        {
            get { return owner; }
        }
    }
}
