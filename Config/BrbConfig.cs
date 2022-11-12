﻿using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Terraria.ID;
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
    public int BorderType { get; set; } = 0;

    [Label("$Mods.BetterRarityBorders.Config.BorderRarityBlacklist.Name")]
    [Tooltip("$Mods.BetterRarityBorders.Config.BorderRarityBlacklist.Tooltip")]
    public List<string> BorderRarityBlacklist = new();
    
    [Label("$Mods.BetterRarityBorders.Config.BorderContextBlacklist.Name")]
    [Tooltip("$Mods.BetterRarityBorders.Config.BorderContextBlacklist.Tooltip")]
    public List<int> BorderContextBlacklist = new();

    [Header("$Mods.BetterRarityBorders.Config.RarityHeader")]
    [Label("$Mods.BetterRarityBorders.Config.CoinRarities.Name")]
    [Tooltip("$Mods.BetterRarityBorders.Config.CoinRarities.Tooltip")]
    [DefaultValue(true)]
    public bool CoinRarities { get; set; } = true;
    
    [Label("$Mods.BetterRarityBorders.Config.CustomRarityOverrides.Name")]
    [Tooltip("$Mods.BetterRarityBorders.Config.CustomRarityOverrides.Tooltip")]
    public List<string> CustomRarityOverrides = new();

    public override void OnChanged() {
        base.OnChanged();

        if (CoinRarities) {
            Mod.BorderColorOverrides[ItemID.CopperCoin] = () => Colors.CoinCopper;
            Mod.BorderColorOverrides[ItemID.SilverCoin] = () => Colors.CoinSilver;
            Mod.BorderColorOverrides[ItemID.GoldCoin] = () => Colors.CoinGold;
            Mod.BorderColorOverrides[ItemID.PlatinumCoin] = () => Colors.CoinPlatinum;
        }
        else {
            Mod.BorderColorOverrides.Remove(ItemID.CopperCoin); 
            Mod.BorderColorOverrides.Remove(ItemID.SilverCoin);
            Mod.BorderColorOverrides.Remove(ItemID.GoldCoin);
            Mod.BorderColorOverrides.Remove(ItemID.PlatinumCoin);
        }
    }
}