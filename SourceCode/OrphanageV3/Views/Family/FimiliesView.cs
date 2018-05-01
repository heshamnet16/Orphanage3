using OrphanageV3.Controlls;
using OrphanageV3.ViewModel.Family;
using OrphanageV3.Views.Father;
using OrphanageV3.Views.Helper.Interfaces;
using OrphanageV3.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace OrphanageV3.Views.Family
{
    public partial class FimiliesView : Telerik.WinControls.UI.RadForm, IView
    {
        private FamiliesViewModel _familiesViewModel = Program.Factory.Resolve<FamiliesViewModel>();
        private IRadGridHelper _radGridHelper = Program.Factory.Resolve<IRadGridHelper>();
        private IEnumerable<int> _FamiliesIdsList;
        private IEnumerable<OrphanageDataModel.RegularData.Family> _FamiliesList;

        public string GetTitle() => this.Text;

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

        public FimiliesView(IEnumerable<OrphanageDataModel.RegularData.Family> FamiliesList)
        {
            InitializeComponent();
            _FamiliesList = FamiliesList;
            orphanageGridView1.ShowHiddenRows = true;
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
            _familiesViewModel.BailsLoaded += _familiesViewModel_BailsLoaded;
            orphanageGridView1.GridView.SelectionChanged += GridView_SelectionChanged;
            // set RadGridHelper
            _radGridHelper.GridView = orphanageGridView1.GridView;
        }

        private void _familiesViewModel_BailsLoaded(object sender, EventArgs e)
        {
            btnBail.Enabled = true;
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
                    btnBail.Image = isBailed ? Properties.Resources.UnBailPic : Properties.Resources.BailPic;
                    btnBail.ToolTipText = isBailed ? Properties.Resources.UnsetBail : Properties.Resources.SetBail;
                }
                if (IsExcludedObject != null)
                {
                    bool isExcluded;
                    bool.TryParse(IsExcludedObject.ToString(), out isExcluded);
                    btnExclude.Image = isExcluded ? Properties.Resources.UnhidePic : Properties.Resources.HidePic;
                    btnExclude.ToolTipText = isExcluded ? Properties.Resources.UnExclude : Properties.Resources.Exclude;
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
            {
                if (_FamiliesList != null)
                    _familiesViewModel.LoadFamilies(_FamiliesList);
                else
                    _familiesViewModel.LoadFamilies();
            }
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
            or.MdiParent = this.MdiParent;
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
            or.MdiParent = this.MdiParent;
            or.Show();
        }

        private async void btnExclude_Click(object sender, EventArgs e)
        {
            if (btnExclude.ToolTipText == Properties.Resources.Exclude)
                await _familiesViewModel.Exclude(orphanageGridView1.SelectedIds);
            else
                await _familiesViewModel.UnExclude(orphanageGridView1.SelectedIds);
        }

        private void btnBail_Click(object sender, EventArgs e)
        {
            if (orphanageGridView1.SelectedIds == null || orphanageGridView1.SelectedIds.Count == 0) return;
            if (btnBail.ToolTipText == Properties.Resources.SetBail)
            {
                ChooserView.ChooserView bailsChooser = new ChooserView.ChooserView(_familiesViewModel.FamiliesBails.ToList<object>(), Properties.Resources.ChooseBail);
                bailsChooser.MultiSelect = false;
                bailsChooser.ShowDialog();
                if (bailsChooser.DialogResult == DialogResult.OK)
                {
                    var bail = (ViewModel.Bail.BailModel)bailsChooser.SelectedObject;
                    if (bail != null && bail.Id > 0)
                    {
                        _familiesViewModel.BailFamilies(bail.Id, orphanageGridView1.SelectedIds);
                    }
                }
            }
            else
            {
                _familiesViewModel.UnBailFamilies(orphanageGridView1.SelectedIds);
            }
        }

        public OrphanageGridView GetOrphanageGridView()
        {
            return orphanageGridView1;
        }

        public void Update(int ObjectId)
        {
            _familiesViewModel.Update(ObjectId);
        }
    }
}