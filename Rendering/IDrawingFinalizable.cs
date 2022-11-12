using Microsoft.Xna.Framework.Graphics;

namespace BetterRarityBorders.Rendering;

public interface IDrawingFinalizable
{
    void FinalizeDrawing(SpriteBatch sb);
}