using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
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
    public partial class SendChatMessageActionConfigView : ActionConfigControl
    {

        private readonly SendChatMessageActionConfigViewModel _viewModel;

        public SendChatMessageActionConfigView(PluginAction action)
        {
            InitializeComponent();

            this._viewModel = new SendChatMessageActionConfigViewModel(action);
        }

        private void SendChatMessageActionConfigView_Load(object sender, EventArgs e)
        {
            this.message.Text = this._viewModel.Message;
        }

        public override bool OnActionSave()
        {
            this._viewModel.Message = this.message.Text;
            return this._viewModel.SaveConfig();
        }

    }
}
