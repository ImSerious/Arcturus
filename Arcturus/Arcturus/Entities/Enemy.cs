using System;
using System.Collections.Generic;
using Arcturus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arcturus.Worlds;

namespace Arcturus.Entities
{
    /// <summary>
    /// Represents a enemy.
    /// </summary>
    public class Enemy : Entity
    {
        int experienceReward;
        Rectangle aggressiveZone;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name of the enemy</param>
        /// <param name="experienceReward">Experience rewarded when the enemy is killed</param>
        public Enemy(string name,int experienceReward)
        {
            this.Name = name;
            this.experienceReward = experienceReward;
            this.aggressiveZone = new Rectangle();
        }

        /// <summary>
        /// Updating the current enemy
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public override void Update(GameTime gameTime,Tile[,] tiles)
        {
            Move(Direction.Left);

            if (Health <= 0)
            {
                IsAlive = false;
                Death();
            }
        }

        /// <summary>
        /// Enemy die and possibly dropping an item.
        /// </summary>
        public override void Death()
        {
            
        }

        /// <summary>
        /// Experience rewarded by killing the enemy.
        /// </summary>
        public int ExperienceReward
        {
            get { return experienceReward; }
        }

    }
}
