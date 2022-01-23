using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuchByte.TwitchPlugin.Actions
{
    public class StreamMarkerAction : PluginAction
    {
        public override string Name => "Stream marker";

        public override string Description => "Places a stream marker\r\nOnly works while you're live";

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            TwitchHelper.Marker();
        }
    }
}
