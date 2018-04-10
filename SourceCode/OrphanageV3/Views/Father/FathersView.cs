using OrphanageV3.ViewModel.Father;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace OrphanageV3.Views.Father
{
    public partial class FathersView : Telerik.WinControls.UI.RadForm
    {
        private FathersViewModel _fathersViewModel = Program.Factory.Resolve<FathersViewModel>();
        private IRadGridHelper _radGridHelper = Program.Factory.Resolve<IRadGridHelper>();
        private IEnumerable<int> _FathersIdsList;

        public FathersView()
        {
            InitializeComponent();
            _FathersIdsList = null;
            SetObjectsDefaultsAndEvents();
            TranslateControls();
        }

        public FathersView(IEnumerable<int> FathersIdsList)
        {
            InitializeComponent();
            _FathersIdsList = FathersIdsList;
            SetObjectsDefaultsAndEvents();
            TranslateControls();
        }

        private void TranslateControls()
        {
            this.Text = Properties.Resources.Fathers;
            btnColumn.ToolTipText = Properties.Resources.Columns;
            btnEdit.ToolTipText = Properties.Resources.Edit;
            btnSetColor.ToolTipText = Properties.Resources.Edit + " " + Properties.Resources.Color;
            btnShowFamilies.ToolTipText = Properties.Resources.ShowFamilies;
            btnShowMothers.ToolTipText = Properties.Resources.ShowMothers;
            btnShowOrphans.ToolTipText = Properties.Resources.ShowOrphans;
        }

        private void SetObjectsDefaultsAndEvents()
        {
            this.Text = Properties.Resources.OrphanViewTitle;
            _fathersViewModel.DataLoaded += _Fathers_DataLoaded;
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
                var retObject = _radGridHelper.GetValueBySelectedRow("OrphansCount");
                if (retObject != null)
                {
                    int orphansCount;
                    int.TryParse(retObject.ToString(), out orphansCount);
                    bool value = orphansCount > 0 ? true : false;
                    btnShowOrphans.Enabled = value;
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

        private void _Fathers_DataLoaded(object sender, EventArgs e)
        {
            orphanageGridView1.GridView.DataSource = _fathersViewModel.Fathers;
        }

        private void FathersView_Load(object sender, EventArgs e)
        {
            //load saved layout
            if (System.IO.File.Exists(Properties.Settings.Default.FatherLayoutFilePath))
                orphanageGridView1.GridView.LoadLayout(Properties.Settings.Default.FatherLayoutFilePath);
            //load orphans data
            if (_FathersIdsList != null)
                _fathersViewModel.LoadFathers(_FathersIdsList);
            else
                _fathersViewModel.LoadFathers();
            //set default grid values
            orphanageGridView1.GridView.AllowAutoSizeColumns = true;
            orphanageGridView1.GridView.PageSize = Properties.Settings.Default.DefaultPageSize;
        }

        private async void btnShowOrphans_Click(object sender, EventArgs e)
        {
            IList<int> ret = new List<int>();
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            foreach (var id in selectedIds)
            {
                var retOIds = await _fathersViewModel.OrphansIds(id);
                if (retOIds != null && retOIds.Count > 0)
                    foreach (var retId in retOIds)
                        ret.Add(retId);
            }
            Orphan.OrphansView or = new Orphan.OrphansView(ret);
            or.MdiParent = this.MdiParent;
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
                    _radGridHelper.UpdateRowColor("ColorMark", await _fathersViewModel.SetColor(id, radColorDialog.Color.ToArgb()), "Id", id);
                }
            }
        }

        private void btnColumn_Click(object sender, EventArgs e)
        {
            orphanageGridView1.ShowColumnsChooser();
        }

        private void FathersView_FormClosing(object sender, FormClosingEventArgs e)
        {
            orphanageGridView1.GridView.SaveLayout(Properties.Settings.Default.FatherLayoutFilePath);
        }

        private void btnShowMothers_Click(object sender, EventArgs e)
        {
            IList<int> ret = new List<int>();
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            foreach (var id in selectedIds)
            {
                var retOIds = _fathersViewModel.MothersIds(id);
                if (retOIds != null && retOIds.Count > 0)
                    foreach (var retId in retOIds)
                        ret.Add(retId);
            }
            Mother.MothersView or = new Mother.MothersView(ret);
            or.MdiParent = this.MdiParent;
            or.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var objId = _radGridHelper.GetValueBySelectedRow("Id");
            if (objId != null)
            {
                int id = (int)objId;
                FatherEditView motherEditView = new FatherEditView(id);
                motherEditView.ShowDialog();
                _fathersViewModel.Update(id);
            }
        }

        private void btnShowFamilies_Click(object sender, EventArgs e)
        {
            IList<int> ret = new List<int>();
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            foreach (var id in selectedIds)
            {
                var retOIds = _fathersViewModel.FamiliesIds(id);
                if (retOIds != null && retOIds.Count > 0)
                    foreach (var retId in retOIds)
                        ret.Add(retId);
            }
            Family.FimiliesView or = new Family.FimiliesView(ret);
            or.MdiParent = this.MdiParent;
            or.Show();
        }
    }
}