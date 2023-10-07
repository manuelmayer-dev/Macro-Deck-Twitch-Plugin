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
    public partial class SetEmoteChatActionConfigView : ActionConfigControl
    {


        private readonly SetEmoteChatActionConfigViewModel _viewModel;

        public SetEmoteChatActionConfigView(PluginAction action)
        {
            InitializeComponent();
            radioOn.Text = PluginLanguageManager.PluginStrings.On;
            radioOff.Text = PluginLanguageManager.PluginStrings.Off;
            radioToggle.Text = PluginLanguageManager.PluginStrings.Toggle;
            _viewModel = new SetEmoteChatActionConfigViewModel(action);
        }

        private void SetEmoteChatActionConfigView_Load(object sender, EventArgs e)
        {
            switch (_viewModel.Method)
            {
                case Models.SetEmoteChatActionMethod.On:
                    radioOn.Checked = true;
                    break;
                case Models.SetEmoteChatActionMethod.Off:
                    radioOff.Checked = true;
                    break;
                case Models.SetEmoteChatActionMethod.Toggle:
                    radioToggle.Checked = true;
                    break;
            }
        }

        public override bool OnActionSave()
        {
            if (radioOn.Checked)
            {
                _viewModel.Method = Models.SetEmoteChatActionMethod.On;
            }
            else if (radioOff.Checked)
            {
                _viewModel.Method = Models.SetEmoteChatActionMethod.Off;
            }
            else if (radioToggle.Checked)
            {
                _viewModel.Method = Models.SetEmoteChatActionMethod.Toggle;
            }

            return _viewModel.SaveConfig();
        }
    }
}
