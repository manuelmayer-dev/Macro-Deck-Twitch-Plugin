using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.TwitchPlugin.Language;
using SuchByte.TwitchPlugin.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SuchByte.TwitchPlugin.Views
{
    public partial class SetSlowChatActionConfigView : ActionConfigControl
    {

        private readonly SetSlowChatActionConfigViewModel _viewModel;


        public SetSlowChatActionConfigView(PluginAction action)
        {
            InitializeComponent();
            radioOn.Text = PluginLanguageManager.PluginStrings.On;
            radioOff.Text = PluginLanguageManager.PluginStrings.Off;
            radioToggle.Text = PluginLanguageManager.PluginStrings.Toggle;
            lblMessageCooldown.Text = PluginLanguageManager.PluginStrings.MessageCooldown;
            lblSeconds.Text = PluginLanguageManager.PluginStrings.Seconds;

            _viewModel = new SetSlowChatActionConfigViewModel(action);
        }

        private void SetSlowChatActionConfigView_Load(object sender, EventArgs e)
        {
            switch (_viewModel.Method)
            {
                case Models.SetSlowChatActionMethod.On:
                    radioOn.Checked = true;
                    break;
                case Models.SetSlowChatActionMethod.Off:
                    radioOff.Checked = true;
                    break;
                case Models.SetSlowChatActionMethod.Toggle:
                    radioToggle.Checked = true;
                    break;
            }

            cooldown.Value = (decimal)_viewModel.MessageCooldown.TotalSeconds;
        }

        public override bool OnActionSave()
        {
            if (radioOn.Checked)
            {
                _viewModel.Method = Models.SetSlowChatActionMethod.On;
            }
            else if (radioOff.Checked)
            {
                _viewModel.Method = Models.SetSlowChatActionMethod.Off;
            }
            else if (radioToggle.Checked)
            {
                _viewModel.Method = Models.SetSlowChatActionMethod.Toggle;
            }

            _viewModel.MessageCooldown = TimeSpan.FromSeconds((double)cooldown.Value);

            return _viewModel.SaveConfig();
        }
    }
}
