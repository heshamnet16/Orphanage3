using OrphanageV3.ViewModel.Orphan;
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

namespace OrphanageV3.Views.Orphan
{
    public partial class OrphansView : Telerik.WinControls.UI.RadForm
    {
        private OrphansViewModel orphansViewModel = Program.Factory.Resolve<OrphansViewModel>();
        private object lockObj = new object();
        private Thread upadteImageDetailsThread;
        private IList<Thread> PagingThreads = new List<Thread>();
        public OrphansView()
        {
            InitializeComponent();
            this.Text = Properties.Resources.OrphanViewTitle;
            orphansViewModel.OrphansChangedEvent += OrphansViewModel_OrphansChangedEvent;
            orphansViewModel.PhotoLoadedEvent += OrphansViewModel_PhotoLoadedEvent;
            orphanageGridView1.GridView.PageChanging += GridView_PageChanging;
            orphanageGridView1.GridView.SelectionChanged += GridView_SelectionChanged;
            orphanageGridView1.GridView.TableElement.RowHeight = 80;
            orphanageGridView1.GridView.AllowAutoSizeColumns = true;
            orphanageGridView1.GridView.PageSize = 10;
        }

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (upadteImageDetailsThread != null && upadteImageDetailsThread.IsAlive)
                upadteImageDetailsThread.Abort();
            upadteImageDetailsThread = new Thread(new ThreadStart( async() =>
            {
                if (orphanageGridView1.GridView.SelectedRows.Count == 1)
                {
                    GridViewRowInfo row = null;
                    int id = 0;
                    lock (lockObj)
                    {
                        row = orphanageGridView1.GridView.SelectedRows[0];
                        id = int.Parse(row.Cells["ID"].Value.ToString());
                    }
                    picPhoto.Image = await orphansViewModel.GetOrphanFacePhoto(id);

                }
            }));
            upadteImageDetailsThread.Priority = ThreadPriority.Highest;
            upadteImageDetailsThread.IsBackground = true;
            upadteImageDetailsThread.Start();            
        }

        private void GridView_PageChanging(object sender, PageChangingEventArgs e)
        {
            //if (t!=null && t.IsAlive)
            //    t.Abort();
            var t = new Thread(new ThreadStart( () =>
            {
                IList<int> idsList = new List<int>();
                lock (lockObj)
                {
                    var RowsToSkip = (e.NewPageIndex) * orphanageGridView1.GridView.PageSize;
                    var rows = orphanageGridView1.GridView.Rows.Skip(RowsToSkip).Take(orphanageGridView1.GridView.PageSize);
                    foreach (var row in rows)
                    {
                        if (row.Cells["Photo"].Value == null)
                        {
                            idsList.Add((int)row.Cells["ID"].Value);
                        }
                    }
                }
                if (idsList.Count > 0)
                   orphansViewModel.LoadImages(idsList).Wait();
            }));
            t.Priority = ThreadPriority.Normal;
            t.IsBackground = true;
            t.Start();
            foreach(Thread th in PagingThreads)
            {
                if (th.IsAlive)
                {
                    th.Priority = ThreadPriority.Lowest;
                }
            }
            if (PagingThreads.Count(th => th.IsAlive) == 0)
                PagingThreads.Clear();
            PagingThreads.Add(t);
        }
        private void OrphansViewModel_PhotoLoadedEvent(int Oid,Image img)
        {
            var row = getRowById(Oid);
            row.Cells["Photo"].Value = img;
            
        }

        private void OrphansViewModel_OrphansChangedEvent()
        {
            orphanageGridView1.GridView.DataSource = orphansViewModel.Orphans;
            orphanageGridView1.GridView.Columns["Photo"].Width = 80;
        }

        private void OrphansView_Load(object sender, EventArgs e)
        {
            orphansViewModel.LoadData();
        }

        private GridViewRowInfo getRowById(int id)
        {
            lock (lockObj)
            {
                return orphanageGridView1.GridView.Rows.FirstOrDefault(r => (int)r.Cells["ID"].Value == id);
            }
        }
    }
}
