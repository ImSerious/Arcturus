using System;
using Microsoft.Xna.Framework.Graphics;

namespace Arcturus.Items.Stackable
{
    /// <summary>
    /// Represents an ammunition for ranged weapons.
    /// </summary>
    public class Ammo : Item
    {
        int attackPower;
        byte type;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="attackPower">Attack power of the ammo</param>
        /// <param name="type">Type of the ammo</param>
        /// <param name="texture">Texture of the ammo to draw</param>
        public Ammo(int attackPower,byte type,Texture2D texture)
        {
            this.attackPower = attackPower;
            this.type = type;
        }

        /// <summary>
        /// Attack power of the ammo.
        /// </summary>
        public int AttackPower
        {
            get { return attackPower; }
        }

        /// <summary>
        /// Type of the ammo.
        /// </summary>
        public byte Type
        {
            get { return type; }
        }
    }
}
