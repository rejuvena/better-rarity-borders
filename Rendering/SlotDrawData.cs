using Microsoft.Xna.Framework;
using Terraria;

namespace BetterRarityBorders.Rendering;

public readonly record struct SlotDrawData(bool InItemSlot, Vector2 ItemSlotPosition)
{
    public Rectangle Dimensions => new((int) ItemSlotPosition.X, (int) ItemSlotPosition.Y, (int) (40 * Main.inventoryScale), (int) (40 * Main.inventoryScale));
}