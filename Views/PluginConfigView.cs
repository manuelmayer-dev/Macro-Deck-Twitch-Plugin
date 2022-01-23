using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
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
        }

        private void PluginConfigView_Load(object sender, EventArgs e)
        {
            TwitchHelper.LoginFailed += TwitchHelper_LoginFailed;
            TwitchHelper.LoginSuccessful += TwitchHelper_LoginSuccessful;

            TwitchAccount twitchAccount = CredentialsHelper.GetTwitchAccount();
            if (twitchAccount != null)
            {
                this.username.Text = twitchAccount.TwitchUserName;
                this.oAuthToken.Text = twitchAccount.TwitchAccessToken;
            }
        }

        private void TwitchHelper_LoginSuccessful(object sender, EventArgs e)
        {
            CredentialsHelper.UpdateCredentials(TwitchHelper.TwitchAccount);
            this.Invoke(new Action(() =>
            {

                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Info, "Login successful.", MessageBoxButtons.OK);
                }
                this.Close();
            }));
        }

        private void TwitchHelper_LoginFailed(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Error, "Login failed. Please make sure the user name and the OAuth token is correct.", MessageBoxButtons.OK);
                }
                this.Close();
            }));
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.username.Text))
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Error, "User name cannot be empty", MessageBoxButtons.OK);
                }
                return;
            }

            if (string.IsNullOrWhiteSpace(this.oAuthToken.Text))
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Error, "OAuth token cannot be empty", MessageBoxButtons.OK);
                }
                return;
            }

            TwitchHelper.Connect(new Models.TwitchAccount() { TwitchUserName = this.username.Text, TwitchAccessToken = this.oAuthToken.Text });
        }

        private void BtnGetToken_Click(object sender, EventArgs e)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo("https://twitchapps.com/tmi/")
                {
                    UseShellExecute = true,
                }
            };
            p.Start();
        }
    }
}
