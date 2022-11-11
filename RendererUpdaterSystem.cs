﻿using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace BetterRarityBorders;

[UsedImplicitly]
public sealed class RendererUpdaterSystem : ModSystem
{
    public bool IsDrawingInItemSlot { get; private set; }

    public override void Load() {
        base.Load();

        On.Terraria.Main.DrawInterface_27_Inventory += RenderBordersAndParticles;
        On.Terraria.UI.ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += DetermineItemSlowDrawing;
    }

    private static void DetermineItemSlowDrawing(On.Terraria.UI.ItemSlot.orig_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color orig, SpriteBatch spritebatch, Item[] inv, int context, int slot, Vector2 position, Color lightColor) {
        var system = ModContent.GetInstance<RendererUpdaterSystem>();

        system.IsDrawingInItemSlot = true;
        orig(spritebatch, inv, context, slot, position, lightColor);
        system.IsDrawingInItemSlot = false;
    }

    private static void RenderBordersAndParticles(On.Terraria.Main.orig_DrawInterface_27_Inventory orig, Terraria.Main self) {
        var mod = ModContent.GetInstance<BetterRarityBordersMod>();

        foreach (var renderer in mod.Renderers) renderer.Update();

        orig(self);
    }
}