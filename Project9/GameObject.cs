using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HauntedForestEscape
{
    public abstract class GameObject
    {
        public Vector2 Position;
        public Texture2D Texture;

        public GameObject(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public bool CollidesWith(GameObject other)
        {
            Rectangle rect1 = new Rectangle(Position.ToPoint(), new Point(Texture.Width, Texture.Height));
            Rectangle rect2 = new Rectangle(other.Position.ToPoint(), new Point(other.Texture.Width, other.Texture.Height));
            return rect1.Intersects(rect2);
        }
    }
}