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
    public class SendChatMessageAction : PluginAction
    {
        public override string Name => "Send chat message";

        public override string Description => "Sends a message in the chat";

        public override bool CanConfigure => true;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            var message = SendChatMessageActionConfigModel.Deserialize(this.Configuration).Message;
            TwitchHelper.SendChatMessage(message);   
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SendChatMessageActionConfigView(this);
        }
    }
}
