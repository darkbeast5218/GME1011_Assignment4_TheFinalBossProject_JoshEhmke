using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace HauntedForestEscape
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Textures
        private Texture2D playerTexture;
        private Texture2D ghostTexture;
        private Texture2D potionTexture;
        private Texture2D gateClosedTexture;
        private Texture2D gateOpenTexture;

        private Texture2D forestBg;
        private Texture2D caveBg;
        private Texture2D desertBg;
        private Texture2D castleBg;
        private Texture2D swampBg;

        private SpriteFont font;

        private Player player;
        private List<Level> levels = new List<Level>();
        private int currentLevelIndex = 0;

        private List<Ghost> ghosts;
        private List<Potion> potions;
        private Gate gate;
        private Texture2D currentBackground;

        private bool hasEscaped = false;
        private bool isDead = false;
        private double deathTimer = 0;

        private Song backgroundMusic;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load backgrounds
            forestBg = Content.Load<Texture2D>("forest");
            caveBg = Content.Load<Texture2D>("cave");
            desertBg = Content.Load<Texture2D>("desert");
            castleBg = Content.Load<Texture2D>("castle");
            swampBg = Content.Load<Texture2D>("swamp");

            // Load sprites
            playerTexture = Content.Load<Texture2D>("player");
            ghostTexture = Content.Load<Texture2D>("ghost");
            potionTexture = Content.Load<Texture2D>("potion");
            gateClosedTexture = Content.Load<Texture2D>("gateclosed");
            gateOpenTexture = Content.Load<Texture2D>("gateopen");
            font = Content.Load<SpriteFont>("DefaultFont");

            // Load and play music
            backgroundMusic = Content.Load<Song>("bg_music");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.Play(backgroundMusic);

            player = new Player(new Vector2(100, 100), playerTexture);

            DefineLevels();
            LoadLevel(0);
        }

        private void DefineLevels()
        {
            // LEVEL 1
            var ghosts1 = new List<Ghost>
            {
                new Ghost(new Vector2(300, 200), ghostTexture),
                new Ghost(new Vector2(500, 100), ghostTexture)
            };
            var potions1 = new List<Potion>
            {
                new Potion(new Vector2(200, 300), potionTexture),
                new Potion(new Vector2(400, 250), potionTexture)
            };
            var gate1 = new Gate(new Vector2(700, 50), gateClosedTexture, gateOpenTexture, potions1.Count);
            levels.Add(new Level(ghosts1, potions1, gate1, forestBg));

            // LEVEL 2
            var ghosts2 = new List<Ghost>
            {
                new Ghost(new Vector2(250, 150), ghostTexture),
                new Ghost(new Vector2(600, 250), ghostTexture),
                new Ghost(new Vector2(450, 300), ghostTexture)
            };
            var potions2 = new List<Potion>
            {
                new Potion(new Vector2(150, 100), potionTexture),
                new Potion(new Vector2(550, 200), potionTexture),
                new Potion(new Vector2(300, 350), potionTexture)
            };
            var gate2 = new Gate(new Vector2(750, 400), gateClosedTexture, gateOpenTexture, potions2.Count);
            levels.Add(new Level(ghosts2, potions2, gate2, caveBg));

            // LEVEL 3
            var ghosts3 = new List<Ghost>
            {
                new Ghost(new Vector2(100, 200), ghostTexture),
                new Ghost(new Vector2(400, 100), ghostTexture)
            };
            var potions3 = new List<Potion>
            {
                new Potion(new Vector2(350, 200), potionTexture),
                new Potion(new Vector2(600, 150), potionTexture)
            };
            var gate3 = new Gate(new Vector2(700, 400), gateClosedTexture, gateOpenTexture, potions3.Count);
            levels.Add(new Level(ghosts3, potions3, gate3, desertBg));

            // LEVEL 4
            var ghosts4 = new List<Ghost>
            {
                new Ghost(new Vector2(300, 100), ghostTexture),
                new Ghost(new Vector2(500, 300), ghostTexture)
            };
            var potions4 = new List<Potion>
            {
                new Potion(new Vector2(250, 250), potionTexture),
                new Potion(new Vector2(400, 200), potionTexture)
            };
            var gate4 = new Gate(new Vector2(720, 420), gateClosedTexture, gateOpenTexture, potions4.Count);
            levels.Add(new Level(ghosts4, potions4, gate4, castleBg));

            // LEVEL 5
            var ghosts5 = new List<Ghost>
            {
                new Ghost(new Vector2(200, 150), ghostTexture),
                new Ghost(new Vector2(600, 300), ghostTexture)
            };
            var potions5 = new List<Potion>
            {
                new Potion(new Vector2(300, 350), potionTexture),
                new Potion(new Vector2(500, 100), potionTexture)
            };
            var gate5 = new Gate(new Vector2(720, 50), gateClosedTexture, gateOpenTexture, potions5.Count);
            levels.Add(new Level(ghosts5, potions5, gate5, swampBg));
        }

        private void LoadLevel(int index)
        {
            var level = levels[index];

            ghosts = level.Ghosts;
            potions = level.Potions;
            gate = level.Gate;
            currentBackground = level.BackgroundTexture;

            player.Position = new Vector2(100, 100);
            player.PotionsCollected = 0;
            player.ResetHealth();

            isDead = false;
            hasEscaped = false;
            deathTimer = 0;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (isDead)
            {
                deathTimer += gameTime.ElapsedGameTime.TotalSeconds;

                if (deathTimer >= 2)
                {
                    LoadLevel(currentLevelIndex);
                }

                return;
            }

            if (hasEscaped)
            {
                currentLevelIndex++;
                if (currentLevelIndex >= levels.Count)
                    currentLevelIndex = 0;

                LoadLevel(currentLevelIndex);
                return;
            }

            player.Update(gameTime);

            foreach (var ghost in ghosts)
            {
                ghost.Update(gameTime);

                if (ghost.CollidesWith(player))
                {
                    player.TakeDamage(1);

                    if (player.Health <= 0 && !isDead)
                    {
                        isDead = true;
                        deathTimer = 0;
                        break;
                    }
                }
            }

            foreach (var potion in potions)
            {
                if (!potion.IsCollected && potion.CollidesWith(player))
                {
                    potion.Collect();
                    player.CollectPotion();
                }
            }

            gate.TryOpen(player);

            if (gate.IsOpen && gate.CollidesWith(player))
            {
                hasEscaped = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(currentBackground, Vector2.Zero, Color.White);

            foreach (var ghost in ghosts)
                ghost.Draw(_spriteBatch);

            foreach (var potion in potions)
                if (!potion.IsCollected)
                    potion.Draw(_spriteBatch);

            gate.Draw(_spriteBatch);
            player.Draw(_spriteBatch);

            _spriteBatch.DrawString(font, $"Level: {currentLevelIndex + 1}", new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(font, $"Health: {player.Health}", new Vector2(10, 30), Color.White);
            _spriteBatch.DrawString(font, $"Potions: {player.PotionsCollected}", new Vector2(10, 50), Color.White);

            if (isDead)
                _spriteBatch.DrawString(font, "YOU DIED! Restarting level...", new Vector2(220, 200), Color.Red);
            else if (hasEscaped)
                _spriteBatch.DrawString(font, "YOU ESCAPED THE LEVEL!", new Vector2(220, 170), Color.LimeGreen);
            else if (gate.IsOpen)
                _spriteBatch.DrawString(font, "The gate is open! Escape now!", new Vector2(10, 70), Color.Yellow);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}