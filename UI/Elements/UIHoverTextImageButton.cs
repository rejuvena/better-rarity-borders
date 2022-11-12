using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;

namespace BetterRarityBorders.UI.Elements;

public class UIHoverTextImageButton : UIImageButton
{
    private readonly LocalizedText Text;

    public UIHoverTextImageButton(Asset<Texture2D> texture, LocalizedText text) : base(texture) {
        Text = text;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch) {
        base.DrawSelf(spriteBatch);

        if (IsMouseHovering) {
            Main.LocalPlayer.mouseInterface = true;
            Main.hoverItemName = Text.Value;
        }
    }
}