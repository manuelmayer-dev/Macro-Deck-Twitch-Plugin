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
using TwitchLib.Api.Core.Enums;
using TwitchLib.Client.Enums;

namespace SuchByte.TwitchPlugin.Views
{
    public partial class PlayAdActionConfigView : ActionConfigControl
    {

        private readonly PlayAdActionConfigViewModel _viewModel;

        public PlayAdActionConfigView(PluginAction action)
        {
            InitializeComponent();
            lblLength.Text = PluginLanguageManager.PluginStrings.GeneralLength;

            _viewModel = new PlayAdActionConfigViewModel(action);
        }

        private void PlayAdActionConfigView_Load(object sender, EventArgs e)
        {
            foreach (var length in (CommercialLength[])Enum.GetValues(typeof(CommercialLength)))
            {
                commercialLenght.Items.Add((int)length);
            }
            commercialLenght.Text = _viewModel.Configuration.Length.ToString();
        }

        public override bool OnActionSave()
        {
            if (Int32.TryParse(commercialLenght.Text, out var length))
            {
                _viewModel.Length = length;
            }
            return _viewModel.SaveConfig();
        }
    }
}
