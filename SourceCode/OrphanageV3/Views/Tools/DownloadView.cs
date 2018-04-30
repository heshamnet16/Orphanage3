using OrphanageV3.ViewModel.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Unity;

namespace OrphanageV3.Views.Tools
{
    public partial class DownloadView : Telerik.WinControls.UI.RadForm
    {
        private DownloadViewModel _downloadViewModel = null;

        public DownloadView()
        {
            InitializeComponent();
            _downloadViewModel = Program.Factory.Resolve<DownloadViewModel>();
            _downloadViewModel.Added += _downloadViewModel_Added;
            _downloadViewModel.Removed += _downloadViewModel_Removed;
        }

        private void _downloadViewModel_Removed(DownloadDataModel downloadDataModel)
        {
            RadDropDownButton btnToDelete = null;
            foreach (var con in flowLayoutPanel1.Controls)
            {
                try
                {
                    var btn = (RadDropDownButton)con;
                    var id = (int)btn.Tag;
                    if (id == downloadDataModel.Id)
                    {
                        btnToDelete = btn;
                        break;
                    }
                }
                catch { }
            }
            if (btnToDelete != null)
                flowLayoutPanel1.Controls.Remove(btnToDelete);
        }

        private void _downloadViewModel_Added(DownloadDataModel downloadDataModel)
        {
            CreateDropDownButton(downloadDataModel);
        }

        private void CreateDropDownButton(DownloadDataModel downloadDataModel)
        {
            RadDropDownButton btn = new RadDropDownButton();
            btn.Text = downloadDataModel.Name;
            btn.Tag = downloadDataModel.Id;
            btn.Size = new Size(flowLayoutPanel1.Width - 5, 30);
            CreateMenuItems(btn);
            flowLayoutPanel1.Controls.Add(btn);
        }

        private void CreateMenuItems(RadDropDownButton btn)
        {
            RadMenuItem mnuCancel = new RadMenuItem(Properties.Resources.CancelText);
            mnuCancel.Tag = btn.Tag;
            mnuCancel.Click += MnuCancel_Click;
            btn.Items.Add(mnuCancel);
            RadMenuItem mnuSave = new RadMenuItem(Properties.Resources.SaveAs);
            mnuSave.Tag = btn.Tag;
            mnuSave.Click += MnuSave_Click;
            btn.Items.Add(mnuSave);
        }

        private void MnuSave_Click(object sender, EventArgs e)
        {
            var mnu = (RadMenuItem)sender;
            var id = (int)mnu.Tag;
            var downloadedModel = _downloadViewModel.Get(id);
            _downloadViewModel.Save(downloadedModel);
        }

        private void MnuCancel_Click(object sender, EventArgs e)
        {
            var mnu = (RadMenuItem)sender;
            var id = (int)mnu.Tag;
            var downloadedModel = _downloadViewModel.Get(id);
            _downloadViewModel.Remove(downloadedModel);
        }

        private void DownloadView_Load(object sender, EventArgs e)
        {
            foreach (var itm in _downloadViewModel.DownloadedDataList)
            {
                CreateDropDownButton(itm);
            }
        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            foreach (var elem in flowLayoutPanel1.Controls)
            {
                var btn = (RadDropDownButton)elem;
                btn.Size = new Size(flowLayoutPanel1.Width - 5, 30);
            }
        }
    }
}