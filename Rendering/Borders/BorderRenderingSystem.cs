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

    void IRenderingSystem.DrawBefore(SpriteBatch sb, bool inItemSlot, ItemDrawData itemDrawData) {
        if (!inItemSlot) return;
        return;
        var texture = ModContent.Request<Texture2D>("BetterRarityBorders/Assets/Default").Value;
        sb.Draw(
            texture,
            itemDrawData.ItemPosition - itemDrawData.ItemOrigin,
            null,
            ItemRarity.GetColor(itemDrawData.Item.rare),
            0f,
            texture.Size() / 2f,
            Main.inventoryScale,
            SpriteEffects.None,
            0f
        );
    }

    void IRenderingSystem.DrawAfter(SpriteBatch sb, bool inItemSlot, ItemDrawData itemDrawData) { }
}