﻿using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.TwitchPlugin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuchByte.TwitchPlugin.ViewModels
{
    internal class SetSubscriberChatActionConfigViewModel : ISerializableConfigViewModel
    {
        private readonly PluginAction _action;

        public SetSubscriberChatActionConfigModel Configuration { get; set; }

        ISerializableConfiguration ISerializableConfigViewModel.SerializableConfiguration => Configuration;

        public SetSubscriberChatActionMethod Method
        {
            get => Configuration.Method;            
            set => Configuration.Method = value;            
        }

        public SetSubscriberChatActionConfigViewModel(PluginAction action)
        {
            Configuration = SetSubscriberChatActionConfigModel.Deserialize(action.Configuration);
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
            _action.ConfigurationSummary = Configuration.Method.ToString();
            _action.Configuration = Configuration.Serialize();
        }

    }
}
