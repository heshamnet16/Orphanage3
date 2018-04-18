using OrphanageDataModel.RegularData;
using OrphanageV3.Extensions;
using OrphanageV3.ViewModel.Account;
using OrphanageV3.ViewModel.Guarantor;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Unity;

namespace OrphanageV3.Views.Guarantor
{
    public partial class GuarantorEditView : Telerik.WinControls.UI.RadForm
    {
        private OrphanageDataModel.Persons.Guarantor _Guarantor = null;
        private OrphanageDataModel.FinancialData.Account _account = null;

        private GuarantorEditViewModel _guarantorEditViewModel;

        private IEntityValidator _GuarantorEntityValidator;

        private bool _isNewGuarantor = false;

        public GuarantorEditView(int GuarantorId)
        {
            InitializeComponent();
            _guarantorEditViewModel = Program.Factory.Resolve<GuarantorEditViewModel>();
            _guarantorEditViewModel.AccountsLoaded += _guarantorEditViewModel_AccountsLoaded;
            _GuarantorEntityValidator = Program.Factory.Resolve<IEntityValidator>();

            LoadGuarantor(GuarantorId);
            TranslateControls();
            _isNewGuarantor = false;
        }

        private void _guarantorEditViewModel_AccountsLoaded(object sender, EventArgs e)
        {
            btnChooseAccount.Enabled = true;
        }

        public GuarantorEditView()
        {
            InitializeComponent();
            _guarantorEditViewModel = Program.Factory.Resolve<GuarantorEditViewModel>();
            _guarantorEditViewModel.AccountsLoaded += _guarantorEditViewModel_AccountsLoaded;

            _GuarantorEntityValidator = Program.Factory.Resolve<IEntityValidator>();
            _GuarantorEntityValidator.controlCollection = Controls;
            _GuarantorEntityValidator.DataEntity = _Guarantor;

            guarantorBindingSource.DataSource = new OrphanageDataModel.Persons.Guarantor();
            _Guarantor = new OrphanageDataModel.Persons.Guarantor();
            _isNewGuarantor = true;
            addressForm1.AddressDataSource = new Address();
            nameForm1.NameDataSource = new Name();
            EnableDisableControls(false);
            TranslateControls();
        }

        private void EnableDisableControls(bool value)
        {
            txtAddress.Enabled = value;
            txtName.Enabled = value;
            txtNote.Enabled = value;
            chkIsPayingMonthly.Enabled = value;
            chkIsStillPaying.Enabled = value;
            chkIsSupportingOnlyFamilies.Enabled = value;
            clrColor.Enabled = value;
        }

        private async void LoadGuarantor(int id)
        {
            _Guarantor = await _guarantorEditViewModel.getGuarantor(id);
            if (_Guarantor != null)
            {
                SetData();
                EnableDisableControls(true);
            }
            else
            {
                EnableDisableControls(false);
            }
        }

        private void SetData()
        {
            guarantorBindingSource.DataSource = _Guarantor;
            clrColor.Value = _Guarantor.ColorMark.HasValue ? Color.FromArgb((int)_Guarantor.ColorMark.Value) : Color.Black;
            _account = _Guarantor.Account;
            nameForm1.NameDataSource = _Guarantor.Name;
            if (_Guarantor.Address != null)
                addressForm1.AddressDataSource = _Guarantor.Address;
            else
                addressForm1.AddressDataSource = new Address();

            txtAccountName.Text = _account.AccountName;
            txtName.Text = nameForm1.FullName;
            txtAddress.Text = addressForm1.FullAddress;
            _GuarantorEntityValidator.controlCollection = Controls;
            _GuarantorEntityValidator.DataEntity = _Guarantor;
        }

        private void TranslateControls()
        {
            this.Text = txtName.Text;
            lblAccountName.Text = Properties.Resources.AccountName.getDobblePunkt();
            lblIsPayingMonthly.Text = Properties.Resources.IsPayingMonthly.getDobblePunkt();
            lblIsStillPaying.Text = Properties.Resources.IsStillPaying.getDobblePunkt();
            lblOnlyFamilies.Text = Properties.Resources.IsSupportingOnlyFamilies.getDobblePunkt();
            lblAddress.Text = Properties.Resources.Address.getDobblePunkt();
            lblColor.Text = Properties.Resources.Color.getDobblePunkt();
            lblName.Text = Properties.Resources.FullName.getDobblePunkt();
            lblNotes.Text = Properties.Resources.Notes.getDobblePunkt();
            btnSave.Text = Properties.Resources.SaveText;
            btnCancel.Text = Properties.Resources.CancelText;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            _Guarantor = (OrphanageDataModel.Persons.Guarantor)guarantorBindingSource.DataSource;
            _GuarantorEntityValidator.DataEntity = _Guarantor;
            if (_GuarantorEntityValidator.IsValid())
            {
                _Guarantor.Address = (Address)addressForm1.AddressDataSource;
                _Guarantor.Name = (Name)nameForm1.NameDataSource;
                if (!nameForm1.IsValid())
                {
                    errorProvider1.SetError(txtName, Properties.Resources.ErrorMessageCheckName);
                    nameForm1.ShowMe();
                    return;
                }
                if (!addressForm1.IsValid())
                {
                    errorProvider1.SetError(txtAddress, Properties.Resources.ErrorMessageCheckAddress);
                    addressForm1.ShowMe();
                    return;
                }
                if (!_isNewGuarantor)
                {
                    if (await _guarantorEditViewModel.Save(_Guarantor))
                        this.Close();
                }
                else
                {
                    if (await _guarantorEditViewModel.Add(_Guarantor) != null)
                        this.Close();
                }
            }
            else
            {
                ValidateAndShowError();
            }
        }

        private void ValidateAndShowError()
        {
            errorProvider1.Clear();
            if (_GuarantorEntityValidator != null)
            {
                _GuarantorEntityValidator.controlCollection = Controls;
                _GuarantorEntityValidator.DataEntity = this.guarantorBindingSource.DataSource;
                _GuarantorEntityValidator.SetErrorProvider(errorProvider1);
            }
        }

        private void HideNameAddressForms(object sender, EventArgs e)
        {
            nameForm1.HideMe();
            txtName.Text = nameForm1.FullName;
            txtAddress.Text = addressForm1.FullAddress;
            addressForm1.HideMe();
            if (_Guarantor != null)
            {
                _Guarantor.Name = (Name)nameForm1.NameDataSource;
                _Guarantor.Address = (Address)addressForm1.AddressDataSource;
            }
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            HideNameAddressForms(null, null);
            nameForm1.ShowMe();
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            addressForm1.ShowMe();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChooseAccount_Click(object sender, EventArgs e)
        {
            ChooserView.ChooserView chooserView = new ChooserView.ChooserView(new List<object>(_guarantorEditViewModel.Accounts), Properties.Resources.ChooseAccount);
            chooserView.MultiSelect = false;
            chooserView.ShowDialog();
            if (chooserView.DialogResult == DialogResult.OK)
            {
                var accountModel = (AccountModel)chooserView.SelectedObject;
                _account = _guarantorEditViewModel.GetSourceAccount(accountModel.Id);
                ((OrphanageDataModel.Persons.Guarantor)(guarantorBindingSource.DataSource)).AccountId = _account.Id;
                ((OrphanageDataModel.Persons.Guarantor)(guarantorBindingSource.DataSource)).Account = _account;
                EnableDisableControls(true);
                txtAccountName.Text = accountModel.AccountName;
            }
            else
            {
                EnableDisableControls(false);
            }
        }
    }
}