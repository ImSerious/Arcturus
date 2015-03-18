using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arcturus.Entities;
using Arcturus.Items;
using Arcturus.Worlds.Objects;
using Arcturus.Worlds.Weathers;
using Arcturus.Engine;

namespace Arcturus.Worlds
{
    /// <summary>
    /// Represents a world of Arcturus.
    /// </summary>
    public class World
    {
        string name;
        int seed;
        Weather weather;
        TimeSpan currentTime;
        TimeSpan lastTick;
        Rectangle drawZone;
        Tile[,] tiles;
        TileSet tileSet;
        TileSet wallSet;
        List<Entity> entities;
        List<Chest> chests;
        List<Plant> plants;
        List<Item> droppedItems;
        List<Projectile> projectiles;
        public const int WorldWidth = 2000;
        public const int WorldHeight = 1000;
        public const int MaxChests = 500;
        public const int MaxPlants = 2000;
        public const int MaxEntities = 1000;
        public const int MaxProjectiles = 2000;
        public const int MaxDroppedItems = 2000;
        public const int ExperienceRate = 100;
        public const int DropRate = 100;
        public const int CraftRate = 100;
        public const int EventRate = 100;
        public const int WorldTickRate = 10;
        public const float WorldDayDuration = 24F;
        public const float Gravity = 1F;

        /// <summary>
        /// Constructor.
        /// </summary>
        public World()
        {
            weather = new Weather();
            plants = new List<Plant>();
            chests = new List<Chest>();
            entities = new List<Entity>();
            droppedItems = new List<Item>();
            projectiles = new List<Projectile>();
            tiles = new Tile[WorldWidth, WorldHeight];
        }

        /// <summary>
        /// Where to load all content of the world and its components.
        /// </summary>
        public void LoadContent()
        {
            tileSet = new TileSet(Utils.LoadTexture2D("TileSets\\tileset"));
            wallSet = new TileSet(Utils.LoadTexture2D("TileSets\\wallset"));
            weather.LoadContent();
        }

        /// <summary>
        /// Updating the whole world.
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public void Update(GameTime gameTime)
        {
            // Entities
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(gameTime, tiles);

                // If the entity is still alive
                // we update it
                // otherwise we remove it and get the drop
                if (!entities[i].IsAlive)
                {
                    // Get the dropped item and add it in the world
                    var itemDropped = entities[i].Drop();
                    SpawnItem(itemDropped);
                    // Remove the entity
                    entities.RemoveAt(i);
                    i--;
                }
            }

            // Projectiles
            for (int i = 0; i < entities.Count; i++)
            {
                projectiles[i].Update(gameTime);
                // Collision here
            }

            // World Cycle
            WorldCycle(gameTime);
            // World Weather
            weather.Update(gameTime, tiles);
        }

        /// <summary>
        /// All stuff in the world cycle goes here.Time, weather, plants growth, event checking, etc ...
        /// </summary>
        /// <remarks>The cycle is every minutes</remarks>
        /// <param name="gameTime">Current game time</param>
        public void WorldCycle(GameTime gameTime)
        {
            if (lastTick + TimeSpan.FromSeconds(WorldTickRate) < gameTime.TotalGameTime)
            {
                // Update the weather type if needed
                weather.ChangeWeather(Weather.TypeOfWeather.Snow);

                // Update the current time of the world
                currentTime += TimeSpan.FromMinutes(WorldTickRate);

                // Update plants
                for (int i = 0; i < plants.Count; i++)
                {
                    plants[i].Grow(gameTime.TotalGameTime);
                }

                // Update the last tick 
                lastTick = gameTime.TotalGameTime;
            }
        }

        /// <summary>
        /// Drawing the world and its objects.
        /// </summary>
        /// <remark>The world draw only the stuff that the player can see</remark>
        /// <param name="camera">Camera of the world screen , to get the draw zone.</param>
        /// <param name="spriteBatch">Batch used to draw</param>
        public void Draw(Camera camera, SpriteBatch spriteBatch)
        {
            // We get the visible zone from the camera
            // to draw only tiles in the player view
            // + a little offset to avoid tiles flickering
            // if the player is fast moving
            drawZone = camera.GetDrawZone();
            // Weather
            weather.Draw(spriteBatch);

            // Tiles
            for (int x = drawZone.Left; x < drawZone.Right; x++)
            {
                for (int y = drawZone.Top; y < drawZone.Bottom; y++)
                {
                    if (x >= 0 && y >= 0 && x < WorldWidth && y < WorldHeight)
                    {
                        // Draw the tile if there is one to draw
                        if (tiles[x, y].Type != 0)
                        {
                            var position = Utils.GetTileWorldPosition(x, y);
                            tileSet.Draw(spriteBatch, tiles[x,y],position);
                        }

                        // Then Draw the wall if there is one to draw
                        // Note : it's useless to draw the wall if they is already a tile
                        else if (tiles[x, y].Wall != 0)
                        {
                            var position = Utils.GetTileWorldPosition(x, y);
                            wallSet.Draw(spriteBatch, tiles[x, y].Wall, position);
                        }
                    }
                }
            }

            // Entities
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Draw(spriteBatch);
            }

            // Projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(spriteBatch);
            }

            // Plants
            for (int i = 0; i < plants.Count; i++)
            {
                plants[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Spawn an lootable item in the world.
        /// </summary>
        /// <remarks>If the limit is reached , we remove the oldest element in the list and add the new one</remarks>
        /// <param name="item">Item to add in the world</param>
        public void SpawnItem(Item item)
        {
            if (droppedItems.Count >= MaxDroppedItems)
            {
                droppedItems.RemoveAt(0);
                droppedItems.Add(item);
            }
        }

        /// <summary>
        /// Remove item from the world.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        public void RemoveItem(Item item)
        {

        }

        /// <summary>
        /// Spawn an entity in the world.
        /// </summary>
        /// <remarks>If the limit is reached , we remove the oldest element in the list and add the new one</remarks>
        /// <param name="entity">Entity to add in the world</param>
        public void SpawnEntity(Entity entity)
        {
            if (entities.Count >= MaxEntities)
            {
                entities.RemoveAt(0);
                entities.Add(entity);
            }
        }

        /// <summary>
        /// Spawn a projectile in the world.
        /// </summary>
        /// <remarks>If the limit is reached , we remove the oldest element in the list and add the new one</remarks>
        /// <param name="projectile">Projectile to add in the world</param>
        public void SpawnProjectile(Projectile projectile)
        {
            if (projectiles.Count >= MaxProjectiles)
            {
                projectiles.RemoveAt(0);
                projectiles.Add(projectile);
            }
        }

        /// <summary>
        /// Spawn a plant in the world.
        /// </summary>
        /// <remarks>If the limit is reached , we remove the oldest element in the list and add the new one</remarks>
        /// <param name="plant">Plant to add in the world</param>
        public void SpawnPlant(Plant plant)
        {
            if (plants.Count >= MaxPlants)
            {
                plants.RemoveAt(0);
                plants.Add(plant);
            }
        }

        /// <summary>
        /// Spawn a tile in the world.
        /// </summary>
        /// <param name="tile">Tile to spawn</param>
        /// <param name="positionX">Position X of the tile</param>
        /// <param name="positionY">Position Y of the tile</param>
        public void SpawnTile(Tile tile, int positionX, int positionY)
        {
            if (tiles[positionX, positionY].Type == 0)
            {
                // Put the tile
                tiles[positionX, positionY].Type = tile.Type;
                tiles[positionX, positionY].Collision = tile.Collision;
                // Refresh the bitmask
                SetBitmask(positionX, positionY);
            }
        }

        /// <summary>
        /// Remove a tile from the world.
        /// </summary>
        /// <param name="positionX">X coords</param>
        /// <param name="positionY">Y coords</param>
        public void RemoveTile(int positionX, int positionY)
        {
            if (tiles[positionX, positionY].Type != 0)
            {
                tiles[positionX, positionY].Type = 0;
                tiles[positionX, positionY].Collision = 0;
            }
        }

        /// <summary>
        /// Spawn a wall in the world.
        /// </summary>
        /// <param name="wall">Wall to spawn</param>
        /// <param name="positionX">Position X of the wall</param>
        /// <param name="positionY">Position Y of the wall</param>
        public void SpawnWall(byte wall, int positionX, int positionY)
        {
            if (tiles[positionX, positionY].Wall == 0)
                tiles[positionX, positionY].Wall = wall;
        }

        /// <summary>
        /// Set the bitmask of the tile and the neighbour tiles.
        /// </summary>
        /// <param name="x">X position in the world.</param>
        /// <param name="y">Y position in the world.</param>
        public void SetBitmask(int x, int y)
        {
            Tile centerTile = tiles[x, y];
            Tile leftTile = tiles[x - 1, y];
            Tile rightTile = tiles[x + 1, y];
            Tile bottomTile = tiles[x, y + 1];
            Tile topTile = tiles[x, y - 1];

            // Bottom tile
            if (bottomTile.Type == centerTile.Type)
            {
                bottomTile.Bitmasking += 1;
                centerTile.Bitmasking += 4;
            }
            else
            {
                bottomTile.Bitmasking -= 1;
                centerTile.Bitmasking -= 4;
            }
            
            // Left tile
            if (leftTile.Type == centerTile.Type)
            {
                leftTile.Bitmasking += 2;
                centerTile.Bitmasking += 8;
            }
            else
            {
                bottomTile.Bitmasking -= 1;
                centerTile.Bitmasking -= 4;
            }

            // Top tile
            if (topTile.Type == centerTile.Type)
            {
                topTile.Bitmasking += 4;
                centerTile.Bitmasking += 1;
            }
            else
            {
                bottomTile.Bitmasking -= 1;
                centerTile.Bitmasking -= 4;
            }

            // Right tile
            if (rightTile.Type == centerTile.Type)
            {
                rightTile.Bitmasking += 8;
                centerTile.Bitmasking += 2;
            }
            else
            {
                bottomTile.Bitmasking -= 1;
                centerTile.Bitmasking -= 4;
            }
        }

        /// <summary>
        /// Save the entire world in a ".arc" file.
        /// </summary>
        /// <remarks>A lot of short variables are used , to make the weight of the save file less big</remarks>
        /// <param name="filePath">Path of the file</param>
        public void Save(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    // Header
                    binaryWriter.Write(Arcturus.Version);
                    binaryWriter.Write(name);
                    binaryWriter.Write(seed);
                    binaryWriter.Write(currentTime.TotalHours);
                    // World size
                    binaryWriter.Write(tiles.GetLength(0));
                    binaryWriter.Write(tiles.GetLength(1));
                    // Tiles
                    for (int x = 0; x < WorldWidth; x++)
                    {
                        for (int y = 0; y < WorldHeight; y++)
                        {
                            binaryWriter.Write(tiles[x, y].Type);
                            binaryWriter.Write(tiles[x, y].Wall);
                        }
                    }
                    // Chests
                    binaryWriter.Write(chests.Count);
                    for (int i = 0; i < chests.Count; i++)
                    {
                        binaryWriter.Write((short)chests[i].WorldPosition.X);
                        binaryWriter.Write((short)chests[i].WorldPosition.Y);
                        // Items in chests
                        for (int j = 0; j < Chest.MaxCapacity; j++)
                        {
                            var currentItem = chests[i].ContainedItems[j];
                            binaryWriter.Write(currentItem.ID);
                            if (currentItem.ID != 0)
                                binaryWriter.Write(currentItem.Quantity);
                        }
                    }
                    // Plants
                    binaryWriter.Write(plants.Count);
                    for (int i = 0; i < plants.Count; i++)
                    {
                        binaryWriter.Write(plants[i].ID);
                        binaryWriter.Write(plants[i].GrowState);
                        binaryWriter.Write(plants[i].WorldPosition.X);
                        binaryWriter.Write(plants[i].WorldPosition.Y);
                    }
                    binaryWriter.Close();
                    fileStream.Close();
                }
            }
        }

        /// <summary>
        /// Load a world from a ".arc" file.
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        /// <returns></returns>
        public static World Load(string filePath)
        {
            World world = new World();

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    // Checking version
                    string fileVersion = binaryReader.ReadString();
                    string gameVersion = Arcturus.Version;
                    int comparaison = Utils.CheckVersion(fileVersion, gameVersion);

                    if (comparaison == -1)
                    {
                        // The file is more recent than the game
                        return null;
                    }

                    // World header
                    world.name = binaryReader.ReadString();
                    world.seed = binaryReader.ReadInt32();
                    world.currentTime = TimeSpan.FromHours(binaryReader.ReadDouble());
                    // World size
                    int worldWidth = binaryReader.ReadInt32();
                    int worldHeight = binaryReader.ReadInt32();
                    // World tiles
                    world.tiles = new Tile[worldWidth, worldHeight];
                    for (int x = 0; x < worldWidth; x++)
                    {
                        for (int y = 0; y < worldHeight; y++)
                        {
                            byte tileType = binaryReader.ReadByte();
                            byte wallType = binaryReader.ReadByte();
                            world.tiles[x, y] = new Tile(tileType);
                            world.tiles[x, y].Wall = wallType;
                        }
                    }

                    // Chests
                    int numberOfChest = binaryReader.ReadInt32();
                    world.chests = new List<Chest>();

                    for (int i = 0; i < numberOfChest; i++)
                    {
                        Chest chest = new Chest();
                        short chestPositionX = binaryReader.ReadInt16();
                        short chestPositionY = binaryReader.ReadInt16();

                        for (int j = 0; j < Chest.MaxCapacity; j++)
                        {
                            short itemID = binaryReader.ReadInt16();
                            if (itemID != 0)
                            {
                                Item item = null; // TODO : concordance ID / Item
                                item.Quantity = binaryReader.ReadInt16();
                                chest.ContainedItems[j] = item;
                            }
                        }
                        world.chests.Add(chest);
                    }

                    // Plants
                    int numberOfPlants = binaryReader.ReadInt32();
                    for (int i = 0; i < numberOfPlants; i++)
                    {
                        byte id = binaryReader.ReadByte();
                        byte growState = binaryReader.ReadByte();
                        short plantPositionX = binaryReader.ReadInt16();
                        short plantPositionY = binaryReader.ReadInt16();
                        Plant plant = null; // TODO : concorcande ID / Plant
                        plant.GrowState = growState;
                        world.plants.Add(plant);
                    }

                    fileStream.Close();
                    binaryReader.Close();
                }
            }

            world.LoadContent();
            // return the loaded world
            return world;
        }

        /// <summary>
        /// Name of the world.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Seed used to generate the world.
        /// </summary>
        public int Seed
        {
            get { return seed; }
            set { seed = value; }
        }

        /// <summary>
        /// Current time of the world.
        /// </summary>
        public TimeSpan CurrentTime
        {
            get { return currentTime; }
        }

        /// <summary>
        /// Current weather of the world.
        /// </summary>
        public Weather Weather
        {
            get { return weather; }
        }

        /// <summary>
        /// Tiles of the world
        /// </summary>
        public Tile[,] Tiles
        {
            get { return tiles; }
        }

    }
}
