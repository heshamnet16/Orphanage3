using OrphanageV3.ViewModel.Mother;
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
namespace OrphanageV3.Views.Mother
{
    public partial class MothersView : Telerik.WinControls.UI.RadForm
    {
        private MothersViewModel _mothersViewModel = Program.Factory.Resolve<MothersViewModel>();
        private IRadGridHelper _radGridHelper = Program.Factory.Resolve<IRadGridHelper>();
        private IList<int> _MothersIdsList;

        public MothersView()
        {
            InitializeComponent();
            _MothersIdsList = null;
            SetObjectsDefaultsAndEvents();
            TranslateControls();
        }

        public MothersView(IList<int> MothersIdsList)
        {
            InitializeComponent();
            _MothersIdsList = MothersIdsList;
            SetObjectsDefaultsAndEvents();
            TranslateControls();
        }
        private void TranslateControls()
        {
            this.Text = Properties.Resources.Mothers;
            btnColumn.ToolTipText = Properties.Resources.Columns;
            btnEdit.ToolTipText = Properties.Resources.Edit;
            btnSetColor.ToolTipText = Properties.Resources.Edit + " " + Properties.Resources.Color;
            btnShowFamilies.ToolTipText = Properties.Resources.ShowFamilies;
            btnShowFathers.ToolTipText = Properties.Resources.ShowFathers;
            btnShowOrphans.ToolTipText = Properties.Resources.ShowOrphans;
        }
        private void SetObjectsDefaultsAndEvents()
        {
            this.Text = Properties.Resources.OrphanViewTitle;
            _mothersViewModel.DataLoaded += _Mothers_DataLoaded;
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
                    btnShowFamilies.Enabled = value;
                    btnShowFathers.Enabled = value;
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

        private void _Mothers_DataLoaded(object sender, EventArgs e)
        {
            orphanageGridView1.GridView.DataSource = _mothersViewModel.Mothers;
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
                    _radGridHelper.UpdateRowColor("ColorMark", await _mothersViewModel.SetColor(id, radColorDialog.Color.ToArgb()), "Id", id);
                }
            }
        }

        private void btnColumn_Click(object sender, EventArgs e)
        {
            orphanageGridView1.ShowColumnsChooser();
        }

        private void OrphansView_FormClosing(object sender, FormClosingEventArgs e)
        {
            orphanageGridView1.GridView.SaveLayout(Properties.Settings.Default.MotherLayoutFilePath);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var objId = _radGridHelper.GetValueBySelectedRow("Id");
            if (objId != null)
            {//MotherEditView motherEditView = new MotherEditView(id);
             //motherEditView.ShowDialog();
             //_caregiversViewModel.Update(id);
            }
        }

        private void MothersView_Load(object sender, EventArgs e)
        {
            //load saved layout 
            if (System.IO.File.Exists(Properties.Settings.Default.MotherLayoutFilePath))
                orphanageGridView1.GridView.LoadLayout(Properties.Settings.Default.MotherLayoutFilePath);
            //load orphans data
            if (_MothersIdsList != null)
                _mothersViewModel.LoadMothers(_MothersIdsList);
            else
                _mothersViewModel.LoadMothers();
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
                var retOIds = await _mothersViewModel.OrphansIds(id);
                if (retOIds != null && retOIds.Count > 0)
                    foreach (var retId in retOIds)
                        ret.Add(retId);
            }
            Orphan.OrphansView or = new Orphan.OrphansView(ret);
            or.Show();
        }
    }
}
