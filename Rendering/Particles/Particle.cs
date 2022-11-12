using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;

namespace BetterRarityBorders.Rendering.Particles;

public abstract class Particle
{
    protected abstract Lazy<Asset<Texture2D>> Texture { get; }

    public bool HasBeenDrawnThisFrame { get; set; } = false;

    public bool IsAlive { get; protected set; } = true;

    protected virtual bool DrawAdditively { get; } = false;

    protected Vector2 Position;
    protected float Z; // TODO: could make pos a vec3
    protected Vector2 Velocity;
    protected float Scale;
    protected float Rotation;
    protected Color Color;
    protected float Alpha;
    protected Rectangle Frame;
    
    protected Particle(Vector2 position, Rectangle frame, float z = 0f, Vector2? velocity = null, float scale = 1f, float rotation = 0f, Color? color = null, float alpha = 1f) {
        Position = position;
        Z = z;
        Velocity = velocity ?? Vector2.Zero;
        Scale = scale;
        Rotation = rotation;
        Color = color ?? Color.White;
        Alpha = alpha;
        Frame = frame;
    }

    public virtual void Update() {
        HasBeenDrawnThisFrame = false;
        Position += Velocity;
    }

    public virtual void DrawBefore(SpriteBatch sb, SlotDrawData slotDrawData, ItemDrawData itemDrawData) {
        if (Z >= 0.5f) return;
        HasBeenDrawnThisFrame = true;
        DrawParticle(sb);
    }

    public virtual void DrawAfter(SpriteBatch sb, SlotDrawData slotDrawData, ItemDrawData itemDrawData) {
        if (Z < 0.5f) return;
        HasBeenDrawnThisFrame = true;
        DrawParticle(sb);
    }

    private void DrawParticle(SpriteBatch sb) {
        if (DrawAdditively) {
            sb.End();
            sb.Begin(default, BlendState.Additive, SamplerState.PointClamp, default, default, default, Main.UIScaleMatrix);
        }

        var tex = Texture.Value.Value;
        var frame = Frame;
        sb.Draw(tex, Position, frame, Color * Alpha, Rotation, frame.Size() / 2f, Scale, 0, 0);

        if (DrawAdditively) {
            sb.End();
            sb.Begin(default, default, SamplerState.PointClamp, default, default, default, Main.UIScaleMatrix);
        }
    }
}