using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HauntedForestEscape
{
    public class Ghost : GameObject
    {
        private float speed = 1.5f;
        private Vector2 direction = new Vector2(1, 0); // basic left/right

        public Ghost(Vector2 position, Texture2D texture)
            : base(position, texture)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Position += direction * speed;

            // Bounce back and forth
            if (Position.X <= 0 || Position.X >= 760) // Assuming 800px wide screen
                direction *= -1;
        }
    }
}