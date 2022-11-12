using System.Collections.Generic;
using BetterRarityBorders.Rendering;
using BetterRarityBorders.Rendering.Borders;
using BetterRarityBorders.Rendering.Particles;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace BetterRarityBorders;

[UsedImplicitly]
public sealed class BetterRarityBordersMod : Mod
{
    public List<IRenderingSystem> Renderers { get; } = new()
    {
        new BorderRenderingSystem(),
        new ParticleRenderingSystem()
    };

    public Dictionary<int, Color> BorderColorOverrides { get; } = new();

    public Color GetBorderColor(Item item) {
        return BorderColorOverrides.TryGetValue(item.type, out var color) ? color : ItemRarity.GetColor(item.rare);
    }

    public override object? Call(params object[] args) {
        if (args.Length == 0 || args[0] is not string key) return null;

        switch (key.ToLower()) {
            case "setbordercolor":
                if (args.Length < 3 || args[1] is not int type || args[2] is not Color color) return null;
                BorderColorOverrides[type] = color;
                return true;
        }

        return null;
    }
}