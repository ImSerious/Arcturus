using System;
using System.Text;

namespace Arcturus.Items.Stackable
{
    public class Stack
    {
        int capacity;
        int quantity;
        Item item;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="item">Item that will be stack</param>
        /// <param name="initialQuantity">Initial quantity in the stack</param>
        public Stack(Item item, int initialQuantity)
        {
            this.capacity = 999;
            this.item = item;
            this.quantity = initialQuantity;
        }

        /// <summary>
        /// Add an amount to the quantity.Substract and tell a new stack is needed if the capacity is reached.
        /// </summary>
        /// <param name="amount">The amount to add to the stack</param>
        /// <returns>True is a new stack is needed,false if not</returns>
        public bool Add(ref int amount)
        {
            if (quantity + amount <= capacity)
            {
                quantity += amount;
                return false;
            }
            else
            {
                amount -= (capacity - amount);
                quantity = capacity;
                return true;
            }
        }

        /// <summary>
        /// Initial quantity in the stack.
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
        }

        /// <summary>
        /// Maximum capacity of the stack.999 by default.
        /// </summary>
        public int Capacity
        {
            get { return capacity; }
        }

        /// <summary>
        /// Item that will be stack.
        /// </summary>
        public Item Item
        {
            get { return item; }
        }
    }
}
