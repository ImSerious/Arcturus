using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcturus.Items;

namespace Arcturus.Mechanics.Crafting
{
    /// <summary>
    /// A craft recipe.
    /// </summary>
    public class CraftRecipe
    {
        byte successRate;
        List<Item> ingredients;
        Item itemProduced;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ingredients">Ingredients for the craft.</param>
        /// <param name="itemProduced">The result of the craft.</param>
        public CraftRecipe(List<Item> ingredients,Item itemProduced)
        {
            this.ingredients = ingredients;
            this.itemProduced = itemProduced;
        }

        /// <summary>
        /// Success rate of the craft.
        /// </summary>
        public byte SuccessRate
        {
            get { return successRate; }
            set { successRate = value; }
        }

        /// <summary>
        /// Ingredients for the craft.
        /// </summary>
        public List<Item> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        /// <summary>
        /// The result of the craft.
        /// </summary>
        public Item ItemProduced
        {
            get { return itemProduced; }
            set { itemProduced = value; }
        }
    }
}
