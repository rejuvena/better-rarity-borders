using BetterRarityBorders.Rendering;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace BetterRarityBorders;

[UsedImplicitly]
public sealed class RendererUpdaterSystem : ModSystem
{
    public SlotDrawData SlotDrawData { get; private set; }

    public override void Load() {
        base.Load();

        On.Terraria.Main.DrawInterface_27_Inventory += UpdateAndFinalizeRenderers;
        On.Terraria.UI.ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += DetermineItemSlowDrawing;
    }

    private static void DetermineItemSlowDrawing(On.Terraria.UI.ItemSlot.orig_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color orig, SpriteBatch spritebatch, Item[] inv, int context, int slot, Vector2 position, Color lightColor) {
        var system = ModContent.GetInstance<RendererUpdaterSystem>();

        system.SlotDrawData = new SlotDrawData(true, position, context, slot);
        orig(spritebatch, inv, context, slot, position, lightColor);
        system.SlotDrawData = new SlotDrawData();
    }

    private static void UpdateAndFinalizeRenderers(On.Terraria.Main.orig_DrawInterface_27_Inventory orig, Main self) {
        var mod = ModContent.GetInstance<BetterRarityBordersMod>();

        foreach (var renderer in mod.Renderers) renderer.Update();

        orig(self);
        
        foreach (var renderer in mod.Renderers) if (renderer is IDrawingFinalizable finalizable) finalizable.FinalizeDrawing(Main.spriteBatch);
    }
}