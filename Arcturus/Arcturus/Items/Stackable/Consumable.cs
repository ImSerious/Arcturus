using System;
using Arcturus.Mechanics;
using Arcturus.Entities;

namespace Arcturus.Items.Stackable
{
    public class Consumable : Item
    {
        Status status;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Consumable(Status status)
        {
            this.status = status;
        }

       /// <summary>
       /// Use the consumable on the target.
       /// </summary>
       /// <param name="player">The player</param>
       /// <param name="currentTime">Current time</param>
        public void UseOn(Player player , TimeSpan currentTime)
        {
            player.AddStatus(status, currentTime); // Doute : currentTime ?
        }
    }
}
