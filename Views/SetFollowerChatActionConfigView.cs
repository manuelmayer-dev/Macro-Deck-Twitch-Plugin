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
    public partial class SetFollowerChatActionConfigView : ActionConfigControl
    {
        private readonly SetFollowerChatActionConfigViewModel _viewModel;

        public SetFollowerChatActionConfigView(PluginAction action)
        {
            InitializeComponent();
            radioOn.Text = PluginLanguageManager.PluginStrings.On;
            radioOff.Text = PluginLanguageManager.PluginStrings.Off;
            radioToggle.Text = PluginLanguageManager.PluginStrings.Toggle;
            lblRequiredFollowTime.Text = PluginLanguageManager.PluginStrings.RequiredFollowTime;
            _viewModel = new SetFollowerChatActionConfigViewModel(action);
        }

        private void SetStateView_Load(object sender, EventArgs e)
        {
            switch (_viewModel.Method)
            {
                case Models.SetFollowerChatActionMethod.On:
                    radioOn.Checked = true;
                    break;
                case Models.SetFollowerChatActionMethod.Off:
                    radioOff.Checked = true;
                    break;
                case Models.SetFollowerChatActionMethod.Toggle:
                    radioToggle.Checked = true;
                    break;
            }

            unit.Items.Add(PluginLanguageManager.PluginStrings.Minutes);
            unit.Items.Add(PluginLanguageManager.PluginStrings.Hours);
            unit.Items.Add(PluginLanguageManager.PluginStrings.Days);

            if (_viewModel.RequiredFollowTime.TotalHours < 1)
            {
                requiredFollowTime.Value = (int)_viewModel.RequiredFollowTime.TotalMinutes;
                unit.Text = PluginLanguageManager.PluginStrings.Minutes;
            }
            else if (_viewModel.RequiredFollowTime.TotalHours < 24)
            {
                requiredFollowTime.Value = (int)_viewModel.RequiredFollowTime.TotalHours;
                unit.Text = PluginLanguageManager.PluginStrings.Hours;
            } else
            {
                requiredFollowTime.Value = (int)_viewModel.RequiredFollowTime.TotalDays;
                unit.Text = PluginLanguageManager.PluginStrings.Days;
            }
        }

        public override bool OnActionSave()
        {
            if (radioOn.Checked)
            {
                _viewModel.Method = Models.SetFollowerChatActionMethod.On;
            } else if (radioOff.Checked)
            {
                _viewModel.Method = Models.SetFollowerChatActionMethod.Off;
            } else if (radioToggle.Checked)
            {
                _viewModel.Method = Models.SetFollowerChatActionMethod.Toggle;
            }

            if (unit.Text.Equals(PluginLanguageManager.PluginStrings.Minutes))
            {
                _viewModel.RequiredFollowTime = TimeSpan.FromMinutes((double)requiredFollowTime.Value);
            }
            else if (unit.Text.Equals(PluginLanguageManager.PluginStrings.Hours))
            {
                _viewModel.RequiredFollowTime = TimeSpan.FromHours((double)requiredFollowTime.Value);
            }
            else if (unit.Text.Equals(PluginLanguageManager.PluginStrings.Days))
            {
                _viewModel.RequiredFollowTime = TimeSpan.FromDays((double)requiredFollowTime.Value);
            }

            return _viewModel.SaveConfig();
        }
    }
}
