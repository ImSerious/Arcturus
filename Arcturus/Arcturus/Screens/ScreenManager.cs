using System;
using Arcturus.Entities;
using Arcturus.Worlds;

namespace Arcturus.Screens
{
    /// <summary>
    /// A manager to switch and keep the state of each screen.
    /// </summary>
    public class ScreenManager
    {
        /// <summary>
        /// The screen that is updated and draw.
        /// </summary>
        public Screen CurrentScreen { get; set; }
        /// <summary>
        /// Screen of the creation of the character.
        /// </summary>
        public Screen CreationScreen { get; set; }
        /// <summary>
        /// Screen of the world.
        /// </summary>
        public Screen WorldScreen { get; set; }
        /// <summary>
        /// Screen where the world is generated.
        /// </summary>
        public Screen GenerationScreen { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ScreenManager()
        {
            WorldScreen = new WorldScreen(WorldGenerator.Generate("World", 1337), new Player()); // TODO : test only
            WorldScreen.Initialize();
        }

        /// <summary>
        /// Change the old screen for the new one.
        /// </summary>
        /// <param name="screen">The screen to show</param>
        public void SwitchScreen(Screen screen)
        {
            CurrentScreen = screen;
        }
    }
}
