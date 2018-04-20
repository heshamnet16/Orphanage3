using OrphanageV3.ViewModel.Bail;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Unity;

namespace OrphanageV3.Views.Bail
{
    public partial class BailsView : Telerik.WinControls.UI.RadForm
    {
        private BailsViewModel _bailsViewModel = Program.Factory.Resolve<BailsViewModel>();
        private IRadGridHelper _radGridHelper = Program.Factory.Resolve<IRadGridHelper>();
        private IEnumerable<int> _bailsIds = null;

        public BailsView()
        {
            InitializeComponent();

            _bailsViewModel.DataLoaded += _bailsViewModel_DataLoaded;
            orphanageGridView1.GridView.SelectionChanged += GridView_SelectionChanged;

            _radGridHelper.GridView = orphanageGridView1.GridView;
            TranslateControls();
        }

        public BailsView(IEnumerable<int> bailsIds)
        {
            InitializeComponent();

            _bailsViewModel.DataLoaded += _bailsViewModel_DataLoaded;
            orphanageGridView1.GridView.SelectionChanged += GridView_SelectionChanged;

            _radGridHelper.GridView = orphanageGridView1.GridView;
            TranslateControls();
            this._bailsIds = bailsIds;
        }

        private void TranslateControls()
        {
            this.Text = Properties.Resources.Bails;
            btnColumn.ToolTipText = Properties.Resources.Columns;
            btnDelete.ToolTipText = Properties.Resources.Detele;
            btnEdit.ToolTipText = Properties.Resources.Edit;
            btnShowFamilies.ToolTipText = Properties.Resources.ShowFamilies;
            btnShowOrphans.ToolTipText = Properties.Resources.ShowOrphans;
        }

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void UpdateControls()
        {
            if (orphanageGridView1.SelectedRows != null)
            {
                var retObjectOrphansCount = _radGridHelper.GetValueBySelectedRow("OrphansCount");
                if (retObjectOrphansCount != null)
                {
                    int orphansCount;
                    int.TryParse(retObjectOrphansCount.ToString(), out orphansCount);
                    bool value = orphansCount > 0 ? true : false;
                    btnShowOrphans.Enabled = value;
                }
                var retObjectFamiliesCount = _radGridHelper.GetValueBySelectedRow("FamiliesCount");
                if (retObjectFamiliesCount != null)
                {
                    int familiesCount;
                    int.TryParse(retObjectFamiliesCount.ToString(), out familiesCount);
                    bool value = familiesCount > 0 ? true : false;
                    btnShowFamilies.Enabled = value;
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

        private void _bailsViewModel_DataLoaded(object sender, EventArgs e)
        {
            orphanageGridView1.GridView.DataSource = _bailsViewModel.Bails;
        }

        private void BailsView_Load(object sender, EventArgs e)
        {
            if (_bailsIds != null)
            {
                _bailsViewModel.LoadBails(_bailsIds);
            }
            else
            {
                _bailsViewModel.LoadBails();
            }

            if (File.Exists(Properties.Settings.Default.BailsLayoutFilePath))
            {
                orphanageGridView1.GridView.LoadLayout(Properties.Settings.Default.BailsLayoutFilePath);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            foreach (var id in selectedIds)
            {
                var ret = await _bailsViewModel.Delete(id, true);
                if (ret)
                    _radGridHelper.HideRow("Id", id);
            }
        }

        private void btnColumn_Click(object sender, EventArgs e)
        {
            orphanageGridView1.ShowColumnsChooser();
        }

        private async void btnShowOrphans_Click(object sender, EventArgs e)
        {
            IList<int> ret = new List<int>();
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            var orphansIds = await _bailsViewModel.OrphansIds(selectedIds);
            Orphan.OrphansView or = new Orphan.OrphansView(orphansIds);
            or.MdiParent = this.MdiParent;
            or.Show();
        }

        private void BailsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            orphanageGridView1.GridView.SaveLayout(Properties.Settings.Default.BailsLayoutFilePath);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = (int)_radGridHelper.GetValueBySelectedRow("Id");
            BailEditView caregiverEditView = new BailEditView(id);
            caregiverEditView.ShowDialog();
            _bailsViewModel.Update(id);
        }

        private async void btnShowFamilies_Click(object sender, EventArgs e)
        {
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            var familiesIds = await _bailsViewModel.FamiliesIds(selectedIds);
            Family.FimiliesView familiesView = new Family.FimiliesView(familiesIds);
            familiesView.MdiParent = this.MdiParent;
            familiesView.Show();
        }
    }
}