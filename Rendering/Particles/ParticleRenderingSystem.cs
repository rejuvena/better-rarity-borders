﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace BetterRarityBorders.Rendering.Particles;

public sealed class ParticleRenderingSystem : IRenderingSystem
{
    private readonly List<Particle> Particles = new();

    public void AddParticle(Particle particle) {
        Particles.Add(particle);
    }

    void IRenderingSystem.Update() {
        Particles.ForEach(x => x.Update());
        Particles.RemoveAll(x => !x.IsAlive);
    }

    void IRenderingSystem.DrawBefore(SpriteBatch sb, SlotDrawData slotDrawData, ItemDrawData itemDrawData) {
        Particles.ForEach(x => x.DrawBefore(sb, slotDrawData, itemDrawData));
    }

    void IRenderingSystem.DrawAfter(SpriteBatch sb, SlotDrawData slotDrawData, ItemDrawData itemDrawData) {
        Particles.ForEach(x => x.DrawAfter(sb, slotDrawData, itemDrawData));
    }
    
    public void SpawnParticles(SlotDrawData slotDrawData, ItemDrawData itemDrawData) {
        float sin = Main.rand.NextFloat(6.28f);
        if (itemDrawData.Item.rare == ItemRarityID.Blue && Main.rand.NextBool(90))
            Particles.Add(new SparkleParticle(itemDrawData.ItemPosition + Main.rand.NextVector2Circular(20, 20), z: 0.75f, scale: Main.rand.NextFloat(0.5f, 0.75f), velocity: Vector2.Zero, color: Color.SkyBlue));
    }
}