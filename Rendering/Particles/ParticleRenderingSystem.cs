using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace BetterRarityBorders.Rendering.Particles;

public sealed class ParticleRenderingSystem : IRenderingSystem, IPerItemUpdateable, IDrawingFinalizable
{
    private readonly List<ParticleRender> Particles = new();

    public void AddParticle(Particle particle, SlotDrawData? slotDrawData = null, ItemDrawData? itemDrawData = null) {
        Particles.Add(new ParticleRender(particle, slotDrawData, itemDrawData));
    }

    void IRenderingSystem.Update() {
        Particles.ForEach(x => x.Particle.Update());
        Particles.RemoveAll(x => !x.Particle.IsAlive);
    }

    void IRenderingSystem.DrawBefore(SpriteBatch sb, SlotDrawData slotDrawData, ItemDrawData itemDrawData) {
        Particles.ForEach(x =>
        {
            x.SlotDrawData = slotDrawData;
            x.ItemDrawData = itemDrawData;
            x.Particle.DrawBefore(sb, slotDrawData, itemDrawData);
        });
    }

    void IRenderingSystem.DrawAfter(SpriteBatch sb, SlotDrawData slotDrawData, ItemDrawData itemDrawData) {
        Particles.ForEach(x =>
        {
            x.SlotDrawData = slotDrawData;
            x.ItemDrawData = itemDrawData;
            x.Particle.DrawAfter(sb, slotDrawData, itemDrawData);
        });
    }
    
    void IPerItemUpdateable.UpdatePerItem(SlotDrawData slotDrawData, ItemDrawData itemDrawData) {
        if (itemDrawData.Item.rare == ItemRarityID.Blue && Main.rand.NextBool(90))
            AddParticle(new SparkleParticle(itemDrawData.ItemPosition + Main.rand.NextVector2Circular(20, 20), z: 0.75f, scale: Main.rand.NextFloat(0.5f, 0.75f), velocity: Vector2.Zero, color: Color.SkyBlue));

        if (itemDrawData.Item.rare == ItemRarityID.Green && Main.rand.NextBool(90))
            SpawnOrb(itemDrawData.ItemPosition, Color.Green, slotDrawData.Dimensions, 0.1f);
    }
    
    void IDrawingFinalizable.FinalizeDrawing(SpriteBatch sb) {
        Particles.ForEach(x =>
        {
            if (x.Particle.HasBeenDrawnThisFrame || !x.SlotDrawData.HasValue || !x.ItemDrawData.HasValue) return;
            x.Particle.DrawBefore(sb, x.SlotDrawData.Value, x.ItemDrawData.Value); 
            x.Particle.DrawAfter(sb, x.SlotDrawData.Value, x.ItemDrawData.Value);
        });
    }

    private void SpawnOrb(Vector2 position, Color color, Rectangle frame, float scale)
    {
        color.A = 0;
        float sin = Main.rand.NextFloat(6.28f);
        Vector2 pos = new Vector2(frame.X + (frame.Width * (0.5f * (1 + (float)Math.Sin(sin)))), frame.Y + (frame.Height * Main.rand.NextFloat(0.8f, 1f)));
        int fadeTime = Main.rand.Next(100, 130);
        AddParticle(new OrbParticle(pos, sin, fadeTime, 0.75f, Vector2.Zero, scale, 0, color, 1));
    }
}