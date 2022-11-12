namespace BetterRarityBorders.Rendering.Particles;

public record ParticleRender(Particle Particle, SlotDrawData? SlotDrawData = null, ItemDrawData? ItemDrawData = null)
{
    public SlotDrawData? SlotDrawData { get; set; } = SlotDrawData;

    public ItemDrawData? ItemDrawData { get; set; } = ItemDrawData;
}