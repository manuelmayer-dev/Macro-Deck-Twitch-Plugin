using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.TwitchPlugin.Actions;
using SuchByte.TwitchPlugin.Models;
using SuchByte.TwitchPlugin.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SuchByte.TwitchPlugin
{
    public static class PluginInstance {
        public static Main Main { get; set; }
    }


    public class Main : MacroDeckPlugin
    {
        private ContentSelectorButton statusButton = new ContentSelectorButton();

        private readonly ToolTip statusToolTip = new ToolTip();

        private MainWindow mainWindow;

        public override Image Icon => Properties.Resources.Twitch_Plugin;

        public override string Description => "Control Twitch using Macro Deck 2";

        public Main()
        {
            PluginInstance.Main ??= this;
        }

        public override bool CanConfigure => true;


        public override void Enable()
        {
            this.Actions = new List<PluginAction>()
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

            };

            MacroDeck.MacroDeck.OnMainWindowLoad += MacroDeck_OnMainWindowLoad;
            TwitchHelper.ConnectionStateChanged += TwitchHelper_ConnectionStateChanged;

            if (MacroDeck.MacroDeck.MainWindow != null && !MacroDeck.MacroDeck.MainWindow.IsDisposed)
            {
                MacroDeck_OnMainWindowLoad(MacroDeck.MacroDeck.MainWindow, EventArgs.Empty);
            }

            Connect();
        }

        private void Connect()
        {
            TwitchAccount twitchAccount = CredentialsHelper.GetTwitchAccount();
            if (twitchAccount != null)
            {
                MacroDeckLogger.Trace(this, twitchAccount.TwitchUserName);
                MacroDeckLogger.Trace(this, twitchAccount.TwitchAccessToken);
                TwitchHelper.Connect(twitchAccount);
            }
        }

        private void TwitchHelper_ConnectionStateChanged(object sender, EventArgs e)
        {
            UpdateStautusIcon();
        }

        private void MacroDeck_OnMainWindowLoad(object sender, EventArgs e)
        {
            mainWindow = sender as MainWindow;

            this.statusButton = new ContentSelectorButton
            {
                BackgroundImageLayout = ImageLayout.Stretch,

            };
            statusButton.Click += StatusButton_Click;
            mainWindow.contentButtonPanel.Controls.Add(statusButton);
            UpdateStautusIcon();
        }

        private void UpdateStautusIcon()
        {

            if (this.mainWindow != null && !this.mainWindow.IsDisposed && this.statusButton != null && !this.statusButton.IsDisposed)
            {
                this.mainWindow.Invoke(new Action(() =>
                {
                    this.statusButton.BackgroundImage = TwitchHelper.IsConnected ? Properties.Resources.Twitch_Connected : Properties.Resources.Twitch_Disconnected;
                    this.statusToolTip.SetToolTip(this.statusButton, "Twitch " + (TwitchHelper.IsConnected ? " Connected" : "Disconnected"));
                }));
            }
        }

        private void StatusButton_Click(object sender, EventArgs e)
        {
            if (CredentialsHelper.GetTwitchAccount() == null)
            {
                this.OpenConfigurator();
                return;
            }
            if (TwitchHelper.IsConnected)
            {
                TwitchHelper.Disconnect();
            } else
            {
                this.Connect();
            }
        }

        public override void OpenConfigurator()
        {
            using (var pluginConfig = new PluginConfigView())
            {
                pluginConfig.ShowDialog();
            }
        }
    }
}
