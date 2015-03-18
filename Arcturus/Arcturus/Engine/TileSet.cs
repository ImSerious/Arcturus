using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Arcturus.Worlds;

namespace Arcturus.Engine
{
    /// <summary>
    /// Represents a set of tiles.
    /// </summary>
    public class TileSet
    {
        int rowTiles;
        int columnsTiles;
        Texture2D texture;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texture"></param>
        public TileSet(Texture2D texture)
        {
            this.texture = texture;
            this.rowTiles = texture.Width / Tile.Width;
            this.columnsTiles = texture.Height / Tile.Height;
        }

        /// <summary>
        /// Draw the source of the tileset.
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw.</param>
        /// <param name="tileType">Tile to draw.</param>
        /// <param name="position">Position in the world.</param>
        public void Draw(SpriteBatch spriteBatch, Tile tile, Vector2 position)
        {
            var source = GetTileSource(tile.Type);
            spriteBatch.Draw(texture, position, source, Color.White);
        }

        /// <summary>
        /// Draw the source of the tileset.
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw.</param>
        /// <param name="tileType">Type of the tile to draw.</param>
        /// <param name="position">Position in the world.</param>
        public void Draw(SpriteBatch spriteBatch, short tileType, Vector2 position)
        {
            var source = GetTileSource(tileType);
            spriteBatch.Draw(texture, position, source, Color.White);
        }

        /// <summary>
        /// Get the tile source.
        /// </summary>
        /// <param name="type">Type of the tile</param>
        /// <returns>The source rectangle</returns>
        private Rectangle GetTileSource(short type)
        {
            int positionX = type % rowTiles;
            int positionY = type / columnsTiles;
            Rectangle rectangleSource = new Rectangle(positionX * Tile.Width + (positionX + 1),
                                                      positionY * Tile.Height + (positionY + 1),
                                                      Tile.Width, Tile.Height);
            return rectangleSource;
        }

    }
}
