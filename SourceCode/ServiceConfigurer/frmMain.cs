using ServiceConfigurer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceConfigurer
{
    public partial class frmMain : Telerik.WinControls.UI.RadForm
    {
        public frmMain()
        {
            InitializeComponent();
            setTextandImages();
        }

        private void setTextandImages()
        {
            var picSize = new Size(32, 32);
            pgeDatabase.Image = new Bitmap(Properties.Resources.database_Pic, picSize);
            pgeService.Image = new Bitmap(Properties.Resources.service_Pic, picSize);
            pgeDatabase.Text = Properties.Resources.Database;
            pgeService.Text = Properties.Resources.Service;
        }

        private async void frmMain_Load(object sender, EventArgs e)
        {
            //setTextandImages();
            DatabaseService databaseService = new DatabaseService();
            if(! await databaseService.IsDatabaseUpdated())
                databaseService.UpdateDataBase();
        }
    }
}
