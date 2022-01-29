
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
            this.lblStreamTitle = new System.Windows.Forms.Label();
            this.streamTitle = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.game = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.lblGame = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStreamTitle
            // 
            this.lblStreamTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStreamTitle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStreamTitle.Location = new System.Drawing.Point(124, 184);
            this.lblStreamTitle.Name = "lblStreamTitle";
            this.lblStreamTitle.Size = new System.Drawing.Size(143, 25);
            this.lblStreamTitle.TabIndex = 0;
            this.lblStreamTitle.Text = "Stream title";
            this.lblStreamTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // streamTitle
            // 
            this.streamTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.streamTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.streamTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.streamTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.streamTitle.Icon = null;
            this.streamTitle.Location = new System.Drawing.Point(273, 184);
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
            this.game.Location = new System.Drawing.Point(273, 215);
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
            // lblGame
            // 
            this.lblGame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblGame.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblGame.Location = new System.Drawing.Point(124, 215);
            this.lblGame.Name = "lblGame";
            this.lblGame.Size = new System.Drawing.Size(143, 25);
            this.lblGame.TabIndex = 4;
            this.lblGame.Text = "Game";
            this.lblGame.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SetTitleGameActionConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.game);
            this.Controls.Add(this.lblGame);
            this.Controls.Add(this.streamTitle);
            this.Controls.Add(this.lblStreamTitle);
            this.Name = "SetTitleGameActionConfigView";
            this.Load += new System.EventHandler(this.SetTitleGameActionConfigView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblStreamTitle;
        private MacroDeck.GUI.CustomControls.RoundedTextBox streamTitle;
        private MacroDeck.GUI.CustomControls.RoundedTextBox game;
        private System.Windows.Forms.Label lblGame;
    }
}
