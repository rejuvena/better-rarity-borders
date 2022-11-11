using Microsoft.Xna.Framework.Graphics;

namespace BetterRarityBorders.Rendering;

public interface IRenderingSystem
{
    void Update();

    void DrawBefore(SpriteBatch sb, bool inItemSlot, ItemDrawData itemDrawData);

    void DrawAfter(SpriteBatch sb, bool inItemSlot, ItemDrawData itemDrawData);
}