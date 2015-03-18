using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arcturus.Worlds;
using Arcturus.Entities;

namespace Arcturus.Screens
{
    /// <summary>
    /// Generation screen, where the world be generate.
    /// </summary>
    public class GenerationScreen : Screen
    {
        World world;
        Player player;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GenerationScreen(Player player)
        {

        }

        public override void LoadContent()
        {

        }

        public override void Initialize()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public override void GetInputs(GameTime gameTime)
        {
            ScreenManager.SwitchScreen(new WorldScreen(world, player));
        }


    }
}
