using OrphanageDataModel.RegularData;
using OrphanageV3.Extensions;
using OrphanageV3.ViewModel.Family;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Drawing;
using Telerik.WinControls.UI;
using Unity;

namespace OrphanageV3.Views.Family
{
    public partial class FamilyEditView : Telerik.WinControls.UI.RadForm
    {
        private OrphanageDataModel.RegularData.Family _Father = null;

        private FamilyEditViewModel _fatherEditViewModel;

        private IEntityValidator _familyEntityValidator;

        public FamilyEditView(int FatherID)
        {
            InitializeComponent();

            _fatherEditViewModel = Program.Factory.Resolve<FamilyEditViewModel>();

            _familyEntityValidator = Program.Factory.Resolve<IEntityValidator>();

            _fatherEditViewModel.ImagesSize = picCardphoto1.Size;
            LoadFamily(FatherID);
        }

        private async void LoadFamily(int fatherId)
        {
            _Father = await _fatherEditViewModel.getFamily(fatherId);
            if (_Father != null)
            {
                familyBindingSource.DataSource = _Father;
                _familyEntityValidator.controlCollection = Controls;
                _familyEntityValidator.DataEntity = _Father;
                addressForm1.AddressDataSource = _Father.PrimaryAddress;
                if (_Father.AlternativeAddress != null)
                    addressForm2.AddressDataSource = _Father.AlternativeAddress;
                else
                    addressForm2.AddressDataSource = new Address();
                SetData();
                txtAddress.Text = addressForm1.FullAddress;
                txtAddress2.Text = addressForm2.FullAddress;
            }
            TranslateControls();
        }

        private void SetData()
        {
            picCardphoto1.SetImageByBytes(_Father.FamilyCardImagePage1Data);
            picCardPhoto2.SetImageByBytes(_Father.FamilyCardImagePage2Data);
            if (_Father.AlternativeAddress == null)
                txtAddress2.Enabled = false;
        }

        private void TranslateControls()
        {
            this.Text = Properties.Resources.Family.getDobblePunkt() + " " + _Father.Father.Name.FullName() + " " + Properties.Resources.AndString + " "
                + _Father.Mother.Name.FullName();
            lblCurrentAddress.Text = Properties.Resources.CurrentAddress.getDobblePunkt();
            lblPrimaryAddress.Text = Properties.Resources.BasicAddress.getDobblePunkt();
            lblFamilyCardNumber.Text = Properties.Resources.FamilyCardNumber.getDobblePunkt();
            lblIsTheyRefugees.Text = Properties.Resources.IsTheyRefugees.getDobblePunkt();
            lblNote.Text = Properties.Resources.Notes.getDobblePunkt();
            lblResidenceStatus.Text = Properties.Resources.ResidenceStatus.getDobblePunkt();
            lblResidenceType.Text = Properties.Resources.ResidenceType.getDobblePunkt();
            lblFinncialStatus.Text = Properties.Resources.FinncialStatus.getDobblePunkt();
            btnOK.Text = Properties.Resources.SaveText;
            btnCancel.Text = Properties.Resources.CancelText;
            grpBasicData.HeaderText = Properties.Resources.BasicData.getDobblePunkt();
            grpFamilyCardPhotoP1.HeaderText = Properties.Resources.FamilyCardPhoto.getDobblePunkt();
            grpFamilyCardPhotoP2.HeaderText = Properties.Resources.FamilyCardPhoto.getDobblePunkt() + "2";

            #region ComboBoxesData

            DescriptionTextListDataItem itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.FinncialStatus_Good_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.FinncialStatus_Good;
            itm.TextWrap = false;
            txtFinncialStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.FinncialStatus_Acceptable_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.FinncialStatus_Acceptable;
            itm.TextWrap = false;
            txtFinncialStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.FinncialStatus_NotBad_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.FinncialStatus_NotBad;
            itm.TextWrap = false;
            txtFinncialStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.FinncialStatus_Bad_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.FinncialStatus_Bad;
            itm.TextWrap = false;
            txtFinncialStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.FinncialStatus_TooBad_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.FinncialStatus_TooBad;
            itm.TextWrap = false;
            txtFinncialStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceStatus_Good_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceStatus_Good;
            itm.TextWrap = false;
            txtResidenceStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceStatus_Acceptable_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceStatus_Acceptable;
            itm.TextWrap = false;
            txtResidenceStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceStatus_Bad_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceStatus_Bad;
            itm.TextWrap = false;
            txtResidenceStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceStatus_TooBad_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceStatus_TooBad;
            itm.TextWrap = false;
            txtResidenceStatus.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceType_Owned_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceType_Owned;
            itm.TextWrap = false;
            txtResidenceType.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceType_Temporarry_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceType_Temporarry;
            itm.TextWrap = false;
            txtResidenceType.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceType_Rental_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceType_Rental;
            itm.TextWrap = false;
            txtResidenceType.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceType_Shelter_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceType_Shelter;
            itm.TextWrap = false;
            txtResidenceType.Items.Add(itm);

            itm = new DescriptionTextListDataItem();
            itm.DescriptionText = Properties.Resources.ResidenceType_Camp_Description;
            itm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            itm.Height = 60;
            itm.Text = Properties.Resources.ResidenceType_Camp;
            itm.TextWrap = false;
            txtResidenceType.Items.Add(itm);

            #endregion ComboBoxesData
        }

        private void ValidateAndShowError()
        {
            familyErrorProvider1.Clear();
            if (_familyEntityValidator != null)
            {
                _familyEntityValidator.controlCollection = Controls;
                _familyEntityValidator.DataEntity = familyBindingSource.DataSource;
                _familyEntityValidator.SetErrorProvider(familyErrorProvider1);
            }
        }

        private async void PhotoChanged(object sender, EventArgs e)
        {
            if (_Father == null) return;
            if (sender is PictureSelector.PictureSelector)
            {
                string url = null;
                Image img = null;
                var picSelector = (PictureSelector.PictureSelector)sender;
                picSelector.ShowLoadingGif();
                if (picSelector.Name == "picCardphoto1")
                {
                    url = _Father.FamilyCardImagePage1URI;
                    img = picCardphoto1.Photo;
                }
                if (picSelector.Name == "picCardPhoto2")
                {
                    url = _Father.FamilyCardImagePage2URI;
                    img = picCardPhoto2.Photo;
                }
                if (url != null)
                {
                    await _fatherEditViewModel.SaveImage(url, img);
                    picSelector.HideLoadingGif();
                }
            }
        }

        private void chkIsTheyRefugees_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            txtAddress2.Enabled = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? true : false;
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            addressForm1.ShowMe();
            addressForm2.HideMe();
            txtAddress2.Text = addressForm2.FullAddress;
        }

        private void txtAddress2_Click(object sender, EventArgs e)
        {
            addressForm2.ShowMe();
            addressForm1.HideMe();
            txtAddress.Text = addressForm1.FullAddress;
        }

        private void HideNameAddressForms(object sender, EventArgs e)
        {
            addressForm1.HideMe();
            addressForm2.HideMe();
            try
            {
                txtAddress.Text = addressForm1.FullAddress;
                txtAddress2.Text = addressForm2.FullAddress;
            }
            catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void BtnOK_Click(object sender, EventArgs e)
        {
            if (_familyEntityValidator.IsValid())
            {
                _Father.PrimaryAddress = (Address)addressForm1.AddressDataSource;
                if (_Father.IsTheyRefugees)
                    try
                    {
                        _Father.AlternativeAddress = (Address)addressForm2.AddressDataSource;
                    }
                    catch { _Father.AlternativeAddress = null; }
                else
                    _Father.AlternativeAddress = null;

                if (await _fatherEditViewModel.Save(_Father))
                    this.Close();
            }
            else
            {
                ValidateAndShowError();
            }
        }
    }
}