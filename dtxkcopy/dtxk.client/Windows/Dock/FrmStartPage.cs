using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using dtxk.cadastre.Common.Factory;

namespace dtxk.client.Windows
{
    public partial class FrmStartPage : DockContent
    {
        public event EventHandler OpenCadModule;
        public event EventHandler OpenReferenceTables;
        public event EventHandler OpenReport;
        public event EventHandler OpenClaim;
        public event EventHandler OpenInfoDissemination;
        public event EventHandler OpenUserManagment;
        public event EventHandler OpenCadastreData;
        public event EventHandler OpenRightRegistration;

        public FrmStartPage()
        {
            InitializeComponent();
        }

        private void metroTileMAP_Click(object sender, EventArgs e)
        {
            if (OpenCadModule != null)
                OpenCadModule(this, null);
        }

        private void metroTileReferences_Click(object sender, EventArgs e)
        {
            if (OpenReferenceTables != null)
                OpenReferenceTables(this, null);
        }

        private void metroTileReport_Click(object sender, EventArgs e)
        {
            if (OpenReport != null)
                OpenReport(this, null);
        }

        private void metroTileAdmin_Click(object sender, EventArgs e)
        {
            if (OpenUserManagment != null)
                OpenUserManagment(this, null);
        }

        private void pictureBoxMap_Click(object sender, EventArgs e)
        {
            metroTileMAP_Click(metroTileMAP, e);
        }

        private void pictureBoxAdmin_Click(object sender, EventArgs e)
        {
            metroTileAdmin_Click(metroTileAdmin, null);
        }

        private void metroTile_Click(object sender, EventArgs e)
        {
            if (OpenClaim != null)
                OpenClaim(this, null);
        }

        private void metroTileInfoDissemination_Click(object sender, EventArgs e)
        {
            if (OpenInfoDissemination != null)
                OpenInfoDissemination(this, null);
        }

        private void metroTileCadastralNo_Click(object sender, EventArgs e)
        {
            if (OpenCadModule != null)
                OpenCadModule(this, null);
        }

        private void metroTileCadastralData_Click(object sender, EventArgs e)
        {
            if (OpenCadastreData != null)
                OpenCadastreData(this, null);
        }

        private void metroTileRights_Click(object sender, EventArgs e)
        {
            if (OpenRightRegistration != null)
                OpenRightRegistration(this, null);
        }
        
    }
}
