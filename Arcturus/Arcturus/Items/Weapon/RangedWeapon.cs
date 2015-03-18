using System;
using Arcturus.Items.Stackable;
using Arcturus.Worlds.Objects;

namespace Arcturus.Items.Weapon
{
    /// <summary>
    /// Represents a ranged weapon
    /// </summary>
    public class RangedWeapon : Weapon
    {
        bool isAmmoNeeded;
        byte ammoType;
        Projectile projectile;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RangedWeapon(bool isAmmoNeeded ,byte ammoType,Projectile projectile)
        {
            this.isAmmoNeeded = isAmmoNeeded;
            this.ammoType = ammoType;
            this.projectile = projectile;
        }

    }
}
