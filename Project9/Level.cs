using HauntedForestEscape;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Level
{
    public List<Ghost> Ghosts { get; }
    public List<Potion> Potions { get; }
    public Gate Gate { get; }
    public Texture2D BackgroundTexture { get; }

    public Level(List<Ghost> ghosts, List<Potion> potions, Gate gate, Texture2D backgroundTexture)
    {
        Ghosts = ghosts;
        Potions = potions;
        Gate = gate;
        BackgroundTexture = backgroundTexture;
    }
}