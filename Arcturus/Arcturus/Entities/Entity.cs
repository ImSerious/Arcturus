using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arcturus.Mechanics;
using Arcturus.UI;
using Arcturus.Worlds;
using Arcturus.Items;
using Arcturus.Tools;

namespace Arcturus.Entities
{
    /// <summary>
    /// Represents an entity of the world , subject to colisions , projectiles , etc ...
    /// </summary>
    public abstract class Entity
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Mana { get; set; }
        public int MaxMana { get; set; }
        public byte Level { get; set; }
        public short AttackPower { get; set; }
        public short DefensePower { get; set; }
        public short Accuracy { get; set; }
        public short DodgeRate { get; set; }
        public short MoveSpeed { get; set; }
        public bool IsAlive { get; set; }
        public bool IsImmuneToFall { get; set; }
        public bool IsFriendly { get; set; }
        public bool IsJumping { get; set; }
        public bool IsOnGround { get; set; }
        public bool IsFlying { get; set; }
        public bool IsMoving { get; set; }
        public Bar HealthBar { get; set; }
        public Bar ManaBar { get; set; }
        public Sprite Sprite { get; set; }
        public Vector2 Velocity { get; set; }
        public Direction CurrentDirection { get; set; }
        public DropList DropList { get; set; }
        public List<Status> Statuses { get; set; }
        /// <summary>
        /// Max fall speed.
        /// </summary>
        const float MaxFallSpeed = 15F;
        /// <summary>
        /// Fall speed.
        /// </summary>
        const float FallSpeed = 0.2F;
        /// <summary>
        /// The limit when a fall will hurt the entity
        /// </summary>
        const float FallSpeedHurtLimit = 11F;
        /// <summary>
        /// Max run speed.
        /// </summary>
        const float MaxRunSpeed = 4F;
        /// <summary>
        /// Power of the jump action
        /// </summary>
        const float JumpPower = 6F;
        /// <summary>
        /// Time to wait before the entity can be hurt again
        /// </summary>
        const float InvicibilityTime = 0.5F;

        /// <summary>
        /// Loading all the content of the entity.
        /// </summary>
        virtual public void LoadContent()
        {
            HealthBar = new Bar(Health, MaxHealth, 100, 10, Color.DarkRed);
            ManaBar = new Bar(Mana, MaxMana, 100, 10, Color.DarkBlue);
            Statuses = new List<Status>();
            DropList = new DropList();
            ExtraLoadContent();
        }

        /// <summary>
        /// Extra loading content.
        /// </summary>
        virtual public void ExtraLoadContent() { }

        /// <summary>
        /// Updating the entity.
        /// </summary>
        /// <param name="gameTime">Current time of the game</param>
        virtual public void Update(GameTime gameTime, Tile[,] tiles)
        {
            // Applying the physic and check collision
            ApplyPhysic();
            ApplyVelocity(gameTime, tiles);
            // Updating the HP & MP bars
            HealthBar.CurrentValue = Health;
            HealthBar.MaxValue = MaxHealth;
            ManaBar.CurrentValue = Mana;
            ManaBar.MaxValue = MaxMana;
            // Updating and checking all statuses
            CheckStatus(gameTime);
            // Updating extra things from inherant classes
            ExtraUpdate(gameTime);
        }

        /// <summary>
        /// Update the extra stuff of the entity , depends on the inherant classes.
        /// </summary>
        /// <param name="gameTime"></param>
        virtual public void ExtraUpdate(GameTime gameTime) { }

        /// <summary>
        /// Draw the basic stuff of the entity
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw</param>
        virtual public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch); // Draw the sprite first

            if (Health < MaxHealth)
            {
                HealthBar.Draw(spriteBatch); // Only displaying health bar if health is not full
            }

            if (Mana < MaxMana)
            {
                ManaBar.Draw(spriteBatch); // Only displaying mana bar if health is not full
            }

            for (int i = 0; i < Statuses.Count; i++)
            {
                spriteBatch.Draw(Statuses[i].Icon, Vector2.Zero, Color.White); // Draw all the statuses the entity have
            }

            ExtraDraw(spriteBatch);
        }

        /// <summary>
        /// Draw the extra stuff of the entity , depends on the inherant classes.
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw</param>
        virtual public void ExtraDraw(SpriteBatch spriteBatch) { }

        /// <summary>
        /// Applying velocity to the entity
        /// </summary>
        /// <param name="gameTime">Current game time.</param>
        /// <param name="tiles">Tiles of the world</param>
        public void ApplyVelocity(GameTime gameTime, Tile[,] tiles)
        {
            // Get the next hitbox of the entity , only with X velocity
            Rectangle nextHitbox = new Rectangle((int)(Position.X + Velocity.X), (int)Position.Y, Hitbox.Width, Hitbox.Height);
           
            // If no collision then apply the velocity to the real position
            if (!CheckCollision(tiles, nextHitbox))
            {
                Position = new Vector2(Position.X + Velocity.X, Position.Y);
            }

            // Otherwise , there is collision, don't apply the velocity and reset the current velocity
            else
            {

                // Correcting position if moving too fast right
                if (Velocity.X > 0)
                {
                    if (Velocity.X > 1)
                    Position = new Vector2(Position.X + Velocity.X, Position.Y);
                    Position = new Vector2(Position.X - (Hitbox.Right % Tile.Width), Position.Y);
                }
                // Correcting position if moving too fast left
                else if (Velocity.X < 0)
                {
                    if (Velocity.X < -1)
                    Position = new Vector2(Position.X + Velocity.X, Position.Y);

                    int penetration = Hitbox.Left % Tile.Width;
                    
                    if (penetration != 0)
                    Position = new Vector2(Position.X + ( Tile.Width - penetration), Position.Y);
                }

                // In any case, we reset the X velocity
                Velocity = new Vector2(0, Velocity.Y);


            }

            // Same thing but with the Y velocity
            nextHitbox = new Rectangle((int)(Position.X), (int)(Position.Y + Velocity.Y), Hitbox.Width, Hitbox.Height);

            // If no collision
            if (!CheckCollision(tiles, nextHitbox))
            {
                Position = new Vector2(Position.X, Position.Y + Velocity.Y);
                IsOnGround = false;
            }

            else
            {
                // Bottom  collision
                if (Velocity.Y >= 0)
                {
                    CheckImpact(Velocity.Y);

                    // Correcting position if falling too fast
                    if (Velocity.Y > 2)
                    {
                        Position = new Vector2(Position.X, Position.Y + Velocity.Y);
                        Position = new Vector2(Position.X, Position.Y - Hitbox.Bottom % Tile.Height);
                    }

                    Velocity = new Vector2(Velocity.X, 0);
                    IsOnGround = true;
                    IsJumping = false;
                }

                // Top collision
                else
                {
                    Velocity = new Vector2(Velocity.X, 0);
                }

            }

            // Only decrease if the entity is not moving anymore
            // to avoid tilting effect
            if (!IsMoving)
            {
                // Decrease the acceleration , need to make it better
                if (Velocity.X < 0)
                {
                    Velocity += new Vector2(0.2F, 0);
                    if (Velocity.X > 0)
                    {
                        Velocity = new Vector2(0, Velocity.Y);
                    }
                }

                else if (Velocity.X > 0)
                {
                    Velocity -= new Vector2(0.2F, 0);
                    if (Velocity.X < 0)
                    {
                        Velocity = new Vector2(0, Velocity.Y);
                    }
                }
            }

            IsMoving = false;
        }

        /// <summary>
        /// Move the entity in one direction.
        /// </summary>
        /// <param name="direction">Direction to move</param>
        public void Move(Direction direction)
        {
            IsMoving = true;

            if (direction == Direction.Right)
            {
                Sprite.SpriteEffect = SpriteEffects.FlipHorizontally;
            }

            else
            {
                Sprite.SpriteEffect = SpriteEffects.None;
            }

            // Calculating the trajectory :
            // Trajectory = Move Speed * Direction
            // Left = -1
            // Right = 1
            int movementDirection = (int)direction;

            Velocity += new Vector2(0.2F * movementDirection * MoveSpeed, 0);

            if (Velocity.X > MaxRunSpeed)
                Velocity = new Vector2(MaxRunSpeed * movementDirection, Velocity.Y);
            else if
                (Velocity.X < -MaxRunSpeed)
                Velocity = new Vector2(MaxRunSpeed * movementDirection, Velocity.Y);


            // Updating the current direction
            CurrentDirection = direction;

        }

        /// <summary>
        /// Makes the entity jump.
        /// </summary>
        public void Jump()
        {
            if (!IsJumping)
            {
                if (IsOnGround)
                {
                    // Makes the entity jumps
                    Velocity += new Vector2(0, -JumpPower);
                    // Refresh the entities states
                    IsOnGround = false;
                    IsJumping = true;
                }
            }
        }

        /// <summary>
        /// Makes the entity influed by gravity and his velocity.
        /// </summary>
        public void ApplyPhysic()
        {
            if (!IsFlying)
            {
                if (!IsOnGround)
                {
                    if (Velocity.Y < MaxFallSpeed)
                    {
                        Velocity += new Vector2(0, FallSpeed * World.Gravity);
                    }
                    else
                    {
                        Velocity = new Vector2(Velocity.X, MaxFallSpeed);
                    }
                }
            }

        }

        /// <summary>
        /// Check if the entity is colliding with the world.
        /// </summary>
        /// <param name="tiles">Tiles of the world</param> 
        /// <param name="nextHitbox">Next position</param>
        public bool CheckCollision(Tile[,] tiles, Rectangle nextHitbox)
        {
            int leftTile = nextHitbox.Left / Tile.Height;
            int rightTile = nextHitbox.Right / Tile.Height;
            int topTile = nextHitbox.Top / Tile.Width;
            int bottomTile = nextHitbox.Bottom / Tile.Width;

            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x <= rightTile; ++x)
                {
                    var tileCollision = tiles[x, y].Collision;

                    if (tileCollision != 0)
                    {
                        Rectangle tileHitbox = new Rectangle(x * Tile.Width, y * Tile.Height, Tile.Width, Tile.Height);

                        if (tileHitbox.Intersects(nextHitbox))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Damage the entity ,depending of the velocity.
        /// </summary>
        /// <param name="velocity">Velocity at the impact time</param>
        private void CheckImpact(float velocity)
        {
            if (!IsImmuneToFall)
            {
                if (velocity >= FallSpeedHurtLimit)
                {
                    int fallDamage = (int)velocity * 10;
                    TakeDamage(fallDamage);
                }
            }
        }

        /// <summary>
        /// Adding a status to the entity.
        /// </summary>
        /// <param name="status">The status to add</param>
        /// <param name="currentTime">Current game time</param>
        public void AddStatus(Status status, TimeSpan currentTime)
        {
            // If there is already the same status.
            if (Statuses.Where(s => s.TypeStatus == status.TypeStatus).Count() != 0)
            {
                Status statusToReset = Statuses.Where(s => s.TypeStatus == status.TypeStatus).FirstOrDefault();
                statusToReset.Reset(currentTime, status.Amount);
            }
            // Otherwise , just add it.
            else
            {
                Statuses.Add(status);
            }
        }

        /// <summary>
        /// Applying a status to the entity.
        /// </summary>
        /// <param name="status">The status</param>
        public void ApplyStatus(Status status)
        {
            switch (status.TypeStatus)
            {
                case Status.Type.HealthRegeneration:
                    Health += status.Amount;
                    break;

                case Status.Type.ManaRegeneration:
                    Mana += status.Amount;
                    break;

                case Status.Type.Poison:
                    Health -= status.Amount;
                    break;
            }
        }

        /// <summary>
        /// Checking all the statues to detect and remove those who are inactive.
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public void CheckStatus(GameTime gameTime)
        {
            for (int i = 0; i < Statuses.Count; i++)
            {
                Statuses[i].Update(gameTime);

                if (!Statuses[i].IsActive)
                {
                    Statuses.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Make damage to the entity.
        /// </summary>
        /// <param name="amount">Amount of damage</param>
        /// <returns>True is the entity is now dead.False if still alive</returns>
        public bool TakeDamage(int amount)
        {
            Health -= amount;

            if (Health <= 0)
            {
                IsAlive = false;
                Death();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Applying a knockback on the entity.
        /// </summary>
        /// <param name="knockback">Power of the knockback</param>
        /// <param name="direction">Direction of the knockback</param>
        public void ApplyKnockback(float knockback , Direction direction)
        {
            Velocity += new Vector2(knockback * (int)direction,- knockback);
        }

        /// <summary>
        /// Doing stuff when the entity die
        /// </summary>
        virtual public void Death() { }

        /// <summary>
        /// Get a random item from the drop list of the entity.
        /// </summary>
        /// <returns></returns>
        virtual public Item Drop()
        {
            return DropList.GetDrop();
        }

        /// <summary>
        /// Current position of the entity.
        /// </summary>
        public Vector2 Position
        {
            get { return Sprite.Position; }
            set { Sprite.Position = value; }
        }

        /// <summary>
        /// Bounds of the entity.
        /// </summary>
        public Rectangle Hitbox
        {
            get { return Sprite.Hitbox; }
        }

        /// <summary>
        /// Represents the direction of the entity.
        /// </summary>
        public enum Direction
        {
            /// <summary>
            /// Left direction
            /// </summary>
            Left = -1,
            /// <summary>
            /// Right direction
            /// </summary>
            Right = 1
        }
    }
}
