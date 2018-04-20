using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using OrphanageV3.ViewModel.Account;
using OrphanageV3.Views.Helper.Interfaces;
using Unity;
using OrphanageV3.Extensions;
using LangChanger;

namespace OrphanageV3.Views.Account
{
    public partial class AccountEditView : Telerik.WinControls.UI.RadForm
    {
        private IDictionary<string, string> _CurrenciesShortcuts = null;
        private IDictionary<string, string> _CurrenciesNames = null;

        private OrphanageDataModel.FinancialData.Account _account = null;

        private int _AccountId = -1;
        private decimal _amountValueholder = 0;

        private AccountEditViewModel _accountEditViewModel;
        private IEntityValidator _accountEntityValidator;

        public AccountEditView()
        {
            InitializeComponent();
            _CurrenciesShortcuts = getAllCurrenciesShortcuts();
            _CurrenciesNames = getAllCurrenciesNames();
            fillCurrencies();
            _accountEntityValidator = Program.Factory.Resolve<IEntityValidator>();
            _accountEditViewModel = Program.Factory.Resolve<AccountEditViewModel>();
            TranslateControls();
        }

        public AccountEditView(int accountId)
        {
            InitializeComponent();
            _CurrenciesShortcuts = getAllCurrenciesShortcuts();
            _CurrenciesNames = getAllCurrenciesNames();
            fillCurrencies();
            _AccountId = accountId;
            _accountEntityValidator = Program.Factory.Resolve<IEntityValidator>();
            _accountEditViewModel = Program.Factory.Resolve<AccountEditViewModel>();
            TranslateControls();
        }

        private void fillCurrencies()
        {
            cmbCurrency.Items.AddRange(_CurrenciesNames.Values);
            if (_AccountId == -1)
            {
                cmbCurrency.SelectedItem = cmbCurrency.Items[0];
            }
        }

        private IDictionary<string, string> getAllCurrenciesNames()
        {
            IDictionary<string, string> returnedCurrencies = new Dictionary<string, string>();
            Properties.Resources resources = new Properties.Resources();
            Type resourcesType = resources.GetType();
            var props = resourcesType.GetProperties();
            foreach (var prop in props)
            {
                if (prop.Name.ToLower().Contains("currencyname") && prop.Name.Length > "currencyname".Length + 1)
                    returnedCurrencies.Add(prop.Name, prop.GetValue(resources).ToString());
            }
            return returnedCurrencies;
        }

        private IDictionary<string, string> getAllCurrenciesShortcuts()
        {
            IDictionary<string, string> returnedCurrencies = new Dictionary<string, string>();
            Properties.Resources resources = new Properties.Resources();
            Type resourcesType = resources.GetType();
            var props = resourcesType.GetProperties();
            foreach (var prop in props)
            {
                if (prop.Name.ToLower().Contains("currencyshortcut") && prop.Name.Length > "currencyshortcut".Length + 1)
                    returnedCurrencies.Add(prop.Name, prop.GetValue(resources).ToString());
            }
            return returnedCurrencies;
        }

        private void TranslateControls()
        {
            this.Text = _account == null ? Properties.Resources.NewAccount : Properties.Resources.Account + " " + _account.AccountName;
            lblAmount.Text = Properties.Resources.Amount.getDobblePunkt();
            lblAccountName.Text = Properties.Resources.AccountName.getDobblePunkt();
            lblCannotBeNegativ.Text = Properties.Resources.CurrentAccount.getDobblePunkt();
            lblCurrencyName.Text = Properties.Resources.CurrencyName.getDobblePunkt();
            lblCurrencyShortcut.Text = Properties.Resources.CurrencyShortcut.getDobblePunkt();
            lblNote.Text = Properties.Resources.Notes.getDobblePunkt();
            btnSave.Text = Properties.Resources.SaveText;
            btnCancel.Text = Properties.Resources.CancelText;
        }

        private void ValidateAndShowError()
        {
            errorProvider1.Clear();
            if (_accountEntityValidator != null)
            {
                _accountEntityValidator.controlCollection = Controls;
                _accountEntityValidator.DataEntity = accountBindingSource.DataSource;
                _accountEntityValidator.SetErrorProvider(errorProvider1);
            }
        }

        private void txtCurrencySho_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbCurrency_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            var fullcurrencyName = cmbCurrency.SelectedText;
            if (_CurrenciesNames.Values.Contains(fullcurrencyName))
            {
                try
                {
                    var key = _CurrenciesNames.Where(k => k.Value == fullcurrencyName).FirstOrDefault().Key;
                    var currencyName = key.Substring("currencyname".Length, key.Length - "currencyname".Length);
                    var currencyShortcutKey = "CurrencyShortcut" + currencyName;
                    if (_CurrenciesShortcuts.ContainsKey(currencyShortcutKey))
                    {
                        txtCurrencyShortcut.Text = _CurrenciesShortcuts[currencyShortcutKey];
                    }
                }
                catch { }
            }
        }

        private void chkCanNotBeNegative_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                _amountValueholder = numAmount.Value;
                numAmount.Minimum = 0;
            }
            else
            {
                numAmount.Minimum = -9999999999999;
                if (numAmount.Value == 0)
                {
                    numAmount.Value = _amountValueholder;
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            _accountEntityValidator.controlCollection = Controls;
            _accountEntityValidator.DataEntity = accountBindingSource.DataSource;
            if (_accountEntityValidator.IsValid())
            {
                _account = (OrphanageDataModel.FinancialData.Account)accountBindingSource.DataSource;
                if (_AccountId == -1)
                {
                    if (await _accountEditViewModel.Add(_account) != null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    if (await _accountEditViewModel.Save(_account))
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            else
            {
                ValidateAndShowError();
            }
        }

        private async void AccountEditView_Load(object sender, EventArgs e)
        {
            if (_AccountId != -1)
            {
                _account = await _accountEditViewModel.getAccount(_AccountId);
                if (_account != null)
                {
                    accountBindingSource.DataSource = _account;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                accountBindingSource.DataSource = new OrphanageDataModel.FinancialData.Account();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void changeToArabic_Enter(object sender, EventArgs e)
        {
            CurLang.SaveCurrentLanguage();
            CurLang.ChangeToArabic();
        }

        private void returnToSavedLanguage_Leave(object sender, EventArgs e)
        {
            CurLang.ReturnToSavedLanguage();
        }
    }
}