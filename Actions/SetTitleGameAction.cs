using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.TwitchPlugin.Models;
using SuchByte.TwitchPlugin.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuchByte.TwitchPlugin.Actions
{
    public class SetTitleGameAction : PluginAction
    {
        public override string Name => "Set title and game";

        public override string Description => "Sets the stream title and the game";

        public override bool CanConfigure => true;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            var configModel = SetTitleGameActionConfigModel.Deserialize(this.Configuration);
            if (configModel != null)
            {
                TwitchHelper.SetTitleGame(configModel.StreamTitle, configModel.Game);
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetTitleGameActionConfigView(this);
        }
    }
}
