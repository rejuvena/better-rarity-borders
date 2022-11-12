using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace BetterRarityBorders.UI.Elements;

public sealed class UIDraggablePanel : UIPanel
{
    private Vector2 Offset;
    private bool Dragging;
    private readonly UIElement[] ExtraChildren;

    public UIDraggablePanel(params UIElement[] countMeAsChildren) {
        ExtraChildren = countMeAsChildren;
    }

    private void DragStart(Vector2 pos) {
        Offset = new Vector2(pos.X - Left.Pixels, pos.Y - Top.Pixels);
        Dragging = true;
    }

    private void DragEnd(Vector2 pos) {
        Dragging = false;

        Left.Set(pos.X - Offset.X, 0f);
        Top.Set(pos.Y - Offset.Y, 0f);

        Recalculate();
    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);

        if (ContainsPoint(Main.MouseScreen)) {
            Main.LocalPlayer.mouseInterface = true;
        }

        if (!Dragging && ContainsPoint(Main.MouseScreen) && Main.mouseLeft) {
            bool upperMost = true;
            if (ExtraChildren.Length > 0) {
                IEnumerable<UIElement> children = Elements.Concat(ExtraChildren);
                if (children.Any(element => element.ContainsPoint(Main.MouseScreen) && element is not UIPanel)) upperMost = false;
            }

            if (upperMost) DragStart(Main.MouseScreen);
        }
        else if (Dragging && !Main.mouseLeft) DragEnd(Main.MouseScreen);

        if (Dragging) {
            Left.Set(Main.mouseX - Offset.X, 0f);
            Top.Set(Main.mouseY - Offset.Y, 0f);
            Recalculate();
        }
        
        var parentSpace = Parent.GetDimensions().ToRectangle();
        if (GetDimensions().ToRectangle().Intersects(parentSpace)) return;
        Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
        Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
        Recalculate();
    }
}