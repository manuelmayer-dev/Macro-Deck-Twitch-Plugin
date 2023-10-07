using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Events;
using SuchByte.MacroDeck.Folders;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Profiles;
using SuchByte.MacroDeck.Variables;
using SuchByte.TwitchPlugin.Actions;
using SuchByte.TwitchPlugin.Language;
using SuchByte.TwitchPlugin.Models;
using SuchByte.TwitchPlugin.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.TwitchPlugin
{
    public static class PluginInstance {
        public static Main Main { get; set; }
    }
    
    public class Main : MacroDeckPlugin
    {
        private ContentSelectorButton _statusButton = new();

        private readonly ToolTip _statusToolTip = new();

        private MainWindow _mainWindow;

        private readonly ChatCommandEvent _chatCommandEvent = new();

        public Main()
        {
            PluginInstance.Main ??= this;
        }

        public override bool CanConfigure => true;

        public override  void Enable()
        {
            PluginLanguageManager.Initialize();
            Actions = new List<PluginAction>()
            {
                new SetTitleGameAction(),
                new ClearChatAction(),
                new PlayAdAction(),
                new SendChatMessageAction(),
                new SetFollowerChatAction(),
                new SetSlowChatAction(),
                new SetEmoteChatAction(),
                new SetSubscriberChatAction(),
                new StreamMarkerAction(),
                new MakeClipAction()
            };
            EventManager.RegisterEvent(_chatCommandEvent);

            MacroDeck.MacroDeck.OnMainWindowLoad += MacroDeck_OnMainWindowLoad;
            TwitchHelper.ConnectionStateChanged += TwitchHelper_ConnectionStateChanged;

            if (MacroDeck.MacroDeck.MainWindow != null && !MacroDeck.MacroDeck.MainWindow.IsDisposed)
            {
                MacroDeck_OnMainWindowLoad(MacroDeck.MacroDeck.MainWindow, EventArgs.Empty);
            }

            Task.Run(async () => await Connect());
        }

        private static async Task Connect()
        {
            var twitchAccount = CredentialsHelper.GetTwitchAccount();
            if (twitchAccount != null)
            {
                await TwitchHelper.Connect(twitchAccount);
            }
        }

        private void TwitchHelper_ConnectionStateChanged(object sender, EventArgs e)
        {
            UpdateStatusIcon();
        }

        private void MacroDeck_OnMainWindowLoad(object sender, EventArgs e)
        {
            _mainWindow = sender as MainWindow;

            _statusButton = new ContentSelectorButton
            {
                BackgroundImageLayout = ImageLayout.Stretch,

            };
            _statusButton.Click += StatusButton_Click;
            _mainWindow?.contentButtonPanel.Controls.Add(_statusButton);
            UpdateStatusIcon();
        }

        private void UpdateStatusIcon()
        {

            if (_mainWindow != null && !_mainWindow.IsDisposed && _statusButton != null && !_statusButton.IsDisposed)
            {
                _mainWindow.Invoke(() =>
                {
                    _statusButton.BackgroundImage = TwitchHelper.IsConnected ? Properties.Resources.Twitch_Connected : Properties.Resources.Twitch_Disconnected;
                    _statusToolTip.SetToolTip(_statusButton, "Twitch " + (TwitchHelper.IsConnected ? " Connected" : "Disconnected"));
                });
            }
        }

        private async void StatusButton_Click(object sender, EventArgs e)
        {
            if (CredentialsHelper.GetTwitchAccount() == null)
            {
                OpenConfigurator();
                return;
            }
            if (TwitchHelper.IsConnected)
            {
                TwitchHelper.Disconnect();
            } else
            {
                await Connect();
            }
        }

        public override void OpenConfigurator()
        {
            using var pluginConfig = new PluginConfigView();
            pluginConfig.ShowDialog();
        }

        public void CommandIssued(object sender, EventArgs e)
        {
            _chatCommandEvent.Trigger(sender);
        }

        private class ChatCommandEvent : IMacroDeckEvent
        {
            public string Name => PluginLanguageManager.PluginStrings.EventName;

            public EventHandler<MacroDeckEventArgs> OnEvent { get; set; }
            public List<string> ParameterSuggestions
            {
                get
                {
                    var commands = new List<string>();
                    var variable = PluginConfiguration.GetValue(PluginInstance.Main, "commandsList");
                    if (!string.IsNullOrWhiteSpace(variable))
                        commands.AddRange(variable.Split(';'));
                    return commands;
                }
                set { }
            }

            public void Trigger(object sender)
            {
                if (OnEvent == null)
                {
                    return;
                }
                
                try
                {
                    foreach (var macroDeckProfile in ProfileManager.Profiles)
                    {
                        foreach (var folder in macroDeckProfile.Folders)
                        {
                            if (folder.ActionButtons == null)
                            {
                                continue;
                            }
                            
                            foreach (var actionButton in folder.ActionButtons.FindAll(actionButton => actionButton.EventListeners != null && actionButton.EventListeners.Find(x => x.EventToListen != null && x.EventToListen.Equals(Name)) != null))
                            {
                                var macroDeckEventArgs = new MacroDeckEventArgs
                                {
                                    ActionButton = actionButton,
                                    Parameter = (string)sender,
                                };
                                OnEvent(this, macroDeckEventArgs);
                            }
                        }
                    }
                }
                catch { }
            }
        }
    }
}
