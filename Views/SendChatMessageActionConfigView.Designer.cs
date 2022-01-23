
using SuchByte.MacroDeck.GUI.CustomControls;

namespace SuchByte.TwitchPlugin.Views
{
    partial class SendChatMessageActionConfigView
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
            this.message = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.SuspendLayout();
            // 
            // message
            // 
            this.message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.message.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.message.Cursor = System.Windows.Forms.Cursors.Hand;
            this.message.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.message.Icon = null;
            this.message.Location = new System.Drawing.Point(96, 80);
            this.message.MaxCharacters = 32767;
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.message.PasswordChar = false;
            this.message.PlaceHolderColor = System.Drawing.Color.Gray;
            this.message.PlaceHolderText = "Message";
            this.message.ReadOnly = false;
            this.message.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.message.SelectionStart = 0;
            this.message.Size = new System.Drawing.Size(665, 242);
            this.message.TabIndex = 1;
            this.message.TabStop = false;
            this.message.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // SendChatMessageActionConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.message);
            this.Name = "SendChatMessageActionConfigView";
            this.Load += new System.EventHandler(this.SendChatMessageActionConfigView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedTextBox message;
    }
}
