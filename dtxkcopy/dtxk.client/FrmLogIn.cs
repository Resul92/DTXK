using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using dtxk.business;
using dtxk.model;
using dtxk.business.Services;
using MetroFramework.Forms;
using dtxk.model.DTO;
using dtxk.utils;
using System.Diagnostics;

namespace dtxk.client
{
    public partial class FrmLogIn : MetroForm
    {
        #region class private members
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private BackgroundWorker _worker;
        private ServiceAppUser _srvUser;
        private BusinessParam _bussnessParam;
        private ErrorProvider _errProvider;
        #endregion

        public FrmLogIn()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Program started");
           
            _errProvider = new ErrorProvider();
            _srvUser = new ServiceAppUser();
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += new DoWorkEventHandler(_worker_DoWork);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_worker_RunWorkerCompleted);

            log.Debug("FrmLogin constuct finished.");
        }

        void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnLogIn.Enabled = true;
            OperationResult result = e.Result as OperationResult;
            progressBar.Visible = false;
            if (result.Status)
            {
                //_service = null;
                this.Hide();
                dtxk.client.FrmMain frm = new dtxk.client.FrmMain((APP_USER)result.Result);
                frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                frm.Show();
            }
            else
            {
                lblError.Text = result.Message;
                _errProvider.SetError(lblError, result.Message);
            }
        }

        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            APP_USER user = e.Argument as APP_USER;

            OperationResult result = _srvUser.GetAppUser(user.USERNAME, user.PASSWORD, _bussnessParam);
            e.Result = result;
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            _errProvider.SetError(lblError, "");

            if (txtUserName.Text.Trim() == "")
            {
                _errProvider.SetError(txtUserName, "Login daxil ediməmişdir");
                return;
            }
            else
            {
                _errProvider.SetError(txtUserName, "");
            }

            if (txtPassword.Text.Trim() == "")
            {
                _errProvider.SetError(txtPassword, "Parol daxil edilməmişdir");
                return;
            }
            else
            {
                _errProvider.SetError(txtPassword, "");
            }

            progressBar.Visible = true;
            Application.DoEvents();
            string login = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            btnLogIn.Enabled = false;
            if (!_worker.IsBusy)
                _worker.RunWorkerAsync(new APP_USER() { USERNAME = login, PASSWORD = password });
            if (rbUtm39.Checked)
            { GisReferences.mapCordSystem = true; }
            else
            {
                GisReferences.mapCordSystem = false;
            }

        }

        private void FrmLogIn_Load(object sender, EventArgs e)
        {
            lblError.Text = "";
            this.Focus();
            txtUserName.Focus();
            rbUtm39.Checked = true;

         //Process.Start(dtxk.gis.Common.ClientConfig.Config.UpdateProcessor);
           
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
