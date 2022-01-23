
using SuchByte.MacroDeck.GUI.CustomControls;

namespace SuchByte.TwitchPlugin.Views
{
    partial class PluginConfigView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            TwitchHelper.LoginFailed -= TwitchHelper_LoginFailed;
            TwitchHelper.LoginSuccessful -= TwitchHelper_LoginSuccessful;

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.username = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.oAuthToken = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.btnGetToken = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.btnOk = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblOAuthToken = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.username.Cursor = System.Windows.Forms.Cursors.Hand;
            this.username.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.username.Icon = null;
            this.username.Location = new System.Drawing.Point(178, 42);
            this.username.MaxCharacters = 32767;
            this.username.Multiline = false;
            this.username.Name = "username";
            this.username.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.username.PasswordChar = false;
            this.username.PlaceHolderColor = System.Drawing.Color.Gray;
            this.username.PlaceHolderText = "";
            this.username.ReadOnly = false;
            this.username.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.username.SelectionStart = 0;
            this.username.Size = new System.Drawing.Size(293, 25);
            this.username.TabIndex = 2;
            this.username.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // oAuthToken
            // 
            this.oAuthToken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.oAuthToken.Cursor = System.Windows.Forms.Cursors.Hand;
            this.oAuthToken.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.oAuthToken.Icon = null;
            this.oAuthToken.Location = new System.Drawing.Point(178, 73);
            this.oAuthToken.MaxCharacters = 32767;
            this.oAuthToken.Multiline = false;
            this.oAuthToken.Name = "oAuthToken";
            this.oAuthToken.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.oAuthToken.PasswordChar = true;
            this.oAuthToken.PlaceHolderColor = System.Drawing.Color.Gray;
            this.oAuthToken.PlaceHolderText = "";
            this.oAuthToken.ReadOnly = false;
            this.oAuthToken.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.oAuthToken.SelectionStart = 0;
            this.oAuthToken.Size = new System.Drawing.Size(293, 25);
            this.oAuthToken.TabIndex = 3;
            this.oAuthToken.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnGetToken
            // 
            this.btnGetToken.BorderRadius = 8;
            this.btnGetToken.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetToken.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetToken.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGetToken.ForeColor = System.Drawing.Color.White;
            this.btnGetToken.HoverColor = System.Drawing.Color.Empty;
            this.btnGetToken.Icon = null;
            this.btnGetToken.Location = new System.Drawing.Point(477, 75);
            this.btnGetToken.Name = "btnGetToken";
            this.btnGetToken.Progress = 0;
            this.btnGetToken.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(225)))));
            this.btnGetToken.Size = new System.Drawing.Size(109, 23);
            this.btnGetToken.TabIndex = 4;
            this.btnGetToken.TabStop = false;
            this.btnGetToken.Text = "Get token";
            this.btnGetToken.UseWindowsAccentColor = true;
            this.btnGetToken.Click += new System.EventHandler(this.BtnGetToken_Click);
            // 
            // btnOk
            // 
            this.btnOk.BorderRadius = 8;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.HoverColor = System.Drawing.Color.Empty;
            this.btnOk.Icon = null;
            this.btnOk.Location = new System.Drawing.Point(511, 164);
            this.btnOk.Name = "btnOk";
            this.btnOk.Progress = 0;
            this.btnOk.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(225)))));
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 5;
            this.btnOk.TabStop = false;
            this.btnOk.Text = "Ok";
            this.btnOk.UseWindowsAccentColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // lblUsername
            // 
            this.lblUsername.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblUsername.Location = new System.Drawing.Point(20, 42);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(152, 25);
            this.lblUsername.TabIndex = 6;
            this.lblUsername.Text = "Twitch user name";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOAuthToken
            // 
            this.lblOAuthToken.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOAuthToken.Location = new System.Drawing.Point(20, 73);
            this.lblOAuthToken.Name = "lblOAuthToken";
            this.lblOAuthToken.Size = new System.Drawing.Size(152, 25);
            this.lblOAuthToken.TabIndex = 7;
            this.lblOAuthToken.Text = "OAuth token";
            this.lblOAuthToken.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PluginConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 198);
            this.Controls.Add(this.lblOAuthToken);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnGetToken);
            this.Controls.Add(this.oAuthToken);
            this.Controls.Add(this.username);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "PluginConfigView";
            this.Text = "PluginConfigView";
            this.Load += new System.EventHandler(this.PluginConfigView_Load);
            this.Controls.SetChildIndex(this.username, 0);
            this.Controls.SetChildIndex(this.oAuthToken, 0);
            this.Controls.SetChildIndex(this.btnGetToken, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.lblUsername, 0);
            this.Controls.SetChildIndex(this.lblOAuthToken, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedTextBox username;
        private RoundedTextBox oAuthToken;
        private ButtonPrimary btnGetToken;
        private ButtonPrimary btnOk;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblOAuthToken;
    }
}