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
    public partial class SendChatMessageActionConfigView : ActionConfigControl
    {

        private readonly SendChatMessageActionConfigViewModel _viewModel;

        public SendChatMessageActionConfigView(PluginAction action)
        {
            InitializeComponent();
            btnAddVariable.Text = PluginLanguageManager.PluginStrings.AddVariable;
            _viewModel = new SendChatMessageActionConfigViewModel(action);
        }

        private void SendChatMessageActionConfigView_Load(object sender, EventArgs e)
        {
            message.Text = _viewModel.Message;
        }

        public override bool OnActionSave()
        {
            _viewModel.Message = message.Text;
            return _viewModel.SaveConfig();
        }

        private void BtnAddVariable_Click(object sender, EventArgs e)
        {
            variablesContextMenu.Items.Clear();
            foreach (var variable in MacroDeck.Variables.VariableManager.Variables)
            {
                var item = new ToolStripMenuItem
                {
                    ForeColor = Color.White,
                    Text = variable.Name,
                };
                item.Click += AddVariableContextMenuItemClick;
                variablesContextMenu.Items.Add(item);
            }
            variablesContextMenu.Show(PointToScreen(new Point(((ButtonPrimary)sender).Bounds.Left, ((ButtonPrimary)sender).Bounds.Bottom)));
        }

        private void AddVariableContextMenuItemClick(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            var selectionIndex = message.SelectionStart;
            message.Text = message.Text.Insert(selectionIndex, "{" + item.Text + "}");
            message.SelectionStart = selectionIndex + ("{" + item.Text + "}").Length;
        }
    }
}
