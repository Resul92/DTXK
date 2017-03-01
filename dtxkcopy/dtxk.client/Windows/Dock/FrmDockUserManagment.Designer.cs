namespace dtxk.client.Windows.Dock
{
    partial class FrmDockUserManagment
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
            this.components = new System.ComponentModel.Container();
            this.tabListUserManagment = new Cyotek.Windows.Forms.TabList();
            this.pageDepartment = new Cyotek.Windows.Forms.TabListPage();
            this.pageEmployee = new Cyotek.Windows.Forms.TabListPage();
            this.pageUsers = new Cyotek.Windows.Forms.TabListPage();
            this.archiveBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dEPARTAMENTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eMPLOYEEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabListUserManagment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.archiveBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEPARTAMENTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eMPLOYEEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabListUserManagment
            // 
            this.tabListUserManagment.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabListUserManagment.Controls.Add(this.pageDepartment);
            this.tabListUserManagment.Controls.Add(this.pageEmployee);
            this.tabListUserManagment.Controls.Add(this.pageUsers);
            this.tabListUserManagment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabListUserManagment.Location = new System.Drawing.Point(0, 0);
            this.tabListUserManagment.Name = "tabListUserManagment";
            this.tabListUserManagment.Size = new System.Drawing.Size(616, 411);
            this.tabListUserManagment.TabIndex = 0;
            this.tabListUserManagment.SelectedIndexChanged += new System.EventHandler(this.tabListUserManagment_SelectedIndexChanged);
            // 
            // pageDepartment
            // 
            this.pageDepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageDepartment.Name = "pageDepartment";
            this.pageDepartment.Size = new System.Drawing.Size(458, 403);
            this.pageDepartment.TabIndex = 0;
            this.pageDepartment.Text = "Şöbələr";
            // 
            // pageEmployee
            // 
            this.pageEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageEmployee.Name = "pageEmployee";
            this.pageEmployee.Size = new System.Drawing.Size(458, 403);
            this.pageEmployee.TabIndex = 1;
            this.pageEmployee.Text = "İşçilər";
            // 
            // pageUsers
            // 
            this.pageUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageUsers.Name = "pageUsers";
            this.pageUsers.Size = new System.Drawing.Size(458, 403);
            this.pageUsers.TabIndex = 2;
            this.pageUsers.Text = "İstifadəçilər";
            // 
            // archiveBindingSource
            // 
            this.archiveBindingSource.DataSource = typeof(dtxk.model.EMPLOYEE);
            // 
            // dEPARTAMENTBindingSource
            // 
            this.dEPARTAMENTBindingSource.DataSource = typeof(dtxk.model.DEPARTAMENT);
            // 
            // eMPLOYEEBindingSource
            // 
            this.eMPLOYEEBindingSource.DataSource = typeof(dtxk.model.EMPLOYEE);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(dtxk.model.DEPARTAMENT);
            // 
            // FrmDockUserManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(616, 411);
            this.Controls.Add(this.tabListUserManagment);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmDockUserManagment";
            this.Text = "İstifadəçilərin idarəetmə sistemi";
            this.Load += new System.EventHandler(this.FrmDockUserManagment_Load);
            this.tabListUserManagment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.archiveBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEPARTAMENTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eMPLOYEEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Cyotek.Windows.Forms.TabList tabListUserManagment;
        private Cyotek.Windows.Forms.TabListPage pageDepartment;
        private Cyotek.Windows.Forms.TabListPage pageEmployee;
        private Cyotek.Windows.Forms.TabListPage pageUsers;
        private System.Windows.Forms.BindingSource eMPLOYEEBindingSource;
        private System.Windows.Forms.BindingSource archiveBindingSource;
        private System.Windows.Forms.BindingSource dEPARTAMENTBindingSource;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}