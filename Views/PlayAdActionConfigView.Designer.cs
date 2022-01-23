
using SuchByte.MacroDeck.GUI.CustomControls;

namespace SuchByte.TwitchPlugin.Views
{
    partial class PlayAdActionConfigView
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
            this.label1 = new System.Windows.Forms.Label();
            this.commercialLenght = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(247, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Length:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // commercialLenght
            // 
            this.commercialLenght.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.commercialLenght.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.commercialLenght.Cursor = System.Windows.Forms.Cursors.Hand;
            this.commercialLenght.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.commercialLenght.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.commercialLenght.Icon = null;
            this.commercialLenght.Location = new System.Drawing.Point(374, 192);
            this.commercialLenght.Name = "commercialLenght";
            this.commercialLenght.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.commercialLenght.SelectedIndex = -1;
            this.commercialLenght.SelectedItem = null;
            this.commercialLenght.Size = new System.Drawing.Size(236, 26);
            this.commercialLenght.TabIndex = 1;
            // 
            // PlayAdActionConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.commercialLenght);
            this.Controls.Add(this.label1);
            this.Name = "PlayAdActionConfigView";
            this.Load += new System.EventHandler(this.PlayAdActionConfigView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private RoundedComboBox commercialLenght;
    }
}
