using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Arcturus.Worlds
{
    /// <summary>
    /// Represents a tile (16x16) of the world. 
    /// </summary>
    public class Tile
    {
        byte wall;
        byte type;
        byte bitmasking;
        byte collision;
        byte resistance;
        /// <summary>
        /// Width of a tile.
        /// </summary>
        public const int Width = 16;
        /// <summary>
        /// Height of a tile.
        /// </summary>
        public const int Height = 16;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type">Type of the tile</param>
        public Tile(byte type)
        {
            this.type = type;
        }

        /// <summary>
        /// Return a new tile with same properties.
        /// </summary>
        /// <returns></returns>
        public Tile Clone()
        {
            return (Tile)base.MemberwiseClone();
        }

        /// <summary>
        /// Type of collision of the tile.
        /// </summary>
        public enum CollisionType
        { 
            /// <summary>
            /// If the tile can be crossed.
            /// </summary>
            Passable = 0,
            /// <summary>
            /// If the tile can't be crossed.
            /// </summary>
            Blocking = 1,
            /// <summary>
            /// If the tile can be crossed , but only from the bottom.
            /// </summary>
            Platform = 2,
        }

        /// <summary>
        /// Wall of the tile.
        /// </summary>
        public byte Wall
        {
            get { return wall; }
            set { wall = value; }
        }

        /// <summary>
        /// Type of the tile.
        /// </summary>
        public byte Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Type of collision of the tile.
        /// </summary>
        public byte Collision
        {
            get { return collision; }
            set { collision = value; }
        }

        /// <summary>
        /// Value of the bitmasking. Between 0 and 16
        /// </summary>
        public byte Bitmasking
        {
            get { return bitmasking; }
            set { bitmasking = value; }
        }

        /// <summary>
        /// Resistance of the tile (depends on tool power).
        /// </summary>
        public byte Resistance
        {
            get { return resistance; }
            set { resistance = value; }
        }
    }
}
