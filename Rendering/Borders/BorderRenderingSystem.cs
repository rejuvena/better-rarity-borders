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
        var texture = ModContent.Request<Texture2D>("BetterRarityBorders/Assets/Default").Value;
        sb.Draw(
            texture,
            slotDrawData.ItemSlotPosition,
            null,
            ItemRarity.GetColor(itemDrawData.Item.rare),
            0f,
            Vector2.Zero, 
            Main.inventoryScale,
            SpriteEffects.None,
            0f
        );
    }

    void IRenderingSystem.DrawAfter(SpriteBatch sb, SlotDrawData slotDrawData, ItemDrawData itemDrawData) { }
}