using System;
using System.Collections.Generic;
using BetterRarityBorders.Rendering;
using BetterRarityBorders.Rendering.Borders;
using BetterRarityBorders.Rendering.Particles;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
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

    public Dictionary<int, Func<Color>> BorderColorOverrides { get; } = new();

    public Color GetBorderColor(Item item) {
        if (BorderColorOverrides.TryGetValue(item.type, out Func<Color>? color)) return color();
        if (item.rare >= ItemRarityID.Count) return RarityLoader.GetRarity(item.rare).RarityColor;
        return ItemRarity.GetColor(item.rare);
    }

    public override object? Call(params object[] args) {
        if (args.Length == 0 || args[0] is not string key) return null;

        switch (key.ToLower()) {
            case "setbordercolor":
                if (args.Length < 3 || args[1] is not int type) return null;
                if (args[2] is Color color)
                    BorderColorOverrides[type] = () => color;
                else if (args[2] is Func<Color> colorFunc)
                    BorderColorOverrides[type] = colorFunc;
                else
                    return null;
                return true;
        }

        return null;
    }
}