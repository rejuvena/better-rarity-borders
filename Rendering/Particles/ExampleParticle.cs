using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;

namespace BetterRarityBorders.Rendering.Particles;

public class ExampleParticle : Particle
{
    protected override Lazy<Asset<Texture2D>> Texture { get; } = new(() => TextureAssets.MagicPixel);

    public ExampleParticle(Vector2 position, float z = 0, Vector2? velocity = null, float scale = 1, float rotation = 0, Color? color = null, float alpha = 1) :
        base(position, new Rectangle(1, 1, 1, 1), z, velocity, scale, rotation, color, alpha) { }
}