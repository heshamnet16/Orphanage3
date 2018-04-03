using OrphanageV3.ViewModel.Orphan;
using OrphanageV3.Views.Family;
using OrphanageV3.Views.Father;
using OrphanageV3.Views.Helper.Interfaces;
using OrphanageV3.Views.Mother;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Unity;

namespace OrphanageV3.Views.Orphan
{
    public partial class OrphansView : Telerik.WinControls.UI.RadForm
    {
        private OrphansViewModel _orphansViewModel = Program.Factory.Resolve<OrphansViewModel>();
        private IRadGridHelper _radGridHelper = Program.Factory.Resolve<IRadGridHelper>();
        private object lockObj = new object();
        private Thread upadteImageDetailsThread;
        private IList<Thread> PagingThreads = new List<Thread>();
        private static object _loading = Properties.Resources.loading;

        private IEnumerable<int> _orphansIds;
        private IEnumerable<OrphanageDataModel.Persons.Orphan> _orphansList;

        public OrphansView()
        {
            InitializeComponent();
            SetObjectsDefaultsAndEvents();
            _orphansIds = null;
        }

        public OrphansView(IEnumerable<int> OrphansIds)
        {
            InitializeComponent();
            SetObjectsDefaultsAndEvents();
            _orphansIds = OrphansIds;
        }

        public OrphansView(IEnumerable<OrphanageDataModel.Persons.Orphan> OrphansList)
        {
            InitializeComponent();
            SetObjectsDefaultsAndEvents();
            _orphansList = OrphansList;
            orphanageGridView1.ShowHiddenRows = true;
        }

        private void SetObjectsDefaultsAndEvents()
        {
            this.Text = Properties.Resources.OrphanViewTitle;
            _orphansViewModel.DataLoaded += OrphansViewModel_DataLoadedEvent;
            orphanageGridView1.GridView.PageChanged += GridView_PageChanged;
            orphanageGridView1.GridView.SelectionChanged += GridView_SelectionChanged;
            orphanageGridView1.GridView.GroupExpanded += GridView_GroupExpanded;
            orphanageGridView1.GridView.SortChanged += GridView_SortChanged;
            orphanageGridView1.HideShowColumnName = "IsExcluded";
            // set RadGridHelper
            _radGridHelper.GridView = orphanageGridView1.GridView;
        }

        private void GridView_SortChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (e.GridViewTemplate.ChildRows != null)
            {
                CloseOtherThreads();
                var ids = e.GridViewTemplate.ChildRows.Where(r => r.Cells["Id"].Value != null).Select(c => (int)c.Cells["Id"].Value).ToList();
                StartThumbnailsThread(ids);
            }
        }

        private void GridView_GroupExpanded(object sender, GroupExpandedEventArgs e)
        {
            CloseOtherThreads();
            var ids = e.DataGroup.Where(r => r.Cells["Id"].Value != null).Select(c => (int)c.Cells["Id"].Value).ToList();
            StartThumbnailsThread(ids);
        }

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateOrphanPictureAndDetails();
            UpdateControls();
        }

        private void StartThumbnailsThread(IList<int> idsListFromGrid)
        {
            foreach (var id in idsListFromGrid)
            {
                _radGridHelper.UpadteCellData("Id", id, "Photo", ref _loading);
            }
            var t = new Thread(new ParameterizedThreadStart((IdsList) =>
            {
                IList<int> idsList = (IList<int>)IdsList;
                if (idsList.Count > 0)
                    try
                    {
                        foreach (var id in idsList)
                        {
                            _orphansViewModel.UpdateOrphanThumbnail(id).Wait();
                            this.Invoke(new MethodInvoker(() =>
                            {
                                _radGridHelper.InvalidateRow("Id", id);
                            }));
                        }
                        //_orphansViewModel.LoadImages(idsList).Wait();
                    }
                    catch (ObjectDisposedException) { }
                    catch (Exception ex)
                    {
                        if (ex.HResult != -2146233063 && ex.HResult != -2146233040 && ex.HResult != -2146233079)
                            throw ex;
                    }
                    finally
                    {
                        GC.Collect();
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
            txtDetails.Text = "............";
            picPhoto.Image = (Bitmap)_loading;
            if (upadteImageDetailsThread != null)
                upadteImageDetailsThread.Abort();
            upadteImageDetailsThread = new Thread(new ThreadStart(async () =>
            {
                if (orphanageGridView1.GridView.SelectedRows.Count == 1)
                {
                    var id = (int)_radGridHelper.GetValueBySelectedRow("Id");
                    var photoTask = _orphansViewModel.GetOrphanFacePhoto(id);
                    var OrphanSummaryTask = _orphansViewModel.GetOrphanSummary(id);
                    BeginInvoke(new MethodInvoker(async () => { txtDetails.Text = await OrphanSummaryTask; }));
                    picPhoto.Image = await photoTask;
                }
            }));
            upadteImageDetailsThread.Priority = ThreadPriority.Highest;
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
            IList<int> idsListFromGrid = _radGridHelper.GetCurrentRows("Id").ToList();
            StartThumbnailsThread(idsListFromGrid);
        }

        private void OrphansViewModel_DataLoadedEvent()
        {
            orphanageGridView1.GridView.DataSource = _orphansViewModel.Orphans;
            foreach (var bind in orphanageGridView1.DataBindings)
            {
            }
            var PicColumn = orphanageGridView1.GridView.Columns["Photo"];
            PicColumn.ImageLayout = ImageLayout.Stretch;
            orphanageGridView1.GridView.Columns["Photo"].Width = 80;
            Thread tt = new Thread(new ThreadStart(() =>
            {
                System.Threading.Thread.Sleep(1000);
                GridView_PageChanged(null, null);
            }));
            tt.Start();
        }

        private void OrphansView_Load(object sender, EventArgs e)
        {
            //load saved layout
            if (System.IO.File.Exists(Properties.Settings.Default.OrphanLayoutFilePath))
                orphanageGridView1.GridView.LoadLayout(Properties.Settings.Default.OrphanLayoutFilePath);
            //load orphans data
            if (_orphansIds != null)
                _orphansViewModel.LoadData(_orphansIds);
            else
            {
                if (_orphansList != null)
                    _orphansViewModel.LoadData(_orphansList);
                else
                    _orphansViewModel.LoadData();
            }
            //set default grid values
            orphanageGridView1.GridView.TableElement.RowHeight = 80;
            orphanageGridView1.GridView.AllowAutoSizeColumns = true;
            orphanageGridView1.GridView.PageSize = Properties.Settings.Default.DefaultPageSize;
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            foreach (var id in selectedIds)
            {
                var ret = await _orphansViewModel.DeleteOrphan(id);
                if (ret)
                    _radGridHelper.HideRow("Id", id);
            }
        }

        private async void btnExclud_Click(object sender, EventArgs e)
        {
            var selectedIds = orphanageGridView1.SelectedIds;
            if (selectedIds == null || selectedIds.Count == 0)
                return;
            foreach (var id in selectedIds)
            {
                if (btnExclud.ToolTipText == Properties.Resources.Exclude)
                {
                    var ret = await _orphansViewModel.ExcludeOrphan(id);
                    if (ret)
                        _radGridHelper.HideRow("Id", id);
                }
                else
                {
                    var ret = await _orphansViewModel.UnExcludeOrphan(id);
                    if (ret)
                        _radGridHelper.ShowRow("Id", id);
                }
            }
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
                    _radGridHelper.UpdateRowColor("ColorMark", await _orphansViewModel.SetColor(id, radColorDialog.Color.ToArgb()), "Id", id);
                }
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
            int id = (int)_radGridHelper.GetValueBySelectedRow("Id");
            OrphanEditView orphanEditView = new OrphanEditView(id);
            orphanEditView.ShowDialog();
            _orphansViewModel.UpdateOrphan(id);
        }

        private async void btnShowSiblings_Click(object sender, EventArgs e)
        {
            foreach (var id in orphanageGridView1.SelectedIds)
            {
                var brothersIds = await _orphansViewModel.GetBrothers(id);
                OrphansView orphansView = new OrphansView(brothersIds);
                orphansView.MdiParent = this.MdiParent;
                orphansView.Show();
            }
        }

        private void btnShowMothers_Click(object sender, EventArgs e)
        {
            var mothersIds = _orphansViewModel.GetMothers(orphanageGridView1.SelectedIds);
            MothersView mothersView = new MothersView(mothersIds);
            mothersView.MdiParent = this.MdiParent;
            mothersView.Show();
        }

        private void btnShowFathers_Click(object sender, EventArgs e)
        {
            var fathersIds = _orphansViewModel.GetFathers(orphanageGridView1.SelectedIds);
            FathersView fathersView = new FathersView(fathersIds);
            fathersView.MdiParent = this.MdiParent;
            fathersView.Show();
        }

        private void btnShowFamilies_Click(object sender, EventArgs e)
        {
            var families = _orphansViewModel.GetFamilies(orphanageGridView1.SelectedIds);
            FimiliesView fimiliesView = new FimiliesView(families);
            fimiliesView.MdiParent = this.MdiParent;
            fimiliesView.Show();
        }
    }
}