using Microsoft.Xna.Framework.Graphics;

namespace BetterRarityBorders.Rendering.Borders;

public class BorderRenderingSystem : IRenderingSystem
{
    void IRenderingSystem.Update() {
        throw new System.NotImplementedException();
    }

    void IRenderingSystem.DrawBefore(SpriteBatch sb, bool inItemSlot, ItemDrawData itemDrawData) {
        throw new System.NotImplementedException();
    }

    void IRenderingSystem.DrawAfter(SpriteBatch sb, bool inItemSlot, ItemDrawData itemDrawData) {
        throw new System.NotImplementedException();
    }
}