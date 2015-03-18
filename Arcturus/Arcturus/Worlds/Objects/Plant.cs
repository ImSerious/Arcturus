using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arcturus.Items;
using Arcturus.Tools;

namespace Arcturus.Worlds.Objects
{
    /// <summary>
    /// Represents a plant (Tree , flowers , etc ...).
    /// </summary>
    public class Plant : WorldObject
    {
        byte id;
        byte growState;
        byte growStateMax;
        byte growRate;
        string name;
        Item harvestReward;
        TimeSpan lastGrow;
        const int growChance = 10;

        /// <summary>
        /// Empty constructor , for cloning purpose.
        /// </summary>
        public Plant() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">ID of the plant</param>
        /// <param name="name">Name of the plant</param>
        /// <param name="sprite">Sprite of the plant</param>
        public Plant(byte id,string name,Sprite sprite)
        {
            this.id = id;
            this.name = name;
            this.Sprite = sprite;
            this.IsActive = true;
        }

        /// <summary>
        /// Clone the current plant.
        /// </summary>
        /// <returns>The cloned plant</returns>
        public Plant Clone()
        {
            Plant plant = new Plant();
            plant.id = this.ID;
            plant.IsActive = true;
            plant.name = this.Name;
            plant.Sprite = this.Sprite.Clone();
            plant.growStateMax = this.growStateMax;
            return plant;
        }

        /// <summary>
        /// Update the current plant.
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public override void Update(GameTime gameTime)
        {
            var currentTime = gameTime.TotalGameTime;

            if (lastGrow + TimeSpan.FromMinutes(growRate) <= currentTime)
            {
                Grow(currentTime);
            }
        }

        /// <summary>
        /// Grow the plant and update harvest reward.
        /// </summary>
        /// <param name="lastGrowTime">Last grow time</param>
        public void Grow(TimeSpan lastGrowTime)
        {
            if (growRate != growStateMax)
            {
                var random = new Random();

                if (random.Next(101) < growChance)
                {
                    growState++;
                    harvestReward.Quantity *= 2;
                    harvestReward.Quantity += random.NextShort(harvestReward.Quantity);
                    lastGrow = lastGrowTime;
                }
            }
        }

        /// <summary>
        /// ID of the plant.
        /// </summary>
        public byte ID
        {
            get { return id; }
        }
        
        /// <summary>
        /// Current grow state of the plant.
        /// </summary>
        public byte GrowState
        {
            get { return growState; }
            set { growRate = value; }
        }

        /// <summary>
        /// Name of the plant.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

    }
}
