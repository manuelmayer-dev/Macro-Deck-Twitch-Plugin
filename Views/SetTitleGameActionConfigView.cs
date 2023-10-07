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
            cbxStreamTitle.Text = PluginLanguageManager.PluginStrings.StreamTitle;
            cbxGame.Text = PluginLanguageManager.PluginStrings.Game;
            _viewModel = new SetTitleGameActionConfigViewModel(action);
        }

        private void SetTitleGameActionConfigView_Load(object sender, EventArgs e)
        {
            streamTitle.Text = _viewModel.StreamTitle;
            cbxStreamTitle.Checked = _viewModel.UseStreamTitle;
            game.Text = _viewModel.Game;
            cbxGame.Checked = _viewModel.UseGame;
        }

        public override bool OnActionSave()
        {
            if (cbxStreamTitle.Checked && string.IsNullOrWhiteSpace(streamTitle.Text))
            {
                return false;
            }
            if (cbxGame.Checked && string.IsNullOrWhiteSpace(game.Text))
            {
                return false;
            }
            if (!cbxStreamTitle.Checked && !cbxGame.Checked)
            {
                return false;
            }

            _viewModel.StreamTitle = streamTitle.Text;
            _viewModel.UseStreamTitle = cbxStreamTitle.Checked;
            _viewModel.Game = game.Text;
            _viewModel.UseGame = cbxGame.Checked;
            return _viewModel.SaveConfig();
        }

        private void BtnAddVariableTitle_Click(object sender, EventArgs e)
        {
            variablesContextMenu.Items.Clear();
            foreach (var variable in MacroDeck.Variables.VariableManager.Variables)
            {
                var item = new ToolStripMenuItem
                {
                    ForeColor = Color.White,
                    Text = variable.Name,
                };
                item.Click += AddVariableContextMenuItemTitleClick;
                variablesContextMenu.Items.Add(item);
            }
            variablesContextMenu.Show(PointToScreen(new Point(((ButtonPrimary)sender).Bounds.Left, ((ButtonPrimary)sender).Bounds.Bottom)));
        }

        private void btnAddVariableGame_Click(object sender, EventArgs e)
        {
            variablesContextMenu.Items.Clear();
            foreach (var variable in MacroDeck.Variables.VariableManager.Variables)
            {
                var item = new ToolStripMenuItem
                {
                    ForeColor = Color.White,
                    Text = variable.Name,
                };
                item.Click += AddVariableContextMenuItemGameClick;
                variablesContextMenu.Items.Add(item);
            }
            variablesContextMenu.Show(PointToScreen(new Point(((ButtonPrimary)sender).Bounds.Left, ((ButtonPrimary)sender).Bounds.Bottom)));
        }

        private void AddVariableContextMenuItemTitleClick(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            var selectionIndex = streamTitle.SelectionStart;
            streamTitle.Text = streamTitle.Text.Insert(selectionIndex, "{" + item.Text + "}");
            streamTitle.SelectionStart = selectionIndex + ("{" + item.Text + "}").Length;
        }
        private void AddVariableContextMenuItemGameClick(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            var selectionIndex = game.SelectionStart;
            game.Text = game.Text.Insert(selectionIndex, "{" + item.Text + "}");
            game.SelectionStart = selectionIndex + ("{" + item.Text + "}").Length;
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
