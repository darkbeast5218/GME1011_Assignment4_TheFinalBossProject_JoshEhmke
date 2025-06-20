using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HauntedForestEscape
{
    public class Player : GameObject
    {
        public int Health { get; private set; }
        public int PotionsCollected { get; set; }

        private float speed = 150f;

        public Player(Vector2 position, Texture2D texture) : base(position, texture)
        {
            Health = 100;
            PotionsCollected = 0;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0)
                Health = 0;
        }

        public void ResetHealth()
        {
            Health = 100;
        }

        public void CollectPotion()
        {
            PotionsCollected++;
        }

        public override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();
            Vector2 movement = Vector2.Zero;

            if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up))
                movement.Y -= 1;
            if (kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down))
                movement.Y += 1;
            if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left))
                movement.X -= 1;
            if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
                movement.X += 1;

            if (movement != Vector2.Zero)
            {
                movement.Normalize();
                Position += movement * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Keep player inside screen (800x480)
                Position = new Vector2(
                    MathHelper.Clamp(Position.X, 0, 800 - Texture.Width),
                    MathHelper.Clamp(Position.Y, 0, 480 - Texture.Height));
            }
        }
    }
}