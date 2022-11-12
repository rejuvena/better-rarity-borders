using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;

namespace BetterRarityBorders.Rendering.Particles;

public abstract class Particle
{
    protected abstract Lazy<Asset<Texture2D>> Texture { get; }

    public bool IsAlive { get; protected set; } = true;

    protected virtual bool DrawAdditively { get; } = false;

    protected Vector2 Position;
    protected float Z; // TODO: could make pos a vec3
    protected Vector2 Velocity;
    protected float Scale;
    protected float Rotation;
    protected Color Color;
    protected float Alpha;
    protected Rectangle? Frame;
    
    protected Particle(Vector2 position, float z = 0f, Vector2? velocity = null, float scale = 1f, float rotation = 0f, Color? color = null, float alpha = 1f, Rectangle? frame = null) {
        Position = position;
        Z = z;
        Velocity = velocity ?? Vector2.Zero;
        Scale = scale;
        Rotation = rotation;
        Color = color ?? Color.White;
        Alpha = alpha;
        Frame = frame;
    }

    public virtual void Update(bool inItemSlot, ItemDrawData itemDrawData) {
        Position += Velocity;
    }

    public virtual void DrawBefore(SpriteBatch sb, bool inItemSlot, ItemDrawData itemDrawData) {
        if (Z >= 0.5f) return;
        DrawParticle(sb);
    }

    public virtual void DrawAfter(SpriteBatch sb, bool inItemSlot, ItemDrawData itemDrawData) {
        if (Z < 0.5f) return;
        DrawParticle(sb);
    }

    private void DrawParticle(SpriteBatch sb) {
        if (DrawAdditively) {
            sb.End();
            sb.Begin(default, BlendState.Additive, SamplerState.PointClamp, default, default, default, Main.UIScaleMatrix);
        }

        var tex = Texture.Value.Value;
        var frame = Frame ?? tex.Bounds;
        sb.Draw(tex, Position, frame, Color * Alpha, Rotation, frame.Size() / 2f, Scale, 0, 0);

        if (DrawAdditively) {
            sb.End();
            sb.Begin(default, default, SamplerState.PointClamp, default, default, default, Main.UIScaleMatrix);
        }
    }
}