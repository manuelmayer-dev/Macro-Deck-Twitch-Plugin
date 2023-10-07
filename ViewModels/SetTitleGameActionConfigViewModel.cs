using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.TwitchPlugin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuchByte.TwitchPlugin.ViewModels
{
    internal class SetTitleGameActionConfigViewModel : ISerializableConfigViewModel
    {
        private readonly PluginAction _action;

        public SetTitleGameActionConfigModel Configuration { get; set; }

        ISerializableConfiguration ISerializableConfigViewModel.SerializableConfiguration => Configuration;

        public string StreamTitle
        {
            get => Configuration.StreamTitle;
            set => Configuration.StreamTitle = value;
        }

        public bool UseStreamTitle
        {
            get => Configuration.UseStreamTitle;
            set => Configuration.UseStreamTitle = value;
        }

        public string Game
        {
            get => Configuration.Game;
            set => Configuration.Game = value;
        }

        public bool UseGame
        {
            get => Configuration.UseGame;
            set => Configuration.UseGame = value;
        }

        public SetTitleGameActionConfigViewModel(PluginAction action)
        {
            Configuration = SetTitleGameActionConfigModel.Deserialize(action.Configuration);
            _action = action;
        }

        public bool SaveConfig()
        {
            try
            {
                SetConfig();
                MacroDeckLogger.Info(PluginInstance.Main, $"{GetType().Name}: config saved");
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"{GetType().Name}: Error while saving config: { ex.Message + Environment.NewLine + ex.StackTrace }");
            }
            return true;
        }

        public void SetConfig()
        {
            if (UseStreamTitle && UseGame) _action.ConfigurationSummary = "Stream title: " + StreamTitle + " | " + "Game: " + Game;
            else if (UseStreamTitle) _action.ConfigurationSummary = "Stream title: " + StreamTitle;
            else if (UseGame) _action.ConfigurationSummary = "Game: " + Game;
            else _action.ConfigurationSummary = "Deactivated";
            _action.Configuration = Configuration.Serialize();
        }

    }
}
