using BetterRarityBorders.Rendering;
using BetterRarityBorders.Rendering.Borders;
using BetterRarityBorders.Rendering.Particles;
using JetBrains.Annotations;
using Terraria.ModLoader;

namespace BetterRarityBorders;

[UsedImplicitly]
public sealed class BetterRarityBordersMod : Mod
{
    public IRenderingSystem BorderRenderer { get; } = new BorderRenderingSystem();

    public IRenderingSystem ParticleRenderer { get; } = new ParticleRenderingSystem();
}