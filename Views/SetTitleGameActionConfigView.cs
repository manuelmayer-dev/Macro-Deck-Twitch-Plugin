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
    public partial class SetTitleGameActionConfigView : ActionConfigControl
    {
        private readonly SetTitleGameActionConfigViewModel _viewModel;

        public SetTitleGameActionConfigView(PluginAction action)
        {
            InitializeComponent();

            this._viewModel = new SetTitleGameActionConfigViewModel(action);
        }

        private void SetTitleGameActionConfigView_Load(object sender, EventArgs e)
        {
            this.streamTitle.Text = this._viewModel.StreamTitle;
            this.game.Text = this._viewModel.Game;
        }

        public override bool OnActionSave()
        {
            if (string.IsNullOrWhiteSpace(this.streamTitle.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.game.Text))
            {
                return false;
            }
            this._viewModel.StreamTitle = this.streamTitle.Text;
            this._viewModel.Game = this.game.Text;
            return this._viewModel.SaveConfig();
        }

    }
}
