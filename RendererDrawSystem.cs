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

        foreach (var renderer in Mod.Renderers) renderer.Update(drawingInItemSlot, drawData);
        foreach (var renderer in Mod.Renderers) renderer.DrawBefore(spriteBatch, drawingInItemSlot, drawData);

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

        for (int i = Mod.Renderers.Count - 1; i >= 0; i--) Mod.Renderers[i].DrawAfter(spriteBatch, drawingInItemSlot, drawData);

        base.PostDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
    }
}