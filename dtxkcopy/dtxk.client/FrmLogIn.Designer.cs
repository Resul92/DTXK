namespace dtxk.client
{
    partial class FrmLogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogIn));
            this.lblError = new System.Windows.Forms.Label();
            this.progressBar = new MetroFramework.Controls.MetroProgressBar();
            this.btnClose = new MetroFramework.Controls.MetroButton();
            this.btnLogIn = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.pcbLogin = new System.Windows.Forms.PictureBox();
            this.rbUtm39 = new System.Windows.Forms.RadioButton();
            this.rbUtm38 = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(24, 257);
            this.lblError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(40, 17);
            this.lblError.TabIndex = 7;
            this.lblError.Text = "Error";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.FontSize = MetroFramework.MetroProgressBarSize.Medium;
            this.progressBar.FontWeight = MetroFramework.MetroProgressBarWeight.Light;
            this.progressBar.HideProgressText = true;
            this.progressBar.Location = new System.Drawing.Point(21, 572);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.MarqueeAnimationSpeed = 50;
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressBarStyle = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.Size = new System.Drawing.Size(445, 28);
            this.progressBar.Style = MetroFramework.MetroColorStyle.Blue;
            this.progressBar.StyleManager = null;
            this.progressBar.TabIndex = 10;
            this.progressBar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.progressBar.Theme = MetroFramework.MetroThemeStyle.Light;
            this.progressBar.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Highlight = false;
            this.btnClose.Location = new System.Drawing.Point(273, 498);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(144, 28);
            this.btnClose.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnClose.StyleManager = null;
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "İmtina et";
            this.btnClose.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLogIn
            // 
            this.btnLogIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogIn.Highlight = true;
            this.btnLogIn.Location = new System.Drawing.Point(89, 498);
            this.btnLogIn.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(144, 28);
            this.btnLogIn.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnLogIn.StyleManager = null;
            this.btnLogIn.TabIndex = 3;
            this.btnLogIn.Text = "Daxil ol";
            this.btnLogIn.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.CustomBackground = false;
            this.metroLabel1.CustomForeColor = false;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabel1.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel1.Location = new System.Drawing.Point(28, 345);
            this.metroLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(65, 20);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel1.StyleManager = null;
            this.metroLabel1.TabIndex = 15;
            this.metroLabel1.Text = "Istifadəçi:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel1.UseStyleColors = false;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.CustomBackground = false;
            this.metroLabel2.CustomForeColor = false;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabel2.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel2.Location = new System.Drawing.Point(53, 381);
            this.metroLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(43, 20);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel2.StyleManager = null;
            this.metroLabel2.TabIndex = 16;
            this.metroLabel2.Text = "Parol:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel2.UseStyleColors = false;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(121, 344);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(296, 22);
            this.txtUserName.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(121, 380);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(296, 22);
            this.txtPassword.TabIndex = 2;
            // 
            // pcbLogin
            // 
            this.pcbLogin.Image = global::dtxk.client.ResourceClient.transparant200;
            this.pcbLogin.Location = new System.Drawing.Point(28, 78);
            this.pcbLogin.Margin = new System.Windows.Forms.Padding(4);
            this.pcbLogin.Name = "pcbLogin";
            this.pcbLogin.Size = new System.Drawing.Size(193, 176);
            this.pcbLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbLogin.TabIndex = 9;
            this.pcbLogin.TabStop = false;
            // 
            // rbUtm39
            // 
            this.rbUtm39.AutoSize = true;
            this.rbUtm39.Location = new System.Drawing.Point(143, 410);
            this.rbUtm39.Margin = new System.Windows.Forms.Padding(4);
            this.rbUtm39.Name = "rbUtm39";
            this.rbUtm39.Size = new System.Drawing.Size(75, 21);
            this.rbUtm39.TabIndex = 17;
            this.rbUtm39.TabStop = true;
            this.rbUtm39.Text = "UTM39";
            this.rbUtm39.UseVisualStyleBackColor = true;
            // 
            // rbUtm38
            // 
            this.rbUtm38.AutoSize = true;
            this.rbUtm38.Location = new System.Drawing.Point(264, 410);
            this.rbUtm38.Margin = new System.Windows.Forms.Padding(4);
            this.rbUtm38.Name = "rbUtm38";
            this.rbUtm38.Size = new System.Drawing.Size(75, 21);
            this.rbUtm38.TabIndex = 18;
            this.rbUtm38.TabStop = true;
            this.rbUtm38.Text = "UTM38";
            this.rbUtm38.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(261, 78);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(193, 176);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // FrmLogIn
            // 
            this.AcceptButton = this.btnLogIn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(497, 625);
            this.Controls.Add(this.rbUtm38);
            this.Controls.Add(this.rbUtm39);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnLogIn);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pcbLogin);
            this.Controls.Add(this.lblError);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmLogIn";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Text = "Daxil ol...";
            this.Load += new System.EventHandler(this.FrmLogIn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.PictureBox pcbLogin;
        private MetroFramework.Controls.MetroProgressBar progressBar;
        private MetroFramework.Controls.MetroButton btnClose;
        private MetroFramework.Controls.MetroButton btnLogIn;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.RadioButton rbUtm39;
        private System.Windows.Forms.RadioButton rbUtm38;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}