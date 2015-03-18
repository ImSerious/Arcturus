using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Arcturus.Items;

namespace Arcturus.Worlds.Objects
{
    /// <summary>
    /// Represents a chest which contains items.
    /// </summary>
    public class Chest : WorldObject
    {
        bool isLocked;
        Item[] containedItems;
        /// <summary>
        /// Constants
        /// </summary>
        public const int MaxCapacity = 20;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Chest()
        {
            containedItems = new Item[MaxCapacity];
        }

        /// <summary>
        /// State of the chest.Locked or not.
        /// </summary>
        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }

        /// <summary>
        /// Items inside the chest.
        /// </summary>
        public Item[] ContainedItems
        {
            get { return containedItems; }
            set { containedItems = value; }
        }
    }
}
