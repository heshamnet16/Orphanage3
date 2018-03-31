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
using OrphanageV3.ViewModel.Orphan;

namespace OrphanageV3.Views.Orphan
{
    public partial class AddOrphanView : Telerik.WinControls.UI.RadForm
    {
        private ViewModel.Family.FamilyModel _familyModel = null;
        private OrphanageDataModel.RegularData.Family _family = null;

        private List<object> _familiesSelectionList = null;
        private List<object> _caregiversSelectionList = null;

        private AddOrphanViewModel _AddOrphanViewModel = null;

        private OrphanageDataModel.Persons.Caregiver _mainCaregiver = null;
        private OrphanageDataModel.Persons.Caregiver _motherCaregiver = null;
        private OrphanageDataModel.Persons.Caregiver _selectedCaregiver = null;
        private OrphanageDataModel.Persons.Caregiver _brothersCaregiver = null;

        private IEntityValidator _entityValidator;
        private IAutoCompleteService _AutoCompleteServic = null;

        private OrphanageDataModel.Persons.Orphan _orphan = null;

        public AddOrphanView()
        {
            InitializeComponent();

            _AddOrphanViewModel = Program.Factory.Resolve<AddOrphanViewModel>();
            _AddOrphanViewModel.CaregiversSelectionListLoad += CaregiverDataLoaded;
            _AddOrphanViewModel.FamiliesSelectionListLoad += FamiliesDataLoaded;
            _AddOrphanViewModel.LoadSelectionData();

            TranslateControls();

            _orphan = new OrphanageDataModel.Persons.Orphan();
            _orphan.Name = new OrphanageDataModel.RegularData.Name();
            _orphan.Education = new Study();
            orphanBindingSource.DataSource = _orphan;
            orphanNameForm.NameDataSource = _orphan.Name;
            studyBindingSource.DataSource = _orphan.Education;
            caregiverBindingSource.DataSource = new OrphanageDataModel.Persons.Caregiver();
            caregiverNameForm.NameDataSource = new OrphanageDataModel.RegularData.Name();
            caregiverAddressForm.AddressDataSource = new OrphanageDataModel.RegularData.Address();

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
            _familiesSelectionList = new List<object>(_AddOrphanViewModel.FamiliesSelectionList);
        }

        private void CaregiverDataLoaded(object sender, EventArgs e)
        {
            _caregiversSelectionList = new List<object>(_AddOrphanViewModel.CaregiversSelectionList);
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

        private async void SetOtherCaregiversOptions()
        {
            _motherCaregiver = await _AddOrphanViewModel.GetCaregiverFromMother(_family.Id);
            _brothersCaregiver = await _AddOrphanViewModel.GetCaregiverFromOrphans(_family.Id);
        }

        private void btnFamilyBrowse_Click(object sender, EventArgs e)
        {
            ChooserView.ChooserView chooserView = new ChooserView.ChooserView(_familiesSelectionList, Properties.Resources.ChooseFamily);
            chooserView.MultiSelect = false;
            chooserView.ShowDialog();
            if (chooserView.DialogResult == DialogResult.OK)
            {
                EnabledDisabledControl(true);
                _familyModel = (ViewModel.Family.FamilyModel)chooserView.SelectedObject;
                _family = _AddOrphanViewModel.GetSourceFamily(_familyModel.Id);
                SetOtherCaregiversOptions();
                txtFatherName.Text = _familyModel.FatherFullName;
                txtMotherName.Text = _familyModel.MotherFullName;
                orphanNameForm.txtEnglishFather.Text = _family.Father.Name.EnglishFirst;
                orphanNameForm.txtFather.Text = _family.Father.Name.First;
                orphanNameForm.txtLast.Text = _family.Father.Name.Last;
                orphanNameForm.txtEnglishLast.Text = _family.Father.Name.EnglishLast;
            }
            else
            {
                EnabledDisabledControl(false);
                _familyModel = null;
                _brothersCaregiver = null;
                _motherCaregiver = null;
                _family = null;
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
                optMotherCaregiver.Enabled = _motherCaregiver != null;
                optBrothersCaregiver.Enabled = _brothersCaregiver != null;
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
            if (e.SelectedPage == wizardCompletionPage1 && e.NextPage == pgeProgress)
            {
                e.Cancel = true;
                radWizard1.SelectedPageChanging -= radWizard1_SelectedPageChanging;
                radWizard1.SelectPreviousPage();
                radWizard1.SelectPreviousPage();
                radWizard1.SelectedPageChanging += radWizard1_SelectedPageChanging;
            }
        }

        private async Task SendData()
        {
            _orphan = (OrphanageDataModel.Persons.Orphan)orphanBindingSource.DataSource;
            _orphan.Name = (OrphanageDataModel.RegularData.Name)orphanNameForm.NameDataSource;
            _orphan.FamilyId = _family.Id;
            _mainCaregiver = (OrphanageDataModel.Persons.Caregiver)caregiverBindingSource.DataSource;
            _mainCaregiver.Name = (OrphanageDataModel.RegularData.Name)caregiverNameForm.NameDataSource;
            _mainCaregiver.Address = (OrphanageDataModel.RegularData.Address)caregiverAddressForm.AddressDataSource;
            _orphan.UserId = _mainCaregiver.UserId = Program.CurrentUser.Id;

            _mainCaregiver = await _AddOrphanViewModel.AddCaregiver(_mainCaregiver);
            if (_mainCaregiver == null)
            {
                lblResult.Text = Properties.Resources.CaregiverAddedFailed;
                return;
            }
            else
            {
                lblResult.Text = Properties.Resources.CaregiverAddedSuccess;
            }
            if (picCaregiverIdPhotoBack.Photo != null)
                await _AddOrphanViewModel.SendImage(_mainCaregiver.IdentityCardImageBackURI, picCaregiverIdPhotoBack.Photo);
            if (picCaregiverIdPhotoFace.Photo != null)
                await _AddOrphanViewModel.SendImage(_mainCaregiver.IdentityCardImageFaceURI, picCaregiverIdPhotoFace.Photo);

            _orphan.CaregiverId = _mainCaregiver.Id;
            _orphan.Caregiver = _mainCaregiver;
            var retOrp = await _AddOrphanViewModel.Add(_orphan);
            if (retOrp != null)
                lblResult.Text += "\n" + Properties.Resources.OrphanAddedSuccess;
            else
            {
                lblResult.Text += "\n" + Properties.Resources.OrphanAddedFailed;
                return;
            }

            if (picObirthCertificate.Photo != null)
                await _AddOrphanViewModel.SendImage(retOrp.BirthCertificatePhotoURI, picObirthCertificate.Photo);
            if (PicOBodyPhoto.Photo != null)
                await _AddOrphanViewModel.SendImage(retOrp.FullPhotoURI, PicOBodyPhoto.Photo);
            if (PicOFacePhoto.Photo != null)
                await _AddOrphanViewModel.SendImage(retOrp.FacePhotoURI, PicOFacePhoto.Photo);
            if (picOFamilyCardPhoto.Photo != null)
                await _AddOrphanViewModel.SendImage(retOrp.FamilyCardPagePhotoURI, picOFamilyCardPhoto.Photo);
            if (picHealthReportPhoto.Photo != null && _orphan.HealthId.HasValue)
                await _AddOrphanViewModel.SendImage(retOrp.HealthStatus.ReporteFileURI, picHealthReportPhoto.Photo);
            if (_orphan.EducationId.HasValue && picEducationCertificate1.Photo != null)
                await _AddOrphanViewModel.SendImage(retOrp.Education.CertificateImageURI, picEducationCertificate1.Photo);
            if (_orphan.EducationId.HasValue && picEdiucationCertificate2.Photo != null)
                await _AddOrphanViewModel.SendImage(retOrp.Education.CertificateImage2URI, picEdiucationCertificate2.Photo);
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
            setMainCaregiver(null);
            caregiverNameForm.Enabled = true;
        }

        private void optMotherCaregiver_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                setMainCaregiver(_motherCaregiver);
                caregiverNameForm.Enabled = false;
            }
        }

        private void optBrothersCaregiver_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                setMainCaregiver(_brothersCaregiver);
                caregiverNameForm.Enabled = false;
            }
        }

        private void optChooseCaregiver_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ChooserView.ChooserView chooserView = new ChooserView.ChooserView(_caregiversSelectionList, Properties.Resources.ChooseFamily);
                chooserView.MultiSelect = false;
                chooserView.ShowDialog();
                if (chooserView.DialogResult == DialogResult.OK)
                {
                    var chosenCaregiverModel = (ViewModel.Caregiver.CaregiverModel)chooserView.SelectedObject;
                    _selectedCaregiver = _AddOrphanViewModel.GetSourceCaregiver(chosenCaregiverModel.Id);
                    setMainCaregiver(_selectedCaregiver);
                }
                else
                {
                    setMainCaregiver(null);
                }
                caregiverNameForm.Enabled = false;
            }
        }

        private async void setMainCaregiver(OrphanageDataModel.Persons.Caregiver caregiver)
        {
            _mainCaregiver = caregiver;
            if (caregiver != null)
            {
                caregiverBindingSource.DataSource = _mainCaregiver;
                caregiverNameForm.NameDataSource = _mainCaregiver.Name;
                caregiverAddressForm.AddressDataSource = _mainCaregiver.Address;
                if (_mainCaregiver.IdentityCardImageBackURI != null && _mainCaregiver.IdentityCardImageBackURI.Length > 0)
                    picCaregiverIdPhotoBack.Photo = await _AddOrphanViewModel.GetImage(_mainCaregiver.IdentityCardImageBackURI);
                if (_mainCaregiver.IdentityCardImageFaceURI != null && _mainCaregiver.IdentityCardImageFaceURI.Length > 0)
                    picCaregiverIdPhotoFace.Photo = await _AddOrphanViewModel.GetImage(_mainCaregiver.IdentityCardImageFaceURI);
            }
            else
            {
                _mainCaregiver = new OrphanageDataModel.Persons.Caregiver();
                _mainCaregiver.Name = new OrphanageDataModel.RegularData.Name();
                _mainCaregiver.Address = new OrphanageDataModel.RegularData.Address();
                caregiverBindingSource.DataSource = _mainCaregiver;
                _mainCaregiver.Id = -1;
                caregiverNameForm.NameDataSource = _mainCaregiver.Name;
                caregiverAddressForm.AddressDataSource = _mainCaregiver.Address;
                picCaregiverIdPhotoBack.Photo = null;
                picCaregiverIdPhotoFace.Photo = null;
            }
        }
    }
}