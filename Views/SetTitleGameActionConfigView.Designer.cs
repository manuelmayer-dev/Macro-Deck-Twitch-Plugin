﻿
namespace SuchByte.TwitchPlugin.Views
{
    partial class SetTitleGameActionConfigView
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.streamTitle = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.game = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.btnAddVariableTitle = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.btnAddVariableGame = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.variablesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cbxStreamTitle = new System.Windows.Forms.CheckBox();
            this.cbxGame = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // streamTitle
            // 
            this.streamTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.streamTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.streamTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.streamTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.streamTitle.Icon = null;
            this.streamTitle.Location = new System.Drawing.Point(260, 184);
            this.streamTitle.MaxCharacters = 32767;
            this.streamTitle.Multiline = false;
            this.streamTitle.Name = "streamTitle";
            this.streamTitle.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.streamTitle.PasswordChar = false;
            this.streamTitle.PlaceHolderColor = System.Drawing.Color.Gray;
            this.streamTitle.PlaceHolderText = "";
            this.streamTitle.ReadOnly = false;
            this.streamTitle.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.streamTitle.SelectionStart = 0;
            this.streamTitle.Size = new System.Drawing.Size(459, 25);
            this.streamTitle.TabIndex = 3;
            this.streamTitle.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // game
            // 
            this.game.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.game.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.game.Cursor = System.Windows.Forms.Cursors.Hand;
            this.game.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.game.Icon = null;
            this.game.Location = new System.Drawing.Point(260, 215);
            this.game.MaxCharacters = 32767;
            this.game.Multiline = false;
            this.game.Name = "game";
            this.game.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.game.PasswordChar = false;
            this.game.PlaceHolderColor = System.Drawing.Color.Gray;
            this.game.PlaceHolderText = "";
            this.game.ReadOnly = false;
            this.game.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.game.SelectionStart = 0;
            this.game.Size = new System.Drawing.Size(459, 25);
            this.game.TabIndex = 5;
            this.game.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnAddVariableTitle
            // 
            this.btnAddVariableTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddVariableTitle.BorderRadius = 8;
            this.btnAddVariableTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddVariableTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddVariableTitle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddVariableTitle.ForeColor = System.Drawing.Color.White;
            this.btnAddVariableTitle.HoverColor = System.Drawing.Color.Empty;
            this.btnAddVariableTitle.Icon = global::SuchByte.TwitchPlugin.Properties.Resources.Variable_Hover;
            this.btnAddVariableTitle.Location = new System.Drawing.Point(720, 184);
            this.btnAddVariableTitle.Name = "btnAddVariableTitle";
            this.btnAddVariableTitle.Progress = 0;
            this.btnAddVariableTitle.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(225)))));
            this.btnAddVariableTitle.Size = new System.Drawing.Size(25, 25);
            this.btnAddVariableTitle.TabIndex = 6;
            this.btnAddVariableTitle.UseVisualStyleBackColor = true;
            this.btnAddVariableTitle.UseWindowsAccentColor = true;
            this.btnAddVariableTitle.Click += new System.EventHandler(this.BtnAddVariableTitle_Click);
            // 
            // btnAddVariableGame
            // 
            this.btnAddVariableGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddVariableGame.BorderRadius = 8;
            this.btnAddVariableGame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddVariableGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddVariableGame.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddVariableGame.ForeColor = System.Drawing.Color.White;
            this.btnAddVariableGame.HoverColor = System.Drawing.Color.Empty;
            this.btnAddVariableGame.Icon = global::SuchByte.TwitchPlugin.Properties.Resources.Variable_Hover;
            this.btnAddVariableGame.Location = new System.Drawing.Point(720, 216);
            this.btnAddVariableGame.Name = "btnAddVariableGame";
            this.btnAddVariableGame.Progress = 0;
            this.btnAddVariableGame.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(225)))));
            this.btnAddVariableGame.Size = new System.Drawing.Size(25, 25);
            this.btnAddVariableGame.TabIndex = 7;
            this.btnAddVariableGame.UseVisualStyleBackColor = true;
            this.btnAddVariableGame.UseWindowsAccentColor = true;
            this.btnAddVariableGame.Click += new System.EventHandler(this.btnAddVariableGame_Click);
            // 
            // variablesContextMenu
            // 
            this.variablesContextMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.variablesContextMenu.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.variablesContextMenu.Name = "variablesContextMenu";
            this.variablesContextMenu.ShowImageMargin = false;
            this.variablesContextMenu.Size = new System.Drawing.Size(156, 26);
            // 
            // cbxStreamTitle
            // 
            this.cbxStreamTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxStreamTitle.Checked = true;
            this.cbxStreamTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxStreamTitle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbxStreamTitle.Location = new System.Drawing.Point(111, 184);
            this.cbxStreamTitle.Name = "cbxStreamTitle";
            this.cbxStreamTitle.Size = new System.Drawing.Size(143, 25);
            this.cbxStreamTitle.TabIndex = 0;
            this.cbxStreamTitle.Text = "Stream title";
            this.cbxStreamTitle.UseVisualStyleBackColor = true;
            this.cbxStreamTitle.CheckedChanged += new System.EventHandler(this.CbxStreamTitle_CheckedChanged);
            // 
            // cbxGame
            // 
            this.cbxGame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxGame.Checked = true;
            this.cbxGame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxGame.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbxGame.Location = new System.Drawing.Point(111, 215);
            this.cbxGame.Name = "cbxGame";
            this.cbxGame.Size = new System.Drawing.Size(143, 25);
            this.cbxGame.TabIndex = 8;
            this.cbxGame.Text = "Game";
            this.cbxGame.UseVisualStyleBackColor = true;
            this.cbxGame.CheckedChanged += new System.EventHandler(this.CbxGame_CheckedChanged);
            // 
            // SetTitleGameActionConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbxGame);
            this.Controls.Add(this.cbxStreamTitle);
            this.Controls.Add(this.btnAddVariableGame);
            this.Controls.Add(this.btnAddVariableTitle);
            this.Controls.Add(this.game);
            this.Controls.Add(this.streamTitle);
            this.Name = "SetTitleGameActionConfigView";
            this.Load += new System.EventHandler(this.SetTitleGameActionConfigView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MacroDeck.GUI.CustomControls.RoundedTextBox streamTitle;
        private MacroDeck.GUI.CustomControls.RoundedTextBox game;
        private MacroDeck.GUI.CustomControls.ButtonPrimary btnAddVariableTitle;
        private MacroDeck.GUI.CustomControls.ButtonPrimary btnAddVariableGame;
        private System.Windows.Forms.ContextMenuStrip variablesContextMenu;
        private System.Windows.Forms.CheckBox cbxStreamTitle;
        private System.Windows.Forms.CheckBox cbxGame;
    }
}
