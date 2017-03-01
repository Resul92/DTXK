using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using dtxk.cadastre.Windows.Docks;

namespace dtxk.client.Windows.Dock
{
    public partial class FrmDockUserManagment : DockContent
    {
        #region class private members
        private bool _isDepartmentLoaded = false;
        private bool _isEmployeeLoaded = false;
        private bool _isUserLoaded = false;
        #endregion

        public FrmDockUserManagment()
        {
            InitializeComponent();
        }

        
        private void LoadDepartmentsContent()
        {
            if (!_isDepartmentLoaded)
            {
                FrmDockDeparments departmentContent = new FrmDockDeparments();
                departmentContent.Dock = DockStyle.Fill;
                pageDepartment.Controls.Add(departmentContent);

                _isDepartmentLoaded = true;
            }
        }

        private void LoadEmployeersContent()
        {
            if (!_isEmployeeLoaded)
            {
                FrmDockEmployers employeeContent = new FrmDockEmployers();
                employeeContent.Dock = DockStyle.Fill;
                pageEmployee.Controls.Add(employeeContent);

                _isEmployeeLoaded = true;
            }
        }

        private void LoadUserContent()
        {
            if (!_isUserLoaded)
            {
                FrmDockUsers userContent = new FrmDockUsers();
                userContent.Dock = DockStyle.Fill;
                pageUsers.Controls.Add(userContent);

                _isUserLoaded = true;
            }
        }

        private void tabListUserManagment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabListUserManagment.SelectedPage == pageDepartment)
                LoadDepartmentsContent();
            else if (tabListUserManagment.SelectedPage == pageEmployee)
                LoadEmployeersContent();
            else if (tabListUserManagment.SelectedPage == pageUsers)
                LoadUserContent();
        }

        private void FrmDockUserManagment_Load(object sender, EventArgs e)
        {
            tabListUserManagment.SelectedPage = pageUsers;
        }

   
    }
}
