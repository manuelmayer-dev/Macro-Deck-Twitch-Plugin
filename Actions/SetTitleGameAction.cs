﻿using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.TwitchPlugin.Language;
using SuchByte.TwitchPlugin.Models;
using SuchByte.TwitchPlugin.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuchByte.TwitchPlugin.Actions
{
    public class SetTitleGameAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSetStreamTitleGame;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetStreamTitleGameDescription;

        public override bool CanConfigure => true;

        public override async void Trigger(string clientId, ActionButton actionButton)
        {
            var configModel = SetTitleGameActionConfigModel.Deserialize(Configuration);
            if (configModel == null)
            {
                return;
            }
            
            foreach (var variable in MacroDeck.Variables.VariableManager.Variables)
            {
                if (configModel.StreamTitle.ToLower().Contains("{" + variable.Name.ToLower() + "}"))
                {
                    configModel.StreamTitle = configModel.StreamTitle.Replace("{" + variable.Name + "}", variable.Value,
                        StringComparison.OrdinalIgnoreCase);
                }
                if (configModel.Game.ToLower().Contains("{" + variable.Name.ToLower() + "}"))
                {
                    configModel.Game = configModel.Game.Replace("{" + variable.Name + "}", variable.Value,
                        StringComparison.OrdinalIgnoreCase);
                }
            }

            await TwitchHelper.SetTitleGame(configModel.UseStreamTitle ? configModel.StreamTitle : null,
                configModel.UseGame ? configModel.Game : null);
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetTitleGameActionConfigView(this);
        }
    }
}
