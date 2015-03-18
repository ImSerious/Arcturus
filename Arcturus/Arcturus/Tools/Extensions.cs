using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Arcturus.Engine;
using Microsoft.Xna.Framework;
using System.IO;

namespace Arcturus.Tools
{
    static class Extensions
    {
        /// <summary>
        /// Gets a random float. 
        /// </summary>
        /// <param name="random">Random object</param>
        /// <param name="seed">The seed for the generated number</param>
        /// <returns>Random generated float</returns>
        static public float NextFloat(this Random random, float seed)
        {
            float randomNumber = (float)random.NextDouble();
            randomNumber = (float)Math.Round(randomNumber, 2);
            return seed * randomNumber;
        }

        /// <summary>
        /// Gets a random float.
        /// </summary>
        /// <param name="random">Random object</param>
        /// <param name="beginRange">The begin range</param>
        /// <param name="EndRange">The end range</param>
        /// <returns>Random generated float</returns>
        static public float NextFloat(this Random random, float beginRange, float EndRange)
        {
            float difference = EndRange - beginRange;
            float randomNumber = beginRange + random.NextFloat(difference);
            return randomNumber;
        }

        /// <summary>
        /// Gets a short float
        /// </summary>
        /// <param name="random">Random object</param>
        /// <param name="seed">The seed for the generated number</param>
        /// <returns></returns>
        static public short NextShort(this Random random, short seed)
        {
            return (short)random.Next((int)seed);
        }

        /// <summary>
        /// Begin the draw of the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw</param>
        /// <param name="camera">Camera to draw on</param>
        static public void Begin(this SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.Transform);
        }

        /// <summary>
        /// Write an object in binary format.
        /// </summary>
        /// <param name="binaryWriter">Current instance of a binary writer.</param>
        /// <param name="value">Value to write.</param>
        static public void Write(this BinaryWriter binaryWriter, object value)
        {

        }

        /// <summary>
        /// Read an object in binary format.
        /// </summary>
        /// <param name="binaryReader">Current instance of a binary reader.</param>
        static public object ReadObject(this BinaryReader binaryReader)
        {
            return new Object();
        }
    }
}
