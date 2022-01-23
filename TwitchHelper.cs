using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Variables;
using SuchByte.TwitchPlugin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace SuchByte.TwitchPlugin
{
    public static class TwitchHelper
    {
        private static TwitchClient _client;

        private static string _channel;
        
        public static TwitchAccount TwitchAccount;

        public static event EventHandler LoginSuccessful;

        public static event EventHandler LoginFailed;

        public static event EventHandler ConnectionStateChanged;


        public static void Connect(TwitchAccount account)
        {
            TwitchAccount = account;
            try
            {
                ConnectionCredentials credentials = new ConnectionCredentials(TwitchAccount.TwitchUserName, TwitchAccount.TwitchAccessToken);
                
                if (_client != null && _client.IsConnected)
                {
                    _client.Disconnect();
                }
                _client = new TwitchClient();
                _client.Initialize(credentials, TwitchAccount.TwitchUserName);

                _client.OnLog += Client_OnLog;
                _client.OnError += Client_OnError;
                _client.OnJoinedChannel += Client_OnJoinedChannel;
                _client.OnNewSubscriber += Client_OnNewSubscriber;
                _client.OnConnected += Client_OnConnected;
                _client.OnChannelStateChanged += Client_OnChannelStateChanged;

                _client.OnUserJoined += Client_OnUserJoined;
                _client.OnUserLeft += Client_OnUserLeft;

                _client.OnIncorrectLogin += Client_OnIncorrectLogin;
                _client.OnConnectionError += Client_OnConnectionError;
                _client.OnDisconnected += Client_OnDisconnected;

                _client.Connect();
                MacroDeckLogger.Info(PluginInstance.Main, "Connecting Twitch client...");
            } catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, "Failed to connect Twitch client: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private static void Client_OnError(object sender, TwitchLib.Communication.Events.OnErrorEventArgs e)
        {
            MacroDeckLogger.Error(PluginInstance.Main, $"Error in the protocol: { e.Exception.Message + Environment.NewLine + e.Exception.StackTrace }");
        }

        private static void Client_OnDisconnected(object sender, TwitchLib.Communication.Events.OnDisconnectedEventArgs e)
        {
            if (ConnectionStateChanged != null)
            {
                ConnectionStateChanged(null, EventArgs.Empty);
            }
        }

        private static void Client_OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            if (LoginFailed != null)
            {
                LoginFailed(null, EventArgs.Empty);
            }
            if (ConnectionStateChanged != null)
            {
                ConnectionStateChanged(null, EventArgs.Empty);
            }
        }

        private static void Client_OnIncorrectLogin(object sender, OnIncorrectLoginArgs e)
        {
            if (LoginFailed != null)
            {
                LoginFailed(null, EventArgs.Empty);
            }
        }


        private static void Client_OnUserLeft(object sender, OnUserLeftArgs e)
        {
            MacroDeckLogger.Trace(PluginInstance.Main, "User left");
        }

        private static void Client_OnUserJoined(object sender, OnUserJoinedArgs e)
        {
            MacroDeckLogger.Trace(PluginInstance.Main, "User joined");
        }

        private static void Client_OnChannelStateChanged(object sender, OnChannelStateChangedArgs e)
        {
            MacroDeckLogger.Trace(PluginInstance.Main, "Channel state changed");
        }


        // Todo
        /*
        private static void UpdateChannelStateVariables()
        {
            if (_channelState == null)
            {
                MacroDeckLogger.Warning(PluginInstance.Main, "Channel state = null");
                return;
            }

            try
            {
                VariableManager.SetValue(TwitchAccount.TwitchUserName + "_slow_chat", SlowChat, VariableType.Bool, PluginInstance.Main, true);
                VariableManager.SetValue(TwitchAccount.TwitchUserName + "_followers_only_chat", FollowersOnlyChat, VariableType.Bool, PluginInstance.Main, true);
                VariableManager.SetValue(TwitchAccount.TwitchUserName + "_subs_only_chat", SubsciberOnlyChat, VariableType.Bool, PluginInstance.Main, true);
                VariableManager.SetValue(TwitchAccount.TwitchUserName + "_emotes_only_chat", EmoteOnlyChat, VariableType.Bool, PluginInstance.Main, true);
            }
            catch { }
        }*/

        private static void Client_OnLog(object sender, OnLogArgs e)
        {
            MacroDeckLogger.Trace(PluginInstance.Main, e.Data);
        }

        private static void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            if (LoginSuccessful != null)
            {
                LoginSuccessful(null, EventArgs.Empty);
            }
            MacroDeckLogger.Info(PluginInstance.Main, $"Client connected to Twitch");
        }

        private static void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            _channel = e.Channel;
            MacroDeckLogger.Info(PluginInstance.Main, $"Joined channel { e.Channel }");
            if (ConnectionStateChanged != null)
            {
                ConnectionStateChanged(null, EventArgs.Empty);
            }
        }

        private static void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            MacroDeckLogger.Info(PluginInstance.Main, $"{ e.Subscriber.DisplayName } subscribed");
            VariableManager.SetValue(TwitchAccount.TwitchUserName + "_newest_sub", e.Subscriber.DisplayName, VariableType.String, PluginInstance.Main);
        }

        public static void ClearChat()
        {
            if (_client == null || !_client.IsConnected || _channel == null) return;
            TwitchLib.Client.Extensions.ClearChatExt.ClearChat(_client, _channel);
            MacroDeckLogger.Info(PluginInstance.Main, "Cleared chat");
        }

        public static void SendChatMessage(string message)
        {
            if (_client == null || !_client.IsConnected || _channel == null) return;
            _client.SendMessage(_channel, message);
            MacroDeckLogger.Info(PluginInstance.Main, $"Sent chat message: { message }");
        }

        public static void SetSlowChat(bool state, TimeSpan messageCooldown)
        {
            if (_client == null || !_client.IsConnected || _channel == null) return;
            switch (state)
            {
                case true:
                    TwitchLib.Client.Extensions.SlowModeExt.SlowModeOn(_client, _channel, messageCooldown);
                    break;
                case false:
                    TwitchLib.Client.Extensions.SlowModeExt.SlowModeOff(_client, _channel);
                    break;
            }
            MacroDeckLogger.Info(PluginInstance.Main, $"Set slow chat: { state }");
        }

        public static void PlayAd(CommercialLength length)
        {
            if (_client == null || !_client.IsConnected || _channel == null) return;
            TwitchLib.Client.Extensions.CommercialExt.StartCommercial(_client, _channel, length);
            MacroDeckLogger.Info(PluginInstance.Main, $"Play commercial: { length.ToString() }");
        }

        public static void EmoteChat(bool state)
        {
            if (_client == null || !_client.IsConnected || _channel == null) return;
            switch (state)
            {
                case true:
                    TwitchLib.Client.Extensions.EmoteOnlyExt.EmoteOnlyOn(_client, _channel);
                    break;
                case false:
                    TwitchLib.Client.Extensions.EmoteOnlyExt.EmoteOnlyOff(_client, _channel);
                    break;
            }

            MacroDeckLogger.Info(PluginInstance.Main, $"Set emote chat: { state }");
        }

        public static void FollowerChat(bool state, TimeSpan requiredFollowTime)
        {
            if (_client == null || !_client.IsConnected || _channel == null) return;
            switch (state)
            {
                case true:
                    TwitchLib.Client.Extensions.FollowersOnlyExt.FollowersOnlyOn(_client, _channel, requiredFollowTime);
                    break;
                case false:
                    TwitchLib.Client.Extensions.FollowersOnlyExt.FollowersOnlyOff(_client, _channel);
                    break;
            }

            MacroDeckLogger.Info(PluginInstance.Main, $"Set follower chat: { state }, required follow time: { requiredFollowTime.TotalDays } days");
        }

        public static void SetSubscriberChat(bool state)
        {
            if (_client == null || !_client.IsConnected || _channel == null) return;
            switch (state)
            {
                case true:
                    TwitchLib.Client.Extensions.SubscribersOnly.SubscribersOnlyOn(_client, _channel);
                    break;
                case false:
                    TwitchLib.Client.Extensions.SubscribersOnly.SubscribersOnlyOn(_client, _channel);
                    break;
            }
            MacroDeckLogger.Info(PluginInstance.Main, $"Set subscriber chat: { state }");
        }

        public static void Marker()
        {
            if (_client == null || !_client.IsConnected || _channel == null) return;
            TwitchLib.Client.Extensions.MarkerExt.Marker(_client, _channel);
            MacroDeckLogger.Info(PluginInstance.Main, $"Placed stream marker");
        }

        public static void Disconnect()
        {
            if (!IsConnected) return;
            _client.Disconnect();
        }

        public static bool IsConnected
        {
            get => _client != null && _client.IsConnected && _channel != null;
        }


        //Todo

        /*
        public static bool SlowChat
        {
            get => _channelState != null && _channelState.SlowMode != null && _channelState.SlowMode > 0;
        }

        public static bool FollowersOnlyChat
        {
            get => _channelState != null && _channelState.FollowersOnly != null;
        }

        public static bool SubsciberOnlyChat
        {
            get =>  _channelState != null && _channelState.SubOnly != null && (bool)_channelState.SubOnly;
        }

        public static bool EmoteOnlyChat
        {
            get => _channelState != null && _channelState.EmoteOnly != null && (bool)_channelState.EmoteOnly;
        }*/
    }
}
