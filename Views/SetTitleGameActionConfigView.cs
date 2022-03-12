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
    public partial class SetTitleGameActionConfigView : ActionConfigControl
    {
        private readonly SetTitleGameActionConfigViewModel _viewModel;

        public SetTitleGameActionConfigView(PluginAction action)
        {
            InitializeComponent();
            this.cbxStreamTitle.Text = PluginLanguageManager.PluginStrings.StreamTitle;
            this.cbxGame.Text = PluginLanguageManager.PluginStrings.Game;
            this._viewModel = new SetTitleGameActionConfigViewModel(action);
        }

        private void SetTitleGameActionConfigView_Load(object sender, EventArgs e)
        {
            this.streamTitle.Text = this._viewModel.StreamTitle;
            this.cbxStreamTitle.Checked = this._viewModel.UseStreamTitle;
            this.game.Text = this._viewModel.Game;
            this.cbxGame.Checked = this._viewModel.UseGame;
        }

        public override bool OnActionSave()
        {
            if (this.cbxStreamTitle.Checked && string.IsNullOrWhiteSpace(this.streamTitle.Text))
            {
                return false;
            }
            if (this.cbxGame.Checked && string.IsNullOrWhiteSpace(this.game.Text))
            {
                return false;
            }
            if (!this.cbxStreamTitle.Checked && !cbxGame.Checked)
            {
                return false;
            }

            this._viewModel.StreamTitle = this.streamTitle.Text;
            this._viewModel.UseStreamTitle = this.cbxStreamTitle.Checked;
            this._viewModel.Game = this.game.Text;
            this._viewModel.UseGame = this.cbxGame.Checked;
            return this._viewModel.SaveConfig();
        }

        private void BtnAddVariableTitle_Click(object sender, EventArgs e)
        {
            this.variablesContextMenu.Items.Clear();
            foreach (MacroDeck.Variables.Variable variable in MacroDeck.Variables.VariableManager.Variables)
            {
                ToolStripMenuItem item = new ToolStripMenuItem
                {
                    ForeColor = Color.White,
                    Text = variable.Name,
                };
                item.Click += AddVariableContextMenuItemTitleClick;
                this.variablesContextMenu.Items.Add(item);
            }
            this.variablesContextMenu.Show(PointToScreen(new Point(((ButtonPrimary)sender).Bounds.Left, ((ButtonPrimary)sender).Bounds.Bottom)));
        }

        private void btnAddVariableGame_Click(object sender, EventArgs e)
        {
            this.variablesContextMenu.Items.Clear();
            foreach (MacroDeck.Variables.Variable variable in MacroDeck.Variables.VariableManager.Variables)
            {
                ToolStripMenuItem item = new ToolStripMenuItem
                {
                    ForeColor = Color.White,
                    Text = variable.Name,
                };
                item.Click += AddVariableContextMenuItemGameClick;
                this.variablesContextMenu.Items.Add(item);
            }
            this.variablesContextMenu.Show(PointToScreen(new Point(((ButtonPrimary)sender).Bounds.Left, ((ButtonPrimary)sender).Bounds.Bottom)));
        }

        private void AddVariableContextMenuItemTitleClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            var selectionIndex = this.streamTitle.SelectionStart;
            this.streamTitle.Text = this.streamTitle.Text.Insert(selectionIndex, "{" + item.Text + "}");
            this.streamTitle.SelectionStart = selectionIndex + ("{" + item.Text + "}").Length;
        }
        private void AddVariableContextMenuItemGameClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            var selectionIndex = this.game.SelectionStart;
            this.game.Text = this.game.Text.Insert(selectionIndex, "{" + item.Text + "}");
            this.game.SelectionStart = selectionIndex + ("{" + item.Text + "}").Length;
        }

        private void CbxStreamTitle_CheckedChanged(object sender, EventArgs e)
        {
            streamTitle.Enabled = cbxStreamTitle.Checked;
        }

        private void CbxGame_CheckedChanged(object sender, EventArgs e)
        {
            game.Enabled = cbxGame.Checked;
        }
    }
}
