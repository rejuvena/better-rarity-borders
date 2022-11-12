using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;

namespace BetterRarityBorders.UI.Elements;

public sealed class UISearchBar : UIElement
{
    public bool IsEmpty => string.IsNullOrEmpty(Input);

    private UIPanel BackPanel;
    private string? Input;
    private bool Focused;
    private int CursorBlinkTimer;
    private bool ShowCursorBlink;

    public delegate void TextChangeDelegate(string? oldText, string currentText);

    public event TextChangeDelegate? OnTextChange;

    public UISearchBar(int width, int height) {
        Width.Set(width, 0f);
        Height.Set(height, 0f);

        BackPanel = new UIPanel();
        BackPanel.Width.Set(width, 0f);
        BackPanel.Height.Set(height, 0f);
        BackPanel.BackgroundColor = new Color(22, 25, 55);
        BackPanel.PaddingLeft = BackPanel.PaddingRight = BackPanel.PaddingTop = BackPanel.PaddingBottom = 0;
        Append(BackPanel);
    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);
        if (PlayerInput.Triggers.Current.MouseLeft) {
            if (ContainsPoint(Main.MouseScreen)) {
                Main.clrInput();
                Focused = true;
            }
            else {
                Focused = false;
            }
        }

        if (PlayerInput.Triggers.Current.MouseRight) {
            Focused = true;
            Input = "";
        }

        if (PlayerInput.Triggers.Current.Inventory) {
            Focused = false;
        }
    }

    protected override void DrawChildren(SpriteBatch spriteBatch) {
        base.DrawChildren(spriteBatch);

        PlayerInput.WritingText = Focused;
        Main.LocalPlayer.mouseInterface = Focused;
        if (Focused) {
            Main.instance.HandleIME();

            string newInput = Main.GetInputText(Input);
            if (newInput != Input) {
                OnTextChange?.Invoke(Input, newInput);
                Input = newInput;
            }
        }

        var position = GetDimensions().Position() + new Vector2(6, 4);
        string displayText = Input ?? "";

        if (string.IsNullOrEmpty(displayText) && !Focused) {
            Utils.DrawBorderString(spriteBatch, Language.GetTextValue("Mods.BetterRarityBorders.UI.SearchHintText"), position, Color.DarkGray);
        }

        if (Focused && ++CursorBlinkTimer >= 20) {
            ShowCursorBlink = !ShowCursorBlink;
            CursorBlinkTimer = 0;
        }

        if (Focused && ShowCursorBlink) {
            displayText += Language.GetTextValue("Mods.BetterRarityBorders.UI.SearchCursor");
        }

        Utils.DrawBorderString(spriteBatch, displayText, position, Color.White);
    }
}