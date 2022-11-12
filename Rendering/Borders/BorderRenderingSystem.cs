using BetterRarityBorders.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace BetterRarityBorders.Rendering.Borders;

public sealed class BorderRenderingSystem : IRenderingSystem
{
    void IRenderingSystem.Update() { }

    void IRenderingSystem.DrawBefore(SpriteBatch sb, SlotDrawData slotDrawData, ItemDrawData itemDrawData) {
        if (!slotDrawData.InItemSlot) return;
        if (slotDrawData.Context is 14 or 21 or 30) return;
        var mod = ModContent.GetInstance<BetterRarityBordersMod>();
        var config = ModContent.GetInstance<BrbConfig>();
        var texture = ModContent.Request<Texture2D>($"BetterRarityBorders/Assets/Border{config.BorderType}").Value;
        sb.Draw(
            texture,
            slotDrawData.ItemSlotPosition,
            null,
            mod.GetBorderColor(itemDrawData.Item),
            0f,
            Vector2.Zero,
            Main.inventoryScale,
            SpriteEffects.None,
            0f
        );
    }

    void IRenderingSystem.DrawAfter(SpriteBatch sb, SlotDrawData slotDrawData, ItemDrawData itemDrawData) { }
}