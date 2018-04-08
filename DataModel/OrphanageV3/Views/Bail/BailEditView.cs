using OrphanageV3.Extensions;
using OrphanageV3.ViewModel.Bail;
using OrphanageV3.ViewModel.Guarantor;
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

namespace OrphanageV3.Views.Bail
{
    public partial class BailEditView : Telerik.WinControls.UI.RadForm
    {
        private DateTime _endDatePlaceholder = DateTime.Now;
        private GuarantorsViewModel _guarantorsViewModel;
        private BailEditViewModel _bailEditViewModel;
        private IEntityValidator _bailEntityValidator;

        private IEnumerable<GuarantorModel> _Guarantors = null;
        private int _CurrentGuarantorId = -1;

        private OrphanageDataModel.FinancialData.Bail _bail = null;
        private int _bailId = -1;

        public BailEditView()
        {
            InitializeComponent();
            _guarantorsViewModel = Program.Factory.Resolve<GuarantorsViewModel>();
            _guarantorsViewModel.DataLoaded += GuarantorsLoaded;
            _bailEntityValidator = Program.Factory.Resolve<IEntityValidator>();
            _bailEditViewModel = Program.Factory.Resolve<BailEditViewModel>();
            DisableEnableControls(false);
        }

        public BailEditView(int BailID)
        {
            InitializeComponent();
            _guarantorsViewModel = Program.Factory.Resolve<GuarantorsViewModel>();
            _guarantorsViewModel.DataLoaded += GuarantorsLoaded;
            _bailEntityValidator = Program.Factory.Resolve<IEntityValidator>();
            _bailEditViewModel = Program.Factory.Resolve<BailEditViewModel>();
            _bailId = BailID;
            DisableEnableControls(false);
        }

        private void DisableEnableControls(bool value)
        {
            txtNote.Enabled = value;
            btnSave.Enabled = value;
            dteEndDate.Enabled = value;
            dteStartDate.Enabled = value;
            chkIsExpired.Enabled = value;
            chkIsFamilyBail.Enabled = value;
            chkIsMonthlyBail.Enabled = value;
            chkNoTime.Enabled = value;
            numAmount.Enabled = value;
        }

        private void TranslateControls()
        {
            this.Text = _bail == null ? Properties.Resources.NewBail : Properties.Resources.Bail + " " + _bail.Guarantor.Name.FullName();
            lblAmount.Text = Properties.Resources.Amount.getDobblePunkt();
            lblCurrency.Text = Properties.Resources.CurrencyName.getDobblePunkt();
            lblFrom.Text = Properties.Resources.StartDate.getDobblePunkt();
            lblTo.Text = Properties.Resources.EndDate.getDobblePunkt();
            lblGuarantorName.Text = Properties.Resources.GuarantorName.getDobblePunkt();
            lblIsExpired.Text = Properties.Resources.IsExpired.getDobblePunkt();
            lblIsFamily.Text = Properties.Resources.IsFamilyBail.getDobblePunkt();
            lblIsMonthly.Text = Properties.Resources.IsMonthlyBail.getDobblePunkt();
            lblNoLimit.Text = Properties.Resources.NotLimited.getDobblePunkt();
            lblNote.Text = Properties.Resources.Notes.getDobblePunkt();
            btnSave.Text = Properties.Resources.SaveText;
            btnCancel.Text = Properties.Resources.CancelText;
        }

        private void ValidateAndShowError()
        {
            errorProvider1.Clear();
            if (_bailEntityValidator != null)
            {
                _bailEntityValidator.controlCollection = Controls;
                _bailEntityValidator.DataEntity = bailBindingSource.DataSource;
                _bailEntityValidator.SetErrorProvider(errorProvider1);
            }
        }

        private void GuarantorsLoaded(object sender, EventArgs e)
        {
            _Guarantors = new List<GuarantorModel>(_guarantorsViewModel.Guarantors);
            btnChooseGuarantor.Enabled = true;
        }

        private void chkNoTime_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            bool value = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
            dteEndDate.Enabled = !value;
            dteStartDate.Enabled = !value;
        }

        private void chkIsExpired_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            bool value = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
            if (dteEndDate.Enabled)
            {
                if (value)
                {
                    _endDatePlaceholder = dteEndDate.Value;
                    dteEndDate.Value = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
                }
                else
                {
                    dteEndDate.Value = _endDatePlaceholder;
                }
            }
        }

        private void btnChooseGuarantor_Click(object sender, EventArgs e)
        {
            ChooserView.ChooserView chooserView = new ChooserView.ChooserView(new List<object>(_Guarantors), Properties.Resources.ChooseGuarantor);
            chooserView.MultiSelect = false;
            chooserView.ShowDialog();
            if (chooserView.DialogResult == DialogResult.OK)
            {
                var guaratorModel = (GuarantorModel)chooserView.SelectedObject;
                _CurrentGuarantorId = guaratorModel.Id;
                DisableEnableControls(true);
                txtGuarantorName.Text = guaratorModel.FullName;
            }
            else
            {
                if (_CurrentGuarantorId <= 0)
                {
                    DisableEnableControls(false);
                }
                else
                {
                    DisableEnableControls(true);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ValidateAndShowError();
            if (_bailEntityValidator.IsValid())
            {
            }
        }

        private async void BailEditView_Load(object sender, EventArgs e)
        {
            if (_bailId != -1)
            {
                _bail = await _bailEditViewModel.getBail(_bailId);
                if (_bail != null)
                {
                    bailBindingSource.DataSource = _bail;
                    _CurrentGuarantorId = _bail.GuarantorID;
                    txtGuarantorName.Text = _bail.Guarantor.Name.FullName();
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                bailBindingSource.DataSource = new OrphanageDataModel.FinancialData.Bail();
            }
        }
    }
}