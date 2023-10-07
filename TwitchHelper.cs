using Newtonsoft.Json;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using SuchByte.TwitchPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Chat.ChatSettings;
using TwitchLib.Api.Services;
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

        private static TwitchAPI _api;
        
        public static bool IsConnected => _client is not null && _client.IsConnected && _channel is not null;
        
        public static event EventHandler LoginSuccessful;

        public static event EventHandler LoginFailed;

        public static event EventHandler ConnectionStateChanged;

        private static string _userId = "";
        private static string _username = "";

        private static System.Timers.Timer _updateTimer;

        public static bool SlowChat { get; set; }

        public static bool FollowersOnlyChat { get; set; }

        public static bool SubscribersOnlyChat { get; set; }

        public static bool EmotesOnlyChat { get; set; }


        public static async Task Connect(TwitchAccount account)
        {
            await ConfigTwitchApi(account.TwitchAccessToken);
            await ConfigTwitchChat(account.TwitchAccessToken);
            if (_updateTimer is not null)
            {
                _updateTimer.Enabled = false;
                _updateTimer.Dispose();
            }
            _updateTimer = new System.Timers.Timer
            {
                Interval = 1000*45,
                Enabled = true
            };
            _updateTimer.Elapsed += UpdateTimer_Elapsed;
        }

        private static async Task ConfigTwitchChat(string accessToken)
        {
            try
            {
                var prefix = string.IsNullOrEmpty(PluginConfiguration.GetValue(PluginInstance.Main, "commandPrefix"))
                    ? '!'
                    : PluginConfiguration.GetValue(PluginInstance.Main, "commandPrefix")[0];
                
                var users = await _api.Helix.Users.GetUsersAsync();
                if (users?.Users?.FirstOrDefault() == null)
                {
                    return;
                }
                
                _username = users.Users.FirstOrDefault()?.Login;
                MacroDeckLogger.Info(PluginInstance.Main, $"Using login: {_username}");

                var credentials = new ConnectionCredentials(_username, accessToken);

                if (_client is not null && _client.IsConnected)
                {
                    _client.Disconnect();
                }
                _client = new TwitchClient();
                _client.Initialize(credentials, _username, prefix, prefix);

                _client.OnLog += Client_OnLog;
                _client.OnError += Client_OnError;
                _client.OnJoinedChannel += Client_OnJoinedChannel;
                _client.OnConnected += Client_OnConnected;
                _client.OnChannelStateChanged += Client_OnChannelStateChanged;
                _client.OnNewSubscriber += Client_OnNewSubscriber;
                _client.OnChatCommandReceived += Client_OnChatCommandReceived;

                _client.OnUserJoined += Client_OnUserJoined;
                _client.OnUserLeft += Client_OnUserLeft;

                _client.OnIncorrectLogin += Client_OnIncorrectLogin;
                _client.OnConnectionError += Client_OnConnectionError;
                _client.OnDisconnected += Client_OnDisconnected;

                _client.Connect();
                MacroDeckLogger.Info(PluginInstance.Main, "Connecting Twitch client...");
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, "Failed to connect Twitch client: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private static void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            if (!e.Command.ChatMessage.IsModerator && !e.Command.ChatMessage.IsBroadcaster)
            {
                return;
            }
            
            MacroDeckLogger.Trace(PluginInstance.Main, "Command: [" + e.Command.CommandText + "] " + e.Command.ChatMessage.Username + " (Mod)");
            PluginInstance.Main.CommandIssued(e.Command.CommandText, e);
        }

        private static async void UpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_api == null)
            {
                return;
            }
            
            await UpdateViewerCountAsync();
            await UpdateFollowersAsync();
        }

        private static async Task ConfigTwitchApi(string accessToken)
        {
            _api = new TwitchAPI
            {
                Settings =
                {
                    ClientId = "m656oj5wocmg54tmjtkydhobl93ej4",
                    AccessToken = accessToken
                }
            };

            await GetUserIdAsync();
            await UpdateViewerCountAsync();
            await UpdateFollowersAsync();
            await UpdateSubscribersAsync();
        }


        private static async Task SetTitleGameAsync(string title, string game)
        {
            try
            {
                var modifyChannelInformationRequest = new TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation.ModifyChannelInformationRequest();
                if (!string.IsNullOrEmpty(game))
                {
                    var games = await _api.Helix.Games.GetGamesAsync(gameNames: new List<string> { game });
                    if (games?.Games?.FirstOrDefault() is not null)
                    {
                        modifyChannelInformationRequest.GameId = games.Games.FirstOrDefault()?.Id;
                    }
                }
                if (string.IsNullOrEmpty(title)) modifyChannelInformationRequest.Title = title;

                await _api.Helix.Channels.ModifyChannelInformationAsync(_userId, modifyChannelInformationRequest);
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Error while setting title and game: {ex.Message + Environment.NewLine + ex.StackTrace}");
            }
        }

        private static async Task UpdateFollowersAsync()
        {
            MacroDeckLogger.Trace(PluginInstance.Main, "Updating followers count");
            if (string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_userId)) return;
            try
            {
                var followers = await _api.Helix.Users.GetUsersFollowsAsync(toId: _userId);
                if (followers == null) return;
                var followersCount = followers.TotalFollows;
                VariableManager.SetValue(_username + "_followers", followersCount, VariableType.Integer, PluginInstance.Main, null);
                MacroDeckLogger.Trace(PluginInstance.Main, $"Followers count: {followersCount}");
            }
            catch { }
        }

        private static async Task UpdateSubscribersAsync()
        {
            MacroDeckLogger.Trace(PluginInstance.Main, "Updating subscribers count");
            if (string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_userId)) return;
            try
            {
                var subscribers = await _api.Helix.Subscriptions.GetBroadcasterSubscriptionsAsync(_userId);
                if (subscribers == null) return;
                var subscribersCount = subscribers.Total;
                VariableManager.SetValue(_username + "_subscribers", subscribersCount, VariableType.Integer, PluginInstance.Main, null);
                MacroDeckLogger.Trace(PluginInstance.Main, $"Subscribers count: {subscribersCount}");
            }
            catch { }
        }

        private static async Task GetUserIdAsync()
        {
            try
            {
                var users = await _api.Helix.Users.GetUsersAsync();
                _userId = users?.Users?.FirstOrDefault()?.Id;
            } catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Error while getting user id: {ex.Message + Environment.NewLine + ex.StackTrace}");
            }
        }

        private static async Task UpdateChannelSettingsAsync()
        {
            if (string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_userId)) return;
            try
            {
                var channelSettings = await _api.Helix.Chat.GetChatSettingsAsync(_userId, _userId);
                if (channelSettings?.Data?.FirstOrDefault() == null)
                {
                    return;
                }
                
                SlowChat = channelSettings?.Data?.FirstOrDefault()?.SlowMode is true;
                FollowersOnlyChat = channelSettings?.Data?.FirstOrDefault()?.FollowerMode is true;
                SubscribersOnlyChat = channelSettings?.Data?.FirstOrDefault()?.SubscriberMode is true;
                EmotesOnlyChat = channelSettings?.Data?.FirstOrDefault()?.EmoteMode is true;

                MacroDeckLogger.Trace(PluginInstance.Main, $"Slow chat: {SlowChat}");
                MacroDeckLogger.Trace(PluginInstance.Main, $"Followers only: {FollowersOnlyChat}");
                MacroDeckLogger.Trace(PluginInstance.Main, $"Subs only: {SubscribersOnlyChat}");
                MacroDeckLogger.Trace(PluginInstance.Main, $"Emotes only: {EmotesOnlyChat}");

                VariableManager.SetValue(_username + "_slow_chat", SlowChat, VariableType.Bool, PluginInstance.Main, null);
                VariableManager.SetValue(_username + "_followers_only_chat", FollowersOnlyChat, VariableType.Bool, PluginInstance.Main, null);
                VariableManager.SetValue(_username + "_subs_only_chat", SubscribersOnlyChat, VariableType.Bool, PluginInstance.Main, null);
                VariableManager.SetValue(_username + "_emotes_only_chat", EmotesOnlyChat, VariableType.Bool, PluginInstance.Main, null);
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Error while updating channel settings: {ex.Message + Environment.NewLine + ex.StackTrace}");
            }
        }

        private static async Task UpdateViewerCountAsync()
        {
            MacroDeckLogger.Trace(PluginInstance.Main, "Updating viewers count");
            if (string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_userId)) return;
            try
            {
                var users = await _api.Helix.Users.GetUsersAsync();
                if (users == null) return;
                var stream = await _api.Helix.Streams.GetStreamsAsync(userIds: new List<string> { _userId });
                var viewersCount = stream?.Streams?.FirstOrDefault()?.ViewerCount ?? 0;
                VariableManager.SetValue(_username + "_viewers", viewersCount, VariableType.Integer, PluginInstance.Main, null);
                MacroDeckLogger.Trace(PluginInstance.Main, $"Viewer count: {viewersCount}");
            } catch { }
        }



        private static async void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            await UpdateSubscribersAsync();
        }

        private static void Client_OnError(object sender, TwitchLib.Communication.Events.OnErrorEventArgs e)
        {
            MacroDeckLogger.Error(PluginInstance.Main, $"Error in the protocol: { e.Exception.Message + Environment.NewLine + e.Exception.StackTrace }");
        }

        private static void Client_OnDisconnected(object sender, TwitchLib.Communication.Events.OnDisconnectedEventArgs e)
        {
            ConnectionStateChanged?.Invoke(null, EventArgs.Empty);
        }

        private static void Client_OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            LoginFailed?.Invoke(null, EventArgs.Empty);
            ConnectionStateChanged?.Invoke(null, EventArgs.Empty);
        }

        private static void Client_OnIncorrectLogin(object sender, OnIncorrectLoginArgs e)
        {
            LoginFailed?.Invoke(null, EventArgs.Empty);
        }
        
        private static void Client_OnUserLeft(object sender, OnUserLeftArgs e)
        {
            MacroDeckLogger.Trace(PluginInstance.Main, "User left");
        }

        private static void Client_OnUserJoined(object sender, OnUserJoinedArgs e)
        {
            MacroDeckLogger.Trace(PluginInstance.Main, "User joined");
        }

        private static async void Client_OnChannelStateChanged(object sender, OnChannelStateChangedArgs e)
        {
            MacroDeckLogger.Trace(PluginInstance.Main, "Channel state changed");
            await UpdateChannelSettingsAsync();
        }

        private static void Client_OnLog(object sender, OnLogArgs e)
        {
            MacroDeckLogger.Trace(PluginInstance.Main, e.Data);
        }

        private static void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            LoginSuccessful?.Invoke(null, EventArgs.Empty);
            MacroDeckLogger.Info(PluginInstance.Main, $"Client connected to Twitch");
        }

        private static void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            _channel = e.Channel;
            MacroDeckLogger.Info(PluginInstance.Main, $"Joined channel { e.Channel }");
            ConnectionStateChanged?.Invoke(null, EventArgs.Empty);
        }

        public static void ClearChat()
        {
            if (_client == null || !_client.IsConnected || _channel == null)
            {
                return;
            }
            
            TwitchLib.Client.Extensions.ClearChatExt.ClearChat(_client, _channel);
            MacroDeckLogger.Info(PluginInstance.Main, "Cleared chat");
        }

        public static void SendChatMessage(string message)
        {
            if (_client == null || !_client.IsConnected || _channel == null)
            {
                return;
            }
            
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
            if (_client == null || !_client.IsConnected || _channel == null)
            {
                return;
            }
            
            TwitchLib.Client.Extensions.CommercialExt.StartCommercial(_client, _channel, length);
            MacroDeckLogger.Info(PluginInstance.Main, $"Play commercial: { length.ToString() }");
        }

        public static void SetEmoteChat(bool state)
        {
            if (_client == null || !_client.IsConnected || _channel == null)
            {
                return;
            }
            
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
            if (_client == null || !_client.IsConnected || _channel == null)
            {
                return;
            }
            
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
            if (_client == null || !_client.IsConnected || _channel == null)
            {
                return;
            }
            
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


        public static async Task SetTitleGame(string title, string game)
        {
            if (_api == null) return;
            await SetTitleGameAsync(title, game);
            
            switch (string.IsNullOrEmpty(title))
            {
                case false when !string.IsNullOrEmpty(game):
                    MacroDeckLogger.Info(PluginInstance.Main, $"Set title to {title} and game to {game}");
                    break;
                case false:
                    MacroDeckLogger.Info(PluginInstance.Main, $"Set title to {title}");
                    break;
                default:
                {
                    MacroDeckLogger.Info(PluginInstance.Main,
                        !string.IsNullOrEmpty(game) ? $"Set game to {game}" : $"Set nothing");
                    break;
                }
            }
        }

        public static void Marker()
        {
            if (_client == null || !_client.IsConnected || _channel == null)
            {
                return;
            }
            
            TwitchLib.Client.Extensions.MarkerExt.Marker(_client, _channel);
            MacroDeckLogger.Info(PluginInstance.Main, $"Placed stream marker");
        }

        public static async Task MakeClip()
        {
            if (_api == null || _userId == null)
            {
                return;
            }
            
            await _api.Helix.Clips.CreateClipAsync(_userId, CredentialsHelper.GetTwitchAccount().TwitchAccessToken);
            MacroDeckLogger.Info(PluginInstance.Main, $"Clip created");
        }

        public static void Disconnect()
        {
            if (!IsConnected) return;
            _client.Disconnect();
        }
    }
}
