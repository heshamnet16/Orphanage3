using OrphanageDataModel.RegularData;
using OrphanageV3.Extensions;
using OrphanageV3.ViewModel.Mother;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Drawing;
using Unity;

namespace OrphanageV3.Views.Mother
{
    public partial class MotherEditView : Telerik.WinControls.UI.RadForm
    {
        private readonly int _motherId;

        private OrphanageDataModel.Persons.Mother _Mother = null;

        private MotherEditViewModel _motherEditViewModel;

        private IEntityValidator _motherEntityValidator;

        public MotherEditView(int MotherId)
        {
            InitializeComponent();
            _motherId = MotherId;

            _motherEditViewModel = Program.Factory.Resolve<MotherEditViewModel>();

            _motherEntityValidator = Program.Factory.Resolve<IEntityValidator>();

            LoadMother(MotherId);
        }

        private async void LoadMother(int motherId)
        {
            _Mother = await _motherEditViewModel.getMother(motherId);
            if (_Mother != null)
            {
                motherBindingSource.DataSource = _Mother;
                _motherEntityValidator.controlCollection = Controls;
                _motherEntityValidator.DataEntity = _Mother;
                if (_Mother.Address != null)
                    addressForm1.AddressDataSource = _Mother.Address;
                else
                    addressForm1.AddressDataSource = new Address();
                nameForm1.NameDataSource = _Mother.Name;
                SetData();
                txtName.Text = nameForm1.FullName;
                txtAddress.Text = addressForm1.FullAddress;
            }
            TranslateControls();
        }

        private void SetData()
        {
            picIdentityCardFace.SetImageByBytes(_Mother.IdentityCardPhotoFaceData);
            picIdentityCardBack.SetImageByBytes(_Mother.IdentityCardPhotoBackData);
            clrColor.Value = _Mother.ColorMark.HasValue ? Color.FromArgb((int)_Mother.ColorMark.Value) : Color.Black;
        }

        private void TranslateControls()
        {
            this.Text = txtName.Text;
            lblAddress.Text = Properties.Resources.Address.getDobblePunkt();
            lblColor.Text = Properties.Resources.Color.getDobblePunkt();
            lblIdentityNumber.Text = Properties.Resources.ID.getDobblePunkt();
            lblIdentityBackPhoto.Text = Properties.Resources.BackPhoto.getDobblePunkt();
            lblIdentityFacePhoto.Text = Properties.Resources.FrontPhoto.getDobblePunkt();
            lblIncome.Text = Properties.Resources.Income.getDobblePunkt();
            lblJob.Text = Properties.Resources.Jop.getDobblePunkt();
            lblName.Text = Properties.Resources.FullName.getDobblePunkt();
            lblNote.Text = Properties.Resources.Notes.getDobblePunkt();
            btnSave.Text = Properties.Resources.SaveText;
            btnCancel.Text = Properties.Resources.CancelText;
            grpBasicData.HeaderText = Properties.Resources.BasicData;
            grpOtherData.HeaderText = Properties.Resources.OtherData;
            lblAddress.Text = Properties.Resources.Birthday.getDobblePunkt();
            lblDateOfDeath.Text = Properties.Resources.DateOfDeath.getDobblePunkt();
            lblHasOrphans.Text = Properties.Resources.HasSheOrphans.getDobblePunkt();
            lblHusbandName.Text = Properties.Resources.HusbandName.getDobblePunkt();
            lblIsDead.Text = Properties.Resources.IsDead.getDobblePunkt();
            lblIsMarried.Text = Properties.Resources.IsMarried.getDobblePunkt();
            lblStory.Text = Properties.Resources.Story.getDobblePunkt();
        }

        private void ValidateAndShowError()
        {
            motherErrorProvider1.Clear();
            if (_motherEntityValidator != null)
            {
                _motherEntityValidator.controlCollection = Controls;
                _motherEntityValidator.DataEntity = motherBindingSource.DataSource;
                _motherEntityValidator.SetErrorProvider(motherErrorProvider1);
            }
        }

        private async void PhotoChanged(object sender, EventArgs e)
        {
            if (_Mother == null) return;
            if (sender is PictureSelector.PictureSelector)
            {
                string url = null;
                Image img = null;
                var picSelector = (PictureSelector.PictureSelector)sender;
                picSelector.ShowLoadingGif();
                if (picSelector.Name == "picIDFront")
                {
                    url = _Mother.IdentityCardFaceURI;
                    img = picIdentityCardFace.Photo;
                }
                if (picSelector.Name == "PicIDBack")
                {
                    url = _Mother.IdentityCardBackURI;
                    img = picIdentityCardBack.Photo;
                }
                if (url != null)
                {
                    await _motherEditViewModel.SaveImage(url, img);
                    picSelector.HideLoadingGif();
                }
            }
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            nameForm1.ShowMe();
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            addressForm1.ShowMe();
        }

        private void HideNameAddressForms(object sender, EventArgs e)
        {
            nameForm1.HideMe();
            addressForm1.HideMe();
            try
            {
                _Mother.Name = (Name)nameForm1.NameDataSource;
                txtName.Text = nameForm1.FullName;
                _Mother.Address = (Address)addressForm1.AddressDataSource;
                txtAddress.Text = addressForm1.FullAddress;
            }
            catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void BtnOK_Click(object sender, EventArgs e)
        {
            if (_motherEntityValidator.IsValid())
            {
                _Mother.Address = (Address)addressForm1.AddressDataSource;
                _Mother.Name = (Name)nameForm1.NameDataSource;
                if (await _motherEditViewModel.Save(_Mother))
                    this.Close();
            }
            else
            {
                ValidateAndShowError();
            }
        }

        private void chkIsDead_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                dteDateOfDeath.Enabled = true;
            }
            else
            {
                dteDateOfDeath.Enabled = false;
                _Mother.DateOfDeath = null;
            }
        }

        private void chkIsMarried_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                txtHusbandName.Enabled = true;
            }
            else
            {
                txtHusbandName.Enabled = false;
                _Mother.HusbandName = null;
            }
        }

        private void clrColor_ValueChanged(object sender, EventArgs e)
        {
            if (clrColor.Value != null && clrColor.Value != Color.Black && clrColor.Value != Color.White)
                _Mother.ColorMark = clrColor.Value.ToArgb();
            else
                _Mother.ColorMark = null;
        }
    }
}