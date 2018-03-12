﻿using OrphanageV3.ViewModel.Orphan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Unity;
using System.Linq;
using System.Threading;
using Telerik.WinControls.UI;
using OrphanageV3.Views.Helper;
using OrphanageV3.Views.Helper.Interfaces;

namespace OrphanageV3.Views.Orphan
{
    public partial class OrphansView : Telerik.WinControls.UI.RadForm
    {
        private OrphansViewModel _orphansViewModel = Program.Factory.Resolve<OrphansViewModel>();
        private IRadGridHelper _radGridHelper = Program.Factory.Resolve<IRadGridHelper>();
        private object lockObj = new object();
        private Thread upadteImageDetailsThread;
        private IList<Thread> PagingThreads = new List<Thread>();
        public OrphansView()
        {
            InitializeComponent();
            this.Text = Properties.Resources.OrphanViewTitle;
            _orphansViewModel.OrphansChangedEvent += OrphansViewModel_OrphansChangedEvent;
            orphanageGridView1.GridView.PageChanged += GridView_PageChanged;
            orphanageGridView1.GridView.SelectionChanged += GridView_SelectionChanged;
            orphanageGridView1.GridView.GroupExpanded += GridView_GroupExpanded;
            orphanageGridView1.HideShowColumnName = "IsExcluded";
            // set RadGridHelper
            _radGridHelper.GridView = orphanageGridView1.GridView;
        }

        private void GridView_GroupExpanded(object sender, GroupExpandedEventArgs e)
        {
            CloseOtherThreads();
            var ids = e.DataGroup.Where(r => r.Cells["ID"].Value != null).Select(c=>(int)c.Cells["ID"].Value).ToList();
            StartThumbnailsThread(ids);

        }


        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateOrphanPictureAndDetails();
            UpdateControls();
        }

        private void StartThumbnailsThread(IList<int> idsListFromGrid)
        {
            var t = new Thread(new ParameterizedThreadStart((IdsList) =>
            {
                IList<int> idsList = (IList<int>)IdsList;
                if (idsList.Count > 0)
                    try
                    {
                        foreach (var id in idsList)
                        {
                            _orphansViewModel.UpdateOrphanPhoto(id).Wait();
                            this.Invoke(new MethodInvoker(() =>
                            {
                                _radGridHelper.InvalidateRow("ID", id);
                            }));
                        }
                        //_orphansViewModel.LoadImages(idsList).Wait();
                    }
                    catch (Exception ex)
                    {
                        if (ex.HResult != -2146233063 && ex.HResult != -2146233040)
                            throw ex;
                    }
            }));
            t.Priority = ThreadPriority.Normal;
            t.IsBackground = true;
            t.Start(idsListFromGrid);
            PagingThreads.Add(t);
        }
        private void CloseOtherThreads()
        {
            foreach (Thread th in PagingThreads)
            {
                if (th.IsAlive)
                {
                    //TODO abort or low priority
                    //th.Priority = ThreadPriority.Lowest;
                    th.Interrupt();
                    th.Abort();
                }
            }

            if (PagingThreads.Count(th => th.IsAlive) == 0)
                PagingThreads.Clear();
        }
        private void UpdateOrphanPictureAndDetails()
        {
            if (upadteImageDetailsThread != null && upadteImageDetailsThread.IsAlive)
                upadteImageDetailsThread.Abort();
            upadteImageDetailsThread = new Thread(new ThreadStart(async () =>
            {
                if (orphanageGridView1.GridView.SelectedRows.Count == 1)
                {
                    var id = (int)_radGridHelper.GetValueBySelectedRow("ID");
                    var photoTask = _orphansViewModel.GetOrphanFacePhoto(id);
                    picPhoto.Image = await photoTask;
                    BeginInvoke(new MethodInvoker(async () => { txtDetails.Text = await _orphansViewModel.GetOrphanSummary(id); }));

                }
            }));
            upadteImageDetailsThread.Priority = ThreadPriority.Highest;
            upadteImageDetailsThread.IsBackground = true;
            upadteImageDetailsThread.Start();
        }
        private void UpdateControls()
        {
            var retObject = _radGridHelper.GetValueBySelectedRow("IsExcluded");
            if (retObject != null)
            {
                bool isExcluded;
                bool.TryParse(retObject.ToString(), out isExcluded);
                btnExclud.Image = isExcluded ? Properties.Resources.UnhidePic : Properties.Resources.HidePic;
                btnExclud.ToolTipText = isExcluded ? Properties.Resources.UnExclude : Properties.Resources.Exclude;
            }
        }
        private void GridView_PageChanged(object sender, EventArgs e)
        {
            CloseOtherThreads();
            IList<int> idsListFromGrid = _radGridHelper.GetCurrentRows("ID").ToList();
            StartThumbnailsThread(idsListFromGrid);

        }
        private void OrphansViewModel_PhotoLoadedEvent(int Oid, Image img)
        {
            //var row = _radGridHelper.GetRowByColumnName("ID", Oid);
            //row.Cells["Photo"].Value = img;
            _orphansViewModel.UpdateOrphanPhoto(Oid, img);
        }

        private void OrphansViewModel_OrphansChangedEvent()
        {
            orphanageGridView1.GridView.DataSource = _orphansViewModel.Orphans;
            var PicColumn = orphanageGridView1.GridView.Columns["Photo"];
            PicColumn.ImageLayout = ImageLayout.Stretch;
            orphanageGridView1.GridView.Columns["Photo"].Width = 80;
        }

        private void OrphansView_Load(object sender, EventArgs e)
        {
            //load saved layout 
            if (System.IO.File.Exists(Properties.Settings.Default.OrphanLayoutFilePath))
                orphanageGridView1.GridView.LoadLayout(Properties.Settings.Default.OrphanLayoutFilePath);
            //load orphans data
            _orphansViewModel.LoadData();
            //set default grid values
            orphanageGridView1.GridView.TableElement.RowHeight = 80;
            orphanageGridView1.GridView.AllowAutoSizeColumns = true;
            orphanageGridView1.GridView.PageSize = Properties.Settings.Default.DefaultPageSize;
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var id = (int)_radGridHelper.GetValueBySelectedRow("ID");
            var ret = await _orphansViewModel.DeleteOrphan(id);
            if (ret)
                _radGridHelper.HideRow("ID", id);
        }

        private async void btnExclud_Click(object sender, EventArgs e)
        {
            var id = (int)_radGridHelper.GetValueBySelectedRow("ID");
            var ret = await _orphansViewModel.ExcludeOrphan(id);
            if (ret)
                _radGridHelper.HideRow("ID", id);
        }

        private async void btnSetColor_Click(object sender, EventArgs e)
        {
            var id = (int)_radGridHelper.GetValueBySelectedRow("ID");
            var dialogResult = radColorDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {

                _radGridHelper.UpdateRowColor("ColorMark", await _orphansViewModel.SetColor(id, radColorDialog.Color.ToArgb()), "ID", id);

            }
        }

        private void btnColumn_Click(object sender, EventArgs e)
        {
            orphanageGridView1.ShowColumnsChooser();
        }

        private void OrphansView_FormClosing(object sender, FormClosingEventArgs e)
        {
            orphanageGridView1.GridView.SaveLayout(Properties.Settings.Default.OrphanLayoutFilePath);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = (int)_radGridHelper.GetValueBySelectedRow("ID");
            OrphanEditView orphanEditView = new OrphanEditView(id);
            orphanEditView.ShowDialog();
        }
    }
}
