using BetterRarityBorders.Rendering;
using BetterRarityBorders.Rendering.Particles;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace BetterRarityBorders;

[UsedImplicitly]
public sealed class RendererDrawSystem : GlobalItem
{
    private new BetterRarityBordersMod Mod => (BetterRarityBordersMod) base.Mod;

    public override bool IsLoadingEnabled(Mod mod) {
        return mod is BetterRarityBordersMod && base.IsLoadingEnabled(mod);
    }

    public override bool PreDrawInInventory(
        Item item,
        SpriteBatch spriteBatch,
        Vector2 position,
        Rectangle frame,
        Color drawColor,
        Color itemColor,
        Vector2 origin,
        float scale
    ) {
        var slotDrawData = ModContent.GetInstance<RendererUpdaterSystem>().SlotDrawData;
        var drawData = new ItemDrawData(item, position, frame, drawColor, itemColor, origin, scale);

        foreach (var renderer in Mod.Renderers) if (renderer is ParticleRenderingSystem pRenderer) pRenderer.SpawnParticles(slotDrawData, drawData);
        foreach (var renderer in Mod.Renderers) renderer.DrawBefore(spriteBatch, slotDrawData, drawData);

        return base.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
    }

    public override void PostDrawInInventory(
        Item item,
        SpriteBatch spriteBatch,
        Vector2 position,
        Rectangle frame,
        Color drawColor,
        Color itemColor,
        Vector2 origin,
        float scale
    ) {
        var slotDrawData = ModContent.GetInstance<RendererUpdaterSystem>().SlotDrawData;
        var drawData = new ItemDrawData(item, position, frame, drawColor, itemColor, origin, scale);

        for (int i = Mod.Renderers.Count - 1; i >= 0; i--) Mod.Renderers[i].DrawAfter(spriteBatch, slotDrawData, drawData);

        base.PostDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
    }
}