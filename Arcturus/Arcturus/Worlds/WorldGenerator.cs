using System;
using System.Collections.Generic;
using Arcturus.Tools;

namespace Arcturus.Worlds
{
    /// <summary>
    /// Represents a generator that can create worlds.
    /// </summary>
    static class WorldGenerator
    {
        /// <summary>
        /// Limit of chests to spawn
        /// </summary>
        const int MaxSpawnChest = 200;
        /// <summary>
        /// Limit of plants to spawn
        /// </summary>
        const int MaxSpawnPlants = 500;

        /// <summary>
        /// Generate a world , with a seed.
        /// </summary>
        /// <param name="name">Name of the world</param>
        /// <param name="seed">The seed used for generation</param>
        /// <returns>The freshly created world</returns>
        static public World Generate(string name, int seed)
        {
            // Create the world
            World world = new World();
            world.Seed = seed;
            world.Name = name;
            // Fill the world
            Initialize(world);
            PlaceChests(world);
            PlacePlants(world);
            // Return the created world
            return world;
        }

        /// <summary>
        /// Initiazling the world.
        /// </summary>
        /// <param name="world">World to init</param>
        static private void Initialize(World world)
        {
            // Test purpose
            for (int x = 0; x < World.WorldWidth; x++)
            {
                for (int y = 0; y < World.WorldHeight; y++)
                {
                    Tile emptyTile = new Tile(0);
                    emptyTile.Collision = 0;
                    Tile dirtTile = new Tile(1);
                    dirtTile.Collision = 1;
                    dirtTile.Wall = 1;
                    if (y > (World.WorldHeight / 2) +2)
                        world.Tiles[x, y] = dirtTile;
                    else
                        world.Tiles[x, y] = emptyTile;
                }
            }
        }

        /// <summary>
        /// Place random chests in the world.
        /// </summary>
        /// <param name="world">World to place chests in</param>
        static private void PlaceChests(World world)
        {
            for (int i = 0; i < MaxSpawnChest; i++)
            {

            }
        }

        /// <summary>
        /// Place random plants in the world.
        /// </summary>
        /// <param name="world">World to place plants in</param>
        static private void PlacePlants(World world)
        {
            for (int i = 0; i < MaxSpawnPlants; i++)
            {

            }
        }
    }
}
