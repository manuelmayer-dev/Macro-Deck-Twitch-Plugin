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
    public class SetSlowChatAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSetSlowChat;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetSlowChatDescription;

        public override bool CanConfigure => true;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            var configModel = SetSlowChatActionConfigModel.Deserialize(Configuration);
            if (configModel != null)
            {
                switch (configModel.Method)
                {
                    case SetSlowChatActionMethod.On:
                        TwitchHelper.SetSlowChat(true, TimeSpan.FromSeconds(configModel.MessageCooldown));
                        break;
                    case SetSlowChatActionMethod.Off:
                        TwitchHelper.SetSlowChat(false, TimeSpan.Zero);
                        break;
                    case SetSlowChatActionMethod.Toggle:
                        TwitchHelper.SetSlowChat(!TwitchHelper.SlowChat, TimeSpan.FromSeconds(configModel.MessageCooldown));
                        break;
                }
            }
        }
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetSlowChatActionConfigView(this);
        }
    }
}

