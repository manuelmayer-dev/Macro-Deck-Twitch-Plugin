using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Plugins;
using SuchByte.TwitchPlugin.Language;
using SuchByte.TwitchPlugin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SuchByte.TwitchPlugin.Views
{
    public partial class PluginConfigView : DialogForm
    {
        public PluginConfigView()
        {
            InitializeComponent();
            lblAuthToken.Text = PluginLanguageManager.PluginStrings.AuthToken;
            lblCommandPrefix.Text = PluginLanguageManager.PluginStrings.CommandPrefix;
            lblCommandsList.Text = PluginLanguageManager.PluginStrings.CommandsList;
            btnGetToken.Text = PluginLanguageManager.PluginStrings.GetToken;
            btnOk.Text = LanguageManager.Strings.Ok;
        }

        private void PluginConfigView_Load(object sender, EventArgs e)
        {
            TwitchHelper.LoginFailed += TwitchHelper_LoginFailed;
            TwitchHelper.LoginSuccessful += TwitchHelper_LoginSuccessful;

            commandPrefix.Text = string.IsNullOrEmpty(PluginConfiguration.GetValue(PluginInstance.Main, "commandPrefix")) ? "!" : PluginConfiguration.GetValue(PluginInstance.Main, "commandPrefix");
            commandsList.Text = PluginConfiguration.GetValue(PluginInstance.Main, "commandsList");

            var twitchAccount = CredentialsHelper.GetTwitchAccount();
            if (twitchAccount != null)
            {
                oAuthToken.Text = twitchAccount.TwitchAccessToken;
            }
        }

        private void TwitchHelper_LoginSuccessful(object sender, EventArgs e)
        {
            CredentialsHelper.UpdateCredentials(new TwitchAccount { TwitchAccessToken = oAuthToken.Text });
            Invoke(new Action(() =>
            {

                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Info, PluginLanguageManager.PluginStrings.LoginSuccessful, MessageBoxButtons.OK);
                }
                Close();
            }));
        }

        private void TwitchHelper_LoginFailed(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.LoginFailed, MessageBoxButtons.OK);
                }
                Close();
            }));
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(oAuthToken.Text))
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.TokenCannotBeEmpty, MessageBoxButtons.OK);
                }
                return;
            }
            if (string.IsNullOrEmpty(commandPrefix.Text))
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.PrefixCannotBeEmpty, MessageBoxButtons.OK);
                }
                return;
            }
            else
            {
                PluginConfiguration.SetValue(PluginInstance.Main, "commandPrefix", commandPrefix.Text);
            }
            PluginConfiguration.SetValue(PluginInstance.Main, "commandsList", commandsList.Text);

            TwitchHelper.Connect(new TwitchAccount() { TwitchAccessToken = oAuthToken.Text });
        }

        private void BtnGetToken_Click(object sender, EventArgs e)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo("https://macrodeck.org/twitchauth.php")
                {
                    UseShellExecute = true,
                }
            };
            p.Start();
        }
    }
}
