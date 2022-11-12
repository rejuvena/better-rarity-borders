using System;
using BetterRarityBorders.UI.Elements;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace BetterRarityBorders.UI.States;

public sealed class ConfigButton : UIState
{
    private readonly Lazy<Asset<Texture2D>> IconTexture = new(() => ModContent.Request<Texture2D>("BetterRarityBorders/Assets/ConfigButton"));
    private readonly Lazy<Asset<Texture2D>> HighlightTexture = new(() => ModContent.Request<Texture2D>("BetterRarityBorders/Assets/ConfigButton_MouseOver"));

    public override void OnActivate() {
        var icon = new UIImage(IconTexture.Value);
        icon.Left.Set(570f, 0f);
        icon.Top.Set(245f, 0f);
        Append(icon);

        var highlight = new UIHoverTextImageButton(HighlightTexture.Value, Language.GetText("Mods.BetterRarityBorders.UI.ConfigButtonName"));
        highlight.Left.Set(-2f, 0f);
        highlight.Top.Set(-2f, 0f);
        highlight.SetVisibility(1f, 0f);
        highlight.OnClick += (evt, element) =>
        {
            if (!Main.playerInventory) return;
            ModContent.GetInstance<UIManager>().ToggleConfig();
        };
        icon.Append(highlight);

        base.OnActivate();
    }

    public override void Draw(SpriteBatch spriteBatch) {
        if (Main.playerInventory) base.Draw(spriteBatch);
    }
}