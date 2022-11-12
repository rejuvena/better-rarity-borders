using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace BetterRarityBorders.Config;

[Label("$Mods.BetterRarityBorders.Config.Title")] [UsedImplicitly]
public sealed class BrbConfig : ModConfig
{
    private new BetterRarityBordersMod Mod => (BetterRarityBordersMod) base.Mod;

    public override ConfigScope Mode => ConfigScope.ClientSide;

    [Header("$Mods.BetterRarityBorders.Config.PanelHeader")]
    [Label("$Mods.BetterRarityBorders.Config.BorderType.Name")]
    [Tooltip("$Mods.BetterRarityBorders.Config.BorderType.Tooltip")]
    [DefaultValue(0)]
    [Range(0, 19)]
    [Slider]
    [UsedImplicitly]
    public int BorderType { get; set; } = 0;

    [Label("$Mods.BetterRarityBorders.Config.BorderRarityBlacklist.Name")]
    [Tooltip("$Mods.BetterRarityBorders.Config.BorderRarityBlacklist.Tooltip")]
    [UsedImplicitly]
    public List<string> BorderRarityBlacklist { get; set; } = new();

    [Label("$Mods.BetterRarityBorders.Config.BorderContextBlacklist.Name")]
    [Tooltip("$Mods.BetterRarityBorders.Config.BorderContextBlacklist.Tooltip")]
    [UsedImplicitly]
    public List<int> BorderContextBlacklist { get; set; } = new();

    [Header("$Mods.BetterRarityBorders.Config.RarityHeader")]
    [Label("$Mods.BetterRarityBorders.Config.CoinRarities.Name")]
    [Tooltip("$Mods.BetterRarityBorders.Config.CoinRarities.Tooltip")]
    [DefaultValue(true)]
    [UsedImplicitly]
    public bool CoinRarities { get; set; } = true;

    [Label("$Mods.BetterRarityBorders.Config.CustomRarityOverrides.Name")]
    [Tooltip("$Mods.BetterRarityBorders.Config.CustomRarityOverrides.Tooltip")]
    [UsedImplicitly]
    public List<string> CustomRarityOverrides { get; set; } = new();

    public override void OnChanged() {
        base.OnChanged();

        Mod.UpdateColorOverrides();

        if (CoinRarities) {
            Mod.ItemBorderOverrides[ItemID.CopperCoin] = () => Colors.CoinCopper;
            Mod.ItemBorderOverrides[ItemID.SilverCoin] = () => Colors.CoinSilver;
            Mod.ItemBorderOverrides[ItemID.GoldCoin] = () => Colors.CoinGold;
            Mod.ItemBorderOverrides[ItemID.PlatinumCoin] = () => Colors.CoinPlatinum;
        }

        foreach (string cro in CustomRarityOverrides) {
            static (bool useRarity, int rarityType, int itemType) ExtractTypesFromContent(string content) {
                if (ModContent.TryFind<ModItem>(content, out var item)) return (false, int.MinValue, item.Type);
                if (!content.Contains(';')) return (false, int.MinValue, int.MinValue);
                string[] split = content.Split(';', 2);
                if (split[0] == "Terraria" && ItemID.Search.ContainsName(split[1])) return (true, int.MinValue, ItemID.Search.GetId(split[1]));
                return (true, BetterRarityBordersMod.GetRarityType(content), int.MinValue);
            }

            if (!cro.Contains(';')) continue;
            string[] split = cro.Split(';', 2);
            string content = split[0];
            string color = split[1];
            (bool useRarity, int rarityType, int itemType) = ExtractTypesFromContent(content);
            if ((useRarity && rarityType == int.MinValue) || (!useRarity && itemType == int.MinValue)) continue;

            if (color.StartsWith('#'))
                color = content[1..];
            else if (color.StartsWith("0x")) color = content[2..];

            color = color.ToUpper();
            if (color.Any(x => x is < '0' or > 'F' or > '9' and < 'A')) continue;

            // TODO: support for alpha?
            byte[] bytes;
            switch (color.Length) {
                case 3:
                    bytes = new[]
                    {
                        Convert.ToByte(color[0].ToString(), 16),
                        Convert.ToByte(color[1].ToString(), 16),
                        Convert.ToByte(color[2].ToString(), 16),
                    };
                    break;
                case 6:
                    bytes = new[]
                    {
                        Convert.ToByte(color[0..2], 16),
                        Convert.ToByte(color[2..4], 16),
                        Convert.ToByte(color[4..6], 16),
                    };
                    break;
                default:
                    continue;
            }

            if (useRarity)
                Mod.RarityBorderOverrides[rarityType] = () => new Color(bytes[0], bytes[1], bytes[2]);
            else
                Mod.ItemBorderOverrides[itemType] = () => new Color(bytes[0], bytes[1], bytes[2]);
        }
    }
}