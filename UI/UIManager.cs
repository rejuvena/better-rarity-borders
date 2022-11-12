using System.Collections.Generic;
using BetterRarityBorders.UI.States;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace BetterRarityBorders.UI;

[Autoload(false)]
[UsedImplicitly]
public sealed class UIManager : ModSystem
{
    public UserInterface? ConfigUserInterface { get; private set; }

    public UserInterface? ConfigTogglerUserInterface { get; private set; }

    public ConfigUI? Config { get; private set; }

    public ConfigButton? ConfigButton { get; private set; }

    private GameTime? LastUpdateUIGameTime;

    public override void OnModLoad() {
        base.OnModLoad();

        if (Main.dedServ) return;

        ConfigUserInterface = new UserInterface();
        ConfigTogglerUserInterface = new UserInterface();
        Config = new ConfigUI();
        Config.Activate();
        ConfigButton = new ConfigButton();
        ConfigButton.Activate();

        ConfigTogglerUserInterface.SetState(ConfigButton);
    }

    public override void UpdateUI(GameTime gameTime) {
        base.UpdateUI(gameTime);

        LastUpdateUIGameTime = gameTime;

        if (!Main.playerInventory) CloseConfig();

        ConfigUserInterface?.Update(gameTime);
        ConfigTogglerUserInterface?.Update(gameTime);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
        base.ModifyInterfaceLayers(layers);

        int i = layers.FindIndex((layer) => layer.Name == "Vanilla: Inventory");
        if (i != -1) {
            layers.Insert(
                i - 1,
                new LegacyGameInterfaceLayer(
                    "BRB: Config",
                    delegate
                    {
                        if (LastUpdateUIGameTime != null && ConfigUserInterface?.CurrentState != null)
                            ConfigUserInterface.Draw(Main.spriteBatch, LastUpdateUIGameTime);
                        return true;
                    },
                    InterfaceScaleType.UI
                )
            );
        }

        i = layers.FindIndex((layer) => layer.Name == "Vanilla: Mouse Text");
        if (i != -1) {
            layers.Insert(
                i,
                new LegacyGameInterfaceLayer(
                    "BRB: Config Toggler",
                    delegate
                    {
                        if (LastUpdateUIGameTime != null && ConfigTogglerUserInterface?.CurrentState != null)
                            ConfigTogglerUserInterface.Draw(Main.spriteBatch, LastUpdateUIGameTime);

                        return true;
                    },
                    InterfaceScaleType.UI
                )
            );
        }
    }

    public bool IsConfigClosed() => ConfigUserInterface?.CurrentState == null;

    public void CloseConfig() => ConfigUserInterface?.SetState(null);

    public void OpenConfig() => ConfigUserInterface?.SetState(Config);

    public void ToggleConfig() {
        if (IsConfigClosed()) {
            SoundEngine.PlaySound(SoundID.MenuOpen);
            OpenConfig();
        }
        else {
            SoundEngine.PlaySound(SoundID.MenuClose);
            CloseConfig();
        }
    }
}