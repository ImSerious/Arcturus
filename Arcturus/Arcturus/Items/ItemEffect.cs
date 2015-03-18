using System;
using Arcturus.Mechanics;

namespace Arcturus.Items
{
    /// <summary>
    /// Represents an effect of a item which will cast a status on an entity , depending of the success rate.
    /// </summary>
    public class ItemEffect
    {
        int successRate;
        Random random;
        Status status;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="successRate">The success rate of the effect</param>
        /// <param name="status">The status which will be cast if its successful</param>
        public ItemEffect(int successRate, Status status)
        {
            this.successRate = successRate;
            this.status = status;
            this.random = new Random();
        }

        /// <summary>
        /// Calculate the chances to be successful.
        /// </summary>
        /// <returns>True if successful , false if not</returns>
        public bool IsSuccessful()
        {
            var chances = random.Next(101);

            if (chances >= successRate)
                return true;
            else
                return false;
        }

        /// <summary>
        /// The status of the effect.
        /// </summary>
        public Status Status
        {
            get { return status; }
        }
    }
}
