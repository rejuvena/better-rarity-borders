using System;
using BetterRarityBorders.Rendering;
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
        bool drawingInItemSlot = ModContent.GetInstance<RendererUpdaterSystem>().IsDrawingInItemSlot;
        var drawData = new ItemDrawData(item, position, frame, drawColor, itemColor, origin, scale);

        Mod.BorderRenderer.DrawBefore(spriteBatch, drawingInItemSlot, drawData);
        Mod.ParticleRenderer.DrawBefore(spriteBatch, drawingInItemSlot, drawData);

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
        bool drawingInItemSlot = ModContent.GetInstance<RendererUpdaterSystem>().IsDrawingInItemSlot;
        var drawData = new ItemDrawData(item, position, frame, drawColor, itemColor, origin, scale);
        
        Mod.ParticleRenderer.DrawAfter(spriteBatch, drawingInItemSlot, drawData);
        Mod.BorderRenderer.DrawAfter(spriteBatch, drawingInItemSlot, drawData);

        base.PostDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
    }
}