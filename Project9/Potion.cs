using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HauntedForestEscape
{
    public class Potion : GameObject
    {
        public bool IsCollected { get; private set; }

        public Potion(Vector2 position, Texture2D texture)
            : base(position, texture)
        {
            IsCollected = false;
        }

        public void Collect()
        {
            IsCollected = true;
        }

        public override void Update(GameTime gameTime)
        {
            // Potions don't move
        }
    }
}