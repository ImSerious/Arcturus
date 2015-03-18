using System;
using System.Collections.Generic;
using Arcturus.Tools;

namespace Arcturus.Items
{
    /// <summary>
    /// Represents a list of drops.
    /// </summary>
    public class DropList
    {
        Random random;
        List<Drop> drops;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DropList()
        {
            drops = new List<Drop>();
            random = new Random();
        }

        /// <summary>
        /// Get a random drop from the list
        /// </summary>
        /// <returns>Random drop</returns>
        public Item GetDrop()
        {
            float sum = SumProbabilities();
            float chances = random.NextFloat(sum);

            foreach (var drop in drops)
            {
                chances -= drop.Probability;

                if (chances <= 0)
                {
                    return drop.Item;
                }
            }

            return null;
        }

        /// <summary>
        /// Makes a sum of all probabilities to get the correct sum , even if the sum is not equal to 100%.
        /// </summary>
        /// <returns>The sum of the probabilities</returns>
        private float SumProbabilities()
        {
            float sum = 0;

            foreach (Drop drop in drops)
            {
                sum += drop.Probability;
            }

            return sum;
        }
    }
}
