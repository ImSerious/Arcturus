using System;
using Microsoft.Xna.Framework.Graphics;
using Arcturus.Entities;

namespace Arcturus.Items
{
    /// <summary>
    /// Represents an item of the game.
    /// </summary>
    public abstract class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsStackable { get; set; }
        public byte Tier { get; set; }
        public short ID { get; set; }
        public short Quantity { get; set; }
        public float ReuseTime { get; set; }
        public Sprite Sprite { get; set; }
        public TimeSpan LastUseItem { get; set; }
        public TypeItem TypeOfItem { get; set; }
        /// <summary>
        /// Max stack capacity
        /// </summary>
        const short MaxCapacity = 999;

        /// <summary>
        /// Add an amount to the quantity.Substract and tell a new stack is needed if the capacity is reached.
        /// </summary>
        /// <param name="amount">The amount to add to the stack</param>
        /// <returns>True is a new stack is needed,false if not</returns>
        public bool AddQuantity(ref short amount)
        {
            if (Quantity + amount <= MaxCapacity)
            {
                Quantity += amount;
                return false;
            }
            else
            {
                amount -= (short)(MaxCapacity - amount);
                Quantity = MaxCapacity;
                return true;
            }
        }

        /// <summary>
        /// Draw the item.
        /// </summary>
        /// <param name="spriteBatch">The batch used to draw.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }

        /// <summary>
        /// Type of item
        /// </summary>
        public enum TypeItem
        {
            TileStack,
            Tool,
            RangedWeapon,
            MeleeWeapon,
            MagicWeapon,
            Consumable
        }
    }
}
