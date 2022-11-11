using Microsoft.Xna.Framework.Graphics;

namespace BetterRarityBorders.Rendering.Particles;

public class ParticleRenderingSystem : IRenderingSystem
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