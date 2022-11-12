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

    [Obsolete("Use ItemBorderOverrides instead.")]
    public Dictionary<int, Func<Color>> BorderColorOverrides => ItemBorderOverrides;

    public Dictionary<int, Func<Color>> ItemBorderOverrides { get; } = new();

    public Dictionary<int, Func<Color>> RarityBorderOverrides { get; } = new();

    private readonly List<(int type, Func<Color> colorProvider)> RegisteredItemOverrides = new();
    private readonly List<(int type, Func<Color> colorProvider)> RegisteredRarityOverrides = new();

    public Color GetBorderColor(Item item) {
        if (ItemBorderOverrides.TryGetValue(item.type, out Func<Color>? color)) return color();
        if (RarityBorderOverrides.TryGetValue(item.rare, out color)) return color();
        if (item.rare >= ItemRarityID.Count) return RarityLoader.GetRarity(item.rare).RarityColor;
        return ItemRarity.GetColor(item.rare);
    }

    public static int GetRarityType(string name) {
        Dictionary<string, int> vanillaMap = new()
        {
            {"FieryRed", -13},
            {"Rainbow", -12},
            {"Amber", -11},
            {"Gray", -1},
            {"White", 0},
            {"Blue", 1},
            {"Green", 2},
            {"Orange", 3},
            {"LightRed", 4},
            {"Pink", 5},
            {"LightPurple", 6},
            {"Lime", 7},
            {"Yellow", 8},
            {"Cyan", 9},
            {"Red", 10},
            {"Purple", 11},
        };

        if (!name.Contains(':')) return int.MinValue;
        string[] split = name.Split(':', 2);
        if (split.Length != 2) return int.MinValue;

        string modName = split[0];
        string rarityName = split[1];
        
        if (modName == "Terraria")
            return vanillaMap.TryGetValue(rarityName, out int rarity) ? rarity : int.MinValue;
        if (ModLoader.TryGetMod(modName, out var mod))
            if (mod.TryFind<ModRarity>(rarityName, out var rarity))
                return rarity.Type;

        return int.MinValue;
    }

    public override object? Call(params object[] args) {
        if (args.Length == 0 || args[0] is not string key) return null;

        switch (key.ToLower()) {
            case "setbordercolor": // obsolete
            case "setitembordercolor":
                if (args.Length < 3 || args[1] is not int itemType) return null;
                if (args[2] is Color itemColor)
                    RegisteredItemOverrides.Add((itemType, () => itemColor));
                else if (args[2] is Func<Color> colorFunc)
                    RegisteredItemOverrides.Add((itemType, colorFunc));
                else
                    return null;
                UpdateColorOverrides();
                return true;
            
            case "setraritybordercolor":
                if (args.Length < 3 || args[1] is not int rarityType) return null;
                if (args[2] is Color rarityColor)
                    RegisteredRarityOverrides.Add((rarityType, () => rarityColor));
                else if (args[2] is Func<Color> colorFunc)
                    RegisteredRarityOverrides.Add((rarityType, colorFunc));
                else
                    return null;
                UpdateColorOverrides();
                return true;
        }

        return null;
    }

    public void UpdateColorOverrides() {
        ItemBorderOverrides.Clear();
        RarityBorderOverrides.Clear();
        RegisteredItemOverrides.ForEach(x => ItemBorderOverrides[x.type] = x.colorProvider);
        RegisteredRarityOverrides.ForEach(x => RarityBorderOverrides[x.type] = x.colorProvider);
    }
}