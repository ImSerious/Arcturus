using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Arcturus.Mechanics
{
    /// <summary>
    /// Represents a status of an entity (empoisoned , burned , etc ...)
    /// </summary>
    public class Status
    {
        short amount;
        string name;
        bool isActive;
        float triggerRate;
        Texture2D icon;
        TimeSpan duration;
        TimeSpan beginTime;
        TimeSpan currentTime;
        Type typeStatus; 

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name of the effect</param>
        /// <param name="amount">Amount of the effect (+500HP if hp buff, -50Hp if poison , +100 Attack if attack buff , etc ...)</param>
        /// <param name="triggerRate">The trigger rate of the effect</param>
        /// <param name="icon">The icon that will be displayed on the entity</param>
        public Status(string name, short amount, float triggerRate, Texture2D icon)
        {
            this.name = name;
            this.amount = amount;
            this.triggerRate = triggerRate;
            this.icon = icon;
        }

        /// <summary>
        /// Updating the status , to check if its still active or not.
        /// </summary>
        /// <param name="gameTime">Current time of the game</param>
        public void Update(GameTime gameTime)
        {
            var totalTime = gameTime.TotalGameTime;

            if (duration + beginTime < totalTime)
            {
                isActive = false;
            }

            if (TimeSpan.FromSeconds(triggerRate) + beginTime < totalTime)
            {
                currentTime = totalTime;
            }
        }

        /// <summary>
        /// Reset the current status if its casted again on the entity.
        /// </summary>
        /// <param name="currentTime">Current time of the game.This will be used to last the effect one more time</param>
        /// <param name="newAmount">New amount of the status.The current status will be reset only if the current status is weaker than the new one</param>
        public void Reset(TimeSpan currentTime,short newAmount)
        {
            beginTime = currentTime;
            amount = newAmount;
        }

        /// <summary>
        /// If the status is still active or not.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// Amount of the status.
        /// </summary>
        public short Amount
        {
            get { return amount; }
        }

        /// <summary>
        /// Time left before the end of the status.
        /// </summary>
        public TimeSpan TimeLeft
        {
            get { return (beginTime + duration) - currentTime; }
        }

        /// <summary>
        /// The icon of the status
        /// </summary>
        public Texture2D Icon
        {
            get { return icon; }
        }

        /// <summary>
        /// The type of status
        /// </summary>
        public Type TypeStatus
        {
            get { return typeStatus; }
        }

        /// <summary>
        /// Thhe type of the status
        /// </summary>
        public enum Type
        {
            HealthRegeneration,
            ManaRegeneration,
            Poison
        }
    }
}
