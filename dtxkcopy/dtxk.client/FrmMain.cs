using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using ModernUISample.metro;
using dtxk.client.Windows;
using dtxk.cadastre;
using System.Threading;
using dtxk.cadastre.Windows.Docks;
using dtxk.client.Windows.Dock;
using dtxk.model;
using dtxk.utils;
using MetroFramework.Controls;
using log4net;

using dtxk.tools.Dialogs;
using dtxk.client.Windows.Dialog;
using dtxk.cadastre.Windows.Dialogs;
using dtxk.cadastre.Windows.Dialogs.Options;
using dtxk.tools.Infrastructure;

namespace dtxk.client
{
    public partial class FrmMain : Form
    {
        #region class private members
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private FrmDockDataReferences _frmDataReferences;
        private FrmCad _frmCad;
        private FrmDockClaim _frmClaim;
        private FrmStartPage _frmStartPage;
        private FrmDockReport _frmReport;
        private FrmDockUserManagment _frmUserManagment;
        private FrmDockSearch _frmSearch;
        private FrmDockSearch _frmCadastreData;
        private FrmDockRights _frmRightRegistration;
        #endregion

        #region initialize
        public FrmMain(APP_USER user)
        {
            
            UserSettings.AppUser = user;

            InitializeComponent();


            menuStripMain.Renderer = new JNRenderer();
            toolsMain.Renderer = new JNRenderer();

            menuStripMain.BackColor = Color.FromArgb(255, 245, 245, 245);
            toolsMain.BackColor = Color.FromArgb(255, 245, 245, 245);

            //if (MetroUI.DesignMode == false)
            //{
            //    MetroUI.Style.PropertyChanged += Style_PropertyChanged;
            //    MetroUI.Style.DarkStyle = true;
            //    MetroUI.Style.DarkStyle = false;
            //}

            this.Text += string.Format(" ({0})", user.USERNAME);
        }
        #endregion

        //void Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "DarkStyle")
        //    {
        //        BackColor = MetroUI.Style.BackColor;
        //        Refresh();
        //    }
        //}

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ShowStartPage();
        }

        private void ShowStartPage()
        {
            if (_frmStartPage == null || _frmStartPage.IsDisposed)
            {
                _frmStartPage = new FrmStartPage();
                _frmStartPage.DockPanel = dockPanel;
                _frmStartPage.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                _frmStartPage.OpenCadModule += new EventHandler(_frmStartPage_OpenCadModuleClick);
                _frmStartPage.OpenReferenceTables += new EventHandler(_frmStartPage_OpenDataModuleClick);
                _frmStartPage.OpenClaim += new EventHandler(_frmStartPage_OpenClaim);
                _frmStartPage.OpenReport += new EventHandler(_frmStartPage_OpenReport);
                _frmStartPage.OpenUserManagment += new EventHandler(_frmStartPage_OpenUserManagment);
                _frmStartPage.OpenInfoDissemination += new EventHandler(_frmStartPage_OpenInfoDissemination);
                _frmStartPage.OpenCadastreData += new EventHandler(_frmStartPage_OpenCadastreData);
                _frmStartPage.OpenRightRegistration += new EventHandler(_frmStartPage_OpenRightRegistration);
            }
            _frmStartPage.Show();
        }

        void _frmStartPage_OpenRightRegistration(object sender, EventArgs e)
        {
            ShowRightRegistration();
        }

        void _frmStartPage_OpenCadastreData(object sender, EventArgs e)
        {
            ShowCadastreData();
        }

        void _frmStartPage_OpenInfoDissemination(object sender, EventArgs e)
        {
            ShowInfoDissemination();
        }

        void _frmStartPage_OpenUserManagment(object sender, EventArgs e)
        {
            ShowUserManagment();
        }

        void _frmStartPage_OpenReport(object sender, EventArgs e)
        {
            ShowReport();
        }

        void _frmStartPage_OpenClaim(object sender, EventArgs e)
        {
            ShowClaim();
        }

        void _frmStartPage_OpenDataModuleClick(object sender, EventArgs e)
        {
            ShowReferenceData();
        }

        private void ShowReferenceData()
        {
            if (_frmDataReferences == null || _frmDataReferences.IsDisposed)
            {
                _frmDataReferences = new FrmDockDataReferences();
                _frmDataReferences.DockPanel = dockPanel;
                _frmDataReferences.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                _frmDataReferences.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            }
            _frmDataReferences.Show();
        }

        private void ShowCadastrePage()
        {
            try
            {
                if (UserSettings.CanUseMap == false)
                {
                    MessageBox.Show("Xəritədən istifadə hüququnuz yoxdur.\nƏlavə məlumat üçün adminstratora müraciət edin.", "İstifadə hüququ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
               
              
                if (_frmCad == null || _frmCad.IsDisposed)
                {
                    EnumCoordinateSystem corSys;
                    if (GisReferences.mapCordSystem == false)
                    {
                        corSys = EnumCoordinateSystem.UTM38;
                    }
                    else
                    {
                        corSys = EnumCoordinateSystem.UTM39;

                    }
                    _frmCad = new FrmCad();
                    _frmCad.DockPanel = dockPanel;
                    _frmCad.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                    _frmCad.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
                    _frmCad.FormClosed += new FormClosedEventHandler(_frmCad_FormClosed);
                    _frmCad.FormOpened += new EventHandler(_frmCad_FormOpened);
        

                }
                _frmCad.Show();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        void _frmCad_FormOpened(object sender, EventArgs e)
        {
            _frmStartPage.Close();
        }

        void _frmCad_FormClosed(object sender, FormClosedEventArgs e)
        {
            mnuSplitOptions.Visible = mnuGisExport.Visible = false;
        }

        private void ShowCadastreData()
        {
            if (_frmCadastreData == null || _frmCadastreData.IsDisposed)
            {
                _frmCadastreData = new FrmDockSearch(EnumSearchMode.REGISTER_CADASTRE_DATA);
                _frmCadastreData.DockPanel = dockPanel;
                _frmCadastreData.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                _frmCadastreData.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            }
            _frmCadastreData.Show();
        }

        private void ShowRightRegistration()
        {
            if (_frmRightRegistration == null || _frmRightRegistration.IsDisposed)
            {
                _frmRightRegistration = new FrmDockRights();
                _frmRightRegistration.DockPanel = dockPanel;
                _frmRightRegistration.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                _frmRightRegistration.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            }
            _frmRightRegistration.Show();
        }

        private void ShowReport()
        {
            if (_frmReport == null || _frmReport.IsDisposed)
            {
                _frmReport = new FrmDockReport();
                _frmReport.DockPanel = dockPanel;
                _frmReport.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                _frmReport.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            }
            _frmReport.Show();
        }

        private void ShowClaim()
        {
            if (_frmClaim == null || _frmClaim.IsDisposed)
            {
                _frmClaim = new FrmDockClaim();
                _frmClaim.DockPanel = dockPanel;
                _frmClaim.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                _frmClaim.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            }
            _frmClaim.Show();
        }

        private void ShowUserManagment()
        {
            if (UserSettings.UserType == EnumUserType.User)
            {
                MessageBox.Show("Yalnız 'Administrator' hüquqlu istifadəçilərin daxil olması mümkündür", "İstifadə hüququ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_frmUserManagment == null || _frmUserManagment.IsDisposed)
            {
                _frmUserManagment = new FrmDockUserManagment();
                _frmUserManagment.DockPanel = dockPanel;
                _frmUserManagment.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                _frmUserManagment.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            }
            _frmUserManagment.Show();
        }

        private void ShowInfoDissemination()
        {
            if (_frmSearch == null || _frmSearch.IsDisposed)
            {
                _frmSearch = new FrmDockSearch(EnumSearchMode.SEARCH);
                _frmSearch.DockPanel = dockPanel;
                //_frmSearch.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
                //_frmSearch.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                _frmSearch.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                _frmSearch.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
                _frmSearch.Text = "Informasiyanın alınması";
            }
            _frmSearch.Show();
        }

        void _frmStartPage_OpenCadModuleClick(object sender, EventArgs e)
        {
            ShowCadastrePage();
        }

        private void btnCadastralNo_Click(object sender, EventArgs e)
        {
            ShowCadastrePage();
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            ShowCadastrePage();
        }

        private void btnAddtionalData_Click(object sender, EventArgs e)
        {
            ShowCadastreData();
        }

        private void btnStartPage_Click(object sender, EventArgs e)
        {
            ShowStartPage();
        }

        private void btnClaim_Click(object sender, EventArgs e)
        {
            ShowClaim();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void btnRights_Click(object sender, EventArgs e)
        {
            ShowRightRegistration();
        }

        private void btnUserManagment_Click(object sender, EventArgs e)
        {
            ShowUserManagment();
        }

        private void btnInfoDisstimination_Click(object sender, EventArgs e)
        {
            ShowInfoDissemination();
        }

        private void btnReferenceData_Click(object sender, EventArgs e)
        {
            ShowReferenceData();
        }

        private void dockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (_frmCad != null && !_frmCad.IsDisposed)
            {
                if ((dockPanel.ActiveDocument == _frmClaim ||
                    dockPanel.ActiveDocument == _frmStartPage ||
                    dockPanel.ActiveDocument == _frmReport ||
                    dockPanel.ActiveDocument == _frmUserManagment ||
                    dockPanel.ActiveDocument == _frmSearch ||
                    dockPanel.ActiveDocument == _frmCadastreData ||
                    dockPanel.ActiveDocument == _frmDataReferences ||
                    dockPanel.ActiveDocument == _frmRightRegistration) &&
                    dockPanel.ActiveDocument != _frmCad)
                {
                    _frmCad.HideContent();
                    mnuSplitOptions.Visible = mnuGisExport.Visible = false;            
                }
                else if ((dockPanel.ActiveDocument != _frmClaim &&
                    dockPanel.ActiveDocument != _frmStartPage &&
                    dockPanel.ActiveDocument != _frmReport &&
                    dockPanel.ActiveDocument != _frmUserManagment &&
                    dockPanel.ActiveDocument != _frmSearch &&
                    dockPanel.ActiveDocument != _frmCadastreData &&
                    dockPanel.ActiveDocument != _frmDataReferences &&
                    dockPanel.ActiveDocument != _frmRightRegistration) &&
                    dockPanel.ActiveDocument == _frmCad)
                {
                    _frmCad.ShowContent();
                    mnuSplitOptions.Visible = mnuGisExport.Visible = true;
                }
            }
        }

        

        private class JNRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
            {
                if (!e.Item.Selected)
                {
                    base.OnRenderButtonBackground(e);
                    e.Item.ForeColor = Color.Black;
                }
                else
                {
                    Rectangle menuRectangle = new Rectangle(Point.Empty, e.Item.Size);
                    //Fill Color
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 122, 204)), menuRectangle);
                    e.Item.ForeColor = Color.White;
                    // Border Color
                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(0, 122, 204)), 1, 0, menuRectangle.Width - 2, menuRectangle.Height - 1);
                }
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs myMenu)
            {
                if (!myMenu.Item.Selected)
                {
                    base.OnRenderMenuItemBackground(myMenu);
                    myMenu.Item.ForeColor = Color.Black;
                }
                else
                {
                    Rectangle menuRectangle = new Rectangle(Point.Empty, myMenu.Item.Size);
                    //Fill Color
                    myMenu.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 122, 204)), menuRectangle);
                    myMenu.Item.ForeColor = Color.White;
                    // Border Color
                    myMenu.Graphics.DrawRectangle(new Pen(Color.FromArgb(0, 122, 204)), 1, 0, menuRectangle.Width - 2, menuRectangle.Height - 1);
                }
            }
        }

        private void gisExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmExportToShape frm = new FrmExportToShape();
            frm.ShowDialog();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            (new FrmAbout()).ShowDialog();
        }

        private void mnuMainTools_Click(object sender, EventArgs e)
        {
            toolsMain.Visible = mnuMainTools.Checked;
        }

        private void mnuCadastreOptions_Click(object sender, EventArgs e)
        {
            (new FrmOptions()).ShowDialog();
        }

        private void dockPanel_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_frmCad != null) {
                _frmCad.Close();
            }
       //     EditHelper.StopEditing();
          
            ShowCadastrePage();
        }

        

        

    }
}
