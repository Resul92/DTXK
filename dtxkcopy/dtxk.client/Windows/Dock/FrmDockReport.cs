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
using dtxk.business;
using dtxk.model;
using dtxk.utils;
using dtxk.model.DTO;
using System.Diagnostics;
using dtxk.business.Services;

namespace dtxk.client.Windows.Dock
{
    public partial class FrmDockReport : DockContent
    {
        #region class private members
        private BasicService _basicService;
       private dtxk.business.Services.ServiceParcel _srvParcel;
        private ServicePhysicalPerson _srvPhysicalPerson;
        private ServiceLegalPerson _srvLegalPerson;
        private ServiceAppUser _srvUser;
        private ServiceDocumentedParcel _srvDocPrcl;
        private ServiceDocument _srvDocument;

        private BusinessParam _businessParam;
        private BackgroundWorker _workerSearchCadastreNo;
        private BackgroundWorker _workerSearchPhysicalPerson;
        private BackgroundWorker _workerSearchLegalPerson;
    // private BackgroundWorker _workerSearchParcelOwner;
        private ErrorProvider _errProvider;
        private EnumSearchMode _searchMode;
        decimal? idUser;
        List<PARCEL_OWNER> parcel_owner_list;
        #endregion


       
        public FrmDockReport()
        {
           
            InitializeComponent();

            _errProvider = new ErrorProvider();
            _basicService = new BasicService();
            _srvParcel = new dtxk.business.Services.ServiceParcel(_basicService.dtxkContext);
            _srvPhysicalPerson = new ServicePhysicalPerson(_basicService.dtxkContext);
            _srvLegalPerson = new ServiceLegalPerson(_basicService.dtxkContext);
            _srvDocPrcl = new ServiceDocumentedParcel(_basicService.dtxkContext);
            _srvUser = new ServiceAppUser(_basicService.dtxkContext);
            _businessParam = new BusinessParam(UserSettings.UserId);



            _workerSearchCadastreNo = new BackgroundWorker();
            _workerSearchCadastreNo.DoWork += new DoWorkEventHandler(_backgroundWorker_DoWork);
            _workerSearchCadastreNo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_backgroundWorker_RunWorkerCompleted);

            _workerSearchPhysicalPerson = new BackgroundWorker();
            _workerSearchPhysicalPerson.DoWork += new DoWorkEventHandler(_workerSearchPhysicalPerson_DoWork);
            _workerSearchPhysicalPerson.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_workerSearchPhysicalPerson_RunWorkerCompleted);

            _workerSearchLegalPerson = new BackgroundWorker();
            _workerSearchLegalPerson.DoWork += new DoWorkEventHandler(_workerSearchLegalPerson_DoWork);
            _workerSearchLegalPerson.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_workerSearchLegalPerson_RunWorkerCompleted);

           // _workerSearchParcelOwner = new BackgroundWorker();
           // _workerSearchParcelOwner.DoWork += new DoWorkEventHandler(_workerSearchParcelOwner_DoWork);
          //  _workerSearchParcelOwner.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_workerSearchParcelOwner_RunWorkerCompleted);
        }

        void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<ParcelSearchResult> resultList = e.Result as List<ParcelSearchResult>;
            parcelSearchResultBindingSource.DataSource = resultList;
            gridControl1.DataSource = parcelSearchResultBindingSource;

            statusParcelRowCount.Text = "Cəmi: " + parcelSearchResultBindingSource.Count;
            progressBarCadastreNo.Visible = false;
            progressBarPhysicalPerson.Visible = false;
            progressBarLegalPerson.Visible = false;

            tbCtrlSearch.SelectTab(tbPgCadastral);
        }

        void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerArg args = e.Argument as WorkerArg;
            List<ParcelSearchResult> lstParcel = _srvParcel.LoadAllParcel(_businessParam);
            List<ParcelSearchResult> resutlist = null;
            try
            {
                if (args.ArgType == 0 && !string.IsNullOrEmpty(args.CadstralNo))
                {
                    string cadastralCode = args.CadstralNo.ToString();
                    lstParcel = _srvParcel.FindParcelsStartsWithCadastralNo(cadastralCode, false, _businessParam);
                }
                else if (args.ArgType == 1 && args.PhysicalPersons != null)
                {
                    lstParcel = _srvParcel.FindParcelsByPhysicalPersons(args.PhysicalPersons, _businessParam).ToList();
                }
                else if (args.ArgType == 2 && args.LegalPersons != null)
                {
                    lstParcel = _srvParcel.FindParcelsByLegalPersons(args.LegalPersons, _businessParam).ToList();
                }


                resutlist = lstParcel;

                if (args.Id_OLDDOCTYPE != null && args.Id_OLDDOCTYPE != -1)
                {
                    resutlist = resutlist.Where(c => c.Id_OLDDOCTYPE == args.Id_OLDDOCTYPE).ToList();
                }

                if (args.MinDocumentedArea != null && args.MaxDocumentedArea != null)
                {
                    resutlist = resutlist.Where(c => c.DocumentedArea > args.MinDocumentedArea && c.DocumentedArea < args.MaxDocumentedArea).ToList();
                }
                if (args.MinFacticalArea != null && args.MaxFacticalArea != null)
                {
                    resutlist = resutlist.Where(c => c.FacticalArea > args.MinFacticalArea && c.FacticalArea < args.MaxFacticalArea).ToList();
                }

                if (args.id_user != null && args.id_user != 0 && args.id_user != -1)
                {
                    resutlist = resutlist.Where(c => c.Id_User == args.id_user).ToList();

                }
                if (args.Id_PropertyType != null && args.Id_PropertyType != -1)
                {
                    resutlist = resutlist.Where(c => c.Id_PropertyType == args.Id_PropertyType).ToList();

                }
                if (args.Id_UsingForm != null && args.Id_UsingForm != -1)
                {
                    resutlist = resutlist.Where(c => c.Id_UsingForm == args.Id_UsingForm).ToList();
                }

                if (args.Id_LandCategory != null && args.Id_LandCategory != -1)
                {
                    resutlist = resutlist.Where(c => c.Id_LandCategory == args.Id_LandCategory).ToList();
                }

                if (args.date1 != null && args.date2.Value != null)
                {
                    resutlist = resutlist.Where(c => c.dateTime >= args.date1 && c.dateTime <= args.date2).ToList();
                }
              

            }
            catch (Exception ex)
            { }
            e.Result = resutlist;
        }




        void _workerSearchPhysicalPerson_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<PHYSICAL_PERSON> result = e.Result as List<PHYSICAL_PERSON>;
            pHYSICALPERSONBindingSource.DataSource = result;

            statusPhysicalPersonRowCount.Text = "Cəmi: " + pHYSICALPERSONBindingSource.Count;
            progressBarPhysicalPerson.Visible = false;
        }

        void _workerSearchPhysicalPerson_DoWork(object sender, DoWorkEventArgs e)
        {
            PHYSICAL_PERSON arg = e.Argument as PHYSICAL_PERSON;
            if (arg != null)
            {
                List<PHYSICAL_PERSON> result = _srvPhysicalPerson.FindPhysicalPerson(arg, _businessParam).ToList();
                e.Result = result;
            }
        }

        void _workerSearchLegalPerson_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<LEGAL_PERSON> result = e.Result as List<LEGAL_PERSON>;
            lEGALPERSONBindingSource.DataSource = result;
            statusLegallPersonRowCount.Text = "Cəmi: " + lEGALPERSONBindingSource.Count;
            progressBarLegalPerson.Visible = false;
        }

        void _workerSearchLegalPerson_DoWork(object sender, DoWorkEventArgs e)
        {
            LEGAL_PERSON arg = e.Argument as LEGAL_PERSON;
            if (arg != null)
            {
                List<LEGAL_PERSON> result = _srvLegalPerson.FindLegalPerson(arg, _businessParam).ToList();
                e.Result = result;
            }
        }





        private void btnSearchCadastrNo_Click(object sender, EventArgs e)
        {
            if (txtCodeRegionZone.Text == "")
            {
                _errProvider.SetError(txtCodeRegionZone, "Kadastr zonası mütləq seçilməlidir");

            }
            _errProvider.SetError(txtCodeRegionZone, "");
            StringBuilder codePattern = new StringBuilder(txtCodeRegionZone.Text);
            if (txtCodeRegion.Text != "")
            {
                codePattern.Append(txtCodeRegion.Text);
            }
            else
            {
                codePattern.Append("__");
            }
            if (txtCodeMunicipality.Text != "")
            {
                codePattern.Append(txtCodeMunicipality.Text);
            }
            else
            {
                codePattern.Append("___");
            }
            if (txtCodeTerriturialUnit.Text != "")
            {
                codePattern.Append(txtCodeTerriturialUnit.Text);
            }
            else
            {
                codePattern.Append("__");
            }

            codePattern.Append(txtParcelCadastralNo.Text);
            codePattern.Append("%");
            string cadastralCode = codePattern.ToString();

            if (!string.IsNullOrEmpty(cadastralCode))
            {
                progressBarCadastreNo.Visible = true;
                if (!_workerSearchCadastreNo.IsBusy)
                {
                    WorkerArg arg = new WorkerArg();
                    arg.CadstralNo = cadastralCode;
                    arg.ArgType = 0;

                  //  APP_USER user = cboUser.SelectedItem as APP_USER;
                    PROPERTY_TYPE type = cboPropertyType.SelectedItem as PROPERTY_TYPE;
                    USING_FORM usingForm = cboUsingForm.SelectedItem as USING_FORM;
                    LAND_CATEGORY category = cboCategory.SelectedItem as LAND_CATEGORY;
                    OLD_DOCK_TYPE docType = cboDocType.SelectedItem as OLD_DOCK_TYPE;
                   


                    arg.id_user = idUser;
                    arg.Id_PropertyType = type.ID_PROPERTY_TYPE;
                    arg.Id_UsingForm = usingForm.ID_USING_FORM;
                    arg.Id_LandCategory = category.ID_LAND_CATEGORY;
                    arg.Id_OLDDOCTYPE = docType.ID_OLD_DOCK_TYPE;

                    string argvalue = date1.Value.ToShortDateString();
                    string nowdate = DateTime.Now.ToShortDateString();
                    if (argvalue == nowdate)
                    {
                        arg.date1 = null;

                    }
                    else
                    {
                        arg.date1 = date1.Value;
                    }
                    
                    arg.date2 = date2.Value;
                    arg.MinFacticalArea = Convert.ToInt32(txtMinFacticalArea.Value);
                    arg.MaxFacticalArea = txtMaxFacticalArea.Value != 0 ? Convert.ToInt32(txtMaxFacticalArea.Text) : (int?)null;
                    if (txtMindocArea.Text != "") arg.MinDocumentedArea = Convert.ToInt32(txtMindocArea.Text);
                    arg.MaxDocumentedArea = txtMaxdocArea.Value != 0 ? Convert.ToInt32(txtMaxdocArea.Text) : (int?)null;


                    arg.ParcelOwners = parcel_owner_list;







                    _workerSearchCadastreNo.RunWorkerAsync(arg);
                }
            }
            else
            {
                gridControl1.DataSource = new List<ParcelSearchResult>();
            }





        }


        #region inner class
        class WorkerArg
        {
            public string CadstralNo { get; set; }

            public int ArgType { get; set; } //0 - Code, 1-physical person , 2-legal person

            public List<PHYSICAL_PERSON> PhysicalPersons { get; set; }

            public List<LEGAL_PERSON> LegalPersons { get; set; }

            public List<PARCEL_OWNER> ParcelOwners { get; set; }

            public int MinFacticalArea { get; set; }

            public int? MaxFacticalArea { get; set; }

            public int MinDocumentedArea { get; set; }

            public int? MaxDocumentedArea { get; set; }

            public decimal? id_user { get; set; }

            public decimal? Id_LandCategory { get; set; }

            public decimal? Id_PropertyType { get; set; }

            public decimal? Id_UsingForm { get; set; }

            public decimal? Id_OLDDOCTYPE { get; set; }

            public DateTime? date1 { get; set; }

            public DateTime? date2 { get; set; }



        }
        #endregion

        private void LoadLegalForms()
        {
            List<LEGAL_FORMS> legalForms = _basicService.LoadAllLEGAL_FORMS(_businessParam).OrderBy(c => c.NAME).ToList();
            legalForms.Insert(0, new LEGAL_FORMS { NAME = "---", ID_LEGAL_FORM = -1 });
            cboLegalForms.DataSource = legalForms;
            cboLegalForms.DisplayMember = "NAME";
            cboLegalForms.ValueMember = "ID_LEGAL_FORM";
        }

        private void LoadCadastralZone()
        {
            List<CADASTRAL_ZONE> cadastralZone = _basicService.LoadAllCADASTRAL_ZONE(_businessParam).OrderBy(c => c.NAME).ToList();
            cadastralZone.Insert(0, new CADASTRAL_ZONE { NAME = "---", ID_CADASTRAL_ZONE = -1 });
            cboRegionZone.DataSource = cadastralZone;
            cboRegionZone.DisplayMember = "NAME";
            cboRegionZone.ValueMember = "ID_CADASTRAL_ZONE";
        }

      
        private void LoadUsers()
        {

            List<EMPLOYEE> users = _basicService.LoadAllEMPLOYEE(_businessParam).OrderBy(c => c.NAME).ToList();
            users.Insert(0, new EMPLOYEE { NAME = "---", ID_EMPLOYEE = -1 });
            cboUser.DataSource = users;
            cboUser.DisplayMember = "NAME";
            cboUser.ValueMember = "ID_EMPLOYEE";
        }


        private void LoadPropetyTypes()
        {
            List<PROPERTY_TYPE> ptypes = _basicService.LoadAllPROPERTY_TYPE(_businessParam).OrderBy(c => c.NAME).ToList();
            ptypes.Insert(0, new PROPERTY_TYPE { NAME = "---", ID_PROPERTY_TYPE = -1 });
            cboPropertyType.DataSource = ptypes;
            cboPropertyType.DisplayMember = "NAME";
            cboPropertyType.ValueMember = "ID_PROPERTY_TYPE";
        }



        private void LoadAllUsingForms()
        {
            List<USING_FORM> usingform = _basicService.LoadAllUSING_FORM(_businessParam).OrderBy(c => c.NAME).ToList();
            usingform.Insert(0, new USING_FORM { NAME = "---", ID_USING_FORM = -1 });
            cboUsingForm.DataSource = usingform;
            cboUsingForm.DisplayMember = "NAME";
            cboUsingForm.ValueMember = "ID_USING_FORM";
        }


        private void LoadAllLandCategory()
        {
            List<LAND_CATEGORY> categories = _basicService.LoadAllLAND_CATEGORY(_businessParam).OrderBy(c => c.NAME).ToList();
            categories.Insert(0, new LAND_CATEGORY { NAME = "---", ID_LAND_CATEGORY = -1 });
            cboCategory.DataSource = categories;
            cboCategory.DisplayMember = "NAME";
            cboCategory.ValueMember = "ID_LAND_CATEGORY";
        }

        private void LoadAllDocument_Type()
        {
            List<OLD_DOCK_TYPE> doctype = _basicService.LoadAllOLD_DOCK_TYPE(_businessParam).OrderBy(c => c.NAME).ToList();
            doctype.Insert(0, new OLD_DOCK_TYPE { NAME = "---", ID_OLD_DOCK_TYPE= -1 });
            cboDocType.DataSource = doctype;
            cboDocType.DisplayMember = "NAME";
            cboDocType.ValueMember = "ID_OLD_DOCK_TYPE";
        }


   


        #region load
        private void FrmDockReport_Load(object sender, EventArgs e)
        {
            tbCtrlSearch.SelectedTab = tbPgCadastral;
            LoadCadastralZone();
            LoadLegalForms();
            LoadUsers();
            LoadPropetyTypes();
            LoadAllUsingForms();
            LoadAllLandCategory();
            LoadAllDocument_Type();
        }
        #endregion

        private void btnSearchPhysicalPerson_Click(object sender, EventArgs e)
        {
            string passportNo = txtPassportNo.Text.Trim();
            string pinCode = txtPinCode.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string fatherName = txtFatherName.Text.Trim();

            if (!string.IsNullOrEmpty(passportNo) ||
                !string.IsNullOrEmpty(pinCode) ||
                !string.IsNullOrEmpty(firstName) ||
                !string.IsNullOrEmpty(lastName) ||
                !string.IsNullOrEmpty(fatherName))
            {
                progressBarPhysicalPerson.Visible = true;
                PHYSICAL_PERSON owner = new PHYSICAL_PERSON();
                owner.PASSPORT_NO = passportNo;
                owner.PASSPORT_PIN_CODE = pinCode;
                owner.FIRST_NAME = firstName;
                owner.LAST_NAME = lastName;
                owner.FATHER_NAME = fatherName;
                if (!_workerSearchPhysicalPerson.IsBusy)
                    _workerSearchPhysicalPerson.RunWorkerAsync(owner);
            }
            else
            {
                pHYSICALPERSONBindingSource.DataSource = _srvPhysicalPerson.LoadAllPerson(_businessParam);
                statusPhysicalPersonRowCount.Text = "Cəmi: " + pHYSICALPERSONBindingSource.Count;
            }
        }

        private void btnSearchLegalPerson_Click(object sender, EventArgs e)
        {
            string legalPersonName = txtLegalPersonName.Text.Trim();
            string indentificationNo = txtLegalPersonRegistrationNo.Text.Trim();
            string tinNo = txtLegalPersonTin.Text.Trim();
            LEGAL_FORMS legalForm = cboLegalForms.SelectedItem as LEGAL_FORMS;

            if (!string.IsNullOrEmpty(legalPersonName) ||
                !string.IsNullOrEmpty(indentificationNo) ||
                !string.IsNullOrEmpty(tinNo) ||
                legalForm.ID_LEGAL_FORM != -1)
            {
                LEGAL_PERSON legalPerson = new LEGAL_PERSON();
                legalPerson.NAME = legalPersonName;
                legalPerson.IDENTIFICATION_NO = indentificationNo;
                legalPerson.TIN_NO = tinNo;
                legalPerson.ID_LEGAL_FORM = legalForm.ID_LEGAL_FORM;
                if (!_workerSearchLegalPerson.IsBusy)
                    _workerSearchLegalPerson.RunWorkerAsync(legalPerson);
            }
            else
            {
                lEGALPERSONBindingSource.DataSource = _srvLegalPerson.LoadAllLegalPerson(_businessParam);
                statusLegallPersonRowCount.Text = "Cəmi: " + lEGALPERSONBindingSource.Count;

            }
        }

        private void cboRegionZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            CADASTRAL_ZONE cadastreZone = cboRegionZone.SelectedItem as CADASTRAL_ZONE;
            if (cadastreZone != null && cadastreZone.ID_CADASTRAL_ZONE != -1)
            {
                List<CADASTRAL_REGION> regions = _basicService.LoadAllCADASTRAL_REGION(_businessParam).Where(c => c.ID_CADASTRAL_ZONE == cadastreZone.ID_CADASTRAL_ZONE).OrderBy(c => c.NAME).ToList();
                regions.Insert(0, new CADASTRAL_REGION { NAME = "---", CODE = "", ID_CADASTRAL_REGION = -1 });

                cboRegion.DataSource = regions;
                cboRegion.DisplayMember = "NAME";
                cboRegion.ValueMember = "ID_CADASTRAL_REGION";

                txtCodeRegionZone.Text = cadastreZone.CODE;

                if (regions == null || regions.Count == 0)
                {
                    txtCodeRegion.Text = "";
                    txtCodeMunicipality.Text = "";
                    txtCodeTerriturialUnit.Text = "";

                    cboRegion.DataSource = null;
                    cboMunicipality.DataSource = null;
                    cboTerritorialUnit.DataSource = null;
                }

                txtCodeRegionZone.ReadOnly = true;
            }
            else
            {
                cboRegion.DataSource = null;
                txtCodeRegionZone.ReadOnly = false;
                txtCodeRegionZone.Text = "";
            }
        }

        private void cboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CADASTRAL_REGION region = cboRegion.SelectedItem as CADASTRAL_REGION;
            if (region != null && !string.IsNullOrEmpty(region.CODE))
            {
                List<MUNICIPALITY> municipalities = _basicService.LoadAllMUNICIPALITY(_businessParam).Where(c => c.ID_CADASTRAL_REGION == region.ID_CADASTRAL_REGION).OrderBy(c => c.NAME).ToList();
                municipalities.Insert(0, new MUNICIPALITY { NAME = "---", CODE = "", ID_MUNICIPALITY = -1 });

                cboMunicipality.DataSource = municipalities;
                cboMunicipality.DisplayMember = "NAME";
                cboMunicipality.ValueMember = "ID_MUNICIPALITY";

                CADASTRAL_ZONE cadastreZone = cboRegionZone.SelectedItem as CADASTRAL_ZONE;
                int prevCodeLength = cadastreZone.CODE.Length;
                txtCodeRegion.Text = region.CODE.Substring(prevCodeLength, region.CODE.Length - prevCodeLength);

                if (municipalities == null || municipalities.Count == 0)
                {
                    txtCodeMunicipality.Text = "";
                    txtCodeTerriturialUnit.Text = "";

                    cboMunicipality.DataSource = null;
                    cboTerritorialUnit.DataSource = null;
                }

                txtCodeRegion.ReadOnly = true;
            }
            else
            {
                cboMunicipality.DataSource = null;
                txtCodeRegion.Text = "";
                txtCodeRegion.ReadOnly = false;
            }
        }

        private void cboMunicipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            MUNICIPALITY municipality = cboMunicipality.SelectedItem as MUNICIPALITY;


            if (municipality != null && !string.IsNullOrEmpty(municipality.CODE))
            {
                List<TERRITORIAL_UNIT> territorialUnits = _basicService.LoadAllTERRITORIAL_UNIT(_businessParam).Where(c => c.ID_MUNICIPALITY == municipality.ID_MUNICIPALITY).OrderBy(c => c.NAME).ToList();
                territorialUnits.Insert(0, new TERRITORIAL_UNIT { NAME = "---", CODE = "", ID_TERRITORIAL_UNIT = -1 });

                cboTerritorialUnit.DataSource = territorialUnits;
                cboTerritorialUnit.DisplayMember = "NAME";
                cboTerritorialUnit.ValueMember = "ID_TERRITORIAL_UNIT";

                CADASTRAL_REGION region = cboRegion.SelectedItem as CADASTRAL_REGION;
                int prevCodeLength = region.CODE.Length;
                txtCodeMunicipality.Text = municipality.CODE.Substring(prevCodeLength, municipality.CODE.Length - prevCodeLength);

                if (territorialUnits == null || territorialUnits.Count == 0)
                {
                    txtCodeTerriturialUnit.Text = "";
                    cboTerritorialUnit.DataSource = null;
                }

                txtCodeMunicipality.ReadOnly = true;
            }
            else
            {
                cboTerritorialUnit.DataSource = null;
                txtCodeMunicipality.Text = "";
                txtCodeMunicipality.ReadOnly = false;
            }
        }

        private void cboTerritorialUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TERRITORIAL_UNIT territorialUnit = cboTerritorialUnit.SelectedItem as TERRITORIAL_UNIT;
                if (territorialUnit != null && !string.IsNullOrEmpty(territorialUnit.CODE))
                {
                    MUNICIPALITY municipality = cboMunicipality.SelectedItem as MUNICIPALITY;
                    int prevCodeLength = municipality.CODE.Length;
                    txtCodeTerriturialUnit.Text = territorialUnit.CODE.Substring(prevCodeLength, territorialUnit.CODE.Length - prevCodeLength);

                    txtCodeTerriturialUnit.ReadOnly = true;
                }
                else
                {
                    txtCodeTerriturialUnit.Text = "";
                    txtCodeTerriturialUnit.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnLegalPersonParcel_Click(object sender, EventArgs e)
        {
            gridSearchResultLegalPerson.MainView.PostEditor();
            List<LEGAL_PERSON> legalPersons = lEGALPERSONBindingSource.DataSource as List<LEGAL_PERSON>;
            if (legalPersons != null && legalPersons.Where(c => c.IsSelected).Count() > 0)
            {
                if (!_workerSearchCadastreNo.IsBusy)
                {
                    progressBarLegalPerson.Visible = true;
                    WorkerArg arg = new WorkerArg();
                    arg.ArgType = 2;
                    arg.LegalPersons = legalPersons.Where(c => c.IsSelected).ToList();
                    _workerSearchCadastreNo.RunWorkerAsync(arg);
                }
            }
        }

        private void btnPhysicalPersonParcel_Click(object sender, EventArgs e)
        {
            gridSearchResultPhysicalPerson.MainView.PostEditor();
            List<PHYSICAL_PERSON> physicalPersonList = pHYSICALPERSONBindingSource.DataSource as List<PHYSICAL_PERSON>;
            if (physicalPersonList != null && physicalPersonList.Where(c => c.IsSelected).Count() > 0)
            {
                if (!_workerSearchCadastreNo.IsBusy)
                {
                    progressBarPhysicalPerson.Visible = true;
                    WorkerArg arg = new WorkerArg();
                    arg.ArgType = 1;
                    arg.PhysicalPersons = physicalPersonList.Where(c => c.IsSelected).ToList();
                    _workerSearchCadastreNo.RunWorkerAsync(arg);
                }
            }
        }

        SaveFileDialog dlg = new SaveFileDialog();
        private void btnSaveAsPDF_Click(object sender, EventArgs e)
        {
            dlg.Filter = "Pdf File (.pdf)|*.pdf ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FilePath = dlg.FileName;
                    gridView1.ExportToPdf(FilePath);

                    var confirmationBox = MessageBox.Show("Export etdiyiniz faylı açmaq isteyirsinizmi ?", "Tesdiqləmə suali", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmationBox == DialogResult.Yes)
                    {
                        Process pdfExport = new Process();
                        pdfExport.StartInfo.FileName = dlg.FileName;
                        pdfExport.Start();
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Export baş tutmadı !");

                }
            }
            else
            {

            }
        }

        private void xLSToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dlg.Filter = " Excel 2003 File (.xls)|*.xls ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FilePath = dlg.FileName;
                    gridView1.ExportToXls(FilePath);

                    var confirmationBox = MessageBox.Show("Export etdiyiniz fayli gormek isteyirsinizmi ?", "Tesdiqleme suali", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmationBox == DialogResult.Yes)
                    {
                        Process xlsExport = new Process();
                        xlsExport.StartInfo.FileName = dlg.FileName;
                        xlsExport.Start();
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Export baş tutmadı !");
                }

            }
            else
            {

            }
        }

        private void xLSXToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dlg.Filter = " Excel 2007 File (.xlsx)|*.xlsx ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FilePath = dlg.FileName;
                    gridView1.ExportToXlsx(FilePath);

                    var confirmationBox = MessageBox.Show("Export etdiyiniz fayli gormek isteyirsinizmi ?", "Tesdiqleme suali", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmationBox == DialogResult.Yes)
                    {
                        Process xlsxExport = new Process();
                        xlsxExport.StartInfo.FileName = dlg.FileName;
                        xlsxExport.Start();
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Export baş tutmadı !");
                }

            }
            else
            {

            }
        }


        private void rTFToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dlg.Filter = "RTF file (.rtf)|*.rtf ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FilePath = dlg.FileName;
                    gridView1.ExportToRtf(FilePath);

                    var confirmationBox = MessageBox.Show("Export etdiyiniz fayli gormek isteyirsinizmi ?", "Tesdiqleme suali", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmationBox == DialogResult.Yes)
                    {
                        Process rtfExport = new Process();
                        rtfExport.StartInfo.FileName = dlg.FileName;
                        rtfExport.Start();
                    }
                    else
                    {

                    }
                }
                catch
                {
                    MessageBox.Show("Export baş tutmadı !");
                }

            }
            else
            {

            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            dlg.Filter = " Excel 2003 File (.xls)|*.xls ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FilePath = dlg.FileName;
                    gridViewPhysicalPerson.ExportToXls(FilePath);

                    var confirmationBox = MessageBox.Show("Export etdiyiniz fayli gormek isteyirsinizmi ?", "Tesdiqleme suali", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmationBox == DialogResult.Yes)
                    {
                        Process xlsExport = new Process();
                        xlsExport.StartInfo.FileName = dlg.FileName;
                        xlsExport.Start();
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Export baş tutmadı !");
                }

            }
            else
            {

            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            dlg.Filter = " Excel 2007 File (.xlsx)|*.xlsx ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FilePath = dlg.FileName;
                    gridViewPhysicalPerson.ExportToXlsx(FilePath);

                    var confirmationBox = MessageBox.Show("Export etdiyiniz fayli gormek isteyirsinizmi ?", "Tesdiqleme suali", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmationBox == DialogResult.Yes)
                    {
                        Process xlsxExport = new Process();
                        xlsxExport.StartInfo.FileName = dlg.FileName;
                        xlsxExport.Start();
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Export baş tutmadı !");
                }

            }
            else
            {

            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            dlg.Filter = "RTF file (.rtf)|*.rtf ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FilePath = dlg.FileName;
                    gridViewPhysicalPerson.ExportToRtf(FilePath);

                    var confirmationBox = MessageBox.Show("Export etdiyiniz fayli gormek isteyirsinizmi ?", "Tesdiqleme suali", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmationBox == DialogResult.Yes)
                    {
                        Process rtfExport = new Process();
                        rtfExport.StartInfo.FileName = dlg.FileName;
                        rtfExport.Start();
                    }
                    else
                    {

                    }
                }
                catch
                {
                    MessageBox.Show("Export baş tutmadı !");
                }

            }
            else
            {

            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            dlg.Filter = "Pdf File (.pdf)|*.pdf ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FilePath = dlg.FileName;
                    gridViewPhysicalPerson.ExportToPdf(FilePath);

                    var confirmationBox = MessageBox.Show("Export etdiyiniz faylı açmaq isteyirsinizmi ?", "Tesdiqləmə suali", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmationBox == DialogResult.Yes)
                    {
                        Process pdfExport = new Process();
                        pdfExport.StartInfo.FileName = dlg.FileName;
                        pdfExport.Start();
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Export baş tutmadı !");

                }
            }
            else
            {

            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            dlg.Filter = " Excel 2003 File (.xls)|*.xls ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FilePath = dlg.FileName;
                    gridViewLegalPerson.ExportToXls(FilePath);

                    var confirmationBox = MessageBox.Show("Export etdiyiniz fayli gormek isteyirsinizmi ?", "Tesdiqleme suali", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmationBox == DialogResult.Yes)
                    {
                        Process xlsExport = new Process();
                        xlsExport.StartInfo.FileName = dlg.FileName;
                        xlsExport.Start();
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Export baş tutmadı !");
                }

            }
            else
            {

            }
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            dlg.Filter = " Excel 2007 File (.xlsx)|*.xlsx ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FilePath = dlg.FileName;
                    gridViewLegalPerson.ExportToXlsx(FilePath);

                    var confirmationBox = MessageBox.Show("Export etdiyiniz fayli gormek isteyirsinizmi ?", "Tesdiqleme suali", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmationBox == DialogResult.Yes)
                    {
                        Process xlsxExport = new Process();
                        xlsxExport.StartInfo.FileName = dlg.FileName;
                        xlsxExport.Start();
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Export baş tutmadı !");
                }

            }
            else
            {

            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            dlg.Filter = "RTF file (.rtf)|*.rtf ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FilePath = dlg.FileName;
                    gridViewPhysicalPerson.ExportToRtf(FilePath);

                    var confirmationBox = MessageBox.Show("Export etdiyiniz fayli gormek isteyirsinizmi ?", "Tesdiqleme suali", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmationBox == DialogResult.Yes)
                    {
                        Process rtfExport = new Process();
                        rtfExport.StartInfo.FileName = dlg.FileName;
                        rtfExport.Start();
                    }
                    else
                    {

                    }
                }
                catch
                {
                    MessageBox.Show("Export baş tutmadı !");
                }

            }
            else
            {

            }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            dlg.Filter = "Pdf File (.pdf)|*.pdf ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FilePath = dlg.FileName;
                    gridViewLegalPerson.ExportToPdf(FilePath);

                    var confirmationBox = MessageBox.Show("Export etdiyiniz faylı açmaq isteyirsinizmi ?", "Tesdiqləmə suali", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmationBox == DialogResult.Yes)
                    {
                        Process pdfExport = new Process();
                        pdfExport.StartInfo.FileName = dlg.FileName;
                        pdfExport.Start();
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Export baş tutmadı !");

                }
            }
            else
            {

            }
        }

        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            EMPLOYEE user = cboUser.SelectedItem as EMPLOYEE;

            if (user != null)
            {

                idUser = _srvUser.GetAllUser(_businessParam).Where(c => c.ID_EMPLOYEE == user.ID_EMPLOYEE).Select(c => c.ID_USER).FirstOrDefault();


            }

        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            gridView1.ShowPrintPreview();
        }

        private void pnlSearchCadastreNo_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
