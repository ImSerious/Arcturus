using Arcturus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Arcturus.Worlds;

namespace Arcturus.Entities
{
    /// <summary>
    /// Represents the player.
    /// </summary>
    public class Player : Entity
    {
        long money;
        short skillPoints;
        int currentExperience;
        int nextLevelExperience;
        Item currentItem;
        Item[] backpack;
        World world;
        Rectangle detectionZone;
        Rectangle tileZone;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Player()
        {
            Level = 1;
            money = 100;
            MoveSpeed = 1;
            Health = 50;
            MaxHealth = 50;
            Mana = 20;
            MaxMana = 20;
            nextLevelExperience = 100;
            backpack = new Item[50];
        }

        /// <summary>
        /// Loading extra content for the player.
        /// </summary>
        public override void ExtraLoadContent()
        {
            Sprite = new Sprite(Utils.LoadTexture2D("Textures\\Player\\player"), new Vector2(1000 * 16 , 500 * 16), Color.White);
        }

        /// <summary>
        /// Updating the player.
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public override void ExtraUpdate(GameTime gameTime)
        {

        }

        /// <summary>
        /// Extra draw to do.
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw</param>
        public override void ExtraDraw(SpriteBatch spriteBatch)
        {

        }

        /// <summary>
        /// Add an item to the backback of the player.
        /// </summary>
        /// <param name="item">Item to add</param>
        public void AddItem(Item item)
        {
            for (int i = 0; i < backpack.Length; i++)
            {
                var loopItem = backpack[i];

                if (loopItem != null)
                {
                    // If there is already this item and if its stackable.
                    if (loopItem.ID == item.ID && item.IsStackable)
                    {
                        short quantity = item.Quantity;

                        if (loopItem.AddQuantity(ref quantity))
                        {
                            item.Quantity = quantity; // no break, since we have to create a new stack with the left quantity.              
                        }

                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    // If the slot is free.
                    backpack[i] = item;
                    break;
                }
            }
        }

        /// <summary>
        /// Remove an item from the backpack
        /// </summary>
        /// <param name="item">Item to remove</param>
        public void RemoveItem(Item item)
        {
            for (int i = 0; i < backpack.Length; i++)
            {
                if (ReferenceEquals(backpack[i], item))
                {
                    backpack[i] = null;
                }
            }
        }

        /// <summary>
        /// Drop an item in the world.
        /// </summary>
        /// <param name="item">Item to drop</param>
        public void DropItem(Item item)
        {
            world.SpawnItem(item);
            RemoveItem(item);
        }

        /// <summary>
        /// Uses an item.
        /// </summary>
        /// <param name="item">Item to use</param>
        public void UseItem(Item item)
        {
            switch (item.TypeOfItem)
            {
                case Item.TypeItem.RangedWeapon:

                    break;

                case Item.TypeItem.TileStack:
                   
                    break;

                case Item.TypeItem.Consumable:

                    break;
            }
        }

        /// <summary>
        /// Uses the current item.
        /// </summary>
        public void UseCurrentItem()
        {
            if (currentItem !=null)
            UseItem(currentItem);
        }

        /// <summary>
        /// Modifying the money (+ or -).
        /// </summary>
        /// <param name="amount">Amount to add or remove</param>
        public void ModifyMoney(long amount)
        {
            money += amount;

            if (money <= 0)
                money = 0;
        }

        /// <summary>
        /// Modifying the experience (+ or -).
        /// </summary>
        /// <param name="amount">Amount to add or remove</param>
        public void ModifyExperience(int amount)
        {
            currentExperience += amount;

            if (currentExperience <= 0)
            {
                currentExperience = 0;
            }

            if (currentExperience >= nextLevelExperience)
            {
                LevelUp();
            }
        }

        /// <summary>
        /// Recove an amount of health to the player.
        /// </summary>
        /// <param name="amount">Amount of health to recove</param>
        public void RecoveHealth(int amount)
        {
            Health += amount;

            if (Health >= MaxHealth)
                Health = MaxHealth;

        }

        /// <summary>
        /// Recove an amount of mana to the player.
        /// </summary>
        /// <param name="amount">Amount of mana to recove</param>
        public void RecoveMana(int amount)
        {
            Mana += amount;

            if (Mana >= MaxMana)
                Mana = MaxMana;
        }

        /// <summary>
        /// Level up the player , reseting his current experience and calculate the next level experience.
        /// </summary>
        public void LevelUp()
        {
            // Reset the current experience since the player just level up
            currentExperience = 0;
            // Calculate the next experience needed to level up - The formula is actually : Last Experience + 10% Last Experience
            nextLevelExperience = nextLevelExperience + (nextLevelExperience / 10);
            // Increase the skill points
            skillPoints++;
            // Fully heal the player
            RecoveHealth(MaxHealth);
        }

        /// <summary>
        /// The player die,lost 5% of his current level experience.
        /// </summary>
        public override void Death()
        {
            IsAlive = false;
            ModifyExperience(nextLevelExperience / -20); // Remove 5% of the current level experience.
            ModifyMoney(money / -10); // Remove 10% of the current money.
        }

        /// <summary>
        /// Save the player in a ".arc" file.
        /// </summary>
        /// <param name="filePath">File path to save the file on the hard drive.</param>
        public void Save(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(Name);
                    binaryWriter.Write(currentExperience);
                    binaryWriter.Write(Level);
                    binaryWriter.Write(Health);
                    binaryWriter.Write(MaxHealth);
                    binaryWriter.Write(Position.X);
                    binaryWriter.Write(Position.Y);

                    foreach (Item item in backpack)
                    {
                        if (item != null)
                        {
                            binaryWriter.Write(item.ID);
                            binaryWriter.Write(item.Quantity);
                        }
                    }

                    fileStream.Close();
                    binaryWriter.Close();
                }
            }
        }

        /// <summary>
        /// Load the player from a ".arc" file.
        /// </summary>
        /// <param name="filePath">File path to load the file from the hard drive</param>
        /// <returns></returns>
        static public Player Load(string filePath)
        {
            Player player = new Player();

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {

                    // Player informations
                    player.Name = binaryReader.ReadString();
                    player.currentExperience = binaryReader.ReadInt32();
                    player.Level = binaryReader.ReadByte();
                    player.Health = binaryReader.ReadInt32();
                    player.MaxHealth = binaryReader.ReadInt32();
                    int positionX = binaryReader.ReadInt32();
                    int positionY = binaryReader.ReadInt32();
                    // Backpack items
                    for (int i = 0; i < 50; i++)
                    {
                        short id = binaryReader.ReadInt16();

                        if (id != 0)
                        {
                            player.backpack[i].ID = id;
                            player.backpack[i].Quantity = binaryReader.ReadInt16();
                        }
                    }

                    fileStream.Close();
                    binaryReader.Close();
                }
            }
            return player;
        }

        /// <summary>
        /// Current experience of the player.
        /// </summary>
        public int CurrentExperience
        {
            get { return currentExperience; }
        }

        /// <summary>
        /// Amount of experience recquired to level up.
        /// </summary>
        public int NextLevelExperience
        {
            get { return nextLevelExperience; }
        }

        /// <summary>
        /// Current money of the player.
        /// </summary>
        public long Money
        {
            get { return money; }
        }

        /// <summary>
        /// Current item of the player.
        /// </summary>
        public Item CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; }
        }

        /// <summary>
        /// Backpack of the player.
        /// </summary>
        public Item[] BackPack
        {
            get { return backpack; }
        }

    }
}
