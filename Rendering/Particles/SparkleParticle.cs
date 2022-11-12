using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace BetterRarityBorders.Rendering.Particles;

public class SparkleParticle : Particle
{
    public int timer = 0;

    protected override Lazy<Asset<Texture2D>> Texture { get; } = new(() => ModContent.Request<Texture2D>("BetterRarityBorders/Textures/Sparkle"));

    public SparkleParticle(Vector2 position, float z = 0, Vector2? velocity = null, float scale = 1, float rotation = 0, Color? color = null, float alpha = 1) :
        base(position, new Rectangle(18 * Main.rand.Next(2), 0, 18, 18), z, velocity, scale, rotation, color, alpha) { }

    public override void Update(bool inItemSlot, ItemDrawData itemDrawData)
    {
        Velocity = Vector2.Zero;
        timer++;

        if (timer % 14 == 13)
            Frame.Y += 18;

        if (Frame.Y >= 90 || (Frame.X > 0 && Frame.Y >= 72))
            IsAlive = false;
    }
}