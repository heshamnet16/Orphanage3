using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using OrphanageV3.Extensions;
using OrphanageV3.ViewModel.Orphan;
using Unity;
using OrphanageV3.Views.Helper.Interfaces;
using OrphanageV3.Services.Interfaces;
using System.Linq;
using OrphanageV3.Services;

namespace OrphanageV3.Views.Orphan
{
    public partial class OrphanEditView : Telerik.WinControls.UI.RadForm
    {
        private OrphanViewModel _orphanViewModel = Program.Factory.Resolve<OrphanViewModel>();

        private IControllsHelper _ControllsHelper = Program.Factory.Resolve<IControllsHelper>();

        private IDataFormatterService _DataFormatterService = Program.Factory.Resolve<IDataFormatterService>();

        private IAutoCompleteService _AutoCompleteServic = Program.Factory.Resolve<IAutoCompleteService>();

        private ITranslateService _TranslateService = Program.Factory.Resolve<ITranslateService>();

        private Services.Orphan _CurrentOrphan = null;

        private bool IsLoaded = false;

        public OrphanEditView(Services.Orphan orphan)
        {
            InitializeComponent();
            _AutoCompleteServic.DataLoaded += _AutoCompleteServic_DataLoaded;
            SetLablesString();
            _CurrentOrphan = orphan;
        }

        private void _AutoCompleteServic_DataLoaded(object sender, EventArgs e)
        {
            //fired when string is loaded
            AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
            var stringArray = _AutoCompleteServic.EnglishNameStrings.ToArray();

            this.Invoke(new MethodInvoker(() => { nameForm1.English_First_TextBox.AutoCompleteCustomSource.AddRange(stringArray); }));
            this.Invoke(new MethodInvoker(() => { nameForm1.English_Father_TextBox.AutoCompleteCustomSource.AddRange(stringArray); }));
            this.Invoke(new MethodInvoker(() => { nameForm1.English_Last_TextBox.AutoCompleteCustomSource.AddRange(stringArray); }));
            this.Invoke(new MethodInvoker(() => { txtHSicknessName.AutoCompleteItems.AddRange(_AutoCompleteServic.SicknessNames.ToArray()); }));
            this.Invoke(new MethodInvoker(() => { txtHMedicen.AutoCompleteItems.AddRange(_AutoCompleteServic.MedicenNames.ToArray()); }));
            this.Invoke(new MethodInvoker(() => { txtSReason.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.EducationReasons.ToArray()); }));
            this.Invoke(new MethodInvoker(() => { txtSStudyStage.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.EducationStages.ToArray()); }));
            this.Invoke(new MethodInvoker(() => { txtSschoolNAme.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.EducationSchools.ToArray()); }));
            this.Invoke(new MethodInvoker(() => { txtOBirthPlace.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.BirthPlaces.ToArray()); }));
            this.Invoke(new MethodInvoker(() => { txtOStory.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.OrphanStories.ToArray()); }));

        }

        private void SetLablesString()
        {
            lblAge.Text = Properties.Resources.Age.getDobblePunkt();
            lblBirthday.Text = Properties.Resources.Birthday.getDobblePunkt();
            lblBirthPlace.Text = Properties.Resources.BirthPlace.getDobblePunkt();
            lblCivilRegisterNumber.Text = Properties.Resources.CivilRegisterNumber.getDobblePunkt();
            lblCM.Text = Properties.Resources.Centimeter;
            lblConsanguinityToCaregiver.Text = Properties.Resources.ConsanguinityToCaregiver.getDobblePunkt();
            lblDoctor.Text = Properties.Resources.HealthDoctor.getDobblePunkt();
            lblFootSize.Text = Properties.Resources.FootSize.getDobblePunkt();
            lblGender.Text = Properties.Resources.Gender.getDobblePunkt();
            lblGradRate.Text = Properties.Resources.EducationAvaregeGrade.getDobblePunkt();
            lblHealthCost.Text = Properties.Resources.Cost.getDobblePunkt();
            lblHealthNote.Text = Properties.Resources.Notes.getDobblePunkt();
            lblIdentityNumber.Text = Properties.Resources.IdentityCardNumber.getDobblePunkt();
            lblIsSick.Text = Properties.Resources.IsSick.getDobblePunkt();
            lblIsStudying.Text = Properties.Resources.IsStudying.getDobblePunkt();
            lblKG.Text = Properties.Resources.Kilogramm;
            lblMedicen.Text = Properties.Resources.HealthMedicen.getDobblePunkt();
            lblMonthlyCost.Text = Properties.Resources.EducationMonthlyCost.getDobblePunkt();
            lblName.Text = Properties.Resources.FullName.getDobblePunkt();
            lblNotes.Text = Properties.Resources.Notes.getDobblePunkt();
            lblReason.Text = Properties.Resources.Reason.getDobblePunkt();
            lblSchoolName.Text = Properties.Resources.EducationSchoolName.getDobblePunkt();
            lblSicknessName.Text = Properties.Resources.HealthSicknessName.getDobblePunkt();
            lblStory.Text = Properties.Resources.Story.getDobblePunkt();
            lblStudyStage.Text = Properties.Resources.EducationStage.getDobblePunkt();
            lblTallness.Text = Properties.Resources.Tallness.getDobblePunkt();
            lblWeight.Text = Properties.Resources.Weight.getDobblePunkt();
            grpEducationCertificate1.Text = Properties.Resources.EducationCertificateImage1.getDobblePunkt();
            grpEducationCertificate2.Text = Properties.Resources.EducationCertificateImage2.getDobblePunkt();
            grpFacePhoto.Text = Properties.Resources.FacePhoto.getDobblePunkt();
            grpFamilyCardPhoto.Text = Properties.Resources.FamilyCardPhoto.getDobblePunkt();
            grpFullPhoto.Text = Properties.Resources.BodyPhoto.getDobblePunkt();
            grpHPicReporte.Text = Properties.Resources.HealthReportFilePhoto.getDobblePunkt();
            grpBirthCertificatePhoto.Text = Properties.Resources.BirthCertificatePhoto.getDobblePunkt();
            cmbOGender.Items.Clear();
            cmbOGender.Items.Add(Properties.Resources.FemaleString);
            cmbOGender.Items.Add(Properties.Resources.MaleString);
            txtOBondsManRelation.AutoCompleteCustomSource.Clear();
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple1);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple2);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple3);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple4);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple5);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple6);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple7);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple8);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple9);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple10);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple11);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple12);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple13);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple14);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple15);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple16);
            txtOBondsManRelation.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple17);
            pgeBasic.Text = Properties.Resources.BasicData;
            pgeEducation.Text = Properties.Resources.EducationData;
            pgeHealth.Text = Properties.Resources.HealthData;
            pgeOthers.Text = Properties.Resources.OtherData;
            txtSReason.NullText = Properties.Resources.EducationReasonNullText;
            txtSschoolNAme.NullText = Properties.Resources.EducationSchoolNameNullText;
            txtSStudyStage.NullText = Properties.Resources.EducationStageNullString;
            txtSStudyStage.Text = Properties.Resources.EducationStageDefaultString;
            txtHSicknessName.NullText = Properties.Resources.HealthSicknessNameNullText;
            txtHMedicen.NullText = Properties.Resources.HealthMedicenNullText;
            txtHDoctorName.NullText = Properties.Resources.HealthDoctorNullText;
            Text = Properties.Resources.OrphanEditViewTitle;
            btnSave.Text = Properties.Resources.SaveText;
            btnCancel.Text = Properties.Resources.CancelText;
            btnBondsMan.Text = Properties.Resources.ChangeCaregiverText;
        }

        private void SetValues()
        {
            orphanBindingSource.DataSource = _CurrentOrphan;
            RadPageView1.SelectedPage = pgeBasic;
            _ControllsHelper.SetNameForm(ref nameForm1, _CurrentOrphan.Name);
            txtOName.Text = _DataFormatterService.GetFullNameString(_CurrentOrphan.Name);
            var ageString = _TranslateService.DateToString(_CurrentOrphan.Birthday);
            txtOAge.Text = ageString;
            PicBody.SetImageByBytes(_CurrentOrphan.FullPhotoData);
            picFace.SetImageByBytes(_CurrentOrphan.FacePhotoData);
            RadPageView1.SelectedPage = pgeOthers;
            picObirthCertificate.SetImageByBytes(_CurrentOrphan.BirthCertificatePhotoData);
            picOFamilyCardPhoto.SetImageByBytes(_CurrentOrphan.FamilyCardPagePhotoData);
            //Health
            RadPageView1.SelectedPage = pgeHealth;
            if (_CurrentOrphan.HealthId.HasValue)
            {
                chkHIsSick.Checked = true;
                chkHIsSick_ToggleStateChanged(null, new Telerik.WinControls.UI.StateChangedEventArgs(Telerik.WinControls.Enumerations.ToggleState.On));
                txtHDoctorName.Text = _CurrentOrphan.HealthStatus.SupervisorDoctor;
                if (_CurrentOrphan.HealthStatus.Medicine != null)
                {
                    txtHMedicen.Text = _CurrentOrphan.HealthStatus.Medicine.Replace(";", "+");
                    txtHMedicen.Text += "+";
                }
                if (_CurrentOrphan.HealthStatus.SicknessName != null)
                {
                    txtHSicknessName.Text = _CurrentOrphan.HealthStatus.SicknessName.Replace(";", "+");
                    txtHSicknessName.Text += "+";
                }
                txtHNote.Text = _CurrentOrphan.HealthStatus.Note;
                if (_CurrentOrphan.HealthStatus.Cost.HasValue)
                {
                    numHCost.Value = (decimal)_CurrentOrphan.HealthStatus.Cost.Value;
                }
                picHFace.SetImageByBytes(_CurrentOrphan.HealthStatus.ReporteFileData);
            }
            else
            {
                chkHIsSick.Checked = false;
                chkHIsSick_ToggleStateChanged(null, new Telerik.WinControls.UI.StateChangedEventArgs(Telerik.WinControls.Enumerations.ToggleState.Off));
            }            
            //Education 
            RadPageView1.SelectedPage = pgeEducation;
            if (_CurrentOrphan.EducationId.HasValue)
            {
                if(_CurrentOrphan.Education.Stage.Contains(Properties.Resources.EducationNonStudyKeyword))
                {
                    chkSisStudy.Checked = false;
                    chkSisStudy_ToggleStateChanged(null, new Telerik.WinControls.UI.StateChangedEventArgs(Telerik.WinControls.Enumerations.ToggleState.Off));
                }
                else
                {
                    chkSisStudy.Checked = true;
                    chkSisStudy_ToggleStateChanged(null, new Telerik.WinControls.UI.StateChangedEventArgs(Telerik.WinControls.Enumerations.ToggleState.On));
                    txtSNote.Text = _CurrentOrphan.Education.Note;
                    txtSschoolNAme.Text = _CurrentOrphan.Education.School;
                    if (_CurrentOrphan.Education.DegreesRate.HasValue)
                        numSDegreesRate.Value = (decimal)_CurrentOrphan.Education.DegreesRate.Value;
                    if (_CurrentOrphan.Education.MonthlyCost.HasValue)
                        numSMonthlyCost.Value = (decimal)(_CurrentOrphan.Education.MonthlyCost.Value);
                    picSStarter.SetImageByBytes(_CurrentOrphan.Education.CertificatePhotoFront);
                    PicSstudyCerti.SetImageByBytes(_CurrentOrphan.Education.CertificatePhotoBack);
                }
                txtSStudyStage.Text = _CurrentOrphan.Education.Stage;
                txtSReason.Text = _CurrentOrphan.Education.Reasons;
            }
            else
            {
                chkSisStudy.Checked = false;
                chkSisStudy_ToggleStateChanged(null, new Telerik.WinControls.UI.StateChangedEventArgs(Telerik.WinControls.Enumerations.ToggleState.Off));
            }
            RadPageView1.SelectedPage = pgeBasic;
        }

        private Health fillHealth(Health health)
        {
            if (health == null) return null;

            if ((numHCost.Value > 0))
            {
                health.Cost = (double)numHCost.Value;
            }

            string Medic = "";
            string Sickne = "";
            if (!(txtHMedicen.Items == null) && (txtHMedicen.Items.Count > 0))
            {
                foreach (var itm in txtHMedicen.Items)
                {
                    Medic += itm.Text + ";";
                }

            }

            if ((!(Medic == null) && (Medic.Length > 0)))
            {
                Medic = Medic.Substring(0, Medic.Length - 1);
                health.Medicine = Medic;
            }

            if (!(txtHSicknessName.Items == null) && (txtHSicknessName.Items.Count > 0))
            {
                foreach (var itm in txtHSicknessName.Items)
                {
                    Sickne += itm.Text + ";";
                }

            }

            if ((!(Sickne == null)
                        && (Sickne.Length > 0)))
            {
                Sickne = Sickne.Substring(0, (Sickne.Length - 1));
                health.SicknessName = Sickne;
            }

            health.ReporteFileData = picHFace.PhotoAsBytes;
            health.SupervisorDoctor = txtHDoctorName.Text;
            health.Note = txtHNote.Text;
            return health;
        }

        private void SaveHealth()
        {
            if (!IsLoaded) return;
            if (!_CurrentOrphan.HealthId.HasValue)
            {
                if (chkHIsSick.Checked)
                {
                    Health hlth = new Health();
                    hlth = fillHealth(hlth);
                    _CurrentOrphan.HealthStatus = hlth;
                }
            }
            else
            {
                if (chkHIsSick.Checked && _CurrentOrphan.HealthStatus != null)
                {
                    _CurrentOrphan.HealthStatus = fillHealth(_CurrentOrphan.HealthStatus);
                }
                else
                {
                    _CurrentOrphan.HealthStatus = null;
                }
            }
        }

        private Study fillStudy(Study study)
        {
            study.Note = txtSNote.Text;
            study.CertificatePhotoFront = picSStarter.PhotoAsBytes;
            study.CertificatePhotoBack = PicSstudyCerti.PhotoAsBytes;
            if ((numSDegreesRate.Value > 0))
            {
                study.DegreesRate = (double)numSDegreesRate.Value;
            }
            else
            {
                study.DegreesRate = null;
            }

            if ((numSMonthlyCost.Value > 0))
            {
                study.MonthlyCost = (double)numSMonthlyCost.Value;
            }
            else
            {
                study.MonthlyCost = null;
            }

            study.School = txtSschoolNAme.Text;
            study.Stage = txtSStudyStage.Text;
            study.Reasons = txtSReason.Text;
            return study;
        }

        private void SaveStudy()
        {
            if (!IsLoaded) return;
            if (_CurrentOrphan.Education == null)
            {
                if (chkSisStudy.Checked)
                {
                    // create new study record
                    Study stud = new Study();
                    stud = fillStudy(stud);
                    stud.Reasons = null;
                    _CurrentOrphan.Education = stud;
                }
                else
                {
                    // create new non study record
                    Study stud = new Study();
                    stud.Stage = txtSStudyStage.Text;
                    stud.Reasons = txtSReason.Text;
                    _CurrentOrphan.Education = stud;
                }

            }
            else
            {
                if (chkSisStudy.Checked)
                {
                    // update the record to study
                    var stud = fillStudy(_CurrentOrphan.Education);
                    stud.Reasons = null;
                    _CurrentOrphan.Education = stud;
                }
                else
                {
                    // update the record to non study
                    _CurrentOrphan.Education.Stage = txtSStudyStage.Text;
                    _CurrentOrphan.Education.Reasons = txtSReason.Text;
                    _CurrentOrphan.Education.Note = null;
                    _CurrentOrphan.Education.CertificatePhotoFront = null;
                    _CurrentOrphan.Education.CertificatePhotoBack = null;
                    _CurrentOrphan.Education.DegreesRate = null;
                    _CurrentOrphan.Education.MonthlyCost = null;
                    _CurrentOrphan.Education.School = null;
                }
            }
        }
        private void chkHIsSick_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                picHFace.Enabled = true;
                txtHNote.Enabled = true;
                txtHDoctorName.Enabled = true;
                txtHMedicen.Enabled = true;
                txtHSicknessName.Enabled = true;
                numHCost.Enabled = true;
                grpHPicReporte.Enabled = true;
            }
            else
            {
                grpHPicReporte.Enabled = false;
                picHFace.Enabled = false;
                txtHNote.Enabled = false;
                txtHDoctorName.Enabled = false;
                txtHMedicen.Enabled = false;
                txtHSicknessName.Enabled = false;
                numHCost.Enabled = false;
            }
            SaveHealth();
        }

        private void chkSisStudy_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                txtSNote.Enabled = true;
                txtSschoolNAme.Enabled = true;
                txtSStudyStage.Enabled = true;
                numSDegreesRate.Enabled = true;
                numSMonthlyCost.Enabled = true;
                picSStarter.Enabled = true;
                PicSstudyCerti.Enabled = true;
                grpEducationCertificate1.Enabled = true;
                grpEducationCertificate2.Enabled = true;
                txtSStudyStage.Text = "";
            }
            else
            {
                grpEducationCertificate1.Enabled = false;
                grpEducationCertificate2.Enabled = false;
                txtSNote.Enabled = false;
                txtSschoolNAme.Enabled = false;
                txtSStudyStage.Enabled = false;
                numSDegreesRate.Enabled = false;
                numSMonthlyCost.Enabled = false;
                picSStarter.Enabled = false;
                PicSstudyCerti.Enabled = false;
                txtSStudyStage.Text = Properties.Resources.EducationStageDefaultString;
            }
            SaveStudy();
        }

        private void txtOName_Enter(object sender, EventArgs e)
        {
            nameForm1.ShowMe();
        }

        private void pgeBasic_Click(object sender, EventArgs e)
        {
            nameForm1.HideMe();
        }

        private void OrphanEditView_Load(object sender, EventArgs e)
        {
            SetValues();
            picFace.PhotoChanged += PhotoChanged;
            PicBody.PhotoChanged += PhotoChanged;
            picHFace.PhotoChanged += PhotoChanged;
            picObirthCertificate.PhotoChanged += PhotoChanged;
            picOFamilyCardPhoto.PhotoChanged += PhotoChanged;
            picSStarter.PhotoChanged += PhotoChanged;
            PicSstudyCerti.PhotoChanged += PhotoChanged;
            IsLoaded = true;
        }

        private void PhotoChanged(object sender, EventArgs e)
        {
            if (sender is PictureSelector.PictureSelector)
            {
                string url = null;
                Image img = null;
                var picSelector = (PictureSelector.PictureSelector)sender;
                if (picSelector.Name == "picFace")
                {
                    url = _CurrentOrphan.FacePhotoURI;
                    img = picFace.Photo;
                }
                if (picSelector.Name == "PicBody")
                {
                    url = _CurrentOrphan.FullPhotoURI;
                    img = PicBody.Photo;
                }
                if (picSelector.Name == "picHFace")
                {
                    url = _CurrentOrphan.HealthStatus.ReporteFileURI;
                    img = picHFace.Photo;
                }
                if (picSelector.Name == "picObirthCertificate")
                {
                    url = _CurrentOrphan.BirthCertificatePhotoURI;
                    img = picObirthCertificate.Photo;
                }
                if (picSelector.Name == "picOFamilyCardPhoto")
                {
                    url = _CurrentOrphan.FamilyCardPagePhotoURI;
                    img = picOFamilyCardPhoto.Photo;
                }
                if (picSelector.Name == "picSStarter")
                {
                    url = _CurrentOrphan.Education.CertificateImageURI;
                    img = picSStarter.Photo;
                }
                if (picSelector.Name == "PicSstudyCerti")
                {
                    url = _CurrentOrphan.Education.CertificateImage2URI;
                    img = PicSstudyCerti.Photo;
                }
                if (url != null)
                {
                    _orphanViewModel.UploadImage(url, img);
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            SaveHealth();
            SaveStudy();
           await _orphanViewModel.Save(_CurrentOrphan);
            this.Close();
        }
    }
}

