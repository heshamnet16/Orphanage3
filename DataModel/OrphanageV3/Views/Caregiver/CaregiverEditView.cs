using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using OrphanageV3.Extensions;
using OrphanageV3.ViewModel.Caregiver;
using Unity;
using OrphanageV3.Services.Interfaces;
using System.Linq;
using OrphanageV3.Views.Helper.Interfaces;
using OrphanageDataModel.RegularData;

namespace OrphanageV3.Views.Caregiver
{
    public partial class CaregiverEditView : Telerik.WinControls.UI.RadForm
    {
        private OrphanageDataModel.Persons.Caregiver _Caregiver = null;

        private CaregiverEditViewModel _caregiverEditViewModel ;

        private IAutoCompleteService _AutoCompleteServic ;

        private IDataFormatterService _DataFormatterService ;

        private IEntityValidator _AddressEntityValidator ;

        private IEntityValidator _NameEntityValidator ;

        private IEntityValidator _CaregiverEntityValidator;

        public CaregiverEditView(int CaregiverId)
        {
            InitializeComponent();
        _caregiverEditViewModel = Program.Factory.Resolve<CaregiverEditViewModel>();

        _AutoCompleteServic = Program.Factory.Resolve<IAutoCompleteService>();

        _DataFormatterService = Program.Factory.Resolve<IDataFormatterService>();

        _AddressEntityValidator = Program.Factory.Resolve<IEntityValidator>();

        _NameEntityValidator = Program.Factory.Resolve<IEntityValidator>();

        _CaregiverEntityValidator = Program.Factory.Resolve<IEntityValidator>();

        _caregiverEditViewModel.ImagesSize = picIDFront.Size;
            NameForm1.AutoCompleteService = addressForm1.AutoCompleteService = _AutoCompleteServic;
            NameForm1.DataFormatterService =  addressForm1.DataFormatterService = _DataFormatterService;
            addressForm1.EntityValidator = _AddressEntityValidator;
            NameForm1.EntityValidator = _NameEntityValidator;
            LoadCaregiver(CaregiverId);            
        }


        private async void LoadCaregiver(int id)
        {
            _Caregiver = await _caregiverEditViewModel.getCaregiver(id);
            if (_Caregiver != null)
            {
                caregiverBindingSource.DataSource = _Caregiver;
                _CaregiverEntityValidator.controlCollection = Controls;
                _CaregiverEntityValidator.DataEntity = _Caregiver;
                if (_Caregiver.Address != null)
                    addressForm1.AddressDataSource = _Caregiver.Address;
                else
                    addressForm1.AddressDataSource = new Address();
                NameForm1.NameDataSource = _Caregiver.Name;
                SetData();
            }
            TranslateControls();
        }
        private void CaregiverEditView_Load(object sender, EventArgs e)
        {

        }

        private void SetData()
        {
            picIDFront.SetImageByBytes(_Caregiver.IdentityCardPhotoFaceData);
            PicIDBack.SetImageByBytes(_Caregiver.IdentityCardPhotoBackData);
            txtName.Text = NameForm1.FullName;
            txtAddress.Text = addressForm1.FullAddress;
            clrColor.Value = _Caregiver.ColorMark.HasValue ? Color.FromArgb((int)_Caregiver.ColorMark.Value) : Color.Black;                 
        }

        private void TranslateControls()
        {
            this.Text = txtName.Text;
            lblAddress.Text = Properties.Resources.Address.getDobblePunkt();
            lblColor.Text = Properties.Resources.Color.getDobblePunkt();
            lblIdentityNumber.Text = Properties.Resources.ID.getDobblePunkt();
            lblIdPhotoBack.Text = Properties.Resources.BackPhoto.getDobblePunkt();
            lblIdPhotoFront.Text = Properties.Resources.FrontPhoto.getDobblePunkt();
            lblIncome.Text = Properties.Resources.Income.getDobblePunkt();
            lblJob.Text = Properties.Resources.Jop.getDobblePunkt();
            lblName.Text = Properties.Resources.FullName.getDobblePunkt();
            lblNotes.Text = Properties.Resources.Notes.getDobblePunkt();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_CaregiverEntityValidator.IsValid())
            {
                _Caregiver.Address = (Address)addressForm1.AddressDataSource;
                _Caregiver.Name = (Name)NameForm1.NameDataSource;
                if (await _caregiverEditViewModel.Save(_Caregiver))
                    this.Close();
            }
            else
            {
                ValidateAndShowError();
            }
        }
        private void ValidateAndShowError()
        {
            errorProvider1.Clear();
            if (_CaregiverEntityValidator != null)
            {
                _CaregiverEntityValidator.controlCollection = Controls;
                _CaregiverEntityValidator.DataEntity = this.caregiverBindingSource.DataSource;
                _CaregiverEntityValidator.SetErrorProvider(errorProvider1);
            }
        }
        private void grpIdentityCard_Click(object sender, EventArgs e)
        {
            NameForm1.HideMe();
            _Caregiver.Name = (Name)NameForm1.NameDataSource;
            txtName.Text = NameForm1.FullName;
            addressForm1.HideMe();
            _Caregiver.Address = (Address)addressForm1.AddressDataSource;
            txtAddress.Text = addressForm1.FullAddress;
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            NameForm1.ShowMe();
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            addressForm1.ShowMe();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void PhotoChanged(object sender, EventArgs e)
        {
            if (_Caregiver == null) return;
            if (sender is PictureSelector.PictureSelector)
            {
                string url = null;
                Image img = null;
                var picSelector = (PictureSelector.PictureSelector)sender;
                picSelector.ShowLoadingGif();
                if (picSelector.Name == "picIDFront")
                {
                    url = _Caregiver.IdentityCardImageFaceURI;
                    img = picIDFront.Photo;
                }
                if (picSelector.Name == "PicIDBack")
                {
                    url = _Caregiver.IdentityCardImageBackURI;
                    img = PicIDBack.Photo;
                }
                if (url != null)
                {
                    await _caregiverEditViewModel.SaveImage(url, img);
                    picSelector.HideLoadingGif();
                }
            }
        }

        private void clrColor_ValueChanged(object sender, EventArgs e)
        {
            if (clrColor.Value != null && clrColor.Value != Color.Black && clrColor.Value != Color.White)
                _Caregiver.ColorMark = clrColor.Value.ToArgb();
            else
                _Caregiver.ColorMark = null;
        }
    }
}
