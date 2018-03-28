using OrphanageV3.ViewModel.Family;
using OrphanageV3.Views.Father;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace OrphanageV3.Views.Family
{
    public partial class FimiliesView : Telerik.WinControls.UI.RadForm
    {
        private FamiliesViewModel _familiesViewModel = Program.Factory.Resolve<FamiliesViewModel>();
        private IRadGridHelper _radGridHelper = Program.Factory.Resolve<IRadGridHelper>();
        private IEnumerable<int> _FamiliesIdsList;

        public FimiliesView()
        {
            InitializeComponent();
            _FamiliesIdsList = null;
            SetObjectsDefaultsAndEvents();
            TranslateControls();
        }

        public FimiliesView(IEnumerable<int> FamiliesIdsList)
        {
            InitializeComponent();
            _FamiliesIdsList = FamiliesIdsList;
            SetObjectsDefaultsAndEvents();
            TranslateControls();
        }

        private void TranslateControls()
        {
            this.Text = Properties.Resources.Families;
            btnBail.ToolTipText = Properties.Resources.SetBail;
            btnDelete.ToolTipText = Properties.Resources.Detele;
            btnExclude.ToolTipText = Properties.Resources.Exclude;
            btnShowFathers.ToolTipText = Properties.Resources.ShowFathers;
            btnColumn.ToolTipText = Properties.Resources.Columns;
            btnEdit.ToolTipText = Properties.Resources.Edit;
            btnSetColor.ToolTipText = Properties.Resources.Edit + " " + Properties.Resources.Color;
            btnShowMothers.ToolTipText = Properties.Resources.ShowMothers;
            btnShowOrphans.ToolTipText = Properties.Resources.ShowOrphans;
        }

        private void SetObjectsDefaultsAndEvents()
        {
            this.Text = Properties.Resources.OrphanViewTitle;
            _familiesViewModel.DataLoaded += _Families_DataLoaded;
            orphanageGridView1.GridView.SelectionChanged += GridView_SelectionChanged;
            // set RadGridHelper
            _radGridHelper.GridView = orphanageGridView1.GridView;
        }

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void UpdateControls()
        {
            if (orphanageGridView1.SelectedRows != null)
            {
                var OrphansCountObject = _radGridHelper.GetValueBySelectedRow("OrphansCount");
                var IsBailedObject = _radGridHelper.GetValueBySelectedRow("IsBailed");
                var IsExcludedObject = _radGridHelper.GetValueBySelectedRow("IsExcluded");
                if (OrphansCountObject != null)
                {
                    int orphansCount;
                    int.TryParse(OrphansCountObject.ToString(), out orphansCount);
                    bool value = orphansCount > 0 ? true : false;
                    btnShowOrphans.Enabled = value;
                }
                if (IsBailedObject != null)
                {
                    bool isBailed;
                    bool.TryParse(IsBailedObject.ToString(), out isBailed);
                    btnBail.Enabled = !isBailed;
                }
                if (IsExcludedObject != null)
                {
                    bool isExcluded;
                    bool.TryParse(IsBailedObject.ToString(), out isExcluded);
                    btnExclude.Enabled = !isExcluded;
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

        private void _Families_DataLoaded(object sender, EventArgs e)
        {
            orphanageGridView1.GridView.DataSource = _familiesViewModel.Families;
        }

        private void FimiliesView_Load(object sender, EventArgs e)
        {
            //load saved layout
            if (System.IO.File.Exists(Properties.Settings.Default.FamiliesLayoutFilePath))
                orphanageGridView1.GridView.LoadLayout(Properties.Settings.Default.FamiliesLayoutFilePath);
            //load orphans data
            if (_FamiliesIdsList != null)
                _familiesViewModel.LoadFamilies(_FamiliesIdsList);
            else
                _familiesViewModel.LoadFamilies();
            //set default grid values
            orphanageGridView1.GridView.AllowAutoSizeColumns = true;
            orphanageGridView1.GridView.PageSize = Properties.Settings.Default.DefaultPageSize;
        }

        private void btnShowOrphans_Click(object sender, EventArgs e)
        {
            IList<int> ret = new List<int>();
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            var retOIds = _familiesViewModel.OrphansIds(selectedIds);
            Orphan.OrphansView or = new Orphan.OrphansView(retOIds);
            or.Show();
        }

        private async void btnSetColor_Click(object sender, EventArgs e)
        {
            var dialogResult = radColorDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                var selectedIds = orphanageGridView1.SelectedIds;
                if (selectedIds == null || selectedIds.Count == 0)
                    return;
                foreach (var id in selectedIds)
                {
                    _radGridHelper.UpdateRowColor("ColorMark", await _familiesViewModel.SetColor(id, radColorDialog.Color.ToArgb()), "Id", id);
                }
            }
        }

        private void btnColumn_Click(object sender, EventArgs e)
        {
            orphanageGridView1.ShowColumnsChooser();
        }

        private void FimiliesView_FormClosing(object sender, FormClosingEventArgs e)
        {
            orphanageGridView1.GridView.SaveLayout(Properties.Settings.Default.FamiliesLayoutFilePath);
        }

        private void btnShowMothers_Click(object sender, EventArgs e)
        {
            IList<int> ret = new List<int>();
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            var retOIds = _familiesViewModel.MothersIds(selectedIds);
            Mother.MothersView or = new Mother.MothersView(retOIds);
            or.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var objId = _radGridHelper.GetValueBySelectedRow("Id");
            if (objId != null)
            {
                int id = (int)objId;
                FamilyEditView familyEditView = new FamilyEditView(id);
                familyEditView.ShowDialog();
                _familiesViewModel.Update(id);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            await _familiesViewModel.Delete(orphanageGridView1.SelectedIds);
        }

        private void btnShowFathers_Click(object sender, EventArgs e)
        {
            IList<int> ret = new List<int>();
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            var retOIds = _familiesViewModel.FathersIds(selectedIds);
            FathersView or = new FathersView(retOIds);
            or.Show();
        }

        private async void btnExclude_Click(object sender, EventArgs e)
        {
            await _familiesViewModel.Exclude(orphanageGridView1.SelectedIds);
        }
    }
}