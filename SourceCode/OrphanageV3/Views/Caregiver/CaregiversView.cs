﻿using OrphanageV3.Controlls;
using OrphanageV3.ViewModel.Caregiver;
using OrphanageV3.Views.Helper.Interfaces;
using OrphanageV3.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Unity;

namespace OrphanageV3.Views.Caregiver
{
    public partial class CaregiversView : Telerik.WinControls.UI.RadForm, IView
    {
        private CaregiversViewModel _caregiversViewModel = Program.Factory.Resolve<CaregiversViewModel>();
        private IRadGridHelper _radGridHelper = Program.Factory.Resolve<IRadGridHelper>();

        public string GetTitle() => this.Text;

        public CaregiversView()
        {
            InitializeComponent();
            _caregiversViewModel.DataLoaded += _caregiversViewModel_DataLoaded;
            orphanageGridView1.GridView.SelectionChanged += GridView_SelectionChanged;

            _radGridHelper.GridView = orphanageGridView1.GridView;
            TranslateControls();
        }

        private void TranslateControls()
        {
            this.Text = Properties.Resources.Caregivers;
            btnColumn.ToolTipText = Properties.Resources.Columns;
            btnDelete.ToolTipText = Properties.Resources.Detele;
            btnEdit.ToolTipText = Properties.Resources.Edit;
            btnSetColor.ToolTipText = Properties.Resources.Edit + " " + Properties.Resources.Color;
            btnShowFamilies.ToolTipText = Properties.Resources.ShowFamilies;
            btnShowFathers.ToolTipText = Properties.Resources.ShowFathers;
            btnShowMothers.ToolTipText = Properties.Resources.ShowMothers;
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
                var retObject = _radGridHelper.GetValueBySelectedRow("OrphansCount");
                if (retObject != null)
                {
                    int orphansCount;
                    int.TryParse(retObject.ToString(), out orphansCount);
                    bool value = orphansCount > 0 ? true : false;
                    btnShowFamilies.Enabled = value;
                    btnShowFathers.Enabled = value;
                    btnShowMothers.Enabled = value;
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

        private void _caregiversViewModel_DataLoaded(object sender, EventArgs e)
        {
            orphanageGridView1.GridView.DataSource = _caregiversViewModel.Caregivers;
        }

        private void orphanageGridView1_Load(object sender, EventArgs e)
        {
            _caregiversViewModel.LoadCaregivers();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            foreach (var id in selectedIds)
            {
                var ret = await _caregiversViewModel.Delete(id, true);
                if (ret)
                    _radGridHelper.HideRow("Id", id);
            }
        }

        private void btnColumn_Click(object sender, EventArgs e)
        {
            orphanageGridView1.ShowColumnsChooser();
        }

        private void btnShowOrphans_Click(object sender, EventArgs e)
        {
            IList<int> ret = new List<int>();
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            foreach (var id in selectedIds)
            {
                var retOIds = _caregiversViewModel.OrphansIds(id);
                if (retOIds != null && retOIds.Count > 0)
                    foreach (var retId in retOIds)
                        ret.Add(retId);
            }
            Orphan.OrphansView or = new Orphan.OrphansView(ret);
            or.MdiParent = this.MdiParent;
            or.Show();
        }

        private void CaregiversView_FormClosing(object sender, FormClosingEventArgs e)
        {
            orphanageGridView1.GridView.SaveLayout(Properties.Settings.Default.CaregiverLayoutFilePath);
        }

        private void CaregiversView_Load(object sender, EventArgs e)
        {
            if (File.Exists(Properties.Settings.Default.CaregiverLayoutFilePath))
            {
                orphanageGridView1.GridView.LoadLayout(Properties.Settings.Default.CaregiverLayoutFilePath);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = (int)_radGridHelper.GetValueBySelectedRow("Id");
            CaregiverEditView caregiverEditView = new CaregiverEditView(id);
            caregiverEditView.ShowDialog();
            _caregiversViewModel.Update(id);
        }

        private async void btnShowFamilies_Click(object sender, EventArgs e)
        {
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            var familiesIds = await _caregiversViewModel.FamiliesIds(selectedIds);
            Family.FimiliesView familiesView = new Family.FimiliesView(familiesIds);
            familiesView.MdiParent = this.MdiParent;
            familiesView.Show();
        }

        private async void btnShowMothers_Click(object sender, EventArgs e)
        {
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            var mothersIds = await _caregiversViewModel.MothersIds(selectedIds);
            Mother.MothersView mothersView = new Mother.MothersView(mothersIds);
            mothersView.MdiParent = this.MdiParent;
            mothersView.Show();
        }

        private async void btnShowFathers_Click(object sender, EventArgs e)
        {
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            var fathersIds = await _caregiversViewModel.FathersIds(selectedIds);
            Father.FathersView fathersView = new Father.FathersView(fathersIds);
            fathersView.MdiParent = this.MdiParent;
            fathersView.Show();
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
                    _radGridHelper.UpdateRowColor("ColorMark", await _caregiversViewModel.SetColor(id, radColorDialog.Color.ToArgb()), "Id", id);
                }
            }
        }

        public OrphanageGridView GetOrphanageGridView()
        {
            return orphanageGridView1;
        }

        public void Update(int ObjectId)
        {
            _caregiversViewModel.Update(ObjectId);
        }
    }
}