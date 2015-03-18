using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Arcturus.Tools;
using Arcturus.Worlds;
using Arcturus.Engine;

namespace Arcturus
{
    /// <summary>
    /// All tools used in the game.
    /// </summary>
    public static class Utils
    {

        /// <summary>
        /// Load an texture.
        /// </summary>
        /// <param name="assetName">The name of the texture</param>
        /// <returns>The selected texture</returns>
        static public Texture2D LoadTexture2D(string assetName)
        {
            ContentManager content = GameServices.GetService<ContentManager>();
            Texture2D texture = content.Load<Texture2D>(assetName);
            return texture;
        }

        /// <summary>
        /// Load a sprite font.
        /// </summary>
        /// <param name="assetName">The name of the sprite font</param>
        /// <returns>The selected sprite font</returns>
        static public SpriteFont LoadSpriteFont(string assetName)
        {
            ContentManager content = GameServices.GetService<ContentManager>();
            SpriteFont spriteFont = content.Load<SpriteFont>(assetName);
            return spriteFont;
        }

        /// <summary>
        /// Load a shader file.
        /// </summary>
        /// <param name="assetName">The name of the shader file.</param>
        /// <returns>The selected shader file.</returns>
        static public Effect LoadShader(string assetName)
        {
            ContentManager content = GameServices.GetService<ContentManager>();
            Effect shader = content.Load<Effect>(assetName);
            return shader;
        }

        /// <summary>
        /// Get the trajectory between two vectors.
        /// </summary>
        /// <param name="position">Initial vector</param>
        /// <param name="targetPosition">Target vector</param>
        /// <returns>The trajectory</returns>
        static public Vector2 GetTrajectory(Vector2 position, Vector2 targetPosition)
        {
            var angle = Math.Atan2(targetPosition.Y - position.Y, targetPosition.Y - position.X);
            var trajectory = ConvertVector(Math.Cos(angle), Math.Sin(angle));
            return trajectory;

        }

        /// <summary>
        /// Get the real position of a tile in Pixel format.
        /// </summary>
        /// <param name="x">X coord of the tile</param>
        /// <param name="y">Y coord of the tile</param>
        /// <returns>The world position</returns>
        static public Vector2 GetTileWorldPosition(int x, int y)
        {
            Vector2 worldPosition = new Vector2(x * Tile.Width, y * Tile.Height);
            return worldPosition;
        }

        /// <summary>
        /// Create a vector with int coords.
        /// </summary>
        /// <param name="x">X coord</param>
        /// <param name="y">Y coord</param>
        /// <returns>A vector</returns>
        static public Vector2 ConvertVector(int x, int y)
        {
            Vector2 vector = new Vector2((float)x, (float)y);
            return vector;
        }

        /// <summary>
        /// Create a vector with double coords.
        /// </summary>
        /// <param name="x">X coord</param>
        /// <param name="y">Y coord</param>
        /// <returns>A vector</returns>
        static public Vector2 ConvertVector(double x, double y)
        {
            Vector2 vector = new Vector2((float)x, (float)y);
            return vector;
        }

        /// <summary>
        /// Create a vector with a Point object.
        /// </summary>
        /// <param name="point">Point object</param>
        /// <returns>A vector</returns>
        static public Vector2 ConvertVector(Point point)
        {
            Vector2 vector = new Vector2(point.X, point.Y);
            return vector;
        }

        /// <summary>
        ///   Generate a random color.
        /// </summary>
        /// <returns>The generated color.</returns>
        static public Color RandomColor()
        {
            Random random = new Random();

            Color color = new Color(random.Next(255), random.Next(255), random.Next(255));

            return color;
        }

        /// <summary>
        /// Compare the game version and the file version.
        /// </summary>
        /// <param name="fileVersion">Version of the save file</param>
        /// <param name="gameVersion">Version of the game</param>
        /// <returns>-1 if newer file , 0 if correct version , 1 if old file</returns>
        static public int CheckVersion(string fileVersion, string gameVersion)
        {
            int comparaison;
            var file = new Version(fileVersion);
            var game = new Version(gameVersion);
            comparaison = game.CompareTo(file); // -1 = file recent , 0 = equal , 1 game recent 
            return comparaison;
        }

        /// <summary>
        /// Get the width of the current screen resolution.
        /// </summary>
        /// <returns>Width of the screen resolution</returns>
        static public int GetDesktopWidth()
        {
            return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        }

        /// <summary>
        /// Get the height of the current screen resolution.
        /// </summary>
        /// <returns>Height of the screen resolution</returns>
        static public int GetDesktopHeight()
        {
            return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }
    }
}
