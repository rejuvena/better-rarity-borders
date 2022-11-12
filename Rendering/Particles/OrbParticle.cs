using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Renderers;
using Terraria.ModLoader;

namespace BetterRarityBorders.Rendering.Particles;

public class OrbParticle : Particle
{
    public int timer = 0;

    protected override Lazy<Asset<Texture2D>> Texture { get; } = new(() => ModContent.Request<Texture2D>("BetterRarityBorders/Textures/Glow"));

    protected override bool DrawAdditively => true;

    protected float Sin = 0;

    protected float FadeTime = 0;

    public OrbParticle(Vector2 position, float sin, float fadeTime, float z = 0, Vector2? velocity = null, float scale = 1, float rotation = 0, Color? color = null, float alpha = 1) :
        base(position, new Rectangle(18 * Main.rand.Next(2), 0, 18, 18), z, velocity, scale, rotation, color, alpha)
    { 
        Sin = sin; 
        FadeTime = fadeTime;
        timer = 200;
    }

    public override void Update()
    {
        Sin += 0.05f;

        Velocity.Y = -0.2f;
        Velocity.X = 0.7f * (float)Math.Cos(Sin);

        if (timer > 180)
            Alpha += 0.05f;
        else if (timer < FadeTime)
            Alpha -= 0.025f;

        Alpha = MathHelper.Clamp(Alpha, 0, 1);
        Position += Velocity;
        timer--;

        if (timer <= 0)
            IsAlive = false;
        base.Update();
    }
}