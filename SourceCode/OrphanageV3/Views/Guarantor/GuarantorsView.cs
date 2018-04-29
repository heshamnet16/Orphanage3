using OrphanageV3.Controlls;
using OrphanageV3.ViewModel.Guarantor;
using OrphanageV3.Views.Helper.Interfaces;
using OrphanageV3.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace OrphanageV3.Views.Guarantor
{
    public partial class GuarantorsView : Telerik.WinControls.UI.RadForm, IView
    {
        private GuarantorsViewModel _guarantorsViewModel = Program.Factory.Resolve<GuarantorsViewModel>();
        private IRadGridHelper _radGridHelper = Program.Factory.Resolve<IRadGridHelper>();
        private IEnumerable<int> _guarantorsIds = null;

        public GuarantorsView()
        {
            InitializeComponent();
            _guarantorsViewModel.DataLoaded += _guarantorsViewModel_DataLoaded;
            orphanageGridView1.GridView.SelectionChanged += GridView_SelectionChanged;
            orphanageGridView1.Load += orphanageGridView1_Load;
            _radGridHelper.GridView = orphanageGridView1.GridView;
            TranslateControls();
        }

        public GuarantorsView(IEnumerable<int> guarantorsIds)
        {
            InitializeComponent();
            _guarantorsViewModel.DataLoaded += _guarantorsViewModel_DataLoaded;
            orphanageGridView1.GridView.SelectionChanged += GridView_SelectionChanged;
            orphanageGridView1.Load += orphanageGridView1_Load;
            _radGridHelper.GridView = orphanageGridView1.GridView;
            TranslateControls();
            _guarantorsIds = guarantorsIds;
        }

        private void TranslateControls()
        {
            this.Text = Properties.Resources.Guarantors;
            btnColumn.ToolTipText = Properties.Resources.Columns;
            btnDelete.ToolTipText = Properties.Resources.Detele;
            btnEdit.ToolTipText = Properties.Resources.Edit;
            btnSetColor.ToolTipText = Properties.Resources.Edit + " " + Properties.Resources.Color;
            btnShowFamilies.ToolTipText = Properties.Resources.ShowFamilies;
            btnShowOrphans.ToolTipText = Properties.Resources.ShowOrphans;
            btnShowBails.ToolTipText = Properties.Resources.ShowBails;
        }

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void UpdateControls()
        {
            if (orphanageGridView1.SelectedRows != null)
            {
                var retOrphansCountObject = _radGridHelper.GetValueBySelectedRow("OrphansCount");
                var retFamiliesCountObject = _radGridHelper.GetValueBySelectedRow("FamiliesCount");
                var retBailsCountObject = _radGridHelper.GetValueBySelectedRow("BailsCount");
                if (retOrphansCountObject != null)
                {
                    int orphansCount;
                    int.TryParse(retOrphansCountObject.ToString(), out orphansCount);
                    bool value = orphansCount > 0 ? true : false;
                    btnShowOrphans.Enabled = value;
                }
                if (retFamiliesCountObject != null)
                {
                    int familiesCount;
                    int.TryParse(retFamiliesCountObject.ToString(), out familiesCount);
                    bool value = familiesCount > 0 ? true : false;
                    btnShowFamilies.Enabled = value;
                }
                if (retBailsCountObject != null)
                {
                    int bailsCount;
                    int.TryParse(retBailsCountObject.ToString(), out bailsCount);
                    bool value = bailsCount > 0 ? true : false;
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

        private void _guarantorsViewModel_DataLoaded(object sender, EventArgs e)
        {
            orphanageGridView1.GridView.DataSource = _guarantorsViewModel.Guarantors;
        }

        private void orphanageGridView1_Load(object sender, EventArgs e)
        {
            if (_guarantorsIds != null)
            {
                _guarantorsViewModel.LoadGuarantors(_guarantorsIds);
            }
            else
            {
                _guarantorsViewModel.LoadGuarantors();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            foreach (var id in selectedIds)
            {
                var ret = await _guarantorsViewModel.Delete(id, true);
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
            foreach (var id in selectedIds)
            {
                var retOIds = await _guarantorsViewModel.OrphansIds(id);
                if (retOIds != null && retOIds.Count() > 0)
                    foreach (var retId in retOIds)
                        ret.Add(retId);
            }
            Orphan.OrphansView or = new Orphan.OrphansView(ret);
            or.MdiParent = this.MdiParent;
            or.Show();
        }

        private void GuarantorsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            orphanageGridView1.GridView.SaveLayout(Properties.Settings.Default.GuarantorsLayoutFilePath);
        }

        private void GuarantorsView_Load(object sender, EventArgs e)
        {
            if (File.Exists(Properties.Settings.Default.GuarantorsLayoutFilePath))
            {
                orphanageGridView1.GridView.LoadLayout(Properties.Settings.Default.GuarantorsLayoutFilePath);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = (int)_radGridHelper.GetValueBySelectedRow("Id");
            GuarantorEditView guarantorEditView = new GuarantorEditView(id);
            guarantorEditView.ShowDialog();
            _guarantorsViewModel.Update(id);
        }

        private async void btnShowFamilies_Click(object sender, EventArgs e)
        {
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            var familiesIds = await _guarantorsViewModel.FamiliesIds(selectedIds);
            Family.FimiliesView familiesView = new Family.FimiliesView(familiesIds);
            familiesView.MdiParent = this.MdiParent;
            familiesView.Show();
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
                    _radGridHelper.UpdateRowColor("ColorMark", await _guarantorsViewModel.SetColor(id, radColorDialog.Color.ToArgb()), "Id", id);
                }
            }
        }

        private async void btnShowBails_Click(object sender, EventArgs e)
        {
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            var bailsIds = await _guarantorsViewModel.BailsIds(selectedIds);
            Bail.BailsView bailsView = new Bail.BailsView(bailsIds);
            bailsView.MdiParent = this.MdiParent;
            bailsView.Show();
        }

        public OrphanageGridView GetOrphanageGridView()
        {
            return orphanageGridView1;
        }

        public void Update(int ObjectId)
        {
            _guarantorsViewModel.Update(ObjectId);
        }
    }
}