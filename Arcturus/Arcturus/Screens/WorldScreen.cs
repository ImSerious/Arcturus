using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Arcturus.Worlds;
using Arcturus.Entities;
using Arcturus.Tools;
using Arcturus.Engine;

namespace Arcturus.Screens
{
    /// <summary>
    /// Represents the screen which will draw the world.
    /// </summary>
    public class WorldScreen : Screen
    {
        Background background;
        SpriteFont defaultFont;
        World world;
        Player player;
        Camera camera;

        /// <summary>
        /// Constructor.
        /// </summary>
        public WorldScreen(World world, Player player)
        {
            this.world = world;
            this.player = player;
            this.camera = new Camera(player);
            this.InputManager = new InputManager();
            this.LoadContent();
        }

        /// <summary>
        /// Load the content of the screen.
        /// </summary>
        public override void LoadContent()
        {
            // Adding camera as a service
            GameServices.AddService<Camera>(camera);

            // Graphics ressources
            defaultFont = Utils.LoadSpriteFont("Fonts\\Default");
            background = new Background(Utils.LoadTexture2D("Textures\\Backgrounds\\background"));

            // Load content
            player.LoadContent();
            world.LoadContent();
        }

        /// <summary>
        /// Initiliazing all the stuff.
        /// </summary>
        public override void Initialize()
        {
        }

        /// <summary>
        /// Updating all the world screen.
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public override void Update(GameTime gameTime)
        {
            GetInputs(gameTime);
            world.Update(gameTime);
            player.Update(gameTime, world.Tiles);
            camera.Update(gameTime);
        }

        /// <summary>
        /// Get the inputs of the player , mouse and keyboard.
        /// </summary>
        public override void GetInputs(GameTime gameTime)
        {
            if (IsActive)
            {
                // Refresh the inputs manager
                InputManager.Update();

                // "Left" key
                if (InputManager.IsKeyDown(Keys.Q))
                {
                    player.Move(Entity.Direction.Left);
                }

                // "Right" key
                if (InputManager.IsKeyDown(Keys.D))
                {
                    player.Move(Entity.Direction.Right);
                }

                // "Jump" key
                if (InputManager.IsKeyDown(Keys.Space))
                {
                    player.Jump();
                }

                // "Action" key
                if (InputManager.IsKeyPress(Keys.F))
                {
                    player.ApplyKnockback(6, Entity.Direction.Left);
                }

                // "Save" key
                if (InputManager.IsKeyPress(Keys.S))
                {
                    world.Save("World1.arc");
                }

                // "Left" mouse button
                if (InputManager.IsMouseDown(Mouse.GetState().LeftButton))
                {
                    player.UseCurrentItem();
                    Tile tile = new Tile(2);
                    tile.Collision = 1;
                    world.SpawnTile(tile, (int)(Mouse.GetState().X + camera.Position.X) / 16, (int)(Mouse.GetState().Y + camera.Position.Y) / 16); // Test only
                }

                // "Right" mouse button
                if (InputManager.IsMouseDown(Mouse.GetState().RightButton))
                {
                    world.RemoveTile((int)(Mouse.GetState().X + camera.Position.X) / 16, (int)(Mouse.GetState().Y + camera.Position.Y) / 16); // Test only
                }

                if (InputManager.IsMouseClick(Mouse.GetState().MiddleButton))
                {
                    Tile tile = new Tile(0);
                    tile.Wall = 2;
                    world.SpawnWall(tile.Wall, (int)(Mouse.GetState().X + camera.Position.X) / 16, (int)(Mouse.GetState().Y + camera.Position.Y) / 16);
                }

                // "Scroll up"
                if (InputManager.IsScrollUp())
                {
                    camera.Scale /= 2F;
                }

                // "Scroll down"
                if (InputManager.IsScrollDown())
                {
                    camera.Scale *= 2F;
                }
            }
        }

        /// <summary>
        /// Draw the screen
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw stuff</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw static
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            spriteBatch.End();

            // Draw influed by the camera
            spriteBatch.Begin(camera);
            world.Draw(camera, spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();

            // Draw interface
            spriteBatch.Begin();
            spriteBatch.DrawString(defaultFont, "Player velocity : " + player.Velocity, Vector2.Zero, Color.Black);
            spriteBatch.DrawString(defaultFont, "Player position : " + player.Position, new Vector2(0, 20), Color.Black);
            spriteBatch.DrawString(defaultFont, "Player health : " + player.Health, new Vector2(0, 40), Color.Black);
            spriteBatch.DrawString(defaultFont, "World current time : " + world.CurrentTime, new Vector2(0, 60), Color.Black);
            spriteBatch.DrawString(defaultFont, "Weather particules : " + world.Weather.Particles.Count, new Vector2(0, 80), Color.Black);
            spriteBatch.DrawString(defaultFont, "If player on ground : " + player.IsOnGround, new Vector2(0, 100), Color.Black);
            spriteBatch.End();
        }
    }
}
