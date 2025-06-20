using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HauntedForestEscape
{
    public class Gate : GameObject
    {
        public bool IsOpen { get; private set; }
        private int potionsRequired;
        private Texture2D closedTexture;
        private Texture2D openTexture;

        public Gate(Vector2 position, Texture2D closedTex, Texture2D openTex, int potionsRequired) : base(position, closedTex)
        {
            this.closedTexture = closedTex;
            this.openTexture = openTex;
            this.potionsRequired = potionsRequired;
            IsOpen = false;
        }

        public void TryOpen(Player player)
        {
            if (!IsOpen && player.PotionsCollected >= potionsRequired)
            {
                IsOpen = true;
                Texture = openTexture;
            }
        }
    }
}