using OrphanageDataModel.RegularData;
using OrphanageV3.Extensions;
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
        public AddFamilyView()
        {
            InitializeComponent();
            TranlateControls();
            MotherAddressForm.AddressDataSource = new Address();
            MotherNameForm.NameDataSource = new Name();
            FatherNameForm.NameDataSource = new Name();
            FamilyAddressFormSecondary.AddressDataSource = new Address();
            FamilyAddressFromPrimary.AddressDataSource = new Address();
            familyBindingSource.DataSource = new  OrphanageDataModel.RegularData.Family();
            motherBindingSource.DataSource = new OrphanageDataModel.Persons.Mother();
            fatherBindingSource.DataSource = new OrphanageDataModel.Persons.Father();
            _entityValidator = Program.Factory.Resolve<IEntityValidator>();
        }

        private void TranlateControls()
        {
            this.Text = Properties.Resources.AddNewFamily;
            wizardPageFamily.Title = Properties.Resources.AddNewFamily;
            wizardPageFather.Title = Properties.Resources.AddFather;
            wizardPageMother.Title = Properties.Resources.AddMother;
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
            lblFamilyNote.Text = lblMotherNote.Text = Properties.Resources.Notes.getDobblePunkt() ;
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
            

            lblFatherPhoto.TextAlignment = lblFatherStory.TextAlignment = lblFatherDeathCertificate.TextAlignment = ContentAlignment.MiddleCenter;
            radWizard1.CancelButton.Text = Properties.Resources.CancelText;
            radWizard1.NextButton.Text = Properties.Resources.NextText;
            radWizard1.HelpButton.Visibility = ElementVisibility.Hidden;
        }

        private void fatherValidateAndShowError()
        {
            errorProvider1.Clear();
            if (_entityValidator != null)
            {                
                _entityValidator.controlCollection = panelFather.Controls;
                _entityValidator.DataEntity = fatherBindingSource.DataSource;
                _entityValidator.SetErrorProvider(errorProvider1);
            }
        }

        private void motherValidateAndShowError()
        {
            errorProvider1.Clear();
            if (_entityValidator != null)
            {
                _entityValidator.controlCollection = panelMother.Controls;
                _entityValidator.DataEntity = motherBindingSource.DataSource;
                _entityValidator.SetErrorProvider(errorProvider1);
            }
        }

        private void familyValidateAndShowError()
        {
            errorProvider1.Clear();
            if (_entityValidator != null)
            {
                _entityValidator.controlCollection = panelFamily.Controls;
                _entityValidator.DataEntity = familyBindingSource.DataSource;
                _entityValidator.SetErrorProvider(errorProvider1);
            }
        }

        private void GetData()
        {
            var father = (OrphanageDataModel.Persons.Father)fatherBindingSource.DataSource;
            father.Name = (Name)FatherNameForm.NameDataSource;
            fatherBindingSource.DataSource = father;

            var mother = (OrphanageDataModel.Persons.Mother)motherBindingSource.DataSource;
            mother.Name = (Name)MotherNameForm.NameDataSource;
            mother.Address = (Address)MotherAddressForm.AddressDataSource;

            var family = (OrphanageDataModel.RegularData.Family)familyBindingSource.DataSource;
            family.PrimaryAddress = (Address)FamilyAddressFromPrimary.AddressDataSource;
            if(family.IsTheyRefugees)
            {
                family.AlternativeAddress = (Address)FamilyAddressFormSecondary.AddressDataSource;
            }
            else
            {
                family.AlternativeAddress = null;
            }
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
            FamilyAddressFromPrimary.HideMe();
            txtFamilyPrimaryAddress.Text = FamilyAddressFromPrimary.FullAddress;
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
            FamilyAddressFromPrimary.HideMe();
            txtFamilyPrimaryAddress.Text = FamilyAddressFromPrimary.FullAddress;
        }

        private void ShowFamilyAddressPrimary(object sender, EventArgs e)
        {
            FamilyAddressFromPrimary.ShowMe();
            FamilyAddressFormSecondary.HideMe();
            txtFamilySecondaryAddress.Text = FamilyAddressFormSecondary.FullAddress;
        }

        private void chkFamilyIsTheyRefugees_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            bool value = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? true : false;
            txtFamilySecondaryAddress.Enabled = value;
        }

        private void radWizard1_SelectedPageChanging(object sender, SelectedPageChangingEventArgs e)
        {
            errorProvider1.Clear();
            if (e.SelectedPage == wizardPageFather && e.NextPage == wizardPageMother)
            {
                //Leaving father page
                fatherValidateAndShowError();
                if (!_entityValidator.IsValid())
                    e.Cancel = true;
            }
            if (e.SelectedPage == wizardPageMother && (e.NextPage == wizardPageFamily || e.NextPage == wizardPageFather))
            {
                //Leaving mother page
                motherValidateAndShowError();
                if (!_entityValidator.IsValid())
                    e.Cancel = true;
            }
            if (e.SelectedPage == wizardPageFamily && (e.NextPage == wizardPageProgress || e.NextPage == wizardPageMother) )
            {
                //Leaving family page
                familyValidateAndShowError();
                if (!_entityValidator.IsValid())
                    e.Cancel = true;
                else
                {

                }
            }
        }
    }
}
