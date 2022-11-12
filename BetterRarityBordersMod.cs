using System.Collections.Generic;
using BetterRarityBorders.Rendering;
using BetterRarityBorders.Rendering.Borders;
using BetterRarityBorders.Rendering.Particles;
using JetBrains.Annotations;
using Terraria.ModLoader;

namespace BetterRarityBorders;

[UsedImplicitly]
public sealed class BetterRarityBordersMod : Mod
{
    public readonly List<IRenderingSystem> Renderers = new()
    {
        new BorderRenderingSystem(),
        new ParticleRenderingSystem()
    };
}