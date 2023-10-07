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
    public partial class SetSubscriberChatActionConfigView : ActionConfigControl
    {


        private readonly SetSubscriberChatActionConfigViewModel _viewModel;
        public SetSubscriberChatActionConfigView(PluginAction action)
        {
            InitializeComponent();
            radioOn.Text = PluginLanguageManager.PluginStrings.On;
            radioOff.Text = PluginLanguageManager.PluginStrings.Off;
            radioToggle.Text = PluginLanguageManager.PluginStrings.Toggle;

            _viewModel = new SetSubscriberChatActionConfigViewModel(action);
        }

        private void SetSubscriberChatActionConfigView_Load(object sender, EventArgs e)
        {
            switch (_viewModel.Method)
            {
                case Models.SetSubscriberChatActionMethod.On:
                    radioOn.Checked = true;
                    break;
                case Models.SetSubscriberChatActionMethod.Off:
                    radioOff.Checked = true;
                    break;
                case Models.SetSubscriberChatActionMethod.Toggle:
                    radioToggle.Checked = true;
                    break;
            }
        }

        public override bool OnActionSave()
        {
            if (radioOn.Checked)
            {
                _viewModel.Method = Models.SetSubscriberChatActionMethod.On;
            }
            else if (radioOff.Checked)
            {
                _viewModel.Method = Models.SetSubscriberChatActionMethod.Off;
            }
            else if (radioToggle.Checked)
            {
                _viewModel.Method = Models.SetSubscriberChatActionMethod.Toggle;
            }

            return _viewModel.SaveConfig();
        }
    }
}
