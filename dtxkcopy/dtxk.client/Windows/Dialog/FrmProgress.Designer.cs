namespace dtxk.client.Windows.Dialog
{
    partial class FrmProgress
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
            this.metroProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.SuspendLayout();
            // 
            // metroProgressBar
            // 
            this.metroProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroProgressBar.FontSize = MetroFramework.MetroProgressBarSize.Medium;
            this.metroProgressBar.FontWeight = MetroFramework.MetroProgressBarWeight.Light;
            this.metroProgressBar.HideProgressText = true;
            this.metroProgressBar.Location = new System.Drawing.Point(23, 136);
            this.metroProgressBar.MarqueeAnimationSpeed = 20;
            this.metroProgressBar.Name = "metroProgressBar";
            this.metroProgressBar.ProgressBarStyle = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.metroProgressBar.Size = new System.Drawing.Size(510, 23);
            this.metroProgressBar.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroProgressBar.StyleManager = null;
            this.metroProgressBar.TabIndex = 0;
            this.metroProgressBar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.metroProgressBar.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // FrmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 198);
            this.ControlBox = false;
            this.Controls.Add(this.metroProgressBar);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FrmProgress";
            this.Resizable = false;
            this.Text = "Əməliyyat gedir...";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroProgressBar metroProgressBar;
    }
}