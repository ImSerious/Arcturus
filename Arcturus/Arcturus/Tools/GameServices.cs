using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Arcturus.Tools
{
    /// <summary>
    /// Represents the game services , such as content manager , graphics , etc.
    /// </summary>
    public static class GameServices
    {
        private static GameServiceContainer container;
        /// <summary>
        /// Instance of a service.
        /// </summary>
        public static GameServiceContainer Instance
        {
            get
            {
                if (container == null)
                {
                    container = new GameServiceContainer();
                }
                return container;
            }
        }

        /// <summary>
        /// Get the service.
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <returns>The service</returns>
        public static T GetService<T>()
        {
            return (T)Instance.GetService(typeof(T));
        }

        /// <summary>
        /// Add a service.
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <param name="service">Service to add</param>
        public static void AddService<T>(T service)
        {
            Instance.AddService(typeof(T), service);
        }

        /// <summary>
        /// Remove a service.
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        public static void RemoveService<T>()
        {
            Instance.RemoveService(typeof(T));
        }
    }
}
