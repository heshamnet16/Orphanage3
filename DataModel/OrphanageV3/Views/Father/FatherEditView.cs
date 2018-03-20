using OrphanageDataModel.RegularData;
using OrphanageV3.Extensions;
using OrphanageV3.ViewModel.Father;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Unity;
namespace OrphanageV3.Views.Father
{
    public partial class FatherEditView : Telerik.WinControls.UI.RadForm
    {
        private readonly int _motherId;

        private OrphanageDataModel.Persons.Father _Father = null;

        private FatherEditViewModel _fatherEditViewModel;

        private IEntityValidator _fatherEntityValidator;

        public FatherEditView(int FatherID)
        {
            InitializeComponent();

            _motherId = FatherID;

            _fatherEditViewModel = Program.Factory.Resolve<FatherEditViewModel>();

            _fatherEntityValidator = Program.Factory.Resolve<IEntityValidator>();

            _fatherEditViewModel.ImagesSize = picPhoto.Size;
            LoadFather(FatherID);
        }

        private async void LoadFather(int fatherId)
        {
            _Father = await _fatherEditViewModel.getFather(fatherId);
            if (_Father != null)
            {
               fatherBindingSource.DataSource = _Father;
                _fatherEntityValidator.controlCollection = Controls;
                _fatherEntityValidator.DataEntity = _Father;
                nameForm1.NameDataSource = _Father.Name;
                SetData();
                txtName.Text = nameForm1.FullName;
            }
            TranslateControls();
        }

        private void SetData()
        {
            picPhoto.SetImageByBytes(_Father.PhotoData);
            picDeathCertificate.SetImageByBytes(_Father.DeathCertificatePhotoData);
            clrColor.Value = _Father.ColorMark.HasValue ? Color.FromArgb((int)_Father.ColorMark.Value) : Color.Black;
        }

        private void TranslateControls()
        {
            this.Text = txtName.Text;
            lblColor.Text = Properties.Resources.Color.getDobblePunkt();
            lblIdentityNumber.Text = Properties.Resources.IdentityCardNumber.getDobblePunkt();
            lblDeathCertificatePhoto.Text = Properties.Resources.DeathCertificatePhoto.getDobblePunkt();
            lblPersonalPhoto.Text = Properties.Resources.PersonalPhoto.getDobblePunkt();
            lblJob.Text = Properties.Resources.Jop.getDobblePunkt();
            lblName.Text = Properties.Resources.FullName.getDobblePunkt();
            lblNote.Text = Properties.Resources.Notes.getDobblePunkt();
            btnSave.Text = Properties.Resources.SaveText;
            btnCancel.Text = Properties.Resources.CancelText;
            grpBasicData.HeaderText = Properties.Resources.BasicData;
            grpOtherData.HeaderText = Properties.Resources.OtherData;
            lblDateOfDeath.Text = Properties.Resources.DateOfDeath.getDobblePunkt();
            lblBirthday.Text = Properties.Resources.Birthday.getDobblePunkt();
            lblStory.Text = Properties.Resources.Story.getDobblePunkt();
            lblDeathReason.Text = Properties.Resources.DeathReason.getDobblePunkt();
        }

        private void ValidateAndShowError()
        {
            fatherErrorProvider1.Clear();
            if (_fatherEntityValidator != null)
            {
                _fatherEntityValidator.controlCollection = Controls;
                _fatherEntityValidator.DataEntity = fatherBindingSource.DataSource;
                _fatherEntityValidator.SetErrorProvider(fatherErrorProvider1);
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
                if (picSelector.Name == "picDeathCertificate")
                {
                    url = _Father.DeathCertificateImageURI;
                    img = picDeathCertificate.Photo;
                }
                if (picSelector.Name == "picPhoto")
                {
                    url = _Father.PersonalPhotoURI;
                    img = picPhoto.Photo;
                }
                if (url != null)
                {
                    await _fatherEditViewModel.SaveImage(url, img);
                    picSelector.HideLoadingGif();
                }
            }
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            nameForm1.ShowMe();
        }

        private void HideNameAddressForms(object sender, EventArgs e)
        {
            nameForm1.HideMe();
            try
            {
                _Father.Name = (Name)nameForm1.NameDataSource;
                txtName.Text = nameForm1.FullName;
            }
            catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void BtnOK_Click(object sender, EventArgs e)
        {
            if (_fatherEntityValidator.IsValid())
            {
                _Father.Name = (Name)nameForm1.NameDataSource;
                if (await _fatherEditViewModel.Save(_Father))
                    this.Close();
            }
            else
            {
                ValidateAndShowError();
            }
        }
        private void clrColor_ValueChanged(object sender, EventArgs e)
        {
            if (clrColor.Value != null && clrColor.Value != Color.Black && clrColor.Value != Color.White)
                _Father.ColorMark = clrColor.Value.ToArgb();
            else
                _Father.ColorMark = null;
        }
    }
}
