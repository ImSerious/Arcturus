using System;

namespace Arcturus.Items.Weapon
{
    /// <summary>
    /// Represents a weapon.
    /// </summary>
    public abstract class Weapon : Item
    {
        public int AttackPower { get; set; }
        public float KnockBack { get; set; }
        public float FireRate { get; set; }
        public bool IsSlotable { get; set; }
        public bool IsContinuous { get; set; }
        public ItemEffect ItemEffect { get; set; }

    }
}
