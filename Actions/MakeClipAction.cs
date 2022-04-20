using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Plugins;
using SuchByte.TwitchPlugin.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuchByte.TwitchPlugin.Actions
{
    public class MakeClipAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionMakeClip;

        public override string Description => PluginLanguageManager.PluginStrings.ActionMakeClipDescription;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            TwitchHelper.MakeClip();
        }
    }
}
