using Microsoft.Xna.Framework;
using Terraria;

namespace BetterRarityBorders.Rendering;

public readonly record struct ItemDrawData(
    Item Item,
    Vector2 ItemPosition,
    Rectangle ItemFrame,
    Color DrawColor,
    Color ItemColor,
    Vector2 ItemOrigin,
    float ItemScale
);