using OrphanageV3.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
//using Unity;
namespace OrphanageV3.Views.Summary
{
    public partial class SummaryView : Telerik.WinControls.UI.RadForm
    {
        private readonly IApiClient _apiClient;

        public SummaryView()
        {
            InitializeComponent();
            //_apiClient = Program.Factory.Resolve<IApiClient>();
        }

        private void SummaryView_Load(object sender, EventArgs e)
        {

        }

        private void SummaryView_Click(object sender, EventArgs e)
        {
            //var orphansTask = _apiClient.OrphansController_GetAllAsync(100, 0);
            //gridOrphans.DataSource = orphansTask.Result;
        }
    }
}
