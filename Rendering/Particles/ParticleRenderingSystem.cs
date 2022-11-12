using System.Collections.Generic;
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

    void IRenderingSystem.Update(bool inItemSlot, ItemDrawData itemDrawData) {
        SpawnParticles(inItemSlot, itemDrawData);

        Particles.ForEach(x => x.Update(inItemSlot, itemDrawData));
        Particles.RemoveAll(x => !x.IsAlive);
    }

    void IRenderingSystem.DrawBefore(SpriteBatch sb, bool inItemSlot, ItemDrawData itemDrawData) {
        Particles.ForEach(x => x.DrawBefore(sb, inItemSlot, itemDrawData));
    }

    void IRenderingSystem.DrawAfter(SpriteBatch sb, bool inItemSlot, ItemDrawData itemDrawData) {
        Particles.ForEach(x => x.DrawAfter(sb, inItemSlot, itemDrawData));
    }

    private void SpawnParticles(bool inItemSlot, ItemDrawData itemDrawData) {
        if (itemDrawData.Item.rare == ItemRarityID.White && Main.rand.NextBool(30))
            Particles.Add(new ExampleParticle(itemDrawData.ItemPosition, velocity: new Vector2(Main.rand.NextFloat() * 20f, Main.rand.NextFloat() * 20f)));
    }
}