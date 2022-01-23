using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuchByte.TwitchPlugin.Actions
{
    public class ClearChatAction : PluginAction
    {
        public override string Name => "Clear chat";

        public override string Description => "Clears the chat";

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            TwitchHelper.ClearChat();
        }
    }
}
