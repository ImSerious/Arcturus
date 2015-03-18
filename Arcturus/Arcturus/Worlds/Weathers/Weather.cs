using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arcturus.Tools;
using Arcturus.Engine;

namespace Arcturus.Worlds.Weathers
{
    /// <summary>
    /// Represents the weather of the world.
    /// </summary>
    public class Weather
    {
        float windSpeed;
        float drawOffset;
        Random random;
        Camera camera;
        Effect effect;
        TypeOfWeather typeOfWeather;
        WeatherParticule rainParticule;
        WeatherParticule snowParticule;
        List<Cloud> clouds;
        List<WeatherParticule> particules;
        /// <summary>
        /// Maximum particules used.
        /// </summary>
        const int MaxParticulesWeather = 2500;
        /// <summary>
        /// Maximum clouds in the sky.
        /// </summary>
        const int MaxClouds = 50;
        /// <summary>
        /// Amount of rain particules to spawn.
        /// </summary>
        const int RainParticulesAmount = 20;
        /// <summary>
        /// Amount of snow particules to spawn.
        /// </summary>
        const int SnowParticulesAmount = 3;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Weather()
        {
            windSpeed = 1F;
            drawOffset = 300F;
            random = new Random();
            clouds = new List<Cloud>();
            particules = new List<WeatherParticule>();
        }

        /// <summary>
        /// Loading all the contents.
        /// </summary>
        public void LoadContent()
        {
            rainParticule = new WeatherParticule(Utils.LoadTexture2D("Textures\\Weather\\raindrop"));
            snowParticule = new WeatherParticule(Utils.LoadTexture2D("Textures\\Weather\\snowflake"));
            camera = GameServices.GetService<Camera>();
        }

        /// <summary>
        /// Update the current weather.
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        /// <param name="tiles">Tiles of the world for collision detection</param>
        public void Update(GameTime gameTime, Tile[,] tiles)
        {
            // Spawn the particules if needed
            if (typeOfWeather != TypeOfWeather.Normal)
            {
                switch (typeOfWeather)
                {
                    case TypeOfWeather.Rain:
                        SpawnParticule(rainParticule.Clone(), RainParticulesAmount);
                        break;

                    case TypeOfWeather.Snow:
                        SpawnParticule(snowParticule.Clone(), SnowParticulesAmount);
                        break;
                }
            }

            // Clouds
            for (int i = 0; i < clouds.Count; i++)
            {
                clouds[i].Update();
            }

            // Particules
            for (int i = 0; i < particules.Count; i++)
            {
                particules[i].Update(windSpeed);
                CheckCollision(particules[i], tiles);

                if (!particules[i].IsActive)
                {
                    particules.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Draw all the particules of the current weather , clouds , and sun/moon.
        /// </summary>
        /// <param name="spriteBatch">Batch used to draw</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Clouds
            for (int i = 0; i < clouds.Count; i++)
            {
                clouds[i].Draw(spriteBatch);
            }

            // Particules
            for (int i = 0; i < particules.Count; i++)
            {
                particules[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Change the current weather.
        /// </summary>
        /// <param name="typeOfWeather">New kind of weather.</param>
        public void ChangeWeather(TypeOfWeather typeOfWeather)
        {
            this.typeOfWeather = typeOfWeather;
        }

        /// <summary>
        /// Spawn a particule.
        /// </summary>
        /// <param name="particule">Particule to spawn</param>
        /// <param name="amount">Amount of particule to spawn</param>
        public void SpawnParticule(WeatherParticule particule, int amount)
        {
            if (particules.Count < MaxParticulesWeather)
            {
                Rectangle drawZone = camera.GetDrawZone();
                drawZone.Inflate(50, 0);

                for (int i = 0; i < amount; i++)
                {
                    particule = particule.Clone();
                    particule.Rotation = -0.3F;
                    particule.Size = random.NextFloat(WeatherParticule.MinimumSize, WeatherParticule.MaximumSize);
                    particule.Position = new Vector2(random.Next(drawZone.Left * 16, drawZone.Right * 16), camera.Position.Y - drawOffset);
                    particule.Trajectory = new Vector2(2, 5);
                    particules.Add(particule);
                }
            }
        }

        /// <summary>
        /// Spawn clouds are 
        /// </summary>
        /// <param name="amount">Amount of clouds to spawn</param>
        public void SpawnCloud(int amount)
        {
            if (clouds.Count < MaxClouds)
            {
                for (int i = 0; i < amount; i++)
                {
                    Cloud cloud;
                    // Getting the skyzone
                    // Since the camera will only show the half of the sky in best case
                    // I just spawn cloud in the top half of the sky
                    int skyZone = World.WorldHeight / 2;

                    // Get the random size
                    // Between 30 % and 100 % of the natural size
                    var size = random.NextFloat(0.3F, 1F);

                    // The smaller is the cloud , the slower its moving
                    // To give the impression that its far way
                    var speed = new Vector2(size / 10, 0);

                    // Then spawn the cloud at random position
                    var randomPosition = random.Next(skyZone);
                    var position = new Vector2(-300, randomPosition);
                    cloud = new Cloud(size, speed, position);
                    clouds.Add(cloud);
                }
            }
        }

        /// <summary>
        /// Checking collision with the world.
        /// </summary>
        /// <param name="particule">Particle subject to collide.</param>
        /// <param name="tiles">Tiles of the world</param>
        public void CheckCollision(WeatherParticule particule, Tile[,] tiles)
        {
            int x = (int)particule.Position.X / 16;
            int y = (int)particule.Position.Y / 16;

            Rectangle tileHitbox = new Rectangle(x * Tile.Width, y * Tile.Height, Tile.Width, Tile.Height);

            if (tileHitbox.Intersects(particule.Hitbox))
            {
                if (tiles[x, y].Type != 0)
                {
                    particule.Die();
                }
            }
        }

        /// <summary>
        /// All types of weathers.
        /// </summary>
        public enum TypeOfWeather
        {
            Normal,
            Rain,
            Snow,
            Sandstorm,
            Storm
        }

        /// <summary>
        /// Particules of the weather.
        /// </summary>
        public List<WeatherParticule> Particles
        {
            get { return particules; }
        }
    }
}
