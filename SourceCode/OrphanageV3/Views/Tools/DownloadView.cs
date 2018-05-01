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
    public partial class DownloadView : RadForm
    {
        private DownloadViewModel _downloadViewModel = null;

        private int _RadDropDownButtonWidthPadding = 5;
        private int _RadDropDownButtonHeight = 30;

        public DownloadView()
        {
            InitializeComponent();
            _downloadViewModel = Program.Factory.Resolve<DownloadViewModel>();
            _downloadViewModel.Added += _downloadViewModel_Added;
            _downloadViewModel.Removed += _downloadViewModel_Removed;
            _downloadViewModel.Downloaded += _downloadViewModel_Downloaded;
        }

        private void _downloadViewModel_Downloaded(DownloadDataModel downloadDataModel)
        {
            TableLayoutPanel tblToDelete = getControlById(downloadDataModel.Id);
            if (tblToDelete != null)
            {
                RadWaitingBar radWaitingBar = null;
                foreach (var cont in tblToDelete.Controls)
                {
                    if (cont is RadDropDownButton)
                    {
                        var btn = (RadDropDownButton)cont;
                        btn.Enabled = true;
                    }
                    if (cont is RadWaitingBar)
                    {
                        var wBar = (RadWaitingBar)cont;
                        wBar.StopWaiting();
                        radWaitingBar = wBar;
                    }
                }
                if (radWaitingBar != null)
                {
                    tblToDelete.Controls.Remove(radWaitingBar);
                    tblToDelete.ColumnStyles.RemoveAt(1);
                }
            }
        }

        private void _downloadViewModel_Removed(DownloadDataModel downloadDataModel)
        {
            TableLayoutPanel tblToDelete = getControlById(downloadDataModel.Id);
            if (tblToDelete != null)
                flowLayoutPanel1.Controls.Remove(tblToDelete);
        }

        private TableLayoutPanel getControlById(int DownloadDataModelId)
        {
            foreach (var con in flowLayoutPanel1.Controls)
            {
                try
                {
                    var tbl = (TableLayoutPanel)con;
                    var id = (int)tbl.Tag;
                    if (id == DownloadDataModelId)
                    {
                        return tbl;
                    }
                }
                catch { }
            }
            return null;
        }

        private void _downloadViewModel_Added(DownloadDataModel downloadDataModel)
        {
            CreateDownloadObject(downloadDataModel);
        }

        private TableLayoutPanel CreateTableLayoutPanel(DownloadDataModel downloadDataModel)
        {
            TableLayoutPanel tbl = new TableLayoutPanel();
            tbl.ColumnCount = 2;
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
            tbl.Name = "tableLayoutPanel1";
            tbl.RowCount = 1;
            tbl.Tag = downloadDataModel.Id;
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tbl.Size = new Size(flowLayoutPanel1.Width - 5, _RadDropDownButtonHeight); ;
            return tbl;
        }

        private void CreateDownloadObject(DownloadDataModel downloadDataModel)
        {
            var tbl = CreateTableLayoutPanel(downloadDataModel);
            RadDropDownButton btn = new RadDropDownButton();
            btn.Text = downloadDataModel.Name;
            btn.Tag = downloadDataModel.Id;
            btn.Dock = DockStyle.Fill;
            CreateMenuItems(btn);
            if (downloadDataModel.Data == null)
            {
                //not downloaded yet
                btn.Enabled = false;
                tbl.Controls.Add(btn, 0, 0);
                tbl.Controls.Add(createWatingBar(downloadDataModel), 1, 0);
            }
            else
            {
                //downloaded data
                tbl.Controls.Add(btn, 0, 0);
                tbl.ColumnStyles.RemoveAt(1);
            }
            flowLayoutPanel1.Controls.Add(tbl);
        }

        private RadWaitingBar createWatingBar(DownloadDataModel downloadDataModel)
        {
            RadWaitingBar radWaitingBar1 = new RadWaitingBar();
            DotsSpinnerWaitingBarIndicatorElement dotsSpinnerWaitingBarIndicatorElement1 = new DotsSpinnerWaitingBarIndicatorElement();
            radWaitingBar1.Dock = DockStyle.Fill;
            radWaitingBar1.Location = new Point(3, 3);
            radWaitingBar1.Size = new Size(54, 57);
            radWaitingBar1.TabIndex = 1;
            radWaitingBar1.WaitingIndicators.Add(dotsSpinnerWaitingBarIndicatorElement1);
            radWaitingBar1.WaitingSpeed = 100;
            radWaitingBar1.WaitingStyle = Telerik.WinControls.Enumerations.WaitingBarStyles.DotsSpinner;
            radWaitingBar1.Tag = downloadDataModel.Id;
            radWaitingBar1.StartWaiting();
            return radWaitingBar1;
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
                CreateDownloadObject(itm);
            }
        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            foreach (var elem in flowLayoutPanel1.Controls)
            {
                var tbl = (TableLayoutPanel)elem;
                tbl.Size = new Size(flowLayoutPanel1.Width - _RadDropDownButtonWidthPadding, _RadDropDownButtonHeight);
            }
        }
    }
}