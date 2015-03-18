using System;

namespace Arcturus.Items
{
    /// <summary>
    /// Represents a drop of a monster.
    /// </summary>
    public class Drop
    {
        float probability;
        Item item;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">Item that will be drop</param>
        /// <param name="probability">The probability of the item to be dropped</param>
        public Drop(Item item, float probability)
        {
            this.item = item;
            this.probability = probability;
        }

        /// <summary>
        /// Item to get.
        /// </summary>
        public Item Item
        {
            get { return item; }
        }

        /// <summary>
        /// Probabily of getting the item.
        /// </summary>
        public float Probability
        {
            get { return probability; }
        }
    }
}
