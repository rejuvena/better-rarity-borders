using Microsoft.Xna.Framework.Graphics;

namespace BetterRarityBorders.Rendering;

public interface IRenderingSystem
{
    void Update();

    void DrawBefore(SpriteBatch sb, SlotDrawData slotDrawData, ItemDrawData itemDrawData);

    void DrawAfter(SpriteBatch sb, SlotDrawData slotDrawData, ItemDrawData itemDrawData);
}