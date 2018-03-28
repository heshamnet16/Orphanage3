using OrphanageDataModel.RegularData;
using OrphanageV3.Extensions;
using OrphanageV3.ViewModel.Family;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Unity;

namespace OrphanageV3.Views.Family
{
    public partial class AddFamilyView : Telerik.WinControls.UI.RadForm
    {
        private IEntityValidator _entityValidator;
        private FamilyEditViewModel _familyEditViewModel;

        private bool _result = false;
        public AddFamilyView()
        {
            InitializeComponent();
            TranlateControls();
            MotherAddressForm.AddressDataSource = new Address();
            MotherNameForm.NameDataSource = new Name();
            FatherNameForm.NameDataSource = new Name();
            FamilyAddressFormSecondary.AddressDataSource = new Address();
            FamilyAddressFormPrimary.AddressDataSource = new Address();
            familyBindingSource.DataSource = new OrphanageDataModel.RegularData.Family();
            motherBindingSource.DataSource = new OrphanageDataModel.Persons.Mother();
            fatherBindingSource.DataSource = new OrphanageDataModel.Persons.Father();
            _entityValidator = Program.Factory.Resolve<IEntityValidator>();
            _familyEditViewModel = Program.Factory.Resolve<FamilyEditViewModel>();
            radWaitingBar1.StartWaiting();
            centeringResultLabel();
            radWizard1.FinishButton.Click += FinishButton_Click;
            radWizard1.CancelButton.Click += FinishButton_Click;
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void centeringResultLabel()
        {
            lblResult.Left = (panelComplete.Width / 2) - (lblResult.Width / 2);
            lblResult.Top = (panelComplete.Height / 2) - (lblResult.Height / 2);
        }

        private void TranlateControls()
        {
            this.Text = Properties.Resources.AddNewFamily;
            wizardPageFamily.Title = Properties.Resources.AddNewFamily;
            wizardPageFather.Title = Properties.Resources.AddFather;
            wizardPageMother.Title = Properties.Resources.AddMother;
            wizardCompletionPage1.Title = Properties.Resources.Summary;
            wizardPageProgress.Title = Properties.Resources.Progress;

            lblMotherBirthday.Text = lblFatherBirthday.Text = Properties.Resources.Birthday.getDobblePunkt();
            lblMotherDateOfDeath.Text = lblFatherDateOfDeath.Text = Properties.Resources.DateOfDeath.getDobblePunkt();
            lblFatherDeathCertificate.Text = Properties.Resources.DeathCertificatePhoto.getDobblePunkt();
            lblFatherDeathReason.Text = Properties.Resources.DeathReason.getDobblePunkt();
            lblMotherIdentityCardNumber.Text = lblFatherIdentityCardNumber.Text = Properties.Resources.IdentityCardNumber.getDobblePunkt();
            lblMotherJop.Text = lblFatherJop.Text = Properties.Resources.Jop.getDobblePunkt();
            lblFatherPhoto.Text = Properties.Resources.PersonalPhoto.getDobblePunkt();
            lblMotherStory.Text = lblFatherStory.Text = Properties.Resources.Story.getDobblePunkt();
            lblMotherIsMarried.Text = Properties.Resources.IsMarried.getDobblePunkt();
            lblMotherIsDead.Text = Properties.Resources.IsDead.getDobblePunkt();
            lbllblMotherMonthlyIncome.Text = Properties.Resources.Income.getDobblePunkt();
            lblMotherAddress.Text = Properties.Resources.Address.getDobblePunkt();
            lblMotherHasOrphans.Text = Properties.Resources.HasSheOrphans.getDobblePunkt();
            lblMotherHusbandName.Text = Properties.Resources.HusbandName.getDobblePunkt();
            lblMotherIdentityCardPhoto.Text = Properties.Resources.FrontPhoto.getDobblePunkt();
            lblMotherIdentityCardPhotoBack.Text = Properties.Resources.BackPhoto.getDobblePunkt();
            lblFamilyNote.Text = lblMotherNote.Text = Properties.Resources.Notes.getDobblePunkt();
            grpFamilyBasicData.Text = grpMotherBasicData.Text = grpFatherBasicData.Text = Properties.Resources.BasicData;
            grpFamilyFamilyCardPhoto.Text = grpMotherAdditionalData.Text = grpFatherAdditionalData.Text = Properties.Resources.AdditionalData;
            grpFamilyAddresses.Text = Properties.Resources.Addresses.getDobblePunkt();
            #region ComboBoxesData
            DescriptionTextListDataItem itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.FinncialStatus_Good_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.FinncialStatus_Good;
            itm.TextWrap = false;
            txtFamilyFinncialStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.FinncialStatus_Acceptable_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.FinncialStatus_Acceptable;
            itm.TextWrap = false;
            txtFamilyFinncialStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.FinncialStatus_NotBad_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.FinncialStatus_NotBad;
            itm.TextWrap = false;
            txtFamilyFinncialStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.FinncialStatus_Bad_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.FinncialStatus_Bad;
            itm.TextWrap = false;
            txtFamilyFinncialStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.FinncialStatus_TooBad_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.FinncialStatus_TooBad;
            itm.TextWrap = false;
            txtFamilyFinncialStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceStatus_Good_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceStatus_Good;
            itm.TextWrap = false;
            txtFamilyResidenceStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceStatus_Acceptable_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceStatus_Acceptable;
            itm.TextWrap = false;
            txtFamilyResidenceStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceStatus_Bad_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceStatus_Bad;
            itm.TextWrap = false;
            txtFamilyResidenceStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceStatus_TooBad_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceStatus_TooBad;
            itm.TextWrap = false;
            txtFamilyResidenceStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceType_Owned_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceType_Owned;
            itm.TextWrap = false;
            txtFamilyResidenceType.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceType_Temporarry_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceType_Temporarry;
            itm.TextWrap = false;
            txtFamilyResidenceType.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceType_Rental_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceType_Rental;
            itm.TextWrap = false;
            txtFamilyResidenceType.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceType_Shelter_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceType_Shelter;
            itm.TextWrap = false;
            txtFamilyResidenceType.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceType_Camp_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceType_Camp;
            itm.TextWrap = false;
            txtFamilyResidenceType.Items.Add(itm);

            #endregion

            lblFamilyFamilyCardNumber.Text = Properties.Resources.FamilyCardNumber.getDobblePunkt();
            lblFamilyFamilyCardPhoto1.Text = Properties.Resources.FamilyCardPhoto.getDobblePunkt();
            lblFamilyFamilyCardPhoto2.Text = Properties.Resources.FamilyCardPhoto + " 2".getDobblePunkt();
            lblFamilyFinncialStatus.Text = Properties.Resources.FinncialStatus.getDobblePunkt();
            lblFamilyIsTheyRefugees.Text = Properties.Resources.IsTheyRefugees.getDobblePunkt();
            lblFamilyPrimaryAddress.Text = Properties.Resources.BasicAddress.getDobblePunkt();
            lblFamilyResidenceStatus.Text = Properties.Resources.ResidenceStatus.getDobblePunkt();
            lblFamilyResidenceType.Text = Properties.Resources.ResidenceType.getDobblePunkt();
            lblFamilySecondaryAddress.Text = Properties.Resources.CurrentAddress.getDobblePunkt();


            lblFatherPhoto.TextAlignment = lblFatherStory.TextAlignment = lblResult.TextAlignment =
                lblFatherDeathCertificate.TextAlignment = ContentAlignment.MiddleCenter;
            radWizard1.CancelButton.Text = Properties.Resources.CancelText;
            radWizard1.NextButton.Text = Properties.Resources.NextText;
            radWizard1.FinishButton.Text = Properties.Resources.Finish;
            radWizard1.BackButton.ToolTipText = Properties.Resources.Previous;
            radWizard1.HelpButton.Visibility = ElementVisibility.Hidden;
        }

        private void fatherValidateAndShowError()
        {
            if (_entityValidator != null)
            {
                _entityValidator.controlCollection = panelFather.Controls;
                _entityValidator.DataEntity = fatherBindingSource.DataSource;
                _entityValidator.SetErrorProvider(errorProvider1);
                FatherNameForm.ValidateAndShowError();
            }
        }

        private void motherValidateAndShowError()
        {
            if (_entityValidator != null)
            {
                _entityValidator.controlCollection = panelMother.Controls;
                _entityValidator.DataEntity = motherBindingSource.DataSource;
                _entityValidator.SetErrorProvider(errorProvider1);
                MotherNameForm.ValidateAndShowError();
                MotherAddressForm.ValidateAndShowError();
            }
            if (!MotherAddressForm.IsValid())
            {
                MotherAddressForm.ShowMe();
                MotherAddressForm.ValidateAndShowError();
            }
        }

        private void familyValidateAndShowError()
        {
            if (_entityValidator != null)
            {
                _entityValidator.controlCollection = panelFamily.Controls;
                _entityValidator.DataEntity = familyBindingSource.DataSource;
                _entityValidator.SetErrorProvider(errorProvider1);
                FamilyAddressFormPrimary.ValidateAndShowError();
            }
            if (chkFamilyIsTheyRefugees.Checked && !FamilyAddressFormSecondary.IsValid())
            {
                FamilyAddressFormSecondary.ShowMe();
                FamilyAddressFormSecondary.ValidateAndShowError();
            }
            if (!FamilyAddressFormPrimary.IsValid())
            {
                FamilyAddressFormPrimary.ShowMe();
                FamilyAddressFormPrimary.ValidateAndShowError();
            }

        }

        private OrphanageDataModel.RegularData.Family GetFamily()
        {
            var family = (OrphanageDataModel.RegularData.Family)familyBindingSource.DataSource;
            family.UserId = Program.CurrentUser.Id;
            var father = (OrphanageDataModel.Persons.Father)fatherBindingSource.DataSource;
            father.UserId = Program.CurrentUser.Id;
            father.Name = (Name)FatherNameForm.NameDataSource;
            fatherBindingSource.DataSource = father;
            family.Father = father;

            var mother = (OrphanageDataModel.Persons.Mother)motherBindingSource.DataSource;
            mother.UserId = Program.CurrentUser.Id;
            mother.Name = (Name)MotherNameForm.NameDataSource;
            mother.Address = (Address)MotherAddressForm.AddressDataSource;
            family.Mother = mother;

            family.PrimaryAddress = (Address)FamilyAddressFormPrimary.AddressDataSource;
            if (family.IsTheyRefugees)
            {
                family.AlternativeAddress = (Address)FamilyAddressFormSecondary.AddressDataSource;
            }
            else
            {
                family.AlternativeAddress = null;
            }
            return family;
        }

        private void HideMotherAddress(object sender, EventArgs e)
        {
            MotherAddressForm.HideMe();
            txtMotherAddress.Text = MotherAddressForm.FullAddress;
        }

        private void HideFamilyAddresses(object sender, EventArgs e)
        {
            FamilyAddressFormSecondary.HideMe();
            txtFamilySecondaryAddress.Text = FamilyAddressFormSecondary.FullAddress;
            FamilyAddressFormPrimary.HideMe();
            txtFamilyPrimaryAddress.Text = FamilyAddressFormPrimary.FullAddress;
        }

        private void chkMotherIsDead_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            dteMotherDateOfDeath.Enabled = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? true : false;
        }

        private void chkMotherIsMarried_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            txtMotherHusbandName.Enabled = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? true : false;
        }

        private void ShowMotherAddress(object sender, EventArgs e)
        {
            MotherAddressForm.ShowMe();
        }

        private void ShowFamilyAddressSecondary(object sender, EventArgs e)
        {
            FamilyAddressFormSecondary.ShowMe();
            FamilyAddressFormPrimary.HideMe();
            txtFamilyPrimaryAddress.Text = FamilyAddressFormPrimary.FullAddress;
        }

        private void ShowFamilyAddressPrimary(object sender, EventArgs e)
        {
            FamilyAddressFormPrimary.ShowMe();
            FamilyAddressFormSecondary.HideMe();
            txtFamilySecondaryAddress.Text = FamilyAddressFormSecondary.FullAddress;
        }

        private void chkFamilyIsTheyRefugees_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            bool value = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? true : false;
            txtFamilySecondaryAddress.Enabled = value;
        }

        private void setFamilyAddressFromMotherAddress()
        {
            var add = (Address)MotherAddressForm.AddressDataSource;
            FamilyAddressFormPrimary.AddressDataSource = new Address()
            {
                CellPhone = add.CellPhone,
                City = add.City,
                Country = add.Country,
                Email = add.Email,
                Facebook = add.Facebook,
                Fax = add.Fax,
                HomePhone = add.HomePhone,
                Note = add.Note,
                Street = add.Street,
                Town = add.Town,
                Twitter = add.Twitter,
                WorkPhone = add.WorkPhone
            };
            txtFamilyPrimaryAddress.Text = FamilyAddressFormPrimary.FullAddress;
        }

        private async void radWizard1_SelectedPageChanging(object sender, SelectedPageChangingEventArgs e)
        {
            errorProvider1.Clear();
            if (e.SelectedPage == wizardPageFather && e.NextPage == wizardPageMother)
            {
                //Leaving father page
                fatherValidateAndShowError();
                if (!_entityValidator.IsValid() || !FatherNameForm.IsValid())
                    e.Cancel = true;
            }
            if (e.SelectedPage == wizardPageMother && e.NextPage == wizardPageFamily)
            {
                //Leaving mother page
                motherValidateAndShowError();
                if (!_entityValidator.IsValid() || !MotherNameForm.IsValid() || !MotherAddressForm.IsValid())
                    e.Cancel = true;
                else
                    setFamilyAddressFromMotherAddress();
            }
            if (e.SelectedPage == wizardPageFamily && (e.NextPage == wizardPageProgress))
            {
                //Leaving family page
                familyValidateAndShowError();
                if (!_entityValidator.IsValid() || !FamilyAddressFormPrimary.IsValid() ||
                    (chkFamilyIsTheyRefugees.Checked && !FamilyAddressFormSecondary.IsValid()))
                    e.Cancel = true;
                else
                {
                    var family = GetFamily();
                    var fam = await _familyEditViewModel.Add(family);
                    _result = fam != null ? true : false;
                    if (_result)
                    {
                        if (picFamilyFamilyCardphoto1.Photo != null)
                            await _familyEditViewModel.SaveImage("api/family/media/page1/" + fam.Id, picFamilyFamilyCardphoto1.Photo);
                        if (picFamilyFamilyCardPhoto2.Photo != null)
                            await _familyEditViewModel.SaveImage("api/family/media/page2/" + fam.Id, picFamilyFamilyCardPhoto2.Photo);
                        if (picFatherPhoto.Photo != null)
                            await _familyEditViewModel.SaveImage($"api/father/media/photo/{fam.FatherId}", picFatherPhoto.Photo);
                        if (picFatherDeathCertifi.Photo != null)
                            await _familyEditViewModel.SaveImage($"api/father/media/death/{fam.FatherId}", picFatherDeathCertifi.Photo);
                        if (picMotherIDFace.Photo != null)
                            await _familyEditViewModel.SaveImage($"api/mother/media/idface/{fam.MotherId}", picMotherIDFace.Photo);
                        if (picMotherIDBack.Photo != null)
                            await _familyEditViewModel.SaveImage($"api/mother/media/idback/{fam.MotherId}", picMotherIDBack.Photo);
                    }
                    radWizard1.SelectNextPage();
                }
            }
            if (e.SelectedPage == wizardCompletionPage1 && e.NextPage == wizardPageProgress)
            {
                e.Cancel = true;
                radWizard1.SelectedPageChanging -= radWizard1_SelectedPageChanging;
                radWizard1.SelectPreviousPage();
                radWizard1.SelectPreviousPage();
                radWizard1.SelectedPageChanging += radWizard1_SelectedPageChanging;
            }
            if (e.NextPage == wizardCompletionPage1)
            {
                lblResult.Text = _result ? Properties.Resources.FamilyCreatedMessage : Properties.Resources.FamilyCreatedErrorMessage;
            }
        }

        private void txtFamilyPrimaryAddress_Leave(object sender, EventArgs e)
        {
            FamilyAddressFormPrimary.HideMe();
            txtFamilyPrimaryAddress.Text = FamilyAddressFormPrimary.FullAddress;
        }

        private void txtFamilySecondaryAddress_Leave(object sender, EventArgs e)
        {
            FamilyAddressFormSecondary.HideMe();
            txtFamilySecondaryAddress.Text = FamilyAddressFormSecondary.FullAddress;
        }
    }
}
