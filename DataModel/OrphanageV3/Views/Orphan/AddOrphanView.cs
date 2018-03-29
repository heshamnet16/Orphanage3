using OrphanageV3.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using Unity;
using OrphanageV3.Services.Interfaces;
using OrphanageV3.Views.Helper.Interfaces;
using OrphanageDataModel.RegularData;
using LangChanger;
using System.Threading.Tasks;

namespace OrphanageV3.Views.Orphan
{
    public partial class AddOrphanView : Telerik.WinControls.UI.RadForm
    {
        private ViewModel.Family.FamilyModel _familyModel = null;
        private OrphanageDataModel.RegularData.Family _family = null;
        private List<object> _families = null;
        private ViewModel.Family.FamiliesViewModel _FamiliesViewModel = null;

        private List<object> _caregivers = null;
        private ViewModel.Caregiver.CaregiversViewModel _CaregiversViewModel = null;
        private ViewModel.Caregiver.CaregiverEditViewModel _caregiverEditViewModel = null;
        private ViewModel.Mother.MotherEditViewModel _motherEditViewModel = null;

        private OrphanageDataModel.Persons.Caregiver _BrothersCaregiver = null;
        private OrphanageDataModel.Persons.Mother _mother = null;
        private OrphanageDataModel.Persons.Caregiver _caregiver = null;

        private ViewModel.Orphan.OrphanViewModel _orphanViewModel = null;

        private IEntityValidator _entityValidator;
        private IAutoCompleteService _AutoCompleteServic = null;

        private OrphanageDataModel.Persons.Orphan _orphan = null;

        public AddOrphanView()
        {
            InitializeComponent();
            _CaregiversViewModel = Program.Factory.Resolve<ViewModel.Caregiver.CaregiversViewModel>();
            _CaregiversViewModel.DataLoaded += CaregiverDataLoaded;
            _CaregiversViewModel.LoadCaregivers();

            _FamiliesViewModel = Program.Factory.Resolve<ViewModel.Family.FamiliesViewModel>();
            _FamiliesViewModel.DataLoaded += FamiliesDataLoaded;
            _FamiliesViewModel.LoadFamilies();

            _orphanViewModel = Program.Factory.Resolve<ViewModel.Orphan.OrphanViewModel>();
            _caregiverEditViewModel = Program.Factory.Resolve<ViewModel.Caregiver.CaregiverEditViewModel>();

            _motherEditViewModel = Program.Factory.Resolve<ViewModel.Mother.MotherEditViewModel>();
            TranslateControls();

            _orphan = new OrphanageDataModel.Persons.Orphan();
            _orphan.Name = new OrphanageDataModel.RegularData.Name();
            _orphan.Education = new Study();
            orphanBindingSource.DataSource = _orphan;
            orphanNameForm.NameDataSource = _orphan.Name;
            studyBindingSource.DataSource = _orphan.Education;
            _caregiver = new OrphanageDataModel.Persons.Caregiver();
            _caregiver.Name = new OrphanageDataModel.RegularData.Name();
            _caregiver.Address = new OrphanageDataModel.RegularData.Address();
            caregiverBindingSource.DataSource = _caregiver;
            caregiverNameForm.NameDataSource = _caregiver.Name;
            caregiverAddressForm.AddressDataSource = _caregiver.Address;

            _entityValidator = Program.Factory.Resolve<IEntityValidator>();
            _AutoCompleteServic = Program.Factory.Resolve<IAutoCompleteService>();
            _AutoCompleteServic.DataLoaded += _AutoCompleteServic_DataLoaded;

            radWaitingBar1.StartWaiting();

            DisableEnableEducationControls(false);
            DisableEnableHealthControls(false);

            radWizard1.FinishButton.Click += FinishButton_Click;
            radWizard1.CancelButton.Click += FinishButton_Click;
        }

        private void _AutoCompleteServic_DataLoaded(object sender, EventArgs e)
        {
            AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
            var stringArray = _AutoCompleteServic.EnglishNameStrings.ToArray();

            txtHSicknessName.AutoCompleteItems.AddRange(_AutoCompleteServic.SicknessNames.ToArray());
            txtHMedicen.AutoCompleteItems.AddRange(_AutoCompleteServic.MedicenNames.ToArray());
            txtEducationReasons.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.EducationReasons.ToArray());
            txtEducationStage.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.EducationStages.ToArray());
            txtEducationSchool.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.EducationSchools.ToArray());
            txtOPlaceOfBirth.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.BirthPlaces.ToArray());
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void orphanValidateAndShowError()
        {
            if (_entityValidator != null)
            {
                _entityValidator.controlCollection = pnlOrphan.Controls;
                _entityValidator.DataEntity = orphanBindingSource.DataSource;
                _entityValidator.SetErrorProvider(errorProvider1);
                orphanNameForm.ValidateAndShowError();
            }
        }

        private void educationValidateAndShowError()
        {
            if (_entityValidator != null)
            {
                _entityValidator.controlCollection = pnlEducation.Controls;
                _entityValidator.DataEntity = studyBindingSource.DataSource;
                _entityValidator.SetErrorProvider(errorProvider1);
            }
        }

        private void caregiverValidateAndShowError()
        {
            if (_entityValidator != null)
            {
                _entityValidator.controlCollection = pnlCaregiver.Controls;
                _entityValidator.DataEntity = caregiverBindingSource.DataSource;
                _entityValidator.SetErrorProvider(errorProvider1);
                caregiverNameForm.ValidateAndShowError();
            }
        }

        private void FamiliesDataLoaded(object sender, EventArgs e)
        {
            btnFamilyBrowse.Enabled = true;
            _families = new List<object>(_FamiliesViewModel.Families);
        }

        private void CaregiverDataLoaded(object sender, EventArgs e)
        {
            _caregivers = new List<object>(_CaregiversViewModel.Caregivers);
            optChooseCaregiver.Enabled = true;
        }

        private void TranslateControls()
        {
            this.Text = Properties.Resources.AddNewFamily;
            pgeCaregiver.Title = Properties.Resources.AddCaregiver;
            pgeCaregiverOtherData.Title = Properties.Resources.AddFamilyOtherData;
            pgeEducation.Title = Properties.Resources.EducationData;
            pgeHealth.Title = Properties.Resources.HealthData;
            wizardCompletionPage1.Title = Properties.Resources.Summary;
            pgeProgress.Title = Properties.Resources.Progress;
            pgeOrphan.Title = Properties.Resources.AddOrphan;

            lblFatherName.Text = Properties.Resources.FatherName.getDobblePunkt();
            lblMotherName.Text = Properties.Resources.MotherFirstName.getDobblePunkt();
            lblOrphanBirthday.Text = Properties.Resources.Birthday.getDobblePunkt();
            lblOrphanCivilRegisterNumber.Text = Properties.Resources.CivilRegisterNumber.getDobblePunkt();
            lblOrphanConsanguinityToCaregiver.Text = Properties.Resources.ConsanguinityToCaregiver.getDobblePunkt();
            lblOrphanFootSize.Text = Properties.Resources.FootSize.getDobblePunkt();
            lblOrphanGender.Text = Properties.Resources.Gender.getDobblePunkt();
            lblOrphanIdentityCardNumber.Text = Properties.Resources.IdentityCardNumber.getDobblePunkt();
            lblOrphanPlaceOfBirth.Text = Properties.Resources.BirthPlace.getDobblePunkt();
            lblOrphanTallness.Text = Properties.Resources.Tallness.getDobblePunkt();
            lblOrphanWeight.Text = Properties.Resources.Weight.getDobblePunkt();
            grpOrphanBasicData.Text = grpCaregiverBaiscData.Text = Properties.Resources.BasicData;
            grpOrphanFamily.Text = Properties.Resources.ChooseFamily;
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

            //Orphan Education
            lblEducationAvaregeGrads.Text = Properties.Resources.EducationAvaregeGrade.getDobblePunkt();
            lblEducationIsStudying.Text = Properties.Resources.IsStudying.getDobblePunkt();
            lblEducationMonthlyCost.Text = Properties.Resources.EducationMonthlyCost.getDobblePunkt();
            lblEducationNote.Text = lblCaregiverNote.Text =
                lblCaregiverNote.Text = lblHealthNote.Text = Properties.Resources.Notes.getDobblePunkt();
            lblEducationReason.Text = Properties.Resources.Reason.getDobblePunkt();
            lblEducationSchoolName.Text = Properties.Resources.EducationSchoolName.getDobblePunkt();
            lblEducationStudyStage.Text = Properties.Resources.EducationStage.getDobblePunkt();
            grpEducation.Text = Properties.Resources.EducationData;
            grpOBodyPhoto.Text = Properties.Resources.BodyPhoto;
            grpOFacePhoto.Text = Properties.Resources.FacePhoto;
            grpPicCertificate1.Text = Properties.Resources.EducationCertificateImage1;
            grpPicCertificate2.Text = Properties.Resources.EducationCertificateImage2;
            txtEducationSchool.NullText = Properties.Resources.EducationSchoolNameNullText;
            txtEducationReasons.NullText = Properties.Resources.EducationReasonNullText;
            txtEducationStage.NullText = Properties.Resources.EducationStageNullString;
            txtOrphanGender.Items.Add(Properties.Resources.FemaleString);
            txtOrphanGender.Items.Add(Properties.Resources.MaleString);

            //Orphan Health
            lblHealthDoctorName.Text = Properties.Resources.HealthDoctor.getDobblePunkt();
            lblHealthIsSick.Text = Properties.Resources.IsSick.getDobblePunkt();
            lblHealthMedicaments.Text = Properties.Resources.HealthMedicen.getDobblePunkt();
            lblHealthMonthlyCost.Text = Properties.Resources.EducationMonthlyCost.getDobblePunkt();
            lblHealthSicknessName.Text = Properties.Resources.HealthSicknessName.getDobblePunkt();
            grpHealth.Text = Properties.Resources.HealthData;
            grpHealthReportePhoto.Text = Properties.Resources.HealthReportFilePhoto;
            grpOrphanFamilyCardPhoto.Text = Properties.Resources.FamilyCardPhoto;
            grpOrphanBirthCertificate.Text = Properties.Resources.BirthCertificatePhoto;
            txtHSicknessName.NullText = Properties.Resources.HealthSicknessNameNullText;
            txtHMedicen.NullText = Properties.Resources.HealthMedicenNullText;
            txtHDoctorName.NullText = Properties.Resources.HealthDoctorNullText;

            //Caregiver
            lblCaregiverIdentityCardNumber.Text = Properties.Resources.IdentityCardNumber.getDobblePunkt();
            lblCaregiverJob.Text = Properties.Resources.Jop.getDobblePunkt();
            lblCaregiverMonthlyIncome.Text = Properties.Resources.Income.getDobblePunkt();
            grpCaregiverBaiscData.Text = Properties.Resources.BasicData;
            grpCaregiverIdPhotoBack.Text = Properties.Resources.BackPhoto;
            grpCaregiverIdPhotoFace.Text = Properties.Resources.FacePhoto;
            grpChooseCaregiver.Text = Properties.Resources.ChooseCaregiver;
            optBrothersCaregiver.Text = Properties.Resources.BrothersCaregiver;
            optChooseCaregiver.Text = Properties.Resources.ChooseCaregiver;
            optMotherCaregiver.Text = Properties.Resources.MotherCaregiver;
            optNewCaregiver.Text = Properties.Resources.NewCaregiver;

            radWizard1.CancelButton.Text = Properties.Resources.CancelText;
            radWizard1.NextButton.Text = Properties.Resources.NextText;
            radWizard1.FinishButton.Text = Properties.Resources.Finish;
            radWizard1.BackButton.ToolTipText = Properties.Resources.Previous;
            radWizard1.HelpButton.Visibility = ElementVisibility.Hidden;
            radWizard1.NextButton.Enabled = false;
            lblResult.TextAlignment = ContentAlignment.MiddleCenter;
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

            //health.ReporteFileData = picHealthReportPhoto.PhotoAsBytes;
            //health.ReporteFileURI = "api/orphan/media/healthreport/" + _orphan.Id;
            health.SupervisorDoctor = txtHDoctorName.Text;
            health.Note = txtHNote.Text;
            return health;
        }

        private void SaveHealth()
        {
            if (chkHIsSick.Checked)
            {
                Health hlth = new Health();
                hlth = fillHealth(hlth);
                _orphan.HealthStatus = hlth;
            }
            else
            {
                _orphan.HealthStatus = null;
            }
        }

        private void SaveEducation()
        {
            if (chkSisStudy.Checked)
            {
                _orphan.Education = (Study)studyBindingSource.DataSource;
            }
            else
            {
                _orphan.Education = null;
            }
        }

        private void btnFamilyBrowse_Click(object sender, EventArgs e)
        {
            ChooserView.ChooserView chooserView = new ChooserView.ChooserView(_families, Properties.Resources.ChooseFamily);
            chooserView.MultiSelect = false;
            chooserView.ShowDialog();
            if (chooserView.DialogResult == DialogResult.OK)
            {
                EnabledDisabledControl(true);
                _familyModel = (ViewModel.Family.FamilyModel)chooserView.SelectedObject;
                _family = _FamiliesViewModel.GetSourceFamily(_familyModel.Id);
                txtFatherName.Text = _familyModel.FatherFullName;
                txtMotherName.Text = _familyModel.MotherFullName;
                orphanNameForm.txtEnglishFather.Text = _family.Father.Name.EnglishFirst;
                orphanNameForm.txtFather.Text = _family.Father.Name.First;
                orphanNameForm.txtLast.Text = _family.Father.Name.Last;
                orphanNameForm.txtEnglishLast.Text = _family.Father.Name.EnglishLast;
                GetBrothersCaregiver();
                GetMother();
            }
            else
            {
                EnabledDisabledControl(false);
                _familyModel = null;
                _BrothersCaregiver = null;
                _mother = null;
                txtFatherName.Text = string.Empty;
                txtMotherName.Text = string.Empty;
            }
        }

        private void EnabledDisabledControl(bool value)
        {
            radWizard1.NextButton.Enabled = value;
            orphanNameForm.Enabled = value;
            grpOrphanBasicData.Enabled = value;
        }

        private async void GetBrothersCaregiver()
        {
            if (_familyModel != null)
            {
                var orphanIds = _FamiliesViewModel.OrphansIds(_familyModel.Id);
                if (orphanIds != null && orphanIds.Count > 0)
                {
                    int caregiverId = -1;
                    bool sameCaregiver = true;
                    OrphanageDataModel.Persons.Orphan orphan = null;
                    foreach (var orphanId in orphanIds)
                    {
                        orphan = await _orphanViewModel.getOrphan(orphanId);
                        if (caregiverId == -1)
                            caregiverId = orphan.CaregiverId;

                        //check if all orphan has the same caregiver;
                        if (caregiverId != orphan.CaregiverId)
                        {
                            sameCaregiver = false;
                            break;
                        }
                    }
                    if (orphan != null && sameCaregiver && caregiverId != -1)
                    {
                        _BrothersCaregiver = await _caregiverEditViewModel.getCaregiver(caregiverId);
                        optBrothersCaregiver.Enabled = true;
                    }
                    else
                        optBrothersCaregiver.Enabled = false;
                }
            }
        }

        private async void GetMother()
        {
            if (_familyModel != null)
            {
                var orphanIds = _FamiliesViewModel.OrphansIds(_familyModel.Id);
                if (orphanIds != null && orphanIds.Count > 0)
                {
                    int motherId = -1;
                    bool sameMother = true;
                    OrphanageDataModel.Persons.Orphan orphan = null;
                    foreach (var orphanId in orphanIds)
                    {
                        orphan = await _orphanViewModel.getOrphan(orphanId);
                        if (motherId == -1)
                            motherId = orphan.Family.MotherId;

                        //check if all orphan has the same caregiver;
                        if (motherId != orphan.Family.MotherId)
                        {
                            sameMother = false;
                            break;
                        }
                    }
                    if (orphan != null && sameMother && motherId != -1)
                    {
                        _mother = await _motherEditViewModel.getMother(motherId);
                        optMotherCaregiver.Enabled = true;
                    }
                    else
                        optMotherCaregiver.Enabled = false;
                }
                else
                {
                    var motherId = _FamiliesViewModel.MothersIds(_familyModel.Id);
                    _mother = await _motherEditViewModel.getMother(motherId);
                    optMotherCaregiver.Enabled = true;
                }
            }
        }

        private void AddOrphanView_Load(object sender, EventArgs e)
        {
            this.Width++;
        }

        private void chkSisStudy_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            DisableEnableEducationControls(args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? true : false);
        }

        private void DisableEnableEducationControls(bool value)
        {
            txtEducationNote.Enabled = value;
            txtEducationReasons.Enabled = !value;
            txtEducationSchool.Enabled = value;
            txtEducationStage.Enabled = value;
            numEducationDegreesRate.Enabled = value;
            numEducationMonthlyCost.Enabled = value;
            grpPicCertificate1.Enabled = value;
            grpPicCertificate2.Enabled = value;
            if (value)
            {
                txtEducationStage.Text = null;
            }
            else
            {
                txtEducationStage.Text = Properties.Resources.EducationStageDefaultString;
            }
        }

        private void chkHIsSick_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            DisableEnableHealthControls(args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? true : false);
        }

        private void DisableEnableHealthControls(bool value)
        {
            txtHDoctorName.Enabled = value;
            txtHMedicen.Enabled = value;
            txtHNote.Enabled = value;
            txtHSicknessName.Enabled = value;
            numHCost.Enabled = value;
            grpHealthReportePhoto.Enabled = value;
        }

        private async void radWizard1_SelectedPageChanging(object sender, Telerik.WinControls.UI.SelectedPageChangingEventArgs e)
        {
            if (e.SelectedPage == pgeOrphan && e.NextPage == pgeEducation)
            {
                //leaving Orphan Page
                orphanValidateAndShowError();
                if (!_entityValidator.IsValid() || !orphanNameForm.IsValid())
                    e.Cancel = true;
            }
            if (e.SelectedPage == pgeEducation && e.NextPage == pgeHealth)
            {
                //leaving Education Page
                educationValidateAndShowError();
                if (!_entityValidator.IsValid())
                    e.Cancel = true;
                else
                    SaveEducation();
            }
            if (e.SelectedPage == pgeHealth && e.NextPage == pgeCaregiver)
            {
                //leaving Health Page
                SaveHealth();
            }
            if (e.SelectedPage == pgeCaregiver && e.NextPage == pgeCaregiverOtherData)
            {
                //leaving caregiver page
                caregiverValidateAndShowError();
                if (!_entityValidator.IsValid() || !caregiverNameForm.IsValid())
                    e.Cancel = true;
            }
            if (e.SelectedPage == pgeCaregiverOtherData && e.NextPage == pgeProgress)
            {
                //leaving caregiver other data page
                caregiverAddressForm.ValidateAndShowError();
                if (!caregiverAddressForm.IsValid())
                    e.Cancel = true;
                else
                {
                    // already in the progress page
                    radWizard1.NextButton.Enabled = false;
                    radWizard1.BackButton.Enabled = false;
                    radWizard1.SelectedPageChanging -= radWizard1_SelectedPageChanging;
                    await SendData();
                    radWizard1.SelectNextPage();
                    radWizard1.NextButton.Enabled = true;
                    radWizard1.BackButton.Enabled = true;
                    radWizard1.SelectedPageChanging += radWizard1_SelectedPageChanging;
                }
            }
        }

        private async Task SendData()
        {
            _orphan = (OrphanageDataModel.Persons.Orphan)orphanBindingSource.DataSource;
            _orphan.Name = (OrphanageDataModel.RegularData.Name)orphanNameForm.NameDataSource;
            _orphan.FamilyId = _family.Id;
            _caregiver = (OrphanageDataModel.Persons.Caregiver)caregiverBindingSource.DataSource;
            _caregiver.Name = (OrphanageDataModel.RegularData.Name)caregiverNameForm.NameDataSource;
            _caregiver.Address = (OrphanageDataModel.RegularData.Address)caregiverAddressForm.AddressDataSource;
            _orphan.UserId = _caregiver.UserId = Program.CurrentUser.Id;
            if (_caregiver.Id > 0)
            {
                _orphan.CaregiverId = _caregiver.Id;
            }
            else
            {
                _caregiver = await _caregiverEditViewModel.Add(_caregiver);
                if (picCaregiverIdPhotoBack.Photo != null)
                    await _orphanViewModel.SaveImage(_caregiver.IdentityCardImageBackURI, picCaregiverIdPhotoBack.Photo);
                if (picCaregiverIdPhotoFace.Photo != null)
                    await _orphanViewModel.SaveImage(_caregiver.IdentityCardImageFaceURI, picCaregiverIdPhotoFace.Photo);
            }
            if (_caregiver == null)
            {
                lblResult.Text = Properties.Resources.FamilyCreatedErrorMessage;
            }
            else
            {
                _orphan.CaregiverId = _caregiver.Id;
                var retOrp = await _orphanViewModel.Add(_orphan);
                if (picObirthCertificate.Photo != null)
                    await _orphanViewModel.SaveImage(retOrp.BirthCertificatePhotoURI, picObirthCertificate.Photo);
                if (PicOBodyPhoto.Photo != null)
                    await _orphanViewModel.SaveImage(retOrp.FullPhotoURI, PicOBodyPhoto.Photo);
                if (PicOFacePhoto.Photo != null)
                    await _orphanViewModel.SaveImage(retOrp.FacePhotoURI, PicOFacePhoto.Photo);
                if (picOFamilyCardPhoto.Photo != null)
                    await _orphanViewModel.SaveImage(retOrp.FamilyCardPagePhotoURI, picOFamilyCardPhoto.Photo);
                if (picHealthReportPhoto.Photo != null && _orphan.HealthId.HasValue)
                    await _orphanViewModel.SaveImage(retOrp.HealthStatus.ReporteFileURI, picHealthReportPhoto.Photo);
                if (_orphan.EducationId.HasValue && picEducationCertificate1.Photo != null)
                    await _orphanViewModel.SaveImage(retOrp.Education.CertificateImageURI, picEducationCertificate1.Photo);
                if (_orphan.EducationId.HasValue && picEdiucationCertificate2.Photo != null)
                    await _orphanViewModel.SaveImage(retOrp.Education.CertificateImage2URI, picEdiucationCertificate2.Photo);
            }
        }

        private void ChangeLanguageToArabic_Enter(object sender, EventArgs e)
        {
            CurLang.SaveCurrentLanguage();
            CurLang.ChangeToArabic();
        }

        private void ReturnSavedLanguage_Enter(object sender, EventArgs e)
        {
            CurLang.ReturnToSavedLanguage();
        }

        private void optNewCaregiver_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            _caregiver = new OrphanageDataModel.Persons.Caregiver();
            _caregiver.Id = -1;
            caregiverBindingSource.DataSource = _caregiver;
            picCaregiverIdPhotoBack.Photo = null;
            picCaregiverIdPhotoFace.Photo = null;
            caregiverNameForm.Enabled = true;
        }

        private void optMotherCaregiver_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                _caregiver = new OrphanageDataModel.Persons.Caregiver()
                {
                    ColorMark = _mother.ColorMark,
                    Jop = _mother.Jop,
                    IdentityCardId = _mother.IdentityCardNumber,
                    Note = _mother.Note,
                };
                _caregiver.Name = new OrphanageDataModel.RegularData.Name()
                {
                    First = _mother.Name.First,
                    Father = _mother.Name.Father,
                    Last = _mother.Name.Last,
                    EnglishFather = _mother.Name.EnglishFather,
                    EnglishFirst = _mother.Name.EnglishFirst,
                    EnglishLast = _mother.Name.EnglishLast
                };
                caregiverNameForm.NameDataSource = _caregiver.Name;
                if (_mother.Address != null)
                    _caregiver.Address = new Address()
                    {
                        CellPhone = _mother.Address.CellPhone,
                        City = _mother.Address.City,
                        Country = _mother.Address.Country,
                        Email = _mother.Address.Email,
                        Facebook = _mother.Address.Facebook,
                        Fax = _mother.Address.Fax,
                        HomePhone = _mother.Address.HomePhone,
                        Note = _mother.Address.Note,
                        Street = _mother.Address.Street,
                        Town = _mother.Address.Town,
                        Twitter = _mother.Address.Twitter,
                        WorkPhone = _mother.Address.WorkPhone
                    };
                else
                    _caregiver.Address = new Address();
                caregiverAddressForm.AddressDataSource = _caregiver.Address;
                caregiverBindingSource.DataSource = _caregiver;
                picCaregiverIdPhotoBack.Photo = _mother.IdentityCardBack;
                picCaregiverIdPhotoFace.Photo = _mother.IdentityCardFace;
                caregiverNameForm.Enabled = false;
            }
        }

        private void optBrothersCaregiver_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                _caregiver = _BrothersCaregiver;
                caregiverBindingSource.DataSource = _caregiver;
                caregiverNameForm.NameDataSource = _caregiver.Name;
                caregiverAddressForm.AddressDataSource = _caregiver.Address;
                caregiverNameForm.Enabled = false;
            }
        }

        private void optChooseCaregiver_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ChooserView.ChooserView chooserView = new ChooserView.ChooserView(_caregivers, Properties.Resources.ChooseFamily);
                chooserView.MultiSelect = false;
                chooserView.ShowDialog();
                if (chooserView.DialogResult == DialogResult.OK)
                {
                    caregiverNameForm.Enabled = true;
                    var chosenCaregiverModel = (ViewModel.Caregiver.CaregiverModel)chooserView.SelectedObject;
                    _caregiver = _CaregiversViewModel.GetSourceCaregiver(chosenCaregiverModel.Id);
                    caregiverBindingSource.DataSource = _caregiver;
                    caregiverNameForm.NameDataSource = _caregiver.Name;
                    caregiverAddressForm.AddressDataSource = _caregiver.Address;
                }
                else
                {
                    caregiverNameForm.Enabled = false;
                }
            }
        }
    }
}