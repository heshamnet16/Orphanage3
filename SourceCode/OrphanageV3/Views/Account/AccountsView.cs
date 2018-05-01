using OrphanageV3.Controlls;
using OrphanageV3.ViewModel.Account;
using OrphanageV3.Views.Helper.Interfaces;
using OrphanageV3.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Unity;

namespace OrphanageV3.Views.Account
{
    public partial class AccountsView : Telerik.WinControls.UI.RadForm, IView
    {
        private AccountsViewModel _accountViewModel = Program.Factory.Resolve<AccountsViewModel>();
        private IRadGridHelper _radGridHelper = Program.Factory.Resolve<IRadGridHelper>();
        private IEnumerable<int> _accountsIds = null;

        public string GetTitle() => this.Text;

        public AccountsView()
        {
            InitializeComponent();

            _accountViewModel.DataLoaded += _accountsViewModel_DataLoaded;

            _radGridHelper.GridView = orphanageGridView1.GridView;
            TranslateControls();
        }

        public AccountsView(IEnumerable<int> accountsIds)
        {
            InitializeComponent();

            _accountViewModel.DataLoaded += _accountsViewModel_DataLoaded;

            _radGridHelper.GridView = orphanageGridView1.GridView;
            TranslateControls();
            this._accountsIds = accountsIds;
        }

        private void TranslateControls()
        {
            this.Text = Properties.Resources.Accounts;
            btnColumn.ToolTipText = Properties.Resources.Columns;
            btnDelete.ToolTipText = Properties.Resources.Detele;
            btnEdit.ToolTipText = Properties.Resources.Edit;
            btnShowBails.ToolTipText = Properties.Resources.ShowBails;
            btnShowGuarantors.ToolTipText = Properties.Resources.ShowGuarantors;
        }

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void UpdateControls()
        {
            if (orphanageGridView1.SelectedRows != null)
            {
                var retObjectGuarantorsCount = _radGridHelper.GetValueBySelectedRow("GuarantorsCount");
                if (retObjectGuarantorsCount != null)
                {
                    int guarantorCount;
                    int.TryParse(retObjectGuarantorsCount.ToString(), out guarantorCount);
                    bool value = guarantorCount > 0 ? true : false;
                    btnShowGuarantors.Enabled = value;
                }
                var retObjectBailsCount = _radGridHelper.GetValueBySelectedRow("BailsCount");
                if (retObjectBailsCount != null)
                {
                    int bailesCount;
                    int.TryParse(retObjectBailsCount.ToString(), out bailesCount);
                    bool value = bailesCount > 0 ? true : false;
                    btnShowBails.Enabled = value;
                }

                if (orphanageGridView1.SelectedRows.Count == 1)
                {
                    btnEdit.Enabled = true;
                }
                else
                {
                    btnEdit.Enabled = false;
                }
            }
        }

        private void _accountsViewModel_DataLoaded(object sender, EventArgs e)
        {
            orphanageGridView1.GridView.DataSource = _accountViewModel.Accounts;
            orphanageGridView1.GridView.SelectionChanged += GridView_SelectionChanged;
        }

        private void AccountsView_Load(object sender, EventArgs e)
        {
            if (_accountsIds != null)
            {
                _accountViewModel.LoadAccounts(_accountsIds);
            }
            else
            {
                _accountViewModel.LoadAccounts();
            }

            if (File.Exists(Properties.Settings.Default.AccountsLayoutFilePath))
            {
                orphanageGridView1.GridView.LoadLayout(Properties.Settings.Default.AccountsLayoutFilePath);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            foreach (var id in selectedIds)
            {
                var ret = await _accountViewModel.Delete(id, true);
                if (ret)
                    _radGridHelper.HideRow("Id", id);
            }
        }

        private void btnColumn_Click(object sender, EventArgs e)
        {
            orphanageGridView1.ShowColumnsChooser();
        }

        private async void btnShowGuarantos_Click(object sender, EventArgs e)
        {
            IList<int> ret = new List<int>();
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            var guarantorsIds = await _accountViewModel.GuarantorsIds(selectedIds);
            Guarantor.GuarantorsView guarantorsView = new Guarantor.GuarantorsView(guarantorsIds);
            guarantorsView.MdiParent = this.MdiParent;
            guarantorsView.Show();
        }

        private void AccountsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            orphanageGridView1.GridView.SaveLayout(Properties.Settings.Default.AccountsLayoutFilePath);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = (int)_radGridHelper.GetValueBySelectedRow("Id");
            AccountEditView caregiverEditView = new AccountEditView(id);
            caregiverEditView.ShowDialog();
            _accountViewModel.Update(id);
        }

        private async void btnShowBails_Click(object sender, EventArgs e)
        {
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            var bailsIds = await _accountViewModel.BailsIds(selectedIds);
            Bail.BailsView bailsView = new Bail.BailsView(bailsIds);
            bailsView.MdiParent = this.MdiParent;
            bailsView.Show();
        }

        public OrphanageGridView GetOrphanageGridView()
        {
            return this.orphanageGridView1;
        }

        public void Update(int ObjectId)
        {
            _accountViewModel.Update(ObjectId);
        }
    }
}