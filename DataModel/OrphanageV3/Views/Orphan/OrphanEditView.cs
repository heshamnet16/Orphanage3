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
using OrphanageDataModel.RegularData;

namespace OrphanageV3.Views.Orphan
{
    public partial class OrphanEditView : Telerik.WinControls.UI.RadForm
    {
        private OrphanViewModel _orphanViewModel = Program.Factory.Resolve<OrphanViewModel>();

        private IDataFormatterService _DataFormatterService = Program.Factory.Resolve<IDataFormatterService>();

        private IAutoCompleteService _AutoCompleteServic = Program.Factory.Resolve<IAutoCompleteService>();

        private ITranslateService _TranslateService = Program.Factory.Resolve<ITranslateService>();

        private IEntityValidator _OrphanEntityValidator = Program.Factory.Resolve<IEntityValidator>();

        private IEntityValidator _NameEntityValidator = Program.Factory.Resolve<IEntityValidator>();

        private OrphanageDataModel.Persons.Orphan _CurrentOrphan = null;

        private bool _CertificatePhotoChanged = false;
        private bool _CertificatePhoto2Changed = false;
        private bool _HealthPhotoChanged = false;

        public OrphanEditView(int orphanId)
        {
            InitializeComponent();
            _AutoCompleteServic.DataLoaded += _AutoCompleteServic_DataLoaded;
            SetLablesString();
            loadOrphan(orphanId);
            nameForm1.AutoCompleteService = _AutoCompleteServic;
            nameForm1.DataFormatterService = _DataFormatterService;
            nameForm1.EntityValidator = _NameEntityValidator;
        }
        private async void loadOrphan(int Oid)
        {
            _CurrentOrphan = await _orphanViewModel.getOrphan(Oid);
            SetValues();
            nameForm1.NameDataSource = _CurrentOrphan.Name;
            _OrphanEntityValidator.controlCollection = Controls;
            _OrphanEntityValidator.DataEntity = orphanBindingSource.DataSource;
        }
        private void _AutoCompleteServic_DataLoaded(object sender, EventArgs e)
        {
            //fired when string is loaded
            AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
            var stringArray = _AutoCompleteServic.EnglishNameStrings.ToArray();

           txtHSicknessName.AutoCompleteItems.AddRange(_AutoCompleteServic.SicknessNames.ToArray());
           txtHMedicen.AutoCompleteItems.AddRange(_AutoCompleteServic.MedicenNames.ToArray()); 
            txtSReason.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.EducationReasons.ToArray()); 
            txtSStudyStage.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.EducationStages.ToArray());
            txtSschoolNAme.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.EducationSchools.ToArray()); 
            txtOPlaceOfBirth.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.BirthPlaces.ToArray()); 
            txtOStory.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.OrphanStories.ToArray()); 

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
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Clear();
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple1);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple2);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple3);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple4);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple5);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple6);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple7);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple8);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple9);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple10);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple11);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple12);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple13);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple14);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple15);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple16);
            txtOConsanguinityToCaregiver.AutoCompleteCustomSource.Add(Properties.Resources.OrphanConsanguinityToCaregiverSimple17);
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
            if (_CurrentOrphan == null) return;
            orphanBindingSource.DataSource = _CurrentOrphan;
            RadPageView1.SelectedPage = pgeBasic;
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
                EnabledDisHealthControls(true);
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
                EnabledDisHealthControls(false);
            }
            //Education 
            RadPageView1.SelectedPage = pgeEducation;
            if (_CurrentOrphan.EducationId.HasValue)
            {
                if (_CurrentOrphan.Education.Stage.Contains(Properties.Resources.EducationNonStudyKeyword))
                {
                    EnabledDisEducationControls(false);
                }
                else
                {
                    EnabledDisEducationControls(true);
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
                EnabledDisEducationControls(false);
            }
            RadPageView1.SelectedPage = pgeBasic;

            _CertificatePhotoChanged = false;
            _CertificatePhoto2Changed = false;
            _HealthPhotoChanged = false;
        }

        private Health fillHealth(Health health)
        {
            if (health == null) return null;

            if ((numHCost.Value > 0))
            {
                health.Cost = numHCost.Value;
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
            health.ReporteFileURI = "api/orphan/media/healthreport/" + _CurrentOrphan.Id;
            health.SupervisorDoctor = txtHDoctorName.Text;
            health.Note = txtHNote.Text;
            return health;
        }

        private void SaveHealth()
        {
            //if (!IsLoaded) return;
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
            study.CertificateImageURI = "api/orphan/media/education/" + _CurrentOrphan.Id;
            study.CertificatePhotoBack = PicSstudyCerti.PhotoAsBytes;
            study.CertificateImage2URI = "api/orphan/media/education2/" + _CurrentOrphan.Id;
            if ((numSDegreesRate.Value > 0))
            {
                study.DegreesRate = numSDegreesRate.Value;
            }
            else
            {
                study.DegreesRate = null;
            }

            if ((numSMonthlyCost.Value > 0))
            {
                study.MonthlyCost = numSMonthlyCost.Value;
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
            if (_CurrentOrphan.Education == null)
            {
                // add new education record
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

        private void EnabledDisHealthControls(bool value)
        {
            picHFace.Enabled = value;
            txtHNote.Enabled = value;
            txtHDoctorName.Enabled = value;
            txtHMedicen.Enabled = value;
            txtHSicknessName.Enabled = value;
            numHCost.Enabled = value;
            grpHPicReporte.Enabled = value;
            chkHIsSick.ToggleStateChanged -= chkHIsSick_ToggleStateChanged;
            chkHIsSick.ToggleState = value ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
            chkHIsSick.ToggleStateChanged += chkHIsSick_ToggleStateChanged;
        }
        private void chkHIsSick_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                EnabledDisHealthControls(true);
            }
            else
            {
                EnabledDisHealthControls(false);
            }
            SaveHealth();
        }

        private void EnabledDisEducationControls(bool value)
        {
            txtSNote.Enabled = value;
            txtSschoolNAme.Enabled = value;
            txtSStudyStage.Enabled = value;
            numSDegreesRate.Enabled = value;
            numSMonthlyCost.Enabled = value;
            picSStarter.Enabled = value;
            PicSstudyCerti.Enabled = value;
            grpEducationCertificate1.Enabled = value;
            grpEducationCertificate2.Enabled = value;
            txtSStudyStage.Text = value ? "" : Properties.Resources.EducationStageDefaultString;
            chkSisStudy.ToggleStateChanged -= chkSisStudy_ToggleStateChanged;
            chkSisStudy.ToggleState = value ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
            chkSisStudy.ToggleStateChanged += chkSisStudy_ToggleStateChanged;
        }
        private void chkSisStudy_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                EnabledDisEducationControls(true);
            }
            else
            {
                EnabledDisEducationControls(false);
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
            //_CurrentOrphan.Name = _ControllsHelper.GetNameFromForm(nameForm1);
            txtOName.Text = _DataFormatterService.GetFullNameString(_CurrentOrphan.Name);
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
        }

        private async void PhotoChanged(object sender, EventArgs e)
        {
            if (sender is PictureSelector.PictureSelector)
            {
                string url = null;
                Image img = null;
                var picSelector = (PictureSelector.PictureSelector)sender;
                picSelector.ShowLoadingGif();
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
                    _HealthPhotoChanged = true;
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
                    _CertificatePhotoChanged = true;
                }
                if (picSelector.Name == "PicSstudyCerti")
                {
                    _CertificatePhoto2Changed = true;
                }
                if (url != null)
                {
                    await _orphanViewModel.SaveImage(url, img);
                    picSelector.HideLoadingGif();
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            SaveHealth();
            SaveStudy();
            _CurrentOrphan.Name = (Name)nameForm1.NameDataSource;
            if (_OrphanEntityValidator.IsValid())
            {
                await _orphanViewModel.Save(_CurrentOrphan);

                //var savedOrphan = await _orphanViewModel.getOrphan(_CurrentOrphan.Id.Value);

                if (_CurrentOrphan.HealthStatus != null && _HealthPhotoChanged)
                    await _orphanViewModel.SaveImage(_CurrentOrphan.HealthStatus.ReporteFileURI, picHFace.Photo);

                if (_CurrentOrphan.Education != null)
                {
                    if (_CertificatePhotoChanged)
                        await _orphanViewModel.SaveImage(_CurrentOrphan.Education.CertificateImageURI, picSStarter.Photo);
                    if (_CertificatePhoto2Changed)
                        await _orphanViewModel.SaveImage(_CurrentOrphan.Education.CertificateImage2URI, PicSstudyCerti.Photo);
                }

                this.Close();
            }
            else
            {
                ValidateAndShowErrors();
            }
        }
        private void ValidateAndShowErrors()
        {
            _OrphanEntityValidator.controlCollection = Controls;
            _OrphanEntityValidator.DataEntity = orphanBindingSource.DataSource;
            _OrphanEntityValidator.SetErrorProvider(OrphanerrorProvider1);

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

